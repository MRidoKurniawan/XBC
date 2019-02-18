﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return PartialView("_List", MenuRepo.All());
        }

        public ActionResult Search(string search = "")
        {
            return PartialView("_Search", UserRepo.Search(search));
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        public ActionResult sortMenu(long id=0)
        {
            return PartialView("_sortMenu",MenuRepo.sortChild(id));
        }

        [HttpPost]

        public ActionResult Create(MenuViewModel model)
        {
            ResponseResult result = MenuRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

    }
}