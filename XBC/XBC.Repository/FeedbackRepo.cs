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
    public class FeedbackRepo
    {
        public static List<FeedbackViewModel> All()
        {
            List<FeedbackViewModel> result = new List<FeedbackViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from t in db.t_test
                          where t.is_delete == false && t.is_bootcamp_test == false
                          select new FeedbackViewModel
                          {
                              id = t.id,
                              Nama = t.name
                          }).ToList();
                if (result == null)
                    result = new List<FeedbackViewModel>();
            }
            return result;
        }

        public static List<Document_TestViewModel> ListDT(long id)
        {
            List<Document_TestViewModel> result = new List<Document_TestViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from dt in db.t_document_test
                          join t in db.t_test
                          on dt.test_id equals t.id
                          where dt.is_delete == false && t.id == id
                          select new Document_TestViewModel
                          {
                              id = dt.id

                          }).ToList();
            }

            return result == null ? new List<Document_TestViewModel>() : result;
        }

        public static List<Document_Test_DetailViewModel> ListDTD(long id)
        {
            List<Document_Test_DetailViewModel> result = new List<Document_Test_DetailViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from dtd in db.t_document_test_detail
                          join dt in db.t_document_test
                          on dtd.document_test_id equals dt.id
                          join q in db.t_question
                          on dtd.question_id equals q.id
                          join t in db.t_test
                          on dt.test_id equals t.id
                          where dt.id == id
                          select new Document_Test_DetailViewModel
                          {
                              id = dtd.id,
                              document_test_id = dt.id,
                              question_id = q.id,
                              test_id = t.id,
                              question = q.question,


                          }).ToList();
            }

            return result == null ? new List<Document_Test_DetailViewModel>() : result;
        }

        public static ResponseResult Update(FeedbackViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    //create
                    if (entity.id == 0)
                    {
                        
                        t_feedback fed = new t_feedback();
                        t_document_test_detail dtd = new t_document_test_detail();
                        fed.test_id = entity.test_id;
                        fed.document_test_id = entity.document_test_id;
                        var Serial = new JavaScriptSerializer();
                        object data = new
                        {
                            fed.test_id,
                            fed.document_test_id,
                            entity.Feedback
                        };
                        var json = Serial.Serialize(data);
                        //string a = data.ToString();
                        //string b;
                        //foreach (var item in FeedbackRepo.ListDTD(entity.document_test_id))
                        //{
                        //    //dtd.question_id = entity.question_id;
                        //    //answer = entity.answer;
                        //    object data2 = new
                        //    {
                        //        //dtd.question_id,
                        //        //answer
                        //    };
                        //    b = data2.ToString();
                        //    a = a + b;
                        //    json = Serial.Serialize(b);

                        //}
                        fed.json_feedback = json;

                        fed.created_by = 1;
                        fed.created_on = DateTime.Now;

                        db.t_feedback.Add(fed);

                        db.SaveChanges();
                        var json1 = new JavaScriptSerializer().Serialize(fed);
                        t_audit_log log = new t_audit_log();
                        log.type = "Insert";
                        log.json_insert = json1;

                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        db.t_audit_log.Add(log);

                        db.SaveChanges();

                        //t_audit_log log = new t_audit_log();
                        //log.type = "Insert";
                        //log.json_insert = json;

                        //log.created_by = 1;
                        //log.created_on = DateTime.Now;

                        //db.t_audit_log.Add(log);

                        //db.SaveChanges();

                        entity.id = fed.id;
                        result.Entity = entity;
                    }
                    else //edit
                    {
                        t_feedback fed = new t_feedback();
                        t_document_test_detail dtd = new t_document_test_detail();
                        fed.test_id = entity.test_id;
                         fed.document_test_id = entity.id;
                        var Serial = new JavaScriptSerializer();
                        object data = new
                        {
                            fed.test_id,
                            fed.document_test_id
                        };
                        var json = Serial.Serialize("");
                        string a = data.ToString();
                        string b;
                        foreach (var item in FeedbackRepo.ListDTD(entity.id))
                        {
                            //dtd.question_id = entity.question_id;
                            //answer = entity.answer;
                            object data2 = new
                            {
                                //dtd.question_id,
                                //answer
                            };
                            b = data2.ToString();
                            a = a + b;
                            json = Serial.Serialize(b);

                        }
                        fed.json_feedback = json;

                        fed.created_by = 1;
                        fed.created_on = DateTime.Now;

                        db.t_feedback.Add(fed);

                        db.SaveChanges();
                        t_audit_log log = new t_audit_log();
                        log.type = "Insert";
                        log.json_insert = json;

                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        db.t_audit_log.Add(log);

                        db.SaveChanges();

                        entity.id = fed.id;
                        result.Entity = entity;
                        //}
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

        public static FeedbackViewModel ListDTD1(long id)
        {
            FeedbackViewModel result = new FeedbackViewModel();
            using (var db = new XBC_Context())
            {
                result = (from dtd in db.t_document_test_detail
                          join dt in db.t_document_test
                          on dtd.document_test_id equals dt.id
                          join q in db.t_question
                          on dtd.question_id equals q.id
                          join t in db.t_test
                          on dt.test_id equals t.id
                          where dt.id == id
                          select new FeedbackViewModel
                          {
                              //id = dtd.id,
                              document_test_id = dtd.document_test_id,
                              test_id = t.id



                          }).FirstOrDefault();
            }

            return result == null ? new FeedbackViewModel() : result;
        }

        //public static FeedbackViewModel byfeedback(long id)
        //{
        //    List<FeedbackViewModel> result = new List<FeedbackViewModel>();
        //    using (var db = new XBC_Context())
        //    {
        //        result = (from dtd in db.t_document_test_detail
        //                  join dt in db.t_document_test
        //                  on dtd.document_test_id equals dt.id
        //                  join q in db.t_question
        //                  on dtd.question_id equals q.id
        //                  join t in db.t_test
        //                  on dt.test_id equals t.id
        //                  where dt.id == id
        //                  select new FeedbackViewModel
        //                  {
        //                      id = dtd.id,
        //                      document_test_id = dt.id,
        //                      test_id = t.id,


        //                  }).FirstOrDefault();
        //    }

        //    return result == null ? new List<FeedbackViewModel>() : result;
        //}
    }
}
