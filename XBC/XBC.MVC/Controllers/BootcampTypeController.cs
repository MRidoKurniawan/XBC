using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class BootcampTypeController : Controller
    {
        // GET: BootcampType
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return PartialView("_List", BootcampTypeRepo.All());
        }
        public ActionResult create()
        {
            return PartialView("_Create");
        }
        [HttpPost]
        public ActionResult Create(BootcampTypeViewModel model)
        {
            ResponseResult result = BootcampTypeRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string search = "")
        {
            return PartialView("_Search", BootcampTypeRepo.GetBySearch(search));
        }
        public ActionResult Edit(long id)
        {
            return PartialView("_Edit", BootcampTypeRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Edit(BootcampTypeViewModel model)
        {
            ResponseResult result = BootcampTypeRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", BootcampTypeRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Delete(BootcampTypeViewModel model)
        {
            ResponseResult result = BootcampTypeRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}