using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repository;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        public ActionResult Index()
        {
            return View();
        }

        // List & Search
        public ActionResult List(string search = "")
        {
            return PartialView("_List", QuestionRepo.All(search));
        }

        // Create
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(QuestionViewModel model)
        {            
            HttpPostedFileBase file = model.imgurl;
            var path = "";
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    // Untuk Mengecek apakah file berformat gambar
                    if ((Path.GetExtension(file.FileName).ToLower() == ".jpg") || (Path.GetExtension(file.FileName).ToLower() == ".png") || (Path.GetExtension(file.FileName).ToLower() == ".gif") || (Path.GetExtension(file.FileName).ToLower() == ".jpeg"))
                    {
                        path = Path.Combine(Server.MapPath("~/Images"), file.FileName);
                        file.SaveAs(path);
                    }
                }
                model.imageUrl = file.FileName;
            }

            ResponseResult result = QuestionRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);

        }

        // Set Choices
        public ActionResult SetC(long id)
        {
            return PartialView("_SetC", QuestionRepo.ById(id));
        }

        [HttpPost]
        public ActionResult SetC(QuestionViewModel model)
        {
            HttpPostedFileBase imga = model.imga;
            HttpPostedFileBase imgb = model.imgb;
            HttpPostedFileBase imgc = model.imgc;
            HttpPostedFileBase imgd = model.imgd;
            HttpPostedFileBase imge = model.imge;
            var path = "";
            if (imga != null) // Image A
            {
                if (imga.ContentLength > 0)
                {
                    // Untuk Mengecek apakah file berformat gambar
                    if ((Path.GetExtension(imga.FileName).ToLower() == ".jpg") || (Path.GetExtension(imga.FileName).ToLower() == ".png") || (Path.GetExtension(imga.FileName).ToLower() == ".gif") || (Path.GetExtension(imga.FileName).ToLower() == ".jpeg"))
                    {
                        path = Path.Combine(Server.MapPath("~/Images"), imga.FileName);
                        imga.SaveAs(path);
                    }
                }
                model.imageA = imga.FileName;
            }
            if (imgb != null) // Image B
            {
                if (imgb.ContentLength > 0)
                {
                    // Untuk Mengecek apakah file berformat gambar
                    if ((Path.GetExtension(imgb.FileName).ToLower() == ".jpg") || (Path.GetExtension(imgb.FileName).ToLower() == ".png") || (Path.GetExtension(imgb.FileName).ToLower() == ".gif") || (Path.GetExtension(imgb.FileName).ToLower() == ".jpeg"))
                    {
                        path = Path.Combine(Server.MapPath("~/Images"), imgb.FileName);
                        imgb.SaveAs(path);
                    }
                }
                model.imageB = imgb.FileName;
            }
            if (imgc != null) //Image C
            {
                if (imgc.ContentLength > 0)
                {
                    // Untuk Mengecek apakah file berformat gambar
                    if ((Path.GetExtension(imgc.FileName).ToLower() == ".jpg") || (Path.GetExtension(imgc.FileName).ToLower() == ".png") || (Path.GetExtension(imgc.FileName).ToLower() == ".gif") || (Path.GetExtension(imgc.FileName).ToLower() == ".jpeg"))
                    {
                        path = Path.Combine(Server.MapPath("~/Images"), imgc.FileName);
                        imgc.SaveAs(path);
                    }
                }
                model.imageC = imgc.FileName;
            }
            if (imgd != null) //Image D
            {
                if (imgd.ContentLength > 0)
                {
                    // Untuk Mengecek apakah file berformat gambar
                    if ((Path.GetExtension(imgd.FileName).ToLower() == ".jpg") || (Path.GetExtension(imgd.FileName).ToLower() == ".png") || (Path.GetExtension(imgd.FileName).ToLower() == ".gif") || (Path.GetExtension(imgd.FileName).ToLower() == ".jpeg"))
                    {
                        path = Path.Combine(Server.MapPath("~/Images"), imgd.FileName);
                        imgd.SaveAs(path);
                    }
                }
                model.imageD = imgd.FileName;
            }
            if (imge != null) //Image E
            {
                if (imge.ContentLength > 0)
                {
                    // Untuk Mengecek apakah file berformat gambar
                    if ((Path.GetExtension(imge.FileName).ToLower() == ".jpg") || (Path.GetExtension(imge.FileName).ToLower() == ".png") || (Path.GetExtension(imge.FileName).ToLower() == ".gif") || (Path.GetExtension(imge.FileName).ToLower() == ".jpeg"))
                    {
                        path = Path.Combine(Server.MapPath("~/Images"), imge.FileName);
                        imge.SaveAs(path);
                    }
                }
                model.imageE = imge.FileName;
            }

            ResponseResult result = QuestionRepo.SetC(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        //Detail
        public ActionResult Details(long id)
        {
            return PartialView("_Details", QuestionRepo.ById(id));
        }

        // Delete
        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", QuestionRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(QuestionViewModel model)
        {
            ResponseResult result = QuestionRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.ErrorMessage,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}