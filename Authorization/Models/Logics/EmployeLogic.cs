using Authorization.Models.DAL.Authority;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Models.Logics
{
    public class EmployeLogic
    {
        private readonly authContext _context;

        public EmployeLogic(authContext context)
        {
            _context = context;
        }

        public async Task<List<DTO.Employe>> GetEmployesAsync()
        {
            var dal = await _context.Employes.AsNoTracking().ToListAsync();
            var dto = dal.Select(x => new DTO.Employe
            {
                UserLogin = x.UserLogin,
                Name = x.EmpName,
                Surname = x.EmpSurname,
                Phone = x.EmpPhone,
                Email = x.EmpEmail,
                RoleName = x.RoleName,
                IsBlocked = x.EmpIsBlocked
            }).ToList();
            return dto;
        }

        public async Task<string> PostEmployesAsync(DTO.Employe emp)
        {
            var dal = new DAL.Authority.Employe
            {
                UserLogin = emp.UserLogin,
                EmpName = emp.Name,
                EmpSurname = emp.Surname,
                EmpEmail = emp.Email,
                EmpPhone = emp.Phone,
                RoleName = emp.RoleName,
                EmpIsBlocked = emp.IsBlocked
            };
            await _context.Employes.AddAsync(dal);
            await _context.SaveChangesAsync();

            var user = new User
            {
                UserLogin = emp.UserLogin
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return dal.UserLogin.ToString();

        }

        public async Task<string> UpdateEmployesAsync(DTO.Employe upd)
        {
            var dal = await _context.Employes.FirstOrDefaultAsync(x => x.UserLogin == upd.UserLogin);
            if (dal == null) return null;
            dal.UserLogin = upd.UserLogin;
            dal.EmpName = upd.Name;
            dal.EmpSurname = upd.Surname;
            dal.EmpPhone = upd.Phone;
            dal.EmpEmail = upd.Email;
            await _context.SaveChangesAsync();
            return dal.UserLogin.ToString();
        }

        public async Task<string> DeleteEmployesAsync(string log)
        {
            var dal = _context.Employes.FirstOrDefault(x => x.UserLogin == log);
            if (dal == null) return null;
            _context.Employes.Remove(dal);
            await _context.SaveChangesAsync();
            return dal.UserLogin.ToString();
        }
    }
}
