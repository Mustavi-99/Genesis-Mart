using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Genesis_Mart.Models;

namespace Genesis_Mart.Controllers
{
    public class HomeController : Controller
    {

        GenesisMartEntities db = new GenesisMartEntities();
        public static int contactE;

        public ActionResult Index()
        { 
            List<Product> products = db.Products.ToList();
            return View(products);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            if (!customer.CUSEmail.EndsWith(".com"))
            {
                ViewBag.Notification = "Email Format Incorrect";
                return View();
            }
            System.Diagnostics.Debug.WriteLine(customer.CUSContactNo.Length);
            if (customer.CUSContactNo.Length != 11 || !customer.CUSContactNo.StartsWith("01"))
            {
                System.Diagnostics.Debug.WriteLine("Check");
                ViewBag.Notification = "Contact Number is invalid";
                return View();
            }
            if (db.Customers.Any(x => x.CUSEmail == customer.CUSEmail || x.CUSName == customer.CUSName))
            {
                ViewBag.Notification = "This account has already existed";
                return View();
            }
            else
            {
                db.Customers.Add(customer);
                db.SaveChanges();

                Session["CUSName"] = customer.CUSName.ToString();
                Session["CUSEmail"] = customer.CUSEmail.ToString();
                return RedirectToAction("Index", "Home");

            }

        }

        public ActionResult Login()
        {
            if (Session["CUSName"] != null)
            {
                ViewBag.loggedIn = "User already logged in";
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Customer customer)
        {
            var checklogin = db.Customers.Where(x=>x.CUSEmail.Equals(customer.CUSEmail) && x.CUSPassword.Equals(customer.CUSPassword)).FirstOrDefault();
            if (checklogin != null)
            {
                Session["CUSName"] = checklogin.CUSName;
                Session["CUSEmail"] = customer.CUSEmail.ToString();
                ViewBag.Login = "Logged In";
                //return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notification = "Wrong Email or Password";
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ProductPreview(int id)
        {
            if (Session["CUSName"] == null)
            {
                ViewBag.user = "Please Log In First";
            }
            Product product = db.Products.Where(temp => temp.PRID.Equals(id)).SingleOrDefault();
            List<Comment> comments = db.Comments.Where(temp => temp.ProductID.Equals(id)).ToList();
            return View(new object[] { product, comments });

        }

        [HttpPost]
        public ActionResult CommentPost(String ProductID, string CommentMessage)
        {

            System.Diagnostics.Debug.WriteLine("check: " + ProductID + " " + CommentMessage);
            int id = Int32.Parse(ProductID);
            
            Comment comment = new Comment
            {
                ProductID = id,
                CommentMessage = CommentMessage,
                CustName = Session["CUSName"].ToString()
            };
            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("ProductPreview/" + ProductID);
        }

        public ActionResult ProductPage(string category)
        {
            List<Product> products;
            if (category == null)
            {
                products = db.Products.ToList();
                ViewBag.productCategory = "All Products";
            }
            else
            {
                products = db.Products.Where(temp => temp.PRType.Equals(category)).ToList();
                ViewBag.productCategory = category;
            }
            return View(products);
        }

        //public ActionResult Product(int id)
        //{
        //    Product product = db.Products.Where(temp => temp.PRID.Equals(id)).SingleOrDefault();
        //    List<Comment> comments = db.Comments.Where(temp=> temp.ProductID.Equals(id)).ToList();
        //    return View(new object[] { product, comments });
        //    //return View(product);
        //}

        

        public ActionResult ContactUs()
        {
            ContactU contactUs = new ContactU();
            System.Diagnostics.Debug.WriteLine(contactE);
            if(contactE == 1)
            {
                contactE = 0;
                ViewBag.Noti = "Email Format Incorrect";
            }
            if (Session["CUSName"] != null)
            {
                string email = Session["CUSEmail"].ToString();
                Customer customer = db.Customers.Where(temp=> temp.CUSEmail.Equals(email)).SingleOrDefault();
                contactUs.Email = customer.CUSEmail;
                contactUs.FullName = customer.CUSName;
            }else
            {
                contactUs.Email = "";
                contactUs.FullName = "";
            }
            return View(contactUs);
        }

        [HttpPost]
        public ActionResult ContactUs(ContactU contactU)
        {
            if (!contactU.Email.EndsWith(".com"))
            {
                contactE = 1;
            }
            else
            {
                db.ContactUs.Add(contactU);
                db.SaveChanges();
            }
            return RedirectToAction("ContactUs");
        }


        public ActionResult CustomerProfile()
        {
            return View();
        }

    }
}