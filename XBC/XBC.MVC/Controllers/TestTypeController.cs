﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class TestTypeController : Controller
    {
        // GET: TestType
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return PartialView("_List", TestTypeRepo.All());
        }
        public ActionResult Create()
        {
            return PartialView("_Create");
        }
        [HttpPost]
        public ActionResult Create(TestTypeViewModel model)
        {
            ResponseResult result = TestTypeRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string search = "")
        {
            return PartialView("_Search", TestTypeRepo.GetBySearch(search));
        }
        public ActionResult Edit(long id)
        {
            return PartialView("_Edit", TestTypeRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Edit(TestTypeViewModel model)
        {
            ResponseResult result = TestTypeRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", TestTypeRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Delete(TestTypeViewModel model)
        {
            ResponseResult result = TestTypeRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}