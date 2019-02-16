using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;

namespace XBC.MVC.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return PartialView("_List", MenuRepo.All());
        }

        public ActionResult Search(string search = "")
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

    }
}