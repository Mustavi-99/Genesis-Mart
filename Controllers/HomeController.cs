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
        public static List<int> CartItems = new List<int>();

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
            if(customer.CUSEmail.Equals("GenesisMart@samune.com") && customer.CUSPassword.Equals("genesis"))
            {
                Session["CUSName"] = "Admin";
                Session["CUSEmail"] = customer.CUSEmail.ToString();
                ViewBag.Login = "Admin Logged In";
                return RedirectToAction("AdminPanel", "Admin");
            }
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
            //if (Session["CUSName"] == null)
            //{
            //    ViewBag.user = "Please Log In First";
            //}
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

        public ActionResult ProductPage(string category,string page)
        {
            List<Product> products;
            var sql="";
            int p;
            if (page==null)
            {
                p = 1;
            }
            else
            {
                p= Int32.Parse(page);
            }
            if (category == null)
            {
                //products = db.Products.ToList();
                sql = "Select * from Product";               
                ViewBag.productCategory = "All Products";
            }
            else
            {
                //products = db.Products.Where(temp => temp.PRType.Equals(category)).ToList();
                sql = "Select * from Product where PRType = '"+category+"' ";
                ViewBag.Category = category;
                ViewBag.productCategory = category;
            }
            products = db.Products.SqlQuery(sql).ToList();
            double resultPerPage = 3;
            var pageFirstresult = (p - 1) * resultPerPage;
            double numberOfresult = products.Count;
            double numberOfPage = Math.Ceiling(numberOfresult / resultPerPage);
            System.Diagnostics.Debug.WriteLine(numberOfPage);
            var query = sql + " order by PRID OFFSET "+pageFirstresult+" rows FETCH FIRST "+resultPerPage+" ROWS ONLY";
            products = db.Products.SqlQuery(query).ToList();
            ViewBag.Page = p;
            ViewBag.NumberOfPages = numberOfPage;
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
            if (Session["CUSEmail"] == null)
            {
                ViewBag.NoUser = "User Needs to Log In";  
            }
            System.Diagnostics.Debug.WriteLine(Session["CUSName"]);
            string username = Session["CUSEmail"].ToString();
            Customer customer = db.Customers.Where(temp => temp.CUSEmail.Equals(username)).SingleOrDefault();

            return View(customer);
        }

        [HttpPost]
        public ActionResult UpdateProfile(Customer customer)
        {
            customer.CUSEmail = Session["CUSEmail"].ToString();
            db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            Session["CUSName"] = customer.CUSName;
            return RedirectToAction("CustomerProfile");
        }

        public ActionResult AddToCart(int id)
        {
            bool flag = true;
            foreach(var item in CartItems)
            {
                if(item.Equals(id))
                {
                    flag = false;
                }
            }
            if(flag)
            {
                CartItems.Add(id);
                if(Session["CUSEmail"]==null)
                {
                    ViewBag.AddedItem = "Login to View Your Cart";
                }
            }
            else
            {
                ViewBag.AddedItem = "Item Already in Cart";
            }

            
            return RedirectToAction("ProductPreview/" + id);
        }

        public ActionResult Orders()
        {
            List<Product> products = new List<Product>();

            foreach (var item in CartItems)
            {
                products.Add(db.Products.Where(temp => temp.PRID.Equals(item)).SingleOrDefault());
                //System.Diagnostics.Debug.WriteLine(item);
            }

            return View(products);
        }


    
        public ActionResult ConfirmOrder(string totalvalue)
        {
            

            OrderList orderlist = new OrderList();
            orderlist.cusEmail = Session["CUSEmail"].ToString();
            orderlist.totalPrice = Int32.Parse(totalvalue);
            string items = "";
            Product prod;
            foreach (var item in CartItems)
            {
                prod = db.Products.Where(temp => temp.PRID.Equals(item)).SingleOrDefault();
                items = items + prod.PRName + " ";
            }
            System.Diagnostics.Debug.WriteLine(items + " "+orderlist.cusEmail+" "+orderlist.totalPrice);

            orderlist.itemName = items;
            db.OrderLists.Add(orderlist);
            db.SaveChanges();

            CartItems.Clear();

            return RedirectToAction("Orders");
        }

        public ActionResult RemoveFromCart(Product product)
        {
            var item = CartItems.Single(temp => temp == product.PRID);
            if(item != null)
            {
                CartItems.Remove(item);
                //System.Diagnostics.Debug.WriteLine("Check");
            }
            return RedirectToAction("Orders");
        }

        public ActionResult OrderHistory()
        {
            return View();
        }





    }
}