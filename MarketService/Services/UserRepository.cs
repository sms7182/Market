using MarketService.Interfaces;
using MarketService.Models;
using MarketService.Models.Contracts;
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

        public void RegisterUser(UserInfo userInfo)
        {
           var user= session.Query<User>().Where(d => d.UserName == userInfo.UserName).FirstOrDefault();
            session.BeginTransaction();
            if (user != null)
            {
                user.Password = userInfo.UserName;
                session.SaveOrUpdate(user);
            }
            else
            {
                user.UserName = userInfo.UserName;
                user.Password = userInfo.Password;
                session.Save(user);
            }
            session.Transaction.Commit();
        }

        
    }
}
