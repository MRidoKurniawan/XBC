using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: Feedback
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", FeedbackRepo.All());
        }

        public ActionResult ChooseDocument(long id)
        {
            return PartialView("_ChooseDocument", FeedbackRepo.ListDT(id));
        }

        public ActionResult Add1(long id)
        {
            return PartialView("_Add1", FeedbackRepo.ListDTD(id));
        }

        public ActionResult Add(long id)
        {
            return View("Add", FeedbackRepo.ListDTD1(id));
        }

        [HttpPost]
        public ActionResult Add(FeedbackViewModel model)
        {
            ResponseResult result = FeedbackRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

    }
}