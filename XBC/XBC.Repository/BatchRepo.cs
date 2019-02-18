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
    public class BatchRepo
    {
        //GetAll & Serch
        public static List<BatchViewModel> All(string search)
        {
            List<BatchViewModel> result = new List<BatchViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from b in db.t_batch
                          join tc in db.t_technology on b.technology_id equals tc.id
                          join tr in db.t_trainer on b.trainer_id equals tr.id
                          where (b.is_delete == false && (b.name.Contains(search) || tc.name.Contains(search)))
                          select new BatchViewModel
                          {
                              id = b.id,
                              technologyName = tc.name,
                              name = b.name,
                              trainerName = tr.name                             
                          }).ToList();
            }

            return result == null ? new List<BatchViewModel>() : result;
        }

        //Get By Id
        public static BatchViewModel ById(long id)
        {
            BatchViewModel result = new BatchViewModel();
            using (var db = new XBC_Context())
            {
                result = (from b in db.t_batch
                          join tc in db.t_technology on b.technology_id equals tc.id
                          join tr in db.t_trainer on b.trainer_id equals tr.id
                          where b.id == id && b.is_delete == false
                          select new BatchViewModel
                          {
                              id = b.id,
                              technologyName = tc.name,
                              name = b.name,
                              trainerName = tr.name
                          }).FirstOrDefault();
            }

            return result == null ? result = new BatchViewModel() : result;
        }

        // Update (Edit & Create)
        public static ResponseResult Update(BatchViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    if (entity.id == 0) // Create
                    {
                        t_batch bt = new t_batch();
                        bt.name = entity.name;
                        bt.technology_id = entity.technologyId;
                        bt.period_to = entity.periodTo;
                        bt.bootcamp_type_id = entity.bootcampTypeId;
                        bt.room_id = entity.roomId;
                        bt.trainer_id = entity.trainerId;
                        bt.period_from = entity.periodFrom;
                        bt.notes = entity.notes;

                        bt.created_by = 1;
                        bt.created_on = DateTime.Now;
                        bt.is_delete = false;
                        db.t_batch.Add(bt);
                        db.SaveChanges();

                        // Audit Log Insert
                        var json = new JavaScriptSerializer().Serialize(bt);
                        t_audit_log log = new t_audit_log();
                        log.type = "INSERT";
                        log.json_insert = json;
                        log.created_by = 1;
                        log.created_on = DateTime.Now;
                        db.t_audit_log.Add(log);
                        db.SaveChanges();

                        entity.id = bt.id;
                        result.Entity = entity;
                    }
                    else // Edit
                    {
                        t_batch bt = db.t_batch.Where(o => o.id == entity.id).FirstOrDefault();
                        if (bt != null)
                        {
                            var jsonBefore = new JavaScriptSerializer().Serialize(bt); // Mengambil Json Before

                            bt.name = entity.name;
                            bt.technology_id = entity.technologyId;
                            bt.period_to = entity.periodTo;
                            bt.bootcamp_type_id = entity.bootcampTypeId;
                            bt.room_id = entity.roomId;
                            bt.trainer_id = entity.trainerId;
                            bt.period_from = entity.periodFrom;
                            bt.notes = entity.notes;

                            bt.modified_by = 1;
                            bt.modified_on = DateTime.Now;
                            db.SaveChanges();

                            // Audit Log Modify
                            var jsonAfter = new JavaScriptSerializer().Serialize(bt);
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
                            result.ErrorMessage = "Test Not Found";
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
}
