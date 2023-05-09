using DataAccess;
using DataAccess.Models;
using SharedLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic
{
    
    public class UserLogic
    {
        DatabaseOperations dbObject;
        public bool CreateUser(UserViewModel requestedUser)
        {
            dbObject = new DatabaseOperations();

            UserModel databaseUser = new UserModel()
            {
                FullName = requestedUser.FullName,
                Email = requestedUser.Email,
                Password = requestedUser.Password
            };

            int result = dbObject.AddUserToDatabase(databaseUser);
            if(result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int LoginUser(UserViewModel requestedUser)
        {
            dbObject = new DatabaseOperations();

            UserModel databaseUser = new UserModel()
            {
                Email = requestedUser.Email,
                Password = requestedUser.Password
            };


            int result = dbObject.VerifyLoginUser(databaseUser);

            return result;
        }
    }
}
