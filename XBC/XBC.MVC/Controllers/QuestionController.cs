using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        public ActionResult Index()
        {
            return View();
        }

        // List & Search
        public ActionResult List(string search = "")
        {
            return PartialView("_List", QuestionRepo.All(search));
        }

        // Create
        public ActionResult Create()
        {
            ViewBag.CategoryList = new SelectList("Id", "Name");
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(QuestionViewModel model)
        {
            ResponseResult result = QuestionRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        //Edit

        // Set Choices
        public ActionResult SetC(long id)
        {
            return PartialView("_SetC", QuestionRepo.ById(id));
        }

        [HttpPost]
        public ActionResult SetC(QuestionViewModel model)
        {
            ResponseResult result = QuestionRepo.SetC(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        //Detail
        public ActionResult Details(long id)
        {
            return PartialView("_Details", QuestionRepo.ById(id));
        }

        // Delete
        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", QuestionRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(QuestionViewModel model)
        {
            ResponseResult result = QuestionRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}