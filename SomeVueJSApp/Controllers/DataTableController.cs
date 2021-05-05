using Microsoft.AspNetCore.Mvc;
using SomeVueJSApp.Data;
using System.Linq;
using System.Threading.Tasks;
using SomeVueJSApp.Data.Models;
using SomeVueJSApp.Models;

namespace SomeVueJSApp.Controllers
{
    [ApiController]
    [Route("api/datatable")]
    public class DataTableController : ControllerBase
    {
        private readonly DataTableContext _context;

        private void AddRequestHeader()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Credentials", true.ToString());
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Origin, Content-Type, Accept");
        }

        public DataTableController(DataTableContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            AddRequestHeader();
            return Ok(_context.Users.ToList());
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromBody] UserDto newUser)
        {
            AddRequestHeader();
            if (newUser == null)
                return BadRequest("User not specified");

            var user = new User();
            user.Surname = newUser.Surname;
            user.Firstname = newUser.Firstname;
            user.Patronymic = newUser.Patronymic;
            user.Age = newUser.Age;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(new { id = user.ID});
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User editUser)
        {
            AddRequestHeader();
            if (editUser == null)
                return BadRequest("User not specified");

            User user = _context.Users.FirstOrDefault(x => x.ID == editUser.ID);

            if (user == null)
                return BadRequest("User is not found");

            user.Surname = editUser.Surname;
            user.Firstname = editUser.Firstname;
            user.Patronymic = editUser.Patronymic;
            user.Age = editUser.Age;
            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            AddRequestHeader();
            User user = _context.Users.FirstOrDefault(x => x.ID == id);

            if (user == null)
                return BadRequest("User is not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
