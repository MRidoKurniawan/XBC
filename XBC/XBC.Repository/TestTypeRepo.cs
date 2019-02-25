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
    public class TestTypeRepo
    {
        //getall
        public static List<TestTypeViewModel> All()
        {
            List<TestTypeViewModel> result = new List<TestTypeViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from ttp in db.t_test_type 
                          where ttp.is_delete == false
                          select new TestTypeViewModel
                          {
                              id = ttp.id,
                              name = ttp.name,
                              notes = ttp.notes,
                              typeofanswer = ttp.type_of_answer,
                              createdBy = ttp.created_by
                          }).ToList();
                if (result == null)
                {
                    result = new List<TestTypeViewModel>();
                }
            }
            return result;
        }

        public static TestTypeViewModel ById( long id)
        {
            TestTypeViewModel result = new TestTypeViewModel();
            using (var db = new XBC_Context())
            {
                result = (from ttp in db.t_test_type
                          where ttp.id == id && ttp.is_delete == false
                          select new TestTypeViewModel
                          {
                              id = ttp.id,
                              name = ttp.name,
                              notes = ttp.notes,
                              typeofanswer = ttp.type_of_answer,
                              createdBy = ttp.created_by
                          }).FirstOrDefault();
                if (result == null)
                    result = new TestTypeViewModel();
            }
            return result;
        }

        public static List<TestTypeViewModel> GetBySearch(string search)
        {
            List<TestTypeViewModel> result = new List<TestTypeViewModel>();
            using (var db = new XBC_Context())
            {
                result =(from ttp in db.t_test_type
                         where ttp.name.Contains(search) && ttp.is_delete == false
                          select new TestTypeViewModel
                          {
                              id = ttp.id,
                              name = ttp.name,
                              notes = ttp.notes,
                              typeofanswer = ttp.type_of_answer,
                              createdBy = ttp.created_by
                          }).ToList();

                if (result == null)
                    result = new List<TestTypeViewModel>();
            }
            return result;
        }
        public static ResponseResult Update(TestTypeViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new XBC_Context())
            {
                if (entity.id == 0)
                {
                    t_test_type tstp = new t_test_type();
                    tstp.name = entity.name;
                    tstp.notes = entity.notes;

                    tstp.created_by = 01;
                    tstp.created_on = DateTime.Now;
                    tstp.is_delete = false;

                    db.t_test_type.Add(tstp);
                    db.SaveChanges();

                    var json = new JavaScriptSerializer().Serialize(tstp);
                    t_audit_log log = new t_audit_log();
                    log.type = "Insert";
                    log.json_insert = json;

                    log.created_by = 1;
                    log.created_on = DateTime.Now;

                    db.t_audit_log.Add(log);

                    db.SaveChanges();

                    entity.id = tstp.id;
                    result.Entity = entity;
                }
                else //edit
                {
                    t_test_type tstp = db.t_test_type.Where(ttp => ttp.id == entity.id).FirstOrDefault();
                    if (tstp != null)
                    {
                        var json = new JavaScriptSerializer().Serialize(tstp);
                        t_audit_log log = new t_audit_log();
                        log.type = "Modify";
                        log.json_before = json;

                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        tstp.name = entity.name;
                        tstp.notes = entity.notes;

                        tstp.modified_by = 01;
                        tstp.modified_on = DateTime.Now;

                        var json2 = new JavaScriptSerializer().Serialize(tstp);
                        log.json_after = json2;
                        db.t_audit_log.Add(log);
                        db.SaveChanges();


                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Test Type not found";
                    }
                }
            }
            return result;
        }
        public static ResponseResult Delete(TestTypeViewModel entity)
        {
            ResponseResult result = new ResponseResult();
           
                using (var db = new XBC_Context())
                {
                    t_test_type ttp = db.t_test_type.Where(o => o.id == entity.id).FirstOrDefault();

                    if (ttp != null)
                    {
                        var json = new JavaScriptSerializer().Serialize(ttp);
                        t_audit_log log = new t_audit_log();
                        log.type = "Modify";
                        log.json_before = json;

                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        ttp.is_delete = true;
                        ttp.deleted_by = 1;
                        ttp.deleted_on = DateTime.Now;
                        var json2 = new JavaScriptSerializer().Serialize(ttp);
                        log.json_after = json2;
                        db.t_audit_log.Add(log);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Test Type not found!";
                    }
                }
            return result;
            }
        }
    }

