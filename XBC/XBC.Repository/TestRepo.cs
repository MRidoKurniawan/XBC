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
    public class TestRepo
    {
        //GetAll & Serch
        public static List<TestViewModel> All(string search)
        {
            List<TestViewModel> result = new List<TestViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from t in db.t_test
                          join u in db.t_user on t.created_by equals u.id
                          where (t.is_delete == false && t.name.Contains(search))
                          select new TestViewModel
                          {
                              id = t.id,
                              name = t.name,
                              isBootcampTest = t.is_bootcamp_test,
                              notes = t.notes,
                              createdBy = t.created_by,
                              UserName = u.username
                          }).ToList();
            }

            return result == null ? new List<TestViewModel>() : result;
        }

        //Get By Id
        public static TestViewModel ById(long id)
        {
            TestViewModel result = new TestViewModel();
            using (var db = new XBC_Context())
            {
                result = (from t in db.t_test
                          where t.id == id && t.is_delete == false
                          select new TestViewModel
                          {
                              id = t.id,
                              name = t.name,
                              isBootcampTest = t.is_bootcamp_test,
                              notes = t.notes,
                              createdBy = t.created_by
                          }).FirstOrDefault();
            }

            return result == null ? result = new TestViewModel() : result;
        }

        // Update (Edit & Create)
        public static ResponseResult Update(TestViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    if (entity.id == 0) // Create
                    {
                        t_test ts = new t_test();
                        ts.name = entity.name;
                        ts.is_bootcamp_test = entity.isBootcampTest;
                        ts.notes = entity.notes;

                        ts.created_by = entity.UserId;
                        ts.created_on = DateTime.Now;
                        ts.is_delete = false;
                        db.t_test.Add(ts); 
                        db.SaveChanges();

                        // Audit Log Insert
                        var json = new JavaScriptSerializer().Serialize(ts);
                        t_audit_log log = new t_audit_log();
                        log.type = "INSERT";
                        log.json_insert = json;
                        log.created_by = entity.UserId;
                        log.created_on = DateTime.Now;
                        db.t_audit_log.Add(log);
                        db.SaveChanges();

                        entity.id = ts.id;
                        result.Entity = entity;
                    }
                    else // Edit
                    {
                        t_test ts = db.t_test.Where(o => o.id == entity.id).FirstOrDefault();
                        if (ts != null)
                        {
                            var Serial = new JavaScriptSerializer();
                            object dataBefore = new //Mengambil Data Before for Log
                            {
                                ts.name,
                                ts.is_bootcamp_test,
                                ts.notes                              
                            };

                            ts.name = entity.name;
                            ts.is_bootcamp_test = entity.isBootcampTest;
                            ts.notes = entity.notes;

                            ts.modified_by = entity.UserId;
                            ts.modified_on = DateTime.Now;
                            db.SaveChanges();

                            // Audit Log Modify
                            object dataAfter = new
                            {
                                ts.name,
                                ts.is_bootcamp_test,
                                ts.notes
                            };

                            t_audit_log log = new t_audit_log();
                            log.type = "MODIFY";
                            log.json_before = Serial.Serialize(dataBefore);
                            log.json_after = Serial.Serialize(dataAfter);
                            log.created_by = entity.UserId;
                            log.created_on = DateTime.Now;
                            db.t_audit_log.Add(log);
                            db.SaveChanges();

                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.ErrorMessage = "Test Not Found";
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

        //Delete
        public static ResponseResult Delete(TestViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_test ts = db.t_test.Where(o => o.id == entity.id).FirstOrDefault();

                    if (ts != null)
                    {
                        var Serial = new JavaScriptSerializer();
                        object dataBefore = new //Mengambil Data Before for Log
                        {
                            ts.name,
                            ts.is_bootcamp_test,
                            ts.notes,
                            ts.is_delete
                        };

                        ts.deleted_by = entity.UserId;
                        ts.deleted_on = DateTime.Now;
                        ts.is_delete = true;
                        db.SaveChanges();

                        // Audit Log Modify
                        object dataAfter = new
                        {
                            ts.name,
                            ts.is_bootcamp_test,
                            ts.notes,
                            ts.is_delete
                        };

                        t_audit_log log = new t_audit_log();
                        log.type = "MODIFY";
                        log.json_before = Serial.Serialize(dataBefore);
                        log.json_after = Serial.Serialize(dataAfter);
                        log.created_by = entity.UserId;
                        log.created_on = DateTime.Now;
                        db.t_audit_log.Add(log);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Test Not Found";
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


        // Validalsi Nama Test Tidak Boleh Sama
        public static TestViewModel CheckName(string name)
        {

            TestViewModel result = new TestViewModel();
            using (var db = new XBC_Context())
            {
                result = (from t in db.t_test
                          where t.name == name && t.is_delete == false
                          select new TestViewModel
                          {
                              id = t.id,
                              name = t.name
                          }).FirstOrDefault();
            }
            return result == null ? new TestViewModel() : result;
        }
    }
}
