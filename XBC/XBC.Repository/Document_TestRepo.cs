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

        public static ResponseResult Update(Document_TestViewModel entity, string nama)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                entity.version = GetNewVersion(nama);
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

        private static int GetNewVersion(string name)
        {
            int newref1;
            string newRef = name;
            using (var db = new XBC_Context())
            {
                var result = (from dt in db.t_document_test
                              join tt in db.t_test_type
                              on dt.test_type_id equals tt.id
                              where tt.name.Contains(newRef)
                              select new { Version = dt.version })
                              .OrderByDescending(o => o.Version)
                              .FirstOrDefault();
                if (result != null)
                {
                    string[] lastRef = result.Version.ToString().Split('-');
                    newRef += (int.Parse(lastRef[0]) + 1).ToString("D4");
                    newref1 = int.Parse(newRef);                }
                else
                {
                    newref1 = 1;
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
    }
}
