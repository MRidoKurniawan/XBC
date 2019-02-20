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

                        var json = new JavaScriptSerializer().Serialize(tec);

                        t_audit_log log = new t_audit_log();
                        log.type = "Insert";
                        log.json_insert = json;

                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        db.t_audit_log.Add(log);

                        db.SaveChanges();
                        entity.id = tec.id;
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
                            var json = new JavaScriptSerializer().Serialize(tec);
                            t_audit_log log = new t_audit_log();
                            log.type = "Modify";
                            log.json_before = json;

                            log.created_by = 1;
                            log.created_on = DateTime.Now;

                            tec.name = entity.name;
                            tec.notes = entity.notes;
                            tec.modified_by = 1;
                            tec.modified_on = DateTime.Now;

                            var json2 = new JavaScriptSerializer().Serialize(tec);
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
                        var json = new JavaScriptSerializer().Serialize(tec);
                        t_audit_log log = new t_audit_log();
                        log.type = "Modify";
                        log.json_before = json;
                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        tec.is_delete = true;
                        tec.deleted_by = 1;
                        tec.deleted_on = DateTime.Now;

                        var json2 = new JavaScriptSerializer().Serialize(tec);
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
    }
}
