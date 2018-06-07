using Suporteri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Suporteri.DataAccessLayer
{
    public class SuporterDAL
    {
        public List<User> GetAllUsers()
        {
            List<User> list = new List<User>();
            using (var context = new ProductContext())
            {
                // Perform data access using the context 
                list = context.Users.ToList();
            }
            return list;
        }       
    }
}