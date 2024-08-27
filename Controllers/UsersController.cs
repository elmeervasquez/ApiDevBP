using ApiDevBP.Configuration;
using ApiDevBP.Entities;
using ApiDevBP.Models;
using ApiDevBP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SQLite;
using System.Reflection;

namespace ApiDevBP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly SQLiteConnection _db;

        private readonly ILogger<UsersController> _logger;

        private readonly IUserService _userService;

        private readonly IOptions<DbSettings> _dbSettings;

        public UsersController(ILogger<UsersController> logger, IUserService userService, IOptions<DbSettings> dbSettings)
        {
            _logger = logger;
            string localDb = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "localDb");
            Console.WriteLine(localDb);
            _db = new SQLiteConnection(localDb);
            _db.CreateTable<UserEntity>();
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = _db.Query<UserEntity>($"Select * from Users");
            if (users != null)
            {
                return Ok(users.Select(x => new UserModel()
                {
                    Name = x.Name,
                    Lastname = x.Lastname
                }));
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(string name, string userName)
        {
            // var result = _db.Insert(new UserEntity()
            // {
            //     Name = user.Name,
            //     Lastname = user.Lastname
            // });
            // return Ok(result > 0);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SaveUser(UserModel user)
        {
            var result = _db.Insert(new UserEntity()
            {
                Name = user.Name,
                Lastname = user.Lastname
            });
            return Ok(result > 0);
        }

    }
}
