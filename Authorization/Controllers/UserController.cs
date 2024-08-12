using Authorization.Models.DAL.Authority;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Controllers
{
    [Route("api/[controller]")] // Route - задает маршрутp для методов и контроллеров,[controller]- placeholder для замены названия контроллера
    [ApiController] // атрибут для указания что данный класс является контроллером апишки
    public class UserController
    {
        private readonly authContext _authContext;

        public UserController (authContext authContext)
        {
            _authContext = authContext;
        }

        //[HttpGet]
        /*public async Task<IActionResult> GetUser(string username)
        {
           // var res = await _authContext
        }
        */
    }
}
