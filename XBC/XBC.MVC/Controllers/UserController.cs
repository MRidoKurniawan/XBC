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
    }
}