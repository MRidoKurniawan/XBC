using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class TestimonyController : Controller
    {
        // GET: Testimony
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return PartialView("_List", TestimonyRepo.All());
        }
        public ActionResult Create()
        {
            return PartialView("_Create");
        }
        [HttpPost]
        public ActionResult Create(TestimonyViewModel model)
        {
            ResponseResult result = TestimonyRepo.Update(model);
            return Json(new
                {
                    success = result.Success,
                    message = result.ErrorMessage,
                    entity = result.Entity
                }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(long id)
        {
            return PartialView("_Edit", TestimonyRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Edit(TestimonyViewModel model)
        {
            ResponseResult result = TestimonyRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", TestimonyRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Delete(TestimonyViewModel model)
        {
            ResponseResult result = TestimonyRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string search = "")
        {
            return PartialView("_Search", TestimonyRepo.GetBySearch(search));
        }
    }
}