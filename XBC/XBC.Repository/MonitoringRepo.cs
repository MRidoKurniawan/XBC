using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repository
{
    public class MonitoringRepo
    {
        //Get All Id
        public static List<MonitoringViewModel> All()
        {
            List<MonitoringViewModel> result = new List<MonitoringViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from m in db.t_monitoring
                          join b in db.t_biodata
                          on m.biodata_id equals b.id
                              where m.is_delete == false
                              select new MonitoringViewModel
                              {
                                  id = m.id,
                                  biodata_id = b.id,
                                  name = b.name,
                                  idle_date = m.idle_date,
                                  placement_date = m.placement_date,
                                  is_delete = m.is_delete
                              }).ToList();

                if (result == null)
                {
                    result = new List<MonitoringViewModel>();
                }
            }
            return result;
        }

        //Get By Id
        public static MonitoringViewModel ById(long id)
        {
            MonitoringViewModel result = new MonitoringViewModel();
            using (var db = new XBC_Context())
            {
                result = (from m in db.t_monitoring
                          join b in db.t_biodata
                          on m.biodata_id equals b.id
                          where m.id == id && m.is_delete == false
                          select new MonitoringViewModel
                          {
                              id = m.id,
                              biodata_id = b.id,
                              name = b.name,
                              idle_date = m.idle_date,
                              placement_date = m.placement_date,
                              is_delete = m.is_delete
                          }).FirstOrDefault();

                if (result == null)
                {
                    result = new MonitoringViewModel();
                }
            }
            return result;
        }

        public static ResponseResult Update(MonitoringViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    //CREATE
                    if (entity.id == 0)
                    {
                        t_monitoring mon = new t_monitoring();
                        mon.biodata_id = entity.biodata_id;
                        mon.idle_date = entity.idle_date;
                        mon.last_project = entity.last_project;
                        mon.idle_reason = entity.idle_reason;
                        mon.is_delete = entity.is_delete;

                        mon.created_by = 1;
                        mon.created_on = DateTime.Now;

                        db.t_monitoring.Add(mon);
                        db.SaveChanges();

                        object data = new
                        {
                            mon.id,
                            mon.biodata_id,
                            mon.idle_date,
                            mon.last_project,
                            mon.idle_reason
                        };
                        var json = new JavaScriptSerializer().Serialize(data);

                        t_audit_log log = new t_audit_log();
                        log.type = "Insert";
                        log.json_insert = json;

                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        db.t_audit_log.Add(log);
                        db.SaveChanges();

                        entity.id = mon.id;
                        result.Entity = entity;
                    }
                    else //EDIT
                    {
                        t_monitoring mon = db.t_monitoring
                            .Where(o => o.id == entity.id)
                            .FirstOrDefault();

                        if(mon != null)
                        {
                            object data = new
                            {
                                mon.id,
                                mon.biodata_id,
                                mon.idle_date,
                                mon.last_project,
                                mon.idle_reason
                            };
                            var json = new JavaScriptSerializer().Serialize(data);
                            t_audit_log log = new t_audit_log();
                            log.type = "Modify";
                            log.json_before = json;

                            log.created_by = 1;
                            log.created_on = DateTime.Now;

                            mon.biodata_id = entity.biodata_id;
                            mon.idle_date = entity.idle_date;
                            mon.last_project = entity.last_project;
                            mon.idle_reason = entity.idle_reason;

                            mon.modified_by = 1;
                            mon.modified_on = DateTime.Now;

                            object data2 = new
                            {
                                mon.id,
                                mon.biodata_id,
                                mon.idle_date,
                                mon.last_project,
                                mon.idle_reason
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
                            result.ErrorMessage = "Monitoring not found!";
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

        //DELETE
        public static ResponseResult Delete(MonitoringViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_monitoring mon = db.t_monitoring
                        .Where(o => o.id == entity.id)
                        .FirstOrDefault();

                    if (mon != null)
                    {
                        object data = new
                        {
                            mon.id,
                            mon.biodata_id,
                            mon.idle_date,
                            mon.last_project,
                            mon.idle_reason
                        };
                        var json = new JavaScriptSerializer().Serialize(data);
                        t_audit_log log = new t_audit_log();
                        log.type = "Modify";
                        log.json_before = json;
                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        mon.is_delete = true;
                        mon.deleted_by = 1;
                        mon.deleted_on = DateTime.Now;

                        object data2 = new
                        {
                            mon.id,
                            mon.biodata_id,
                            mon.idle_date,
                            mon.last_project,
                            mon.idle_reason
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
                        result.ErrorMessage = "Monitoring not found!";
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

        //Get By Search
        public static List<MonitoringViewModel> GetBySearch(string search)
        {
            List<MonitoringViewModel> result = new List<MonitoringViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from mon in db.t_monitoring
                          join bio in db.t_biodata
                          on mon.biodata_id equals bio.id
                          where mon.t_biodata.name.Contains(search) && mon.is_delete == false
                          select new MonitoringViewModel
                          {
                              id = mon.id,
                              biodata_id = bio.id,
                              name = bio.name,
                              idle_date = mon.idle_date,
                              placement_date = mon.placement_date
                          }).ToList();

                if (result == null)
                {
                    result = new List<MonitoringViewModel>();
                }
            }
            return result;
        }

        //Get By Name Biodata
        public static List<BiodataViewModel> ByNameBiodata()
        {
            List<BiodataViewModel> result = new List<BiodataViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from b in db.t_biodata 
                          join m in  db.t_monitoring on b.id equals m.biodata_id into ps from m in ps.DefaultIfEmpty()
                          where (b.is_deleted == false && m.id == null) || (m.is_delete == true && b.is_deleted == false)
                          select new BiodataViewModel
                          {
                              id = b.id,
                              name = b.name,
                              majors = b.majors,
                              gpa = b.gpa,
                              is_deleted = b.is_deleted
                          }).ToList();

                if (result == null)
                {
                    result = new List<BiodataViewModel>();
                }
            }
            return result;
        }
    }
}
