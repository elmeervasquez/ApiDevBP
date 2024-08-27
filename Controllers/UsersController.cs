using System.Net;
using ApiDevBP.Configuration;
using ApiDevBP.Entities;
using ApiDevBP.Models;
using ApiDevBP.Persistence;
using ApiDevBP.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SQLite;

namespace ApiDevBP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IUserService _userService;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = _userService.GetUsers();
            if (users != null)
            {
                return Ok(_mapper.Map<List<UserModel>>(users));
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserModel model)
        {
            var result = _userService.Update(id, _mapper.Map<UserEntity>(model));
            return Ok(_mapper.Map<UserModel>(result));
        }

        [HttpPost]
        public async Task<IActionResult> SaveUser(UserModel user)
        {
            var result = _userService.Create(_mapper.Map<UserEntity>(user));
            return Ok(_mapper.Map<UserModel>(result));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _userService.Delete(id);
            return Ok();
        }

    }
}
