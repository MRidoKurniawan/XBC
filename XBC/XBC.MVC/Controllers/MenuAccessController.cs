using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.ViewModel;
using XBC.Repository;

namespace XBC.MVC.Controllers
{
    public class MenuAccessController : Controller
    {
        // GET: MenuAccess
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return PartialView("_List", MenuAccessRepo.All());
        }
    }
}