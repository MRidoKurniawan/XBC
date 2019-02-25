﻿using System;
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
            return PartialView("_List", MonitoringRepo.All());
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
            ViewBag.BiodataList = new SelectList(MonitoringRepo.ByNameBiodata(), "id", "name");
            return PartialView("_Edit", MonitoringRepo.ById(id));
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
            return PartialView("_Placement", MonitoringRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Placement(MonitoringViewModel model)
        {
            ResponseResult result = MonitoringRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}