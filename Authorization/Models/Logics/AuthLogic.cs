using Authorization.AuthOptions;
using Authorization.Models.DAL.Authority;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace Authorization.Models.Logics
{
    public class AuthLogic
    {
        private readonly authContext _context;
        private readonly Jwt _jwt;
        private readonly int _countDays;
        private readonly UserLogic _userLogic;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthLogic(authContext context, IOptions<Jwt> jwt, IConfiguration configuration, UserLogic userLogic, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _jwt = jwt.Value;
            _countDays = configuration.Get<int>("CountDays");
            _userLogic = userLogic;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<DTO.Get.User> AuthorizationAsync(string loginAd, string appName)
        {
            if (string.IsNullOrEmpty(loginAd)) return null;
            var app = await _context.Apps.AsNoTracking().FirstOrDefaultAsync(x => x.AppName.ToLower() == appName.ToLower());
            if (app == null) return null;

            var account = loginAd.ToLower().Trim();
            var user = await _context.Users.AsNoTracking()
                .Include(emp => emp.UserLoginNavigationEmploye)
                .Include(ap => ap.RolesUsersApps)
                .ThenInclude(ap => ap.App)
                .Include(ro => ro.RolesUsersApps)
                .ThenInclude(ro => ro.Role)
                .Include(re => re.RefreshTokens)
                .FirstOrDefaultAsync(u => u.UserLogin.ToLower().Trim() == account);
            if (user == null) return null;

            var authModel = await GetQuery().AsNoTracking().FirstOrDefaultAsync();

            authModel.AccessToken = CreateAccessToken(authModel);
            authModel.AccessTokenExpiration = DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes);
            if (user.RefreshTokens.Any(rt => rt.IsActive ?? false))
            {
                var activeRefreshToken = user.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                authModel.RefreshToken = activeRefreshToken;
                authModel.RefreshTokenExpiration = activeRefreshToken.Expiration;
            }
            else
            {
                var rafreshToken = CreateRefreshToken();
                authModel.RefreshToken = refreshToken.Token;
                authModel.RefreshTokenExpiration = refreshToken.Expiration;
                user.RefreshTokens.Add(refreshToken);
                _context.Update(user);
                _context.SaveChanges();
            }
            return authModel;
        }

        private RefreshToken CreateRefreshToken() //временно
        private string CreateAccessToken() //временно

        private IQueryable<DTO.Get.User> GetQuery()
        {
            return from user in _context.Users
                   .Include(rua => rua.RolesUsersApps)
                   .ThenInclude(r => r.Role)
                   .ThenInclude(a => a.App)
                   .AsNoTracking()
                   join emp in _context.Employes.DefaultIfEmpty()
                   on user.UserLogin equals emp.UserLogin
                   select new DTO.Get.User
                   {
                       Id = user.UserId,
                       AccId = user.UserLogin,
                       Email = emp.EmpEmail,
                       Name = emp.EmpName,
                       Surname = emp.EmpSurname,
                       Phone = emp.EmpPhone,
                       RoleName = emp.RoleName,
                       Roles = user.RolesUsersApps.Select(x => new DTO.Get.Role
                       {
                           Id = x.RoleId,
                           Name = x.Role.RoleName,
                           Description = x.Role.RoleDescription,
                           App = new DTO.App
                           {
                               Id = x.AppId,
                               Name = x.Role.App.AppName
                           }
                       }).ToList()
                   };
        }
    }
}
