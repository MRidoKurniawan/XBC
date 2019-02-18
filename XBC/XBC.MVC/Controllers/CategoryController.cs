using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string search ="")
        {
            return PartialView("_List", CategoryRepo.GetBySearch(search));
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModel model)
        {
            ResponseResult result = CategoryRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                messgae = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(long id)
        {
            return PartialView("_Edit", CategoryRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModel model)
        {
            ResponseResult result = CategoryRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                messgae = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", CategoryRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(CategoryViewModel model)
        {
            ResponseResult result = CategoryRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                messgae = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetToken()
        //{
        //    return Json(ra);
        //}
    }
}