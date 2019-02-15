using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return PartialView("_List", RoomRepo.All());
        }
        public ActionResult Create()
        {
            return PartialView("_Create");
        }
        [HttpPost]
        public ActionResult Create(RoomViewModel model)
        {
            ResponseResult result = RoomRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(long id)
        {
            return PartialView("_Edit", RoomRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Edit(RoomViewModel model)
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
            return PartialView("_Delete", RoomRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Delete(RoomViewModel model)
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