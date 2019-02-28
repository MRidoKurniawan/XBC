using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;


namespace XBC.MVC.Controllers
{
    public class BootcampTestTypeController : Controller
    {
        // GET: Room
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string search = "")
        {
            ViewBag.id = new SelectList(UserRepo.All(), "id", "username");
            return PartialView("_List", BootcampTestTypeRepo.All(search));
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }
        [HttpPost]
        public ActionResult Create(BootcampTestTypeViewModel model)
        {
            ResponseResult result = BootcampTestTypeRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(long id)
        {
            return PartialView("_Edit", BootcampTestTypeRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Edit(BootcampTestTypeViewModel model)
        {
            ResponseResult result = BootcampTestTypeRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", BootcampTestTypeRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Delete(BootcampTestTypeViewModel model)
        {
            ResponseResult result = BootcampTestTypeRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}