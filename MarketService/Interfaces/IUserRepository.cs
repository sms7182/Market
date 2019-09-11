using MarketService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Interfaces
{
    public interface IUserRepository
    {
        User GetByUserName(string username);
        void RegisterUser(User user);
    }
}
