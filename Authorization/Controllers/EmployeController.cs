using Authorization.Models.DAL.Authority;
using Authorization.Models.Logics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

namespace Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeController : ControllerBase
    {
        private readonly EmployeLogic _logic;
        public EmployeController(EmployeLogic logic)
        { 
            _logic = logic;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmp()
        {
            var res = await _logic.GetEmployesAsync();
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> PostEmp(Models.DTO.Employe add)
        {
            var res = await _logic.PostEmployesAsync(add);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmp(Models.DTO.Employe upd)
        {
            var res = await _logic.UpdateEmployesAsync(upd);
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmp(string del)
        {
            var res = await _logic.DeleteEmployesAsync(del);
            return Ok(res);
        }
    }
}
