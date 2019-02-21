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
                Session["roleid"] = data.role_id;
                return RedirectToAction("Index", "user");
            }else
            {
                return View();
            }
            
        }

        public ActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }
        [HttpPost]
        public ActionResult ForgotPassword(string email = "")
        {
            UserViewModel data = UserRepo.CheckEmail(email);
            ResponseResult result = new ResponseResult();

            if (data.email != null)
            {
                result = HomeRepo.ForgotPassword(data);
            }
            else
            {
                result.Success = false;
                result.ErrorMessage = "Email tidak ditemukan";
            }
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetMenu(long id)
        {
            return PartialView("_GetMenu", HomeRepo.getMenu(id));
        }

        public ActionResult ResetPassword(long id)
        {
            return PartialView("ResetPassword", UserRepo.ById(id));
        }
        [HttpPost]
        public ActionResult ResetPassword(UserViewModel model)
        {
            ResponseResult result = UserRepo.ResetPassword(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);

        }

    }
}