using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserViewModel model)
        {
            UserViewModel data = UserRepo.LogIn(model);
            if (data.PassIsTrue)
            {
                Session["Username"] = data.username;
                Session["id"] = data.id;
                return RedirectToAction("Index", "user");
            }else
            {
                return View();
            }
            
        }
    }
}