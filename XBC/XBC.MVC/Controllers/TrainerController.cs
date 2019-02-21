using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class TrainerController : Controller
    {
        // GET: Trainer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", TrainerRepo.All());
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }
        [HttpPost]
        public ActionResult Create(TrainerViewModel model)
        {
            ResponseResult result = TrainerRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(long id)
        {
            return PartialView("_Edit", TrainerRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Edit(TrainerViewModel model)
        {
            ResponseResult result = TrainerRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", TrainerRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Delete(TrainerViewModel model)
        {
            ResponseResult result = TrainerRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search (string search ="")
        {
            return PartialView("_Search", TrainerRepo.GetBySearch(search));
        }


        // Get By Technology (Punya Rido)
        public ActionResult ListByTechnology(long id)
        {
            return PartialView("_ListByTechnology", TrainerRepo.ByTechnology(id));
        }
    }
}