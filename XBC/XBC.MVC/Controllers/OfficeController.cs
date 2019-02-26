using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class OfficeController : Controller
    {
        // GET: Office
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string search = "")
        {
            return PartialView("_List", OfficeRepo.OfficeAll(search));
        }
        public ActionResult Create()
        {
            return PartialView("_Create");
        }
        [HttpPost]
        public ActionResult Create(OfficeViewModel model)
        {
            ResponseResult result = OfficeRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListByOffice(long id)
        {
            ViewBag.id = id;
            return PartialView("_ListByOffice", OfficeRepo.ByOfficeId(id));
        }
        public ActionResult CreateRoom(long id=0)
        {
            ViewBag.officeId = id;
            return PartialView("_CreateRoom");
        }
        public ActionResult AddListRoom(RoomViewModel model)
        {
            return PartialView("_AddListRoom", model);
        }
        public ActionResult Edit(long id)
        {
            return PartialView("_Edit", OfficeRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Edit(OfficeViewModel model)
        {
            ResponseResult result = OfficeRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddRoom(long id)
        {
            ViewBag.officeId = id;
            return PartialView("_AddRoom");
        }
        [HttpPost]
        public ActionResult AddRoom(RoomViewModel model)
        {
            ResponseResult result = RoomRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditRoom(long id = 0, long officeid = 0)
        {
            ViewBag.officeId = officeid;
            return PartialView("_EditRoom", RoomRepo.ById(id));
        }
        [HttpPost]
        public ActionResult EditRoom(RoomViewModel model)
        {
            ResponseResult result = RoomRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", OfficeRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Delete(OfficeViewModel model)
        {
            ResponseResult result = OfficeRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteRoom(long id, long officeid)
        {
            ViewBag.officeId = officeid;
            return PartialView("_DeleteRoom", RoomRepo.ById(id));
        }
        [HttpPost]
        public ActionResult DeleteRoom(RoomViewModel model)
        {
            ResponseResult result = RoomRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

    }
}