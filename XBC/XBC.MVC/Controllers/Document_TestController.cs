using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class Document_TestController : Controller
    {
        // GET: Documen_Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string search = "")
        {
            return PartialView("_List", Document_TestRepo.GetBySearch(search));
        }

        public ActionResult Create()
        {
            ViewBag.TestList = new SelectList(TestRepo.All(""), "id", "name");
            ViewBag.Test_TypeList = new SelectList(Test_TypeRepo.All(""), "id", "name");
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(Document_TestViewModel model)
        {
            ResponseResult result = Document_TestRepo.Update(model/*, model.test_type_id*/);
            return Json(new
            {
                success = result.Success,
                messgae = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", Document_TestRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(Document_TestViewModel model)
        {
            ResponseResult result = Document_TestRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                messgae = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(long id)
        {
            ViewBag.TestList = new SelectList(TestRepo.All(""), "id", "name");
            ViewBag.Test_TypeList = new SelectList(Test_TypeRepo.All(""), "id", "name");
            return PartialView("_Edit", Document_TestRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(Document_TestViewModel model)
        {
            ResponseResult result = Document_TestRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                messgae = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

    }
}