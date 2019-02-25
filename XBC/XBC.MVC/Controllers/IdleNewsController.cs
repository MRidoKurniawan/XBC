using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class IdleNewsController : Controller
    {
        // GET: IdleNews
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return PartialView("_List", IdleNewsRepo.All());
        }
        public ActionResult Create()
        {
            ViewBag.CategoryList = new SelectList(CategoryRepo.GetBySearch(""), "id", "name");
            return PartialView("_Create");
        }
        [HttpPost]
        public ActionResult Create(IdleNewsViewModel model)
        {
            ResponseResult result = IdleNewsRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string search = " " )
        {
            return PartialView("_Search", IdleNewsRepo.GetBySearch(search));
        }
        public ActionResult Edit(long id)
        {
            ViewBag.CategoryList = new SelectList(CategoryRepo.GetBySearch(""), "id", "name");
            return PartialView("_Edit", IdleNewsRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Edit(IdleNewsViewModel model)
        {
            ResponseResult result = IdleNewsRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Publish(long id)
        {
            return PartialView("_Publish", IdleNewsRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Publish(IdleNewsViewModel model)
        {
            ResponseResult result = IdleNewsRepo.Publish(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Share(long id)
        {
            return PartialView("_Share");
        }
        [HttpPost]
        public ActionResult Share(IdleNewsViewModel model)
        {
            return RedirectToAction("Index", "IdleNews");
        }

        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", IdleNewsRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Delete(IdleNewsViewModel model)
        {
            ResponseResult result = IdleNewsRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}