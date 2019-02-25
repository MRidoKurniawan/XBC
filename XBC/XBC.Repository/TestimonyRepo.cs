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
    public class TestimonyRepo
    {
        //GetAll

        public static List<TestimonyViewModel> All()
        {
            List<TestimonyViewModel> result = new List<TestimonyViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from tes in db.t_testimony where tes.is_delete == false
                          select new TestimonyViewModel
                          {
                              id = tes.id,
                              title = tes.title,
                              content = tes.content,
                              is_delete = tes.is_delete
                          }).ToList();
                if (result == null)
                    result = new List<TestimonyViewModel>();
            }
            return result;
        }
        //GetById
        public static TestimonyViewModel ById(long id)
        {
            TestimonyViewModel result = new TestimonyViewModel();
            using (var db = new XBC_Context())
            {
                result = (from tes in db.t_testimony
                          where tes.id == id && tes.is_delete == false
                          select new TestimonyViewModel
                          {
                              id = tes.id,
                              title = tes.title,
                              content = tes.content,
                              is_delete = tes.is_delete
                          }).FirstOrDefault();
                if (result == null)
                    result = new TestimonyViewModel();
                return result;
            }
        }
        public static ResponseResult Update(TestimonyViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    //Create
                    if (entity.id == 0)
                    {
                        t_testimony tes = new t_testimony();
                        tes.title = entity.title;
                        tes.content = entity.content;
                        tes.is_delete = entity.is_delete;
                        tes.created_by = 1;
                        tes.created_on = DateTime.Now;
                        db.t_testimony.Add(tes);
                        db.SaveChanges();

                        object data = new
                        {
                            tes.id,
                            tes.title,
                            tes.content
                        };
                        var json = new JavaScriptSerializer().Serialize(data);
                        t_audit_log log = new t_audit_log();
                        log.type = "Insert";
                        log.json_insert = json;
                        log.created_by = 1;
                        log.created_on = DateTime.Now;
                        db.t_audit_log.Add(log);
                        db.SaveChanges();

                        entity.id = tes.id;
                        result.Entity = entity;
                    }
                    //edit
                    else
                    {
                        t_testimony tes = db.t_testimony
                            .Where(t => t.id == entity.id)
                            .FirstOrDefault();

                        if (tes != null)
                        {
                            object data = new
                            {
                                tes.id,
                                tes.title,
                                tes.content
                            };
                            var json = new JavaScriptSerializer().Serialize(data);
                            t_audit_log log = new t_audit_log();
                            log.type = "Modify";
                            log.json_before = json;
                            log.created_by = 1;
                            log.created_on = DateTime.Now;

                            tes.title = entity.title;
                            tes.content = entity.content;
                            tes.modified_by = 1;
                            tes.modified_on = DateTime.Now;

                            object data2 = new
                            {
                                tes.id,
                                tes.title,
                                tes.content
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
                            result.ErrorMessage = "Testimony Not Found!";
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
        public static ResponseResult Delete(TestimonyViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_testimony tes = db.t_testimony
                        .Where(t => t.id == entity.id)
                        .FirstOrDefault();

                    if (tes != null)
                    {
                        object data = new
                        {
                            tes.id,
                            tes.title,
                            tes.content,
                            tes.is_delete
                        };
                        var json = new JavaScriptSerializer().Serialize(data);
                        t_audit_log log = new t_audit_log();
                        log.type = "Modify";
                        log.json_before = json;
                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        tes.is_delete = true;
                        tes.deleted_by = 1;
                        tes.deleted_on = DateTime.Now;

                        object data2 = new
                        {
                            tes.id,
                            tes.title,
                            tes.content,
                            tes.is_delete
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
                        result.ErrorMessage = "Testimony Not Found";
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

        public static List<TestimonyViewModel> GetBySearch(String search)
        {
            List<TestimonyViewModel> result = new List<TestimonyViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from tes in db.t_testimony
                          where tes.title.Contains(search) && tes.is_delete == false
                          select new TestimonyViewModel
                          {
                              id = tes.id,
                              title = tes.title,
                              content = tes.content
                          }).ToList();
            }
            return result == null ? new List<TestimonyViewModel>() : result;
        }
    }
}
