using System;
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
            ViewBag.BootcampTypeList = new SelectList(BootcampTypeRepo.All(), "id", "name");
           
            ViewBag.technologyList = new SelectList(TechnologyRepo.All(), "id", "name");
            ViewBag.TerainerList = new SelectList(TrainerRepo.ByTechnology(0), "id", "name"); // Mencari Berdasarkan Technologi

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

        // Edit
        public ActionResult Edit(long id)
        {           
            BatchViewModel model = new BatchViewModel();
            model = BatchRepo.ById(id);

            ViewBag.tanggalmulai = model.periodFrom?.ToString("yyyy'-'MM'-'dd");
            ViewBag.tanggalselesai = model.periodTo?.ToString("yyyy'-'MM'-'dd");

            ViewBag.roomList = new SelectList(RoomRepo.All(), "id", "name");
            ViewBag.BootcampTypeList = new SelectList(BootcampTypeRepo.All(), "id", "name");

            ViewBag.technologyList = new SelectList(TechnologyRepo.All(), "id", "name");
            ViewBag.TerainerList = new SelectList(TrainerRepo.ByTechnology(model.technologyId), "id", "name"); // Mencari Berdasarkan Technologi

            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(BatchViewModel model)
        {
            ResponseResult result = BatchRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }


        // Add Participant
        public ActionResult AddParticipant(long id)
        {
            ViewBag.BiodataList = new SelectList(BatchRepo.ListParticipant(), "id", "name");

            ViewBag.IdBatch = id; // mengirim batch id

            return PartialView("_AddParticipant");
        }

        [HttpPost]
        public ActionResult AddParticipant(ClazzViewModel model)
        {
            ResponseResult result = BatchRepo.Add(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }


        // Set Up Test List
        public ActionResult SetUpTest(long id)
        {
            ViewBag.IdBatch = id; // mengirim bath id


            List<TestViewModel> data = TestRepo.All("");
            foreach (var item in data)
            {
                BatchTestViewModel btmodel = BatchRepo.Check(id, item.id);
                if (btmodel.batch_id == 0)
                {
                    item.check = true;
                }
                else
                {
                    item.check = false;
                }
                
            }

            return PartialView("_SetUpTest", data);
        }

        // Set Up Test Choose
        [HttpPost]
        public ActionResult Choose(BatchTestViewModel model)
        {
            ResponseResult result = BatchRepo.Choose(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        // Set Up Test Cancel
        [HttpPost]
        public ActionResult Cancel(BatchTestViewModel model)
        {
            ResponseResult result = BatchRepo.Cancel(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        // Validalsi Nama Batch Tidak Boleh Sama
        public ActionResult CheckName(string name = "")
        {
            BatchViewModel data = BatchRepo.CheckName(name);
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