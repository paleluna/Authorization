using Authorization.Models.DAL.Authority;
using Authorization.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Models.Logics
{
    public class UserLogic
    {
        private readonly authContext _context;

        public UserLogic (authContext authContext)
        {
            _context = authContext;
        }

        public async Task<DTO.User> GetUserAsync(string get)
        {
            var acc = get.ToLower().Trim(); //вводим ник юзера, tolower - убирает заглавные буквы, trim - убирает пробелы
            var user = await _context.Users.AsNoTracking() // отключаем отслеживание изменений
                .Include(emp => emp.UserLoginNavigationEmploye)
                .FirstOrDefaultAsync(u => u.UserLogin.ToLower().Trim() == acc);

            if (user == null) return null;

            var us = new DTO.User
            {
                Id = user.UserId,
                Login = user.UserLogin,
                Employe = user.UserLoginNavigationEmploye != null ? new DTO.Employe
                {
                    UserLogin = user.UserLoginNavigationEmploye.UserLogin,
                    Name = user.UserLoginNavigationEmploye.EmpName,
                    Surname = user.UserLoginNavigationEmploye.EmpSurname,
                    Email = user.UserLoginNavigationEmploye.EmpEmail,
                    Phone = user.UserLoginNavigationEmploye.EmpPhone,
                    RoleName = user.UserLoginNavigationEmploye.RoleName,
                    IsBlocked = user.UserLoginNavigationEmploye.EmpIsBlocked
                } : null
            };
            return us;
        }
        public async Task<int> AddUserAsync(DTO.Set.SimpleUser add)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserLogin.ToLower().Trim() ==  add.AccId.ToLower().Trim());
            if (user != null)
            {
                var ch = await ChangeUserAsync(add);
                return ch;
            }

            var employe = await _context.Employes.FirstOrDefaultAsync(e => e.UserLogin.ToLower().Trim() == add.AccId.ToLower().Trim());
            if (employe == null)
            {
                employe = new DAL.Authority.Employe
                {
                    UserLogin = add.AccId
                };
                await _context.Employes.AddAsync(employe);
                await _context.SaveChangesAsync();
            }
            var user_new = new DAL.Authority.User
            {
                UserLogin = add.AccId
            };

            user_new.RolesUsersApps = add.Roles.Select(r => new RolesUsersApp
            {
                AppId = r.AppId,
                RoleId = r.RoleId,
                UserId = user_new.UserId
            }).ToList();
            await _context.SaveChangesAsync();
            return user_new.UserId;

        }

        public async Task<int> ChangeUserAsync(DTO.Set.SimpleUser name)
        {
            var u = await _context.Users.FirstOrDefaultAsync(u => u.UserLogin.ToLower() == name.AccId.ToLower().Trim());
            if (u == null) return -1;

            var appIds = name.Roles.Select(t => t.AppId).ToList();
            var app = await _context.Apps.AsNoTracking().Where(a => appIds.Contains(a.AppId)).ToListAsync();
            if(app == null || !app.Any()) return -1;

            var userRole = _context.RolesUsersApps.Where(a => a.UserId == u.UserId && app.Select(i => i.AppId).Contains(a.AppId));
            var removed = userRole.Where(ur => !name.Roles.Select(r => r.RoleId).Contains(u.UserId));
            var added = name.Roles.Where(r => !userRole.Select(ur => ur.RoleId).Contains(u.UserId)).Select(r => new RolesUsersApp
            {
                RoleId = r.RoleId,
                UserId = u.UserId,
                AppId = r.AppId
            });
            _context.RolesUsersApps.RemoveRange(removed);
            await _context.RolesUsersApps.AddRangeAsync(added);
            await _context.SaveChangesAsync();
            return u.UserId;
        }
    }
}
