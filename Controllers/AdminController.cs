using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Genesis_Mart.Models;

namespace Genesis_Mart.Controllers
{
    public class AdminController : Controller
    {
        GenesisMartEntities db = new GenesisMartEntities();

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
            List<Customer> customer = db.Customers.ToList();
            return View(customer);
        }

        public ActionResult AddItem()
        {
            return View();
        }

        public ActionResult DeleteItem()
        {
            return View();
        }

        public ActionResult ContactUsTable()
        {
            List<ContactU> contactUs = db.ContactUs.ToList();
            return View(contactUs);
        }

        public ActionResult RemoveUser(Customer cus)
        {
            System.Diagnostics.Debug.WriteLine(cus.CUSEmail);
            Customer customer = db.Customers.Where(temp => temp.CUSEmail.Equals(cus.CUSEmail)).SingleOrDefault();
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("UserTable");
        }

        public ActionResult RemoveContact(ContactU contact)
        {
            System.Diagnostics.Debug.WriteLine(contact.ContactID);
            ContactU contactU = db.ContactUs.Where(temp => temp.ContactID.Equals(contact.ContactID)).SingleOrDefault();
            db.ContactUs.Remove(contactU);
            db.SaveChanges();
            return RedirectToAction("ContactUsTable");
        }
    }


}