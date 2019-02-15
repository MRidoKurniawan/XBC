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
        //GetAll
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
                          where t.id == id
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

        //// GetBySearch
        //public static List<TestViewModel> GetBySearch(string cari)
        //{
        //    List<TestViewModel> result = new List<TestViewModel>();
        //    using (var db = new XBC_Context())
        //    {
        //        result = (from t in db.t_test
        //                  where (t.is_delete == false && t.name.Contains(cari))
        //                  select new TestViewModel
        //                  {
        //                      id = t.id,
        //                      name = t.name,
        //                      isBootcampTest = t.is_bootcamp_test,
        //                      notes = t.notes,
        //                      createdBy = t.created_by
        //                  }).ToList();
        //    }
        //    return result != null ? result : new List<TestViewModel>();
        //}

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

                        var json = new JavaScriptSerializer().Serialize(ts);
                        t_audit_log log = new t_audit_log();
                        log.type = "Insert";
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
