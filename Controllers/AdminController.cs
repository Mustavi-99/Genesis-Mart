using System;
using System.Collections.Generic;
using System.IO;
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
            return RedirectToAction("AdminPanel");
        }

        public ActionResult AdminPanel()
        {
            if (Session["CUSEmail"] == null || Session["CUSEmail"] != "GenesisMart@samune.com")
            {
                ViewBag.AdminLogin = "Admin needs to log in";
            }
                return View();
        }

        public ActionResult UserTable()
        {
            if (Session["CUSEmail"] == null || Session["CUSEmail"] != "GenesisMart@samune.com")
            {
                ViewBag.AdminLogin = "Admin needs to log in";
            }
            List<Customer> customer = db.Customers.ToList();
            return View(customer);
        }

        public ActionResult AddItem()
        {
            if (Session["CUSEmail"] == null || Session["CUSEmail"] != "GenesisMart@samune.com")
            {
                ViewBag.AdminLogin = "Admin needs to log in";
            }
            return View();
        }

        public ActionResult DeleteItem(string page)
        {
            if (Session["CUSEmail"] == null || Session["CUSEmail"] != "GenesisMart@samune.com")
            {
                ViewBag.AdminLogin = "Admin needs to log in";
            }
            List<Product> products;
            var sql = "";
            int p;
            if (page == null)
            {
                p = 1;
            }
            else
            {
                p = Int32.Parse(page);
            }
            //products = db.Products.ToList();
            sql = "Select * from Product";
            
            products = db.Products.SqlQuery(sql).ToList();
            double resultPerPage = 6;
            var pageFirstresult = (p - 1) * resultPerPage;
            double numberOfresult = products.Count;
            double numberOfPage = Math.Ceiling(numberOfresult / resultPerPage);
            System.Diagnostics.Debug.WriteLine(numberOfPage);
            var query = sql + " order by PRID OFFSET " + pageFirstresult + " rows FETCH FIRST " + resultPerPage + " ROWS ONLY";
            products = db.Products.SqlQuery(query).ToList();
            ViewBag.Page = p;
            ViewBag.NumberOfPages = numberOfPage;
            return View(products);
        }

        public ActionResult ContactUsTable()
        {
            if (Session["CUSEmail"] == null || Session["CUSEmail"] != "GenesisMart@samune.com")
            {
                ViewBag.AdminLogin = "Admin needs to log in";
            }
            List<ContactU> contactUs = db.ContactUs.ToList();
            return View(contactUs);
        }

        public ActionResult RemoveUser(Customer cus)
        {

            Customer customer = db.Customers.Where(temp => temp.CUSEmail.Equals(cus.CUSEmail)).SingleOrDefault();
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("UserTable");
        }

        public ActionResult RemoveContact(ContactU contact)
        {
            ContactU contactU = db.ContactUs.Where(temp => temp.ContactID.Equals(contact.ContactID)).SingleOrDefault();
            db.ContactUs.Remove(contactU);
            db.SaveChanges();
            return RedirectToAction("ContactUsTable");
        }

        public ActionResult ItemAdd(Product pro)
        {
            System.Diagnostics.Debug.WriteLine(pro.PRName+" "+pro.PRDescription+" "+pro.PRRating+" "+pro.PRPrize);
            Product product = new Product
            {
                PRName = pro.PRName,
                PRDescription = pro.PRDescription,
                PRRating = pro.PRRating,
                PRPrize = pro.PRPrize,
                PRType = pro.PRType,
                PRImage = "xboxS.png"
            };
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("AddItem");
        }

        public ActionResult ItemDelete(int id)
        {
            System.Diagnostics.Debug.WriteLine(id);
            Product product = db.Products.Where(temp => temp.PRID.Equals(id)).SingleOrDefault();
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("DeleteItem");
        }

        //[HttpPost]
        //public ActionResult UploadFiles(HttpPostedFileBase file)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (file != null)
        //            {
        //                string path = Path.Combine(Server.MapPath("~/images"), Path.GetFileName(file.FileName));
        //                file.SaveAs(path);
        //            }
        //            ViewBag.FileStatus = "File uploaded successfully.";
        //        }
        //        catch (Exception)
        //        {

        //            ViewBag.FileStatus = "Error while file uploading.";
        //        }

        //    }
        //    return RedirectToAction("AddItem");
        //}
    }


}