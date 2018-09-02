using ExpenseManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseManagement.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        public ExpenseTrackerEntities _context;

        public RegisterController()
        {
            _context = new ExpenseTrackerEntities();
        }

        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.UserRole = "U";
                    _context.Users.Add(user);

                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Index");
                }
            }

            catch (Exception e1)
            {
                return RedirectToAction("Index");
            }

        }

    }
}