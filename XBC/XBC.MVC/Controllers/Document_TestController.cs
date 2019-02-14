using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;

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
    }
}