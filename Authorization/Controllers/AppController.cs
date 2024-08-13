using Authorization.Models.DAL.Authority;
using Authorization.Models.Logics;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly authContext _context;
        private readonly AppLogic _logic;
        public AppController(authContext context, AppLogic logic)
        {
            _context = context;
            _logic = logic;
        }

        [HttpGet]
        public async Task<IActionResult> GetApps()
        {
            var res = await _logic.GetApps();
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddApps(Models.DTO.App add)
        {
            var res = await _logic.AddApps(add);
            return Ok(res);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateApps(Models.DTO.App upd)
        {
            var res = await _logic.UpdateApps(upd);
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteApp(int id)
        {
            var res = await _logic.DeleteApps(id);
            return Ok(res);
        }
    }
}
