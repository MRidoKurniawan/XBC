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
    public class Document_Test_DetailRepo
    {

        public static QuestionViewModel ById(long id)
        {
            QuestionViewModel result = new QuestionViewModel();
            using (var db = new XBC_Context())
            {
                result = (from q in db.t_question
                          select new QuestionViewModel
                          {
                              id = q.id,
                              //question_id=q.id,
                              //document_test_id=dt.id,
                              question = q.question
                          }).FirstOrDefault();
                if (result == null)
                    result = new QuestionViewModel();
            }
            return result;
        }

        public static Document_Test_DetailViewModel ByQuestion(long id)
        {
            Document_Test_DetailViewModel result = new Document_Test_DetailViewModel();
            using (var db = new XBC_Context())
            {
                result = (from q in db.t_question
                          where q.id == id 
                          select new Document_Test_DetailViewModel
                          {
                              question_id = q.id,
                              question = q.question
                          }).FirstOrDefault();

                return result != null ? result : new Document_Test_DetailViewModel();

            }

        }

        public static List<Document_Test_DetailViewModel> ByID(long id)
        {
            List<Document_Test_DetailViewModel> result = new List<Document_Test_DetailViewModel>();
            using(var db = new XBC_Context())
            {
                result = (from dtd in db.t_document_test_detail
                          join q in db.t_question
                          on dtd.question_id equals q.id
                          join dt in db.t_document_test
                          on dtd.document_test_id equals dt.id
                          where dtd.document_test_id == id
                          select new Document_Test_DetailViewModel
                          {
                              id = dtd.id,
                              question = q.question,
                              question_id = q.id,
                              document_test_id = dt.id
                          }).ToList();
                if (result==null)
                {
                    result = new List<Document_Test_DetailViewModel>();
                }
            }
            return result;
        }

        public static ResponseResult Update(Document_Test_DetailViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    //create
                    if (entity.id == 0)
                    {
                        t_document_test_detail dtd = new t_document_test_detail();
                        dtd.question_id = entity.question_id;
                        dtd.document_test_id = entity.document_test_id;

                        dtd.created_by = entity.UserId;
                        dtd.created_on = DateTime.Now;

                        db.t_document_test_detail.Add(dtd);

                        db.SaveChanges();

                        var json = new JavaScriptSerializer().Serialize(dtd);
                        t_audit_log log = new t_audit_log();
                        log.type = "Insert";
                        log.json_insert = json;

                        log.created_by = entity.UserId;
                        log.created_on = DateTime.Now;

                        db.t_audit_log.Add(log);

                        db.SaveChanges();

                        entity.id = dtd.id;
                        result.Entity = entity;
                    }
                    /*else*/ //edit
                    //{
                    //    t_category cat = db.t_category.Where(o => o.id == entity.id).FirstOrDefault();

                    //    if (cat != null)
                    //    {
                    //        var json = new JavaScriptSerializer().Serialize(cat);
                    //        t_audit_log log = new t_audit_log();
                    //        log.type = "Modify";
                    //        log.json_before = json;

                    //        log.created_by = 1;
                    //        log.created_on = DateTime.Now;



                    //        cat.name = entity.name;
                    //        cat.description = entity.description;


                    //        cat.modified_by = 1;
                    //        cat.modified_on = DateTime.Now;

                    //        var json2 = new JavaScriptSerializer().Serialize(cat);
                    //        log.json_after = json2;
                    //        db.t_audit_log.Add(log);
                    //        db.SaveChanges();

                    //        result.Entity = entity;
                    //    }

                    //    else
                    //    {
                    //        result.Success = false;
                    //        result.ErrorMessage = "Category Not Found";
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static Document_Test_DetailViewModel By(long id)
        {
            Document_Test_DetailViewModel result = new Document_Test_DetailViewModel();
            using (var db = new XBC_Context())
            {
                result = (from c in db.t_document_test_detail
                          select new Document_Test_DetailViewModel
                          {
                              document_test_id = id
                          }).FirstOrDefault();
                if (result == null)
                    result = new Document_Test_DetailViewModel();
            }
            return result;
        }

        public static Document_Test_DetailViewModel Byiddtd(long id)
        {
            Document_Test_DetailViewModel result = new Document_Test_DetailViewModel();
            using (var db = new XBC_Context())
            {
                result = (from c in db.t_document_test_detail
                          where c.id== id
                          select new Document_Test_DetailViewModel
                          {
                              id = c.id,
                              document_test_id = c.document_test_id,
                              question_id = c.question_id
                          }).FirstOrDefault();
                if (result == null)
                    result = new Document_Test_DetailViewModel();
            }
            return result;
        }

        public static ResponseResult DeleteQuestion(Document_Test_DetailViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_document_test_detail cat = db.t_document_test_detail.Where(o => o.id == entity.id).FirstOrDefault();
                    if (cat != null)
                    {
                        db.t_document_test_detail.Remove(cat);

                        db.SaveChanges();

                        var json = new JavaScriptSerializer().Serialize(cat);
                        t_audit_log log = new t_audit_log();
                        log.type = "Delete";
                        log.json_delete = json;

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

    }
}
