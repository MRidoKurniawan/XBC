using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        // List & Search
        public ActionResult List(string search = "")
        {
            return PartialView("_List", TestRepo.All(search));
        }

        // Create
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(TestViewModel model)
        {
            ResponseResult result = TestRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        // Edit
        public ActionResult Edit(long id)
        {
            return PartialView("_Edit", TestRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(TestViewModel model)
        {
            ResponseResult result = TestRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        // Delete
        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", TestRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(TestViewModel model)
        {
            ResponseResult result = TestRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }


        // Validalsi Nama Test Tidak Boleh Sama
        public ActionResult CheckName(string name = "")
        {
            TestViewModel data = TestRepo.CheckName(name);
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