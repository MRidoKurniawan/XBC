using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class MonitoringController : Controller
    {
        // GET: Monitoring
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            List<MonitoringViewModel> model = new List<MonitoringViewModel>(); //.
            model = MonitoringRepo.All(); //.
            foreach (var item in model)
            {
                item.date_placement = item.placement_date?.ToString("yyyy'-'MM'-'dd");
                item.date_idle = item.idle_date.ToString("yyyy'-'MM'-'dd");
            }
            //ViewBag.idle_date = model.idle_date.ToString("yyyy'-'MM'-'dd"); //.(BIAR DI LIST IDLE_DATE CUMA MUNCUL TANGGAL)
            //ViewBag.placement_date = model.placement_date?.ToString("yyyy'-'MM'-'dd"); //.(BIAR DI LIST PLACEMENT_DATE CUMA MUNCUL TANGGAL)
            return PartialView("_List", model);
        }

        public ActionResult Create()
        {
            ViewBag.BiodataList = new SelectList(MonitoringRepo.ByNameBiodata(), "id", "name");
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(MonitoringViewModel model)
        {
            ResponseResult result = MonitoringRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit (long id)
        {
            MonitoringViewModel model = new MonitoringViewModel(); //.
            model = MonitoringRepo.ById(id); //.

            ViewBag.idle_date = model.idle_date.ToString("yyyy'-'MM'-'dd"); //.(BIAR PAS NGEDIT ADA TANGGAL LAMANYA)
            ViewBag.BiodataList = new SelectList(MonitoringRepo.ByNameBiodataforEdit(model.biodata_id), "id", "name");
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(MonitoringViewModel model)
        {
            ResponseResult result = MonitoringRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", MonitoringRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(MonitoringViewModel model)
        {
            ResponseResult result = MonitoringRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string search="")
        {
            return PartialView("_Search", MonitoringRepo.GetBySearch(search));
        }

        public ActionResult Placement(long id)
        {
            MonitoringViewModel model = new MonitoringViewModel();
            model = MonitoringRepo.ById(id);

            ViewBag.placement_date = model.placement_date?.ToString("yyyy'-'MM'-'dd");
            return PartialView("_Placement", MonitoringRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Placement(MonitoringViewModel model)
        {
            ResponseResult result = MonitoringRepo.Placement(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}