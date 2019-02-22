using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return PartialView("_List", UserRepo.All());
        }
        public ActionResult Search(string search ="")
        {
            return PartialView("_Search", UserRepo.Search(search));
        }

        public ActionResult Create()
        {
            ViewBag.RoleList = new SelectList(RoleRepo.All(), "Id", "Name");
            return PartialView("_Create");
        }

        [HttpPost]

        public ActionResult Create(UserViewModel model)
        {
            ResponseResult result = UserRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(long id)
        {
            ViewBag.RoleList = new SelectList(RoleRepo.All(), "Id", "Name");
            ViewBag.rdyes = null;
            ViewBag.rdno = null;
            if (UserRepo.GetMobile_flag(id))
            {
                ViewBag.rdyes = "checked";
            }else
            {
                ViewBag.rdno = "checked";
            }
            return PartialView("_Edit", UserRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            ResponseResult result = UserRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ResetPassword(long id)
        {
            return PartialView("_ResetPassword", UserRepo.ById(id));
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

        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", UserRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(UserViewModel model)
        {
            ResponseResult result = UserRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckEmail(string email = "")
        {
            UserViewModel data = UserRepo.CheckEmail(email);
            ResponseResult result = new ResponseResult();
            if (data.email != null)
            {
                result.Success = false;
            }
            return Json(new
            {
                success = result.Success
            }, JsonRequestBehavior.AllowGet);
        }

    }
}