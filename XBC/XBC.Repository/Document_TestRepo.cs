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
        public static List<Document_TestViewModel> GetBySearch(string cari)
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
                              version = dt.version,
                              test_type_id = tt.id,
                              TestType = tt.name,
                              Test = t.name

                          }).ToList();
            }

            return result == null ? new List<Document_TestViewModel>() : result;
        }

        public static string Generate()
        {
            string noken = RandomString();
            string result;
            Document_TestViewModel results = new Document_TestViewModel();
            using (var db = new XBC_Context())
            {
                results = (from dt in db.t_document_test
                           select new Document_TestViewModel
                           {
                               token = noken
                           }).FirstOrDefault();
                if (results == null)
                    result = "";
            }
            result = results.token;
            return result;
        }

        public static ResponseResult CopyDocument(Document_TestViewModel entity)
        {
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    //entity.TestType = GetTestType(id);
                    //entity.version = GetNewVersion(entity.test_type_id);
                    //entity.token = RandomString();
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
                            doct.created_by = entity.UserId;
                            doct.created_on = DateTime.Now;

                            db.t_document_test.Add(doct);

                            db.SaveChanges();

                            var json = new JavaScriptSerializer().Serialize(doct);
                            t_audit_log log = new t_audit_log();
                            log.type = "Insert";
                            log.json_insert = json;

                            log.created_by = entity.UserId;
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
                                t_document_test doct1 = new t_document_test();
                                doct1.test_id = entity.test_id;
                                doct1.test_type_id = entity.test_type_id;
                                doct1.version = entity.version;
                                doct1.token = entity.token;


                                doct1.created_by = entity.UserId;
                                doct1.created_on = DateTime.Now;

                                db.t_document_test.Add(doct1);

                                db.SaveChanges();

                                var Serial = new JavaScriptSerializer();
                                object data2 = new
                                {
                                    doct1.id,
                                    doct1.created_by,
                                    doct1.created_on,
                                    doct1.deleted_by,
                                    doct1.deleted_on,
                                    doct1.is_delete,
                                    doct1.modified_by,
                                    doct1.modified_on,
                                    doct1.test_id,
                                    doct1.test_type_id,
                                    doct1.token,
                                    doct.version
                                };
                                t_audit_log log = new t_audit_log();
                                var json = Serial.Serialize(data2);
                                log.type = "Insert";
                                log.json_insert = json;

                                log.created_by = entity.UserId;
                                log.created_on = DateTime.Now;

                                db.t_audit_log.Add(log);

                                db.SaveChanges();


                                //var json = new JavaScriptSerializer().Serialize(doct1);
                                //t_audit_log log = new t_audit_log();
                                //log.type = "Insert";
                                //log.json_insert = json;

                                //log.created_by = entity.UserId;
                                //log.created_on = DateTime.Now;

                                //db.t_audit_log.Add(log);

                                //db.SaveChanges();

                                List<t_document_test_detail> results = (from dtd in db.t_document_test_detail
                                                                        where dtd.document_test_id == entity.id
                                                                        select dtd).ToList();
                                foreach (t_document_test_detail dtd in results)
                                {
                                    t_document_test_detail od = new t_document_test_detail();
                                    od.question_id = dtd.question_id;
                                    od.document_test_id = doct1.id;

                                    od.created_by = entity.UserId;
                                    od.created_on = DateTime.Now;

                                    db.t_document_test_detail.Add(od);
                                    db.SaveChanges();

                                    Serial = new JavaScriptSerializer();
                                    object data = new
                                    {
                                        od.id,
                                        od.document_test_id,
                                        od.question_id
                                    };
                                    var json2 = Serial.Serialize(data);
                                    log.type = "Insert";
                                    log.json_insert = json2;

                                    log.created_by = entity.UserId;
                                    log.created_on = DateTime.Now;

                                    db.t_audit_log.Add(log);

                                    db.SaveChanges();
                                }

                                entity.id = doct1.id;
                                result.Entity = entity;
                            }

                            else
                            {
                                result.Success = false;
                                result.ErrorMessage = "Document Test Not Found";
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
        }
        public static ResponseResult Update(Document_TestViewModel entity/*, long id*/)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                //entity.TestType = GetTestType(id);
                //entity.version = GetNewVersion(entity.test_type_id);
                if (entity.token == null)
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
                        doct.created_by = entity.UserId;
                        doct.created_on = DateTime.Now;

                        db.t_document_test.Add(doct);

                        db.SaveChanges();

                        var json = new JavaScriptSerializer().Serialize(doct);
                        t_audit_log log = new t_audit_log();
                        log.type = "Insert";
                        log.json_insert = json;

                        log.created_by = entity.UserId;
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
                            var Serial = new JavaScriptSerializer();
                            object data = new
                            {
                                doct.created_by,
                                doct.created_on,
                                doct.is_delete,
                                doct.id,
                                doct.modified_by,
                                doct.modified_on,
                                doct.test_id,
                                doct.test_type_id,
                                doct.token,
                                doct.version

                            };
                            var json = Serial.Serialize(data);

                            if (doct.test_type_id != entity.test_type_id)
                                doct.version = GetNewVersion(entity.test_type_id);

                            doct.test_id = entity.test_id;
                            doct.test_type_id = entity.test_type_id;
                            doct.token = entity.token;


                            doct.modified_by = entity.UserId;
                            doct.modified_on = DateTime.Now;

                            object data2 = new
                            {
                                doct.created_by,
                                doct.created_on,
                                doct.is_delete,
                                doct.id,
                                doct.modified_by,
                                doct.modified_on,
                                doct.test_id,
                                doct.test_type_id,
                                doct.token,
                                doct.version
                            };


                            t_audit_log log = new t_audit_log();
                            log.type = "Modify";
                            log.json_before = json;
                            json = Serial.Serialize(data2);
                            log.json_after = json;


                            log.created_by = entity.UserId;
                            log.created_on = DateTime.Now;

                            db.t_audit_log.Add(log);

                            db.SaveChanges();

                            result.Entity = entity;

                            db.SaveChanges();
                            //var json2 = new JavaScriptSerializer().Serialize(doct);
                            //log.json_after = json2;
                            //db.t_audit_log.Add(log);

                            result.Entity = entity;
                            //string b = data2.ToString();
                            // string a = data.ToString();
                            // b = b + a;
                            // object data3 = new
                            // {
                            //     doct.created_by,
                            //     b
                            // };
                            // json = Serial.Serialize(b);
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

        public static int GetNewVersion(long id)
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
                    newref1 = int.Parse(result.Version.ToString()) + 1;
                }
                else
                {
                    newref1 = 1;
                }
            }
            return newref1;
        }

        public static string GetTestType(long id)
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
        public static long ByIdtest(long id)
        {
            long result;
            Document_TestViewModel results = new Document_TestViewModel();
            using (var db = new XBC_Context())
            {
                results = (from dt in db.t_document_test
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
                if (results == null)
                    result = 0;
            }
            return result = results.test_type_id;
        }

        public static Document_TestViewModel ByIdCopyDocument(long id)
        {
            string ntoken = RandomString();
            long id2 = ByIdtest(id);
            decimal nversion = GetNewVersion(id2);
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
                              token = ntoken,
                              version = nversion,
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
                            doct.id,
                            doct.test_id,
                            doct.test_type_id,
                            doct.token,
                            doct.version,
                            doct.is_delete,
                            doct.deleted_by,
                            doct.deleted_on
                        };
                        var json = Serial.Serialize(data);
                        doct.is_delete = true;


                        doct.deleted_by = entity.UserId;
                        doct.deleted_on = DateTime.Now;




                        object data2 = new
                        {
                            doct.id,
                            doct.test_id,
                            doct.test_type_id,
                            doct.token,
                            doct.version,
                            doct.is_delete,
                            doct.deleted_by,
                            doct.deleted_on
                        };

                        t_audit_log log = new t_audit_log();
                        log.type = "Modify";
                        log.json_before = json;
                        json = Serial.Serialize(data2);
                        log.json_after = json;


                        log.created_by = entity.UserId;
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

        public static List<Document_Test_DetailViewModel> ViewTest(long id)
        {
            List<Document_Test_DetailViewModel> result = new List<Document_Test_DetailViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from dtd in db.t_document_test_detail
                          join dt in db.t_document_test
                          on dtd.document_test_id equals dt.id
                          join q in db.t_question
                          on dtd.question_id equals q.id
                          where dtd.document_test_id == id
                          select new Document_Test_DetailViewModel
                          {
                              id = dtd.id,
                              document_test_id = dt.id,
                              question_id = q.id,
                              question = q.question,
                              questionType = q.question_type,
                              imageUrl = q.image_url,
                              imageA = q.image_a,
                              imageB = q.image_b,
                              imageC = q.image_c,
                              imageD = q.image_d,
                              imageE = q.image_e,
                              optionA = q.option_a,
                              optionB = q.option_b,
                              optionC = q.option_c,
                              optionD = q.option_d,
                              optionE = q.option_e
                          }).ToList();
            }
            return result == null ? new List<Document_Test_DetailViewModel>() : result;
        }
    }
}
