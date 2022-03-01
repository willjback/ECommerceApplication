using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.MVVM.ViewModels
{
    class LoginViewModel
    {
        ECommerceEntities entities;

        public LoginViewModel()
        {
            entities = new ECommerceEntities();
        }
        public bool IsLoginInfoCorrect(string username, string password)
        {
            var user = entities.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}