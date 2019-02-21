using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

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
            List<MenuViewModel> menu = new List<MenuViewModel>(
                MenuRepo.All()
            );
            foreach(var item in menu)
            {
                item.ParentName = MenuRepo.getParentName(item.menu_parent == null ? 0 : (long)item.menu_parent);
            }
            return PartialView("_List", menu);
        }

        public ActionResult Search(string search = "")
        {
            List<MenuViewModel> menu = new List<MenuViewModel>(
                MenuRepo.Search(search)
            );
            foreach (var item in menu)
            {
                item.ParentName = MenuRepo.getParentName(item.menu_parent == null ? 0 : (long)item.menu_parent);
            }
            return PartialView("_Search", menu);
        }

        public ActionResult Create()
        {
            ViewBag.MenuList = new SelectList(MenuRepo.All(), "id", "title");
            return PartialView("_Create");
        }


        public ActionResult sortMenu(long id=0)
        {
            return PartialView("_sortMenu",MenuRepo.sortChild(id));
        }

        [HttpPost]

        public ActionResult Create(MenuViewModel model)
        {
            ResponseResult result = MenuRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(long id)
        {
            ViewBag.MenuList = new SelectList(MenuRepo.All(), "id", "title");
            return PartialView("_Edit", MenuRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(MenuViewModel model)
        {
            ResponseResult result = MenuRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", MenuRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(MenuViewModel model)
        {
            ResponseResult result = MenuRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

    }
}