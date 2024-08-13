using Authorization.Models.DAL.Authority;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Models.Logics
{
    public class AppLogic
    {
        private readonly authContext _context;

        public AppLogic(authContext context)
        {
            _context = context;
        }

        public async Task<List<DTO.App>> GetApps()
        {
            var dal = await _context.Apps.AsNoTracking().ToListAsync();
            var dto = dal.Select(x => new DTO.App
            {
                Id = x.AppId,
                Name = x.AppName
            }).ToList();
            return dto;
        }
        public async Task<string> AddApps(DTO.App add)
        {
            var dal = new DAL.Authority.App
            {
                AppName = add.Name
            };
            await _context.Apps.AddAsync(dal);
            await _context.SaveChangesAsync();
            return dal.AppId.ToString();
        }

        public async Task<string> UpdateApps(DTO.App upd)
        {
            var dal = await _context.Apps.FirstOrDefaultAsync(x => x.AppId == upd.Id);
            if (dal == null) return null;
            dal.AppName = upd.Name;
            await _context.SaveChangesAsync();
            return dal.AppName.ToString();
        }

        public async Task<string> DeleteApps(int id)
        {
            var dal = _context.Apps.FirstOrDefault(x => x.AppId == id);
            if (dal == null) return null;
            _context.Apps.Remove(dal);
            await _context.SaveChangesAsync();
            return dal.AppId.ToString();
        }
    }
}
