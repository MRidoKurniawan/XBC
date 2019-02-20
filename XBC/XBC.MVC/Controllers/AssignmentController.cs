using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class AssignmentController : Controller
    {
        // GET: Assignment
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            ViewBag.tanggalharini = DateTime.Now.ToString("yyyy'-'MM'-'dd");
            List<AssignmentViewModel> Assignment = new List<AssignmentViewModel>(
                AssignmentRepo.All()
            );
            foreach (var item in Assignment)
            {
                item.TanggalMulai = item.start_date.ToString("dd'-'MM'-'yyyy");
                item.TanggalSelesai = item.end_date.ToString("dd'-'MM'-'yyyy");
            }
            return PartialView("_List", Assignment);
        }
        public ActionResult Create()
        {
            
            ViewBag.BiodataList = new SelectList(BiodataRepo.All(), "id", "name");
            return PartialView("_Create");
        }

        public ActionResult Search(DateTime search= default(DateTime))
        {
            ViewBag.tanggalharini = DateTime.Now.ToString("yyyy'-'MM'-'dd");
            List<AssignmentViewModel> Assignment = new List<AssignmentViewModel>(
                AssignmentRepo.Search(search)
            );
            foreach (var item in Assignment)
            {
                item.TanggalMulai = item.start_date.ToString("dd'-'MM'-'yyyy");
                item.TanggalSelesai = item.end_date.ToString("dd'-'MM'-'yyyy");
            }
            return PartialView("_Search", Assignment);
        }

        [HttpPost]
        public ActionResult Create(AssignmentViewModel model)
        {
            ResponseResult result = AssignmentRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Edit(long id)
        {
            AssignmentViewModel assignment = new AssignmentViewModel();
            assignment = AssignmentRepo.ById(id);
            ViewBag.tanggalmulai = assignment.start_date.ToString("yyyy'-'MM'-'dd");
            ViewBag.tanggalselesai = assignment.end_date.ToString("yyyy'-'MM'-'dd");
            ViewBag.BiodataList = new SelectList(BiodataRepo.All(), "id", "name");
            return PartialView("_Edit", assignment);
        }

        [HttpPost]
        public ActionResult Edit(AssignmentViewModel model)
        {
            ResponseResult result = AssignmentRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", AssignmentRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(AssignmentViewModel model)
        {
            ResponseResult result = AssignmentRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Hold(long id)
        {
            return PartialView("_Hold", AssignmentRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Hold(AssignmentViewModel model)
        {
            ResponseResult result = AssignmentRepo.Hold(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Done(long id)
        {
            return PartialView("_Done", AssignmentRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Done(AssignmentViewModel model)
        {
            ResponseResult result = AssignmentRepo.MarkAsDone(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

    }
}