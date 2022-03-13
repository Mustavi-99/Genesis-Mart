using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Genesis_Mart.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminPanel()
        {
            return View();
        }

        public ActionResult UserTable()
        {
            return View();
        }

        public ActionResult AddItem()
        {
            return View();
        }

        public ActionResult DeleteItem()
        {
            return View();
        }
    }


}