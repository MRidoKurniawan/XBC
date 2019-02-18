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
    public class TrainerRepo
    {
        //GetAll
        public static List<TrainerViewModel> All()
        {
            List<TrainerViewModel> result = new List<TrainerViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from tra in db.t_trainer
                          where tra.is_delete == false
                          select new TrainerViewModel
                          {
                              id = tra.id,
                              name = tra.name,
                              notes = tra.notes,
                              is_delete = tra.is_delete
                          }).ToList();

                if (result == null)
                    result = new List<TrainerViewModel>();
            }
            return result;
        }

        //GetById
        public static TrainerViewModel ById(long id)
        {
            TrainerViewModel result = new TrainerViewModel();
            using (var db = new XBC_Context())
            {
                result = (from tra in db.t_trainer
                          where tra.id == id && tra.is_delete == false
                          select new TrainerViewModel
                          {
                              id = tra.id,
                              name = tra.name,
                              notes = tra.notes,
                              is_delete = tra.is_delete
                          }).FirstOrDefault();
                if (result == null)
                    result = new TrainerViewModel();
                return result;
            }
        }
        public static ResponseResult Update(TrainerViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    //Create
                    if (entity.id == 0)
                    {
                        t_trainer tra = new t_trainer();
                        tra.name = entity.name;
                        tra.notes = entity.notes;
                        tra.is_delete = entity.is_delete;
                        tra.created_by = 1;
                        tra.created_on = DateTime.Now;
                        db.t_trainer.Add(tra);
                        db.SaveChanges();

                        var json = new JavaScriptSerializer().Serialize(tra);

                        t_audit_log log = new t_audit_log();
                        log.type = "Insert";
                        log.json_insert = json;

                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        db.t_audit_log.Add(log);

                        db.SaveChanges();
                        entity.id = tra.id;
                        result.Entity = entity;
                    }
                    //edit
                    else
                    {
                        t_trainer tra = db.t_trainer
                            .Where(t => t.id == entity.id)
                            .FirstOrDefault();

                        if (tra != null)
                        {
                            var json = new JavaScriptSerializer().Serialize(tra);
                            t_audit_log log = new t_audit_log();
                            log.type = "Modify";
                            log.json_before = json;

                            log.created_by = 1;
                            log.created_on = DateTime.Now;

                            tra.name = entity.name;
                            tra.notes = entity.notes;

                            tra.modified_by = 1;
                            tra.modified_on = DateTime.Now;

                            var json2 = new JavaScriptSerializer().Serialize(tra);
                            log.json_after = json2;
                            db.t_audit_log.Add(log);
                            db.SaveChanges();

                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.ErrorMessage = "Trainer Not Found!";
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

        public static ResponseResult Delete(TrainerViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_trainer tra = db.t_trainer
                        .Where(t => t.id == entity.id)
                        .FirstOrDefault();

                    if (tra != null)
                    {
                        var json = new JavaScriptSerializer().Serialize(tra);
                        t_audit_log log = new t_audit_log();
                        log.type = "Modify";
                        log.json_before = json;
                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        tra.is_delete = true;
                        tra.deleted_by = 1;
                        tra.deleted_on = DateTime.Now;

                        var json2 = new JavaScriptSerializer().Serialize(tra);
                        log.json_after = json2;
                        db.t_audit_log.Add(log);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Trainer Not Found";
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

        public static List<TrainerViewModel> GetBySearch(String search)
        {
            List<TrainerViewModel> result = new List<TrainerViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from tra in db.t_trainer
                          where tra.name.Contains(search) && tra.is_delete == false
                             select new TrainerViewModel
                             {
                                 id = tra.id,
                                 name = tra.name,
                                 notes = tra.notes
                             }).ToList();
            }
            return result == null ? new List<TrainerViewModel>() : result;
        }
    }
}
