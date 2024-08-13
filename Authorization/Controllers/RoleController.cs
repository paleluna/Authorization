using Authorization.Models.DAL.Authority;
using Authorization.Models.Logics;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly authContext _context;
        private readonly RoleLogic _logic;

        public RoleController(authContext context, RoleLogic logic)
        {
            _context = context;
            _logic = logic;
        }

        [HttpGet]
        public async Task<IActionResult> GetRole()
        {
            var res = await _logic.GetRolesAsync();
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(Models.DTO.Role add)
        {
            var res = await _logic.AddAsync(add);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> PutRole(Models.DTO.Role put)
        {
            var res = await _logic.UpdateAsync(put);
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var res = await _logic.DeleteAsync(id);
            return Ok(res);
        }
    }
}
