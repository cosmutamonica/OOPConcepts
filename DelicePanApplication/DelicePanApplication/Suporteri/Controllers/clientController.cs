using Suporteri.DataAccessLayer;
using Suporteri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Suporteri.Controllers
{
    public class XController : Controller
    {
        // GET: Supporter
        public ActionResult ShowAllClients()
        {
            List<User> list = new SuporterDAL().GetAllUsers();
            return View(list);
        }
    }
}
