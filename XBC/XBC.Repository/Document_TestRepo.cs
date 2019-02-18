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
    public class Document_TestRepo
    {
        public static List<Document_TestViewModel> GetBySearch( string cari)
        {
            List<Document_TestViewModel> result = new List<Document_TestViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from dt in db.t_document_test
                          join t in db.t_test
                          on dt.test_id equals t.id
                          join tt in db.t_test_type
                          on dt.test_type_id equals tt.id
                          where dt.is_delete == false
                          && (tt.name.Contains(cari) || dt.version.ToString().Contains(cari))
                          select new Document_TestViewModel
                          {
                              id = dt.id,
                              version=dt.version,
                              TestType= tt.name,
                              Test = t.name
                          
                          }).ToList();
            }

            return result == null ? new List<Document_TestViewModel>() : result;
        }

        public static ResponseResult Update(Document_TestViewModel entity/*, long id*/)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                //entity.TestType = GetTestType(id);
                entity.version = GetNewVersion(entity.test_type_id);
                entity.token = RandomString();
                using (var db = new XBC_Context())
                {
                    //create
                    if (entity.id == 0)
                    {
                        t_document_test doct = new t_document_test();
                        doct.version = entity.version;
                        doct.token = entity.token;
                        doct.test_type_id = entity.test_type_id;
                        doct.test_id = entity.test_id;
                        doct.created_by = 1;
                        doct.created_on = DateTime.Now;

                        db.t_document_test.Add(doct);

                        db.SaveChanges();

                        var json = new JavaScriptSerializer().Serialize(doct);
                        t_audit_log log = new t_audit_log();
                        log.type = "Insert";
                        log.json_insert = json;

                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        db.t_audit_log.Add(log);

                        db.SaveChanges();

                        entity.id = doct.id;
                        result.Entity = entity;
                    }
                    else //edit
                    {
                        t_document_test doct = db.t_document_test.Where(o => o.id == entity.id).FirstOrDefault();

                        if (doct != null)
                        {
                            var json = new JavaScriptSerializer().Serialize(doct);
                            t_audit_log log = new t_audit_log();
                            log.type = "Modify";
                            log.json_before = json;

                            log.created_by = 1;
                            log.created_on = DateTime.Now;



                            doct.test_id = entity.test_id;
                            doct.test_type_id = entity.test_type_id;


                            doct.modified_by = 1;
                            doct.modified_on = DateTime.Now;

                            var json2 = new JavaScriptSerializer().Serialize(doct);
                            log.json_after = json2;
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

        private static int GetNewVersion(long id)
        {
            int newref1;
            long newRef = id;
            using (var db = new XBC_Context())
            {
                var result = (from dt in db.t_document_test
                                  //join tt in db.t_test_type
                                  //on dt.test_type_id equals tt.id
                                  //where tt.name.Contains(newRef)
                              where dt.test_type_id == id
                              select new { Version = dt.version })
                              .OrderByDescending(o => o.Version)
                              .FirstOrDefault();
                if (result != null)
                {
                    newref1 = int.Parse(result.Version.ToString())+1;
                }
                else
                {
                    newref1 = 1;
                }
            }
            return newref1;
        }

        private static string GetTestType(long id)
        {
            string newref1;
            long newRef = id;
            using (var db = new XBC_Context())
            {
                var result = (from tt in db.t_test_type
                              where tt.id == newRef
                              select new { Name = tt.name })
                              .OrderByDescending(o => o.Name)
                              .FirstOrDefault();
                if (result != null)
                {

                    newref1 = result.Name;
                }
                else
                {
                    newref1 = "";
                }
            }
            return newref1;
        }

        private static Random random = new Random();
        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

         public static Document_TestViewModel ById(long id)
        {
            Document_TestViewModel result = new Document_TestViewModel();
            using (var db = new XBC_Context())
            {
                result = (from dt in db.t_document_test
                          join t in db.t_test
                          on dt.test_id equals t.id
                          join tt in db.t_test_type
                          on dt.test_type_id equals tt.id
                          where dt.id == id
                          select new Document_TestViewModel
                          {
                              id = dt.id,
                              token = dt.token,
                              version = dt.version,
                              test_id = t.id,
                              test_type_id = tt.id
                          }).FirstOrDefault();
                if (result == null)
                    result = new Document_TestViewModel();
            }
            return result;
        }

        public static ResponseResult Delete(Document_TestViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_document_test doct = db.t_document_test.Where(o => o.id == entity.id).FirstOrDefault();
                    if (doct != null)
                    {
                        var Serial = new JavaScriptSerializer();
                        object data = new
                        {
                            doct.is_delete,
                        };
                        var json = Serial.Serialize(data);
                        doct.is_delete = true;


                        doct.deleted_by = 1;
                        doct.deleted_on = DateTime.Now;
                        db.SaveChanges();
                        result.Entity = entity;
                        db.SaveChanges();


                        object data2 = new
                        {
                            doct.is_delete,
                        };

                        t_audit_log log = new t_audit_log();
                        log.type = "Modify";
                        log.json_before = json;
                        json = Serial.Serialize(data2);
                        log.json_after = json;


                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        db.t_audit_log.Add(log);

                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Category Not Found!";
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
