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
    public class TechnologyRepo
    {
        //GetAll
        public static List<TechnologyViewModel> All()
        {
            List<TechnologyViewModel> result = new List<TechnologyViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from tec in db.t_technology
                          where tec.is_delete == false
                          select new TechnologyViewModel
                          {
                              id = tec.id,
                              name = tec.name,
                              notes = tec.notes,
                              created_by = tec.created_by,
                              is_delete = tec.is_delete
                          }).ToList();
                if (result == null)
                    result = new List<TechnologyViewModel>();
            }
            return result;
        }
        public static ResponseResult Update(TechnologyViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    //Create
                    if (entity.id == 0)
                    {
                        t_technology tec = new t_technology();
                        tec.name = entity.name;
                        tec.notes = entity.notes;
                        tec.is_delete = entity.is_delete;
                        tec.created_by = 1;
                        tec.created_on = DateTime.Now;
                        db.t_technology.Add(tec);
                        db.SaveChanges();
                        entity.id = tec.id;

                        foreach (var item in entity.Details)
                        {
                            t_technology_trainer ttr = new t_technology_trainer();
                            ttr.technology_id = entity.id;
                            ttr.trainer_id = item.id;
                            ttr.created_by = 1;
                            ttr.created_on = DateTime.Now;
                            db.t_technology_trainer.Add(ttr);

                            object datattr = new
                            {
                                ttr.id,
                                ttr.trainer_id,
                                ttr.technology_id
                            };
                            var jsonttr = new JavaScriptSerializer().Serialize(datattr);

                            t_audit_log logttr = new t_audit_log();
                            logttr.type = "Insert";
                            logttr.json_insert = jsonttr;
                            logttr.created_by = 1;
                            logttr.created_on = DateTime.Now;

                            db.t_audit_log.Add(logttr);
                            db.SaveChanges();
                        }

                        object data = new
                        {
                            tec.id,
                            tec.name,
                            tec.notes
                        };
                        var json = new JavaScriptSerializer().Serialize(data);

                        t_audit_log log = new t_audit_log();
                        log.type = "Insert";
                        log.json_insert = json;
                        log.created_by = 1;
                        log.created_on = DateTime.Now;
                        db.t_audit_log.Add(log);

                        db.SaveChanges();
                        result.Entity = entity;
                    }
                    //Edit
                    else
                    {
                        t_technology tec = db.t_technology
                            .Where(t => t.id == entity.id)
                            .FirstOrDefault();

                        if (tec != null)
                        {
                            object data = new
                            {
                                tec.id,
                                tec.name,
                                tec.notes
                            };
                            var json = new JavaScriptSerializer().Serialize(data);

                            t_audit_log log = new t_audit_log();
                            log.type = "Modify";
                            log.json_before = json;

                            log.created_by = 1;
                            log.created_on = DateTime.Now;

                            tec.name = entity.name;
                            tec.notes = entity.notes;
                            tec.modified_by = 1;
                            tec.modified_on = DateTime.Now;
                            entity.id = tec.id;
                            if (entity.Details != null)
                            {
                                foreach (var item in entity.Details)
                                {
                                    t_technology_trainer ttr = new t_technology_trainer();
                                    ttr.technology_id = entity.id;
                                    ttr.trainer_id = item.id;
                                    ttr.created_by = 1;
                                    ttr.created_on = DateTime.Now;
                                    db.t_technology_trainer.Add(ttr);

                                    object datattr = new
                                    {
                                        ttr.id,
                                        ttr.trainer_id,
                                        ttr.technology_id
                                    };
                                    var jsonttr = new JavaScriptSerializer().Serialize(datattr);

                                    t_audit_log logttr = new t_audit_log();
                                    logttr.type = "Insert";
                                    logttr.json_insert = jsonttr;
                                    logttr.created_by = 1;
                                    logttr.created_on = DateTime.Now;

                                    db.t_audit_log.Add(logttr);
                                }
                            }

                            object data2 = new
                            {
                                tec.id,
                                tec.name,
                                tec.notes
                            };
                            var json2 = new JavaScriptSerializer().Serialize(data2);
                            log.json_after = json2;
                            db.t_audit_log.Add(log);
                            db.SaveChanges();

                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.ErrorMessage = "Technology Not Found!";
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

        public static ResponseResult Delete(TechnologyViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_technology tec = db.t_technology
                        .Where(t => t.id == entity.id)
                        .FirstOrDefault();

                    if (tec != null)
                    {
                        object data = new
                        {
                            tec.id,
                            tec.name,
                            tec.notes,
                            tec.is_delete
                        };
                        var json = new JavaScriptSerializer().Serialize(data);
                        t_audit_log log = new t_audit_log();
                        log.type = "Modify";
                        log.json_before = json;
                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        tec.is_delete = true;
                        tec.deleted_by = 1;
                        tec.deleted_on = DateTime.Now;

                        object data2 = new
                        {
                            tec.id,
                            tec.name,
                            tec.notes,
                            tec.is_delete
                        };
                        var json2 = new JavaScriptSerializer().Serialize(data2);
                        log.json_after = json2;
                        db.t_audit_log.Add(log);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Technology Not Found";
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

        //GetById
        public static TechnologyViewModel ById(long id)
        {
            TechnologyViewModel result = new TechnologyViewModel();
            using (var db = new XBC_Context())
            {
                result = (from tec in db.t_technology
                          where tec.id == id && tec.is_delete == false
                          select new TechnologyViewModel
                          {
                              id = tec.id,
                              name = tec.name,
                              notes = tec.notes,
                              created_by = tec.created_by,
                              is_delete = tec.is_delete
                          }).FirstOrDefault();
                if (result == null)
                    result = new TechnologyViewModel();
                return result;
            }
        }

        public static List<TechnologyViewModel> GetBySearch(String search)
        {
            List<TechnologyViewModel> result = new List<TechnologyViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from tec in db.t_technology
                          where tec.name.Contains(search) && tec.is_delete == false
                          select new TechnologyViewModel
                          {
                              id = tec.id,
                              name = tec.name,
                              notes = tec.notes,
                              created_by = tec.created_by
                          }).ToList();
            }
            return result == null ? new List<TechnologyViewModel>() : result;
        }

        public static List<TechnologyTrainerViewModel> ListTrainer(long id)
        {
            List<TechnologyTrainerViewModel> result = new List<TechnologyTrainerViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from ttr in db.t_technology_trainer
                          join tra in db.t_trainer on ttr.trainer_id equals tra.id
                          where tra.is_delete == false && ttr.technology_id == id
                          select new TechnologyTrainerViewModel
                          {
                              id = ttr.id,
                              trainer_id = ttr.trainer_id,
                              trainer_name = tra.name,
                              technology_id = ttr.technology_id,
                              created_by = 1,
                              created_on = DateTime.Now
                          }).ToList();
                if (result == null)
                    result = new List<TechnologyTrainerViewModel>();
            }
            return result;
        }
        public static ResponseResult DeleteTrainer(TechnologyTrainerViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_technology_trainer ttr = db.t_technology_trainer
                        .Where(t => t.id == entity.id)
                        .FirstOrDefault();

                    if (ttr != null)
                    {
                        object data = new
                        {
                            ttr.trainer_id,
                        };
                        var json = new JavaScriptSerializer().Serialize(data);
                        t_audit_log log = new t_audit_log();
                        log.type = "Delete";
                        log.json_delete = json;
                        log.created_by = 1;
                        log.created_on = DateTime.Now;
                        db.t_audit_log.Add(log);

                        db.t_technology_trainer.Remove(ttr);
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
        public static TechnologyTrainerViewModel TrainerById(long id)
        {
            TechnologyTrainerViewModel result = new TechnologyTrainerViewModel();
            using (var db = new XBC_Context())
            {
                result = (from ttr in db.t_technology_trainer
                          join tra in db.t_trainer on ttr.trainer_id equals tra.id
                          where ttr.id == id && tra.is_delete == false
                          select new TechnologyTrainerViewModel
                          {
                              id = ttr.id,
                              trainer_id = tra.id,
                          }).FirstOrDefault();
                if (result == null)
                    result = new TechnologyTrainerViewModel();
                return result;
            }
        }
    }
}
