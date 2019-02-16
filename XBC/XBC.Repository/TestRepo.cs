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
                          where (t.is_delete == false && t.name.Contains(search))
                          select new TestViewModel
                          {
                              id = t.id,
                              name = t.name,
                              isBootcampTest = t.is_bootcamp_test,
                              notes = t.notes,
                              createdBy = t.created_by
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

                        ts.created_by = 1;
                        ts.created_on = DateTime.Now;
                        ts.is_delete = false;
                        db.t_test.Add(ts); 
                        db.SaveChanges();

                        // Audit Log Insert
                        var json = new JavaScriptSerializer().Serialize(ts);
                        t_audit_log log = new t_audit_log();
                        log.type = "INSERT";
                        log.json_insert = json;
                        log.created_by = 1;
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
                            var jsonBefore = new JavaScriptSerializer().Serialize(ts); // Mengambil Json Before

                            ts.name = entity.name;
                            ts.is_bootcamp_test = entity.isBootcampTest;
                            ts.notes = entity.notes;

                            ts.modified_by = 1;
                            ts.modified_on = DateTime.Now;
                            db.SaveChanges();

                            // Audit Log Modify
                            var jsonAfter = new JavaScriptSerializer().Serialize(ts);
                            t_audit_log log = new t_audit_log();
                            log.type = "MODIFY";
                            log.json_before = jsonBefore;
                            log.json_after = jsonAfter;
                            log.created_by = 1;
                            log.created_on = DateTime.Now;
                            db.t_audit_log.Add(log);

                            db.SaveChanges();
                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.ErrorMessage = "Category Not Found";
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
                        ts.deleted_by = 1;
                        ts.deleted_on = DateTime.Now;
                        ts.is_delete = true;
                        db.SaveChanges();

                        // Audit Log Delete
                        var json = new JavaScriptSerializer().Serialize(ts);
                        t_audit_log log = new t_audit_log();
                        log.type = "DELETE";
                        log.json_delete = json;
                        log.created_by = 1;
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
    }
}
