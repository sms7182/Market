using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Models.Contracts
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid Id { get; set; }
    }
}
