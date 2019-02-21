using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class TechnologyController : Controller
    {
        // GET: Technology
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return PartialView("_List", TechnologyRepo.All());
        }
        public ActionResult Create()
        {
            return PartialView("_Create");
        }
        [HttpPost]
        public ActionResult Create(TechnologyViewModel model)
        {
            ResponseResult result = TechnologyRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(long id)
        {
            return PartialView("_Edit", TechnologyRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Edit(TechnologyViewModel model)
        {
            ResponseResult result = TechnologyRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", TechnologyRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Delete(TechnologyViewModel model)
        {
            ResponseResult result = TechnologyRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string search = "")
        {
            return PartialView("_Search", TechnologyRepo.GetBySearch(search));
        }

        public ActionResult listTrainer()
        {
            return PartialView("_ListTrainer", new List<TrainerViewModel>());
        }

        public ActionResult addlistTrainer(long id)
        {
            return PartialView("_AddListTrainer", TrainerRepo.ById(id));
        }

        

        public ActionResult AddTrainer()
        {
            ViewBag.TrainerList = new SelectList(TrainerRepo.All(), "id", "name");
            return PartialView("_AddTrainer");
        }
        [HttpPost]
        public ActionResult AddTrainer(TechnologyTrainerViewModel model)
        {
            ResponseResult result = TechnologyRepo.UpdateTrainer(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}