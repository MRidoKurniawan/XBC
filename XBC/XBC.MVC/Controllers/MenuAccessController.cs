using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.ViewModel;
using XBC.Repository;

namespace XBC.MVC.Controllers
{
    public class MenuAccessController : Controller
    {
        // GET: MenuAccess
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return PartialView("_List", MenuAccessRepo.All());
        }
        public ActionResult Create()
        {
            ViewBag.RoleList = new SelectList(RoleRepo.All(), "id", "name");
            ViewBag.MenuList = new SelectList(MenuRepo.All(), "id", "name");
            return PartialView("_Create");
        }
        [HttpPost]
        public ActionResult Create(MenuAccessViewModel model)
        {
            ResponseResult result = MenuAccessRepo.Create(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(long id = 0)
        {
            ViewBag.RoleList = new SelectList(RoleRepo.All(), "id", "name");
            return PartialView("_Search", MenuAccessRepo.GetBySearch(id));
        }
        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", MenuAccessRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Delete(MenuAccessViewModel model)
        {
            ResponseResult result = MenuAccessRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListRole()
        {
            return PartialView("_ListRole", RoleRepo.All());
        }
        public ActionResult ListMenu()
        {
            return PartialView("_ListMenu", MenuRepo.All());
        }
    }
}