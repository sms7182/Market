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
        [HttpGet("byusername")]
        public string GetUser(string userName)
        {

            var user=  UserRepository.GetByUserName(userName);

            if (user != null)
            {
                var userInfojs = JsonConvert.SerializeObject(user);
                return userInfojs;
            }
            return string.Empty;
        }

        [HttpPost("deluser")]
        public ActionResult<string> Delete(Guid id)
        {
            var result = UserRepository.DeleteUser(id);
            var resultJson = JsonConvert.SerializeObject(result);
            return resultJson;
        }

      //  [HttpPost("byuser")]
        public ActionResult<string> Post([FromBody]UserInfo userInfo)
        {            
            var result = false;
            if (userInfo != null)
            {
                result = UserRepository.RegisterUser(userInfo);
            }

            var resultJson = JsonConvert.SerializeObject(result);
            return resultJson;
        }
    }
}