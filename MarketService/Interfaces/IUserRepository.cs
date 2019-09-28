using MarketService.Models;
using MarketService.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Interfaces
{
    public interface IUserRepository
    {
        UserInfo GetByUserName(string username);
        bool RegisterUser(UserInfo user);
        bool DeleteUser(Guid id);


    }
}
