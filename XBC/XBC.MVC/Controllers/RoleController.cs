using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", RoleRepo.All());
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(RoleViewModel model)
        {
            ResponseResult result = RoleRepo.Update(model);
            return Json(new {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit (long id)
        {
            return PartialView("_Edit", RoleRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(RoleViewModel model)
        {
            ResponseResult result = RoleRepo.Update(model);
            return Json(new
                {
                    success = result.Success,
                    message = result.ErrorMessage,
                    entity = result.Entity
                }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", RoleRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(RoleViewModel model)
        {
            ResponseResult result = RoleRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string search="")
        {
            return PartialView("_Search", RoleRepo.GetBySearch(search));
        }

        public ActionResult CheckName(string name="")
        {
            RoleViewModel data = RoleRepo.CheckName(name);
            ResponseResult result = new ResponseResult();
            if (data.name != null)
            {
                result.Success = false;
            }
            return Json(new
            {
                success = result.Success
            }, JsonRequestBehavior.AllowGet);
        }
    }
}