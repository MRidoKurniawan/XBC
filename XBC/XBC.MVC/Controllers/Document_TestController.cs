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
            ViewBag.Test_TypeList = new SelectList(TestTypeRepo.GetBySearch(""), "id", "name");
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(Document_TestViewModel model)
        {
            ResponseResult result = Document_TestRepo.Update(model/*, model.test_type_id*/);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
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
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(long id)
        {
            ViewBag.TestList = new SelectList(TestRepo.All(""), "id", "name");
            ViewBag.Test_TypeList = new SelectList(TestTypeRepo.GetBySearch(""), "id", "name");
            return PartialView("_Edit", Document_TestRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(Document_TestViewModel model)
        {
            ResponseResult result = Document_TestRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListEdit(long id)
        {
            return PartialView("_ListEdit", Document_Test_DetailRepo.ByID(id));
        }

        public ActionResult OrderByQuestion(long id)
        {
            //id => Product Id
            return PartialView("_OrderByQuestion", Document_Test_DetailRepo.ByQuestion(id));
        }

        public ActionResult AddQuestion(long id)
        {
            ViewBag.QuestionList = new SelectList(QuestionRepo.All(""), "id", "question");
            return PartialView("_AddQuestion",Document_Test_DetailRepo.By(id));
        }

        [HttpPost]
        public ActionResult AddQuestion(Document_Test_DetailViewModel model)
        {
            ResponseResult result = Document_Test_DetailRepo.Update(model/*, model.test_type_id*/);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteQuestion(long id)
        {
            return PartialView("_DeleteQuestion", Document_Test_DetailRepo.Byiddtd(id));
        }

        [HttpPost]
        public ActionResult DeleteQuestion(Document_Test_DetailViewModel model)
        {
            ResponseResult result = Document_Test_DetailRepo.DeleteQuestion(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GenerateCreate()
        {
            return Json(new
            {
                generate = Document_TestRepo.RandomString()
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewVersion(long id)
        {
            return Json(new
            {
                generate = Document_TestRepo.GetNewVersion(id)
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CopyDocument(long id)
        {
            ViewBag.TestList = new SelectList(TestRepo.All(""), "id", "name");
            ViewBag.Test_TypeList = new SelectList(TestTypeRepo.GetBySearch(""), "id", "name");
            return PartialView("_CopyDocument", Document_TestRepo.ByIdCopyDocument(id));
        }

        [HttpPost]
        public ActionResult CopyDocument(Document_TestViewModel model)
        {
            ResponseResult result = Document_TestRepo.CopyDocument(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewTest(long id)
        {
            return View("ViewTest", Document_TestRepo.ViewTest(id));
        }
    }
}