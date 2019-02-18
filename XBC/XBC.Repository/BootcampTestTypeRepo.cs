using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;
using System.Web.Script.Serialization;

namespace XBC.Repository
{
    public class BootcampTestTypeRepo
    {

        public static List<BootcampTestTypeViewModel> All(string search)
        {
            List<BootcampTestTypeViewModel> result = new List<BootcampTestTypeViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from btt in db.t_bootcamp_test_type
                          where (btt.name.Contains(search) && btt.is_delete == false)
                          select new BootcampTestTypeViewModel
                          {
                              id = btt.id,
                              name = btt.name,
                              notes = btt.notes,
                              isDelete = btt.is_delete
                          }).ToList();
            }
            return result == null ? new List<BootcampTestTypeViewModel>() : result;
        }
        public static BootcampTestTypeViewModel ById(long id)
        {
            BootcampTestTypeViewModel result = new BootcampTestTypeViewModel();
            using (var db = new XBC_Context())
            {
                result = (from btt in db.t_bootcamp_test_type
                          where btt.id == id
                          select new BootcampTestTypeViewModel
                          {
                              id = btt.id,
                              name = btt.name,
                              notes = btt.notes,
                              isDelete = btt.is_delete
                          }).FirstOrDefault();
                if (result == null)
                {
                    result = new BootcampTestTypeViewModel();
                }
            }
            return result;
        }
        public static ResponseResult Update(BootcampTestTypeViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    if (entity.id == 0)
                    {
                        t_bootcamp_test_type btt = new t_bootcamp_test_type();
                        btt.name = entity.name;
                        btt.notes = entity.notes;
                        btt.is_delete = false;

                        btt.created_by = 1;
                        btt.created_on = DateTime.Now;
                        db.t_bootcamp_test_type.Add(btt);
                        db.SaveChanges();
                        //insert AuditLog
                        var json = new JavaScriptSerializer().Serialize(btt);
                        t_audit_log log = new t_audit_log();
                        log.type = "Insert";
                        log.json_insert = json;
                        log.created_by = 1;
                        log.created_on = DateTime.Now;
                        db.t_audit_log.Add(log);
                        db.SaveChanges();

                        entity.id = btt.id;
                        result.Entity = entity;
                    }
                    else
                    {
                        t_bootcamp_test_type btt = db.t_bootcamp_test_type.Where(o => o.id == entity.id).FirstOrDefault();
                        if (btt != null)
                        {
                            var jsonbefore = new JavaScriptSerializer().Serialize(btt);
                            btt.name = entity.name;
                            btt.notes = entity.notes;
                            btt.is_delete = false;

                            btt.modified_by = 1;
                            btt.modified_on = DateTime.Now;

                            db.SaveChanges();
                            var json = new JavaScriptSerializer().Serialize(btt);

                            t_audit_log log = new t_audit_log();
                            log.type = "Modified";
                            log.json_insert = json;

                            log.created_by = 1;
                            log.created_on = DateTime.Now;

                            db.t_audit_log.Add(log);
                            db.SaveChanges();
                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.ErrorMessage = "Bootcamp Test Type not Found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ResponseResult Delete(BootcampTestTypeViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_bootcamp_test_type btt = db.t_bootcamp_test_type.Where(o => o.id == entity.id).FirstOrDefault();
                    if (btt != null)
                    {
                        btt.is_delete = true;
                        btt.deleted_by = 1;
                        btt.created_on = DateTime.Now;
                        db.SaveChanges();
                        var json = new JavaScriptSerializer().Serialize(btt);

                        t_audit_log log = new t_audit_log();
                        log.type = "Delete";
                        log.json_insert = json;

                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        db.t_audit_log.Add(log);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Bootcamp Test Type Not Found";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
