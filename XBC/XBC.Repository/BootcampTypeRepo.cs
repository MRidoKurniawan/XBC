using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.ViewModel;
using XBC.DataModel;
using System.Web.Script.Serialization;

namespace XBC.Repository
{
    public class BootcampTypeRepo
    {
        //getall
        public static List<BootcampTypeViewModel> All()
        {
            List<BootcampTypeViewModel> result = new List<BootcampTypeViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from botp in db.t_bootcamp_type
                          where botp.is_delete == false
                          select new BootcampTypeViewModel
                          {
                              id = botp.id,
                              name = botp.name,
                              notes = botp.notes,
                              createdBy = botp.created_by
                          }).ToList();
                if (result == null)
                {
                    result = new List<BootcampTypeViewModel>();
                }
            }
            return result;
        }
        public static BootcampTypeViewModel ById(long id)
        {
            BootcampTypeViewModel result = new BootcampTypeViewModel();
            using (var db = new XBC_Context())
            {
                result = (from botp in db.t_bootcamp_type
                          where botp.id == id && botp.is_delete == false
                          select new BootcampTypeViewModel
                          {
                              id = botp.id,
                              name = botp.name,
                              notes = botp.notes,
                              createdBy = botp.created_by
                          }).FirstOrDefault();
                if (result == null)
                    result = new BootcampTypeViewModel();
            }
            return result;
        }
        public static List<BootcampTypeViewModel> GetBySearch(string search)
        {
            List<BootcampTypeViewModel> result = new List<BootcampTypeViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from botp in db.t_bootcamp_type
                          where botp.name.Contains(search) && botp.is_delete == false
                          select new BootcampTypeViewModel
                          {
                              id = botp.id,
                              name = botp.name,
                              notes = botp.notes,
                              createdBy = botp.created_by
                          }).ToList();
                if (result == null)
                    result = new List<BootcampTypeViewModel>();
            }
            return result;
        }
        public static ResponseResult Update(BootcampTypeViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new XBC_Context())
            {
                if (entity.id == 0)
                {
                    t_bootcamp_type botp = new t_bootcamp_type();
                    botp.name = entity.name;
                    botp.notes = entity.notes;

                    botp.created_by = 01;
                    botp.created_on = DateTime.Now;
                    botp.is_delete = false;

                    db.t_bootcamp_type.Add(botp);
                    db.SaveChanges();

                    var json = new JavaScriptSerializer().Serialize(botp);
                    t_audit_log log = new t_audit_log();
                    log.type = "Insert";
                    log.json_insert = json;

                    log.created_by = 1;
                    log.created_on = DateTime.Now;

                    db.t_audit_log.Add(log);

                    db.SaveChanges();

                    entity.id = botp.id;
                    result.Entity = entity;
                }
                else //edit
                {
                    t_bootcamp_type botp = db.t_bootcamp_type.Where(btp => btp.id == entity.id).FirstOrDefault();
                    if (botp != null)
                    {
                        var json = new JavaScriptSerializer().Serialize(botp);
                        t_audit_log log = new t_audit_log();
                        log.type = "Modify";
                        log.json_before = json;

                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        botp.name = entity.name;
                        botp.notes = entity.notes;

                        botp.modified_by = 01;
                        botp.modified_on = DateTime.Now;

                        var json2 = new JavaScriptSerializer().Serialize(botp);
                        log.json_after = json2;
                        db.t_audit_log.Add(log);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = " Bootcamp Type not found";
                    }
                }
            }
            return result;
        }
        public static ResponseResult Delete(BootcampTypeViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new XBC_Context())
            {
                t_bootcamp_type botp = db.t_bootcamp_type.Where(o => o.id == entity.id).FirstOrDefault();
                if (botp != null)
                {
                    var json = new JavaScriptSerializer().Serialize(botp);
                    t_audit_log log = new t_audit_log();
                    log.type = "Modify";
                    log.json_before = json;

                    log.created_by = 1;
                    log.created_on = DateTime.Now;

                    botp.is_delete = true;
                    botp.deleted_by = 1;
                    botp.deleted_on = DateTime.Now;
                    var json2 = new JavaScriptSerializer().Serialize(botp);
                    log.json_after = json2;
                    db.t_audit_log.Add(log);
                    db.SaveChanges();

                    result.Entity = entity;
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "Bootcamp Type not found!";
                }
            }
            return result;
        }
    }
}
