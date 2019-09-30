﻿using MarketService.Interfaces;
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
        public UserInfo GetByUserName(string username)
        {
           var user= session.Query<User>().Where(d => d.UserName == username).FirstOrDefault();
            UserInfo userInfo = null;
            if(user!=null)
            {
                userInfo.Id = user.Id;
                userInfo.PhoneNumber = user.UserName;
                userInfo.Password = user.Password;
            }

            return userInfo;
        }

        public bool RegisterUser(UserInfo userInfo)
        {
            try
            {
                var user = session.Query<User>().Where(d => d.UserName == userInfo.PhoneNumber).FirstOrDefault();
                session.BeginTransaction();
                if (user != null)
                {
                    user.Password = userInfo.Password;
                    session.SaveOrUpdate(user);
                }
                else
                {
                    user.UserName = userInfo.PhoneNumber;
                    user.Password = userInfo.Password;
                    session.Save(user);
                }
                session.Transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;                
            }
        }
        public bool DeleteUser(Guid id)
        {
            try
            {
                var user = session.Get<User>(id);
                session.BeginTransaction();
                if (user != null)
                {
                    session.Delete(user);
                }
                
                session.Transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
