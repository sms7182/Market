using MarketService.Interfaces;
using MarketService.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Services
{
    public class UserRepository : IUserRepository
    {
        ISession session;
        public UserRepository(ISession _session)
        {
            session = _session;
        }
        public User GetByUserName(string username)
        {
           var user= session.Query<User>().Where(d => d.UserName == username).FirstOrDefault();
            return user;
        }

        public void RegisterUser(User user)
        {
            session.Save(user);
        }
    }
}
