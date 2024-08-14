using Authorization.Models.DAL.Authority;
using Authorization.Models.DTO.Set;
using Authorization.Models.Logics;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Controllers
{
    [Route("api/[controller]")] // Route - задает маршрутp для методов и контроллеров,[controller]- placeholder для замены названия контроллера
    [ApiController] // атрибут для указания что данный класс является контроллером апишки
    public class UserController : ControllerBase
    {
        private readonly authContext _context;
        private readonly UserLogic _logic;

        public UserController(authContext context, UserLogic logic)
        {
            _context = context;
            _logic = logic;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string user)
        {
            var res = await _logic.GetUserAsync(user);
            if (res == null) return NotFound();
            return Ok(res);
        }

        [HttpPost]

        public async Task<IActionResult> AddUserAsync(SimpleUser name)
        {
            var res = await _logic.AddUserAsync(name);
            if (res < 0) return NoContent();
            else return Ok(res);
        }

    }
}
