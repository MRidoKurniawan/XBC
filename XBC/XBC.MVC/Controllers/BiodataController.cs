using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class BiodataController : Controller
    {
        // GET: Biodata
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", BiodataRepo.All());
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create (BiodataViewModel model)
        {
            ResponseResult result = BiodataRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit (long id)
        {
            ViewBag.rdyes = null;
            ViewBag.rdno = null;
            if (BiodataRepo.GetGender(id).Equals("M"))
            {
                ViewBag.rdyes = "checked";
            }
            else if (BiodataRepo.GetGender(id).Equals("F"))
            {
                ViewBag.rdno = "checked";
            }
                ViewBag.BootcampTestTypeList = new SelectList(BiodataRepo.ByBtt(), "id", "name");
                return PartialView("_Edit", BiodataRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(BiodataViewModel model)
        {
            ResponseResult result = BiodataRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", BiodataRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(BiodataViewModel model)
        {
            ResponseResult result = BiodataRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string search="")
        {
            return PartialView("_Search", BiodataRepo.GetBySearch(search));
        }
    }
}