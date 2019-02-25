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
    public class QuestionRepo
    {
        //GetAll & Serch
        public static List<QuestionViewModel> All(string search)
        {
            List<QuestionViewModel> result = new List<QuestionViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from q in db.t_question
                          where (q.is_deleted == false && q.question.Contains(search))
                          select new QuestionViewModel
                          {
                              id = q.id,
                              questionType = q.question_type,
                              question = q.question
                          }).ToList();
            }

            return result == null ? new List<QuestionViewModel>() : result;
        }

        // Get By Id
        public static QuestionViewModel ById(long id)
        {
            QuestionViewModel result = new QuestionViewModel();
            using (var db = new XBC_Context())
            {
                result = (from q in db.t_question
                          where q.id == id && q.is_deleted == false
                          select new QuestionViewModel
                          {
                              id = q.id,
                              questionType = q.question_type,
                              question = q.question,
                              imageUrl = q.image_url,
                              optionA = q.option_a,
                              optionB = q.option_b,
                              optionC = q.option_c,
                              optionD = q.option_d,
                              optionE = q.option_e,
                              imageA = q.image_a,
                              imageB = q.image_b,
                              imageC = q.image_c,
                              imageD = q.image_d,
                              imageE = q.image_e
                          }).FirstOrDefault();
            }

            return result == null ? result = new QuestionViewModel() : result;
        }

        // Update (Edit & Create)
        public static ResponseResult Update(QuestionViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    if (entity.id == 0) // Create
                    {
                        t_question qs = new t_question();
                        qs.question_type = entity.questionType;
                        qs.question = entity.question;
                        qs.image_url = entity.imageUrl;

                        qs.created_by = 1;
                        qs.created_on = DateTime.Now;
                        qs.is_deleted = false;
                        db.t_question.Add(qs);
                        db.SaveChanges();

                        // Audit Log Insert
                        var json = new JavaScriptSerializer().Serialize(qs);
                        t_audit_log log = new t_audit_log();
                        log.type = "INSERT";
                        log.json_insert = json;
                        log.created_by = 1;
                        log.created_on = DateTime.Now;
                        db.t_audit_log.Add(log);
                        db.SaveChanges();
                        object data = new
                        {
                            qs.id,
                            qs.question,
                            qs.question_type,
                            qs.image_url
                        };
                        entity.id = qs.id;
                        result.Entity = data;
                    }
                    else // Edit
                    {
                        //t_question qs = db.t_question.Where(o => o.id == entity.id).FirstOrDefault();
                        //if (qs != null)
                        //{
                        //    var jsonBefore = new JavaScriptSerializer().Serialize(qs); // Mengambil Json Before

                        //    //qs.name = entity.name;
                        //    //ts.is_bootcamp_test = entity.isBootcampTest;
                        //    //ts.notes = entity.notes;

                        //    qs.modified_by = 1;
                        //    qs.modified_on = DateTime.Now;
                        //    db.SaveChanges();

                        //    // Audit Log Modify
                        //    var jsonAfter = new JavaScriptSerializer().Serialize(qs);
                        //    t_audit_log log = new t_audit_log();
                        //    log.type = "MODIFY";
                        //    log.json_before = jsonBefore;
                        //    log.json_after = jsonAfter;
                        //    log.created_by = 1;
                        //    log.created_on = DateTime.Now;
                        //    db.t_audit_log.Add(log);

                        //    db.SaveChanges();
                        //    result.Entity = entity;
                        //}
                        //else
                        //{
                        //    result.Success = false;
                        //    result.ErrorMessage = "Question Not Found";
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

        // Set Choice
        public static ResponseResult SetC(QuestionViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_question qs = db.t_question.Where(o => o.id == entity.id).FirstOrDefault();
                    if (qs != null)
                    {
                        var jsonBefore = new JavaScriptSerializer().Serialize(qs); // Mengambil Json Before

                        qs.option_a = entity.optionA;
                        qs.option_b = entity.optionB;
                        qs.option_c = entity.optionC;
                        qs.option_d = entity.optionD;
                        qs.option_e = entity.optionE;
                        qs.image_a = entity.imageA;
                        qs.image_b = entity.imageB;
                        qs.image_c = entity.imageC;
                        qs.image_d = entity.imageD;
                        qs.image_e = entity.imageE;

                        qs.modified_by = 1;
                        qs.modified_on = DateTime.Now;
                        db.SaveChanges();

                        // Audit Log Modify
                        var jsonAfter = new JavaScriptSerializer().Serialize(qs);
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
                        result.ErrorMessage = "Question Not Found";
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
        public static ResponseResult Delete(QuestionViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_question qs = db.t_question.Where(o => o.id == entity.id).FirstOrDefault();

                    if (qs != null)
                    {
                        qs.deleted_by = 1;
                        qs.deleted_on = DateTime.Now;
                        qs.is_deleted = true;
                        db.SaveChanges();

                        // Audit Log Delete
                        var json = new JavaScriptSerializer().Serialize(qs);
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
                        result.ErrorMessage = "Question Not Found";
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
