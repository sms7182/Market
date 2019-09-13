using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketService.Interfaces;
using MarketService.Models;
using MarketService.Models.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MarketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IUserRepository UserRepository { get; set; }
        public LoginController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        [HttpGet("byuser")]
        public string GetUser(string userName)
        {

            var user=  UserRepository.GetByUserName(userName);
            var userInfojs = JsonConvert.SerializeObject(user);
            return userInfojs;
        }


        [HttpPost("byusername")]
        public void Post(string userjs)
        {
            var userInfo = JsonConvert.DeserializeObject<UserInfo>(userjs);
            if (userInfo != null)
            {
                UserRepository.RegisterUser(userInfo);
            }
        }
    }
}