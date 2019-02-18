﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class BatchController : Controller
    {
        // GET: Batch
        public ActionResult Index()
        {
            return View();
        }

        // List & Search
        public ActionResult List(string search = "")
        {
            return PartialView("_List", BatchRepo.All(search));
        }

        // Create
        public ActionResult Create()
        {
            ViewBag.roomList = new SelectList(RoomRepo.All(), "id", "name");

            ViewBag.TerainerList = new SelectList(TrainerRepo.All(), "id", "name");
            ViewBag.technologyList = new SelectList(TechnologyRepo.All(), "id", "name");

            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(BatchViewModel model)
        {
            ResponseResult result = BatchRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}