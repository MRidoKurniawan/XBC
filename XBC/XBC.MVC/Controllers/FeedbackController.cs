using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;

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

        public ActionResult Add(long id)
        {
            return PartialView("_Add", FeedbackRepo.ListDTD(id));
        }

    }
}