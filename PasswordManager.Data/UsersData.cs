﻿using PasswordManager.Database;
using PasswordManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Data
{
    public class UsersData
    {
        private DB Database = DB.Instance();

        private static UsersData _instance;

        protected UsersData()
        {
        }

        public static UsersData Instance()
        {
            if (_instance == null)
            {
                _instance = new UsersData();
            }

            return _instance;
        }

        public int AddNewUser(User user, Settings settings, PasswordOptions passwordOptions)
        {
            return Database.AddNewUser(user, settings, passwordOptions);
        }

        public User SelectUser(User user)
        {
            return Database.GetUserByID(user.ID);
        }

        public User LoginUser(User user)
        {
            return Database.GetUserByEmail(user.Email);
        }

        public int UpdateUser(User user)
        {
            return Database.UpdateUser(user);
        }
    }
}
