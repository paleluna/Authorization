using Authorization.Models.DAL.Authority;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;

namespace Authorization.Models.Logics
{
    public class RoleLogic
    {
        private readonly authContext _context;

        public RoleLogic(authContext context)
        {
            _context = context;
        }

        public async Task<List<Models.DTO.Role>> GetRolesAsync()
        {
            return await _context.Roles.Include(a => a.App).Select(r => new Models.DTO.Role
            {
                Id = r.RoleId,
                Name = r.RoleName,
                Description = r.RoleDescription,
                AppId = r.AppId,
                App = new Models.DTO.App
                {
                    Id = r.App.AppId,
                    Name = r.App.AppName
                }
            }).ToListAsync();
        }

        public async Task<string> AddAsync(DTO.Role add)
        {
            var dal = new DAL.Authority.Role
            {
                RoleName = add.Name,
                RoleDescription = add.Description,
                AppId = add.AppId,
            };
            _context.Roles.Add(dal);
            await _context.SaveChangesAsync();
            return dal.RoleId.ToString();
        }

        public async Task<string> UpdateAsync(DTO.Role upd)
        {
            var dal = await _context.Roles.FirstOrDefaultAsync(i => i.RoleId == upd.Id);
            if (dal == null) return null;
            dal.RoleName = upd.Name;
            dal.RoleDescription = upd.Description;
            await _context.SaveChangesAsync();
            return dal.RoleName.ToString();
        }

        public async Task<string> DeleteAsync(int id)
        {
            var dal = await _context.Roles.FirstOrDefaultAsync(i => i.RoleId == id);
            if (dal == null) return null;
            _context.Roles.Remove(dal);
            await _context.SaveChangesAsync();
            return dal.RoleId.ToString();
        }
    }
}
