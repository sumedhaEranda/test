using StokManagmentSystem.Models;
using StokManagmentSystem.Service;
using System;
using System.Web.Mvc;

namespace StokManagmentSystem.Controllers
{
    // This controller manages the home-related views and actions
    public class HomeController : Controller
    {
        private UserService userService = new UserService();

        // GET: Home/Index
        // Displays the default view for the home page
        public ActionResult Index()
        {
            return View();
        }

        // POST: Home/Index
        // Handles the form submission for user login
        [HttpPost]
        public ActionResult Index(UserModel userinfo)
        {
            if (ModelState.IsValid)
            {
                bool result = userService.ValidateUserCredentials(userinfo);
                if (result)
                {
                    return RedirectToAction("Index", "Dashome");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                }
            }

            return View(userinfo);
        }

        // GET: Home/About
        // Displays the 'About' page
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // GET: Home/Contact
        // Displays the 'Contact' page
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}