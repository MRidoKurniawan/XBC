using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.DataModel;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string search = "")
        {
            ViewBag.BatchName = new SelectList(BatchRepo.All(search), "id", "name");
            return PartialView("_List", ClazzRepo.BySearch(search));
        }
        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", ClazzRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Delete(ClazzViewModel model)
        {
            ResponseResult result = ClazzRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}