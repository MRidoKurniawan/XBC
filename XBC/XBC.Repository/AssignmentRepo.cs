using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.ViewModel;
using XBC.DataModel;
using System.Web.Script.Serialization;
using System.Net.Mail;
using System.Net;

namespace XBC.Repository
{
    public class AssignmentRepo
    {
        public static List<AssignmentViewModel> All()
        {

            List<AssignmentViewModel> result = new List<AssignmentViewModel>();
            try
            {
                using (var db = new XBC_Context())
                {
                    result = (from m in db.t_assignment
                              where m.is_delete == false && m.is_hold != true
                              select new AssignmentViewModel

                              {
                                  id = m.id,
                                  biodata_id = m.biodata_id,
                                  title = m.title,
                                  start_date = m.start_date,
                                  end_date = m.end_date,
                                  description = m.description,
                                  realization_date = m.realization_date,
                                  notes = m.notes,
                                  is_done = m.is_done
                              }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return result == null ? new List<AssignmentViewModel>() : result;
        }

        public static List<AssignmentViewModel> Search(DateTime cari)
        {

            List<AssignmentViewModel> result = new List<AssignmentViewModel>();
            try
            {
                using (var db = new XBC_Context())
                {
                    result = (from m in db.t_assignment
                              where m.is_delete == false && m.start_date == cari && m.is_hold != true
                              select new AssignmentViewModel

                              {
                                  id = m.id,
                                  biodata_id = m.biodata_id,
                                  title = m.title,
                                  start_date = m.start_date,
                                  end_date = m.end_date,
                                  description = m.description,
                                  realization_date = m.realization_date,
                                  notes = m.notes
                              }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return result == null ? new List<AssignmentViewModel>() : result;
        }

        public static AssignmentViewModel ById(long id)
        {

            AssignmentViewModel result = new AssignmentViewModel();
            try
            {
                using (var db = new XBC_Context())
                {
                    result = (from m in db.t_assignment
                              where m.is_delete == false && m.id == id && m.is_hold != true
                              select new AssignmentViewModel

                              {
                                  id = m.id,
                                  biodata_id = m.biodata_id,
                                  title = m.title,
                                  start_date = m.start_date,
                                  end_date = m.end_date,
                                  description = m.description,
                                  realization_date = m.realization_date,
                                  notes = m.notes
                              }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return result == null ? new AssignmentViewModel() : result;
        }

        public static UserViewModel cekEmail(string email)
        {
            UserViewModel result = new UserViewModel();
            try
            {
                using (var db = new XBC_Context())
                {
                    result = (from m in db.t_user
                              where m.is_delete == false && m.email == email
                              select new UserViewModel
                              {
                                  id = m.id,
                                  email = m.email,
                                  username = m.username
                              }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return result==null? new UserViewModel():result;
        }

        public static ResponseResult Update(AssignmentViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    if (entity.id == 0)
                    {
                        t_assignment assignment = new t_assignment();
                        assignment.biodata_id = entity.biodata_id;
                        assignment.title = entity.title;
                        assignment.start_date = entity.start_date;
                        assignment.end_date = entity.end_date;
                        assignment.description = entity.description;

                        assignment.created_by = entity.UserId;
                        assignment.created_on = DateTime.Now;
                        assignment.is_delete = false;

                        db.t_assignment.Add(assignment);
                        db.SaveChanges();

                        var json = new JavaScriptSerializer().Serialize(assignment);

                        t_audit_log log = new t_audit_log();
                        log.type = "INSERT";
                        log.json_insert = json;

                        log.created_by = entity.UserId;
                        log.created_on = DateTime.Now;

                        db.t_audit_log.Add(log);

                        db.SaveChanges();

                        entity.id = assignment.id;
                        result.Entity = entity;
                    }
                    else
                    {
                        t_assignment assignment = db.t_assignment.Where(o => o.id == entity.id).FirstOrDefault();
                        if (assignment != null)
                        {
                            var Serial = new JavaScriptSerializer();
                            object data = new
                            {
                                assignment.biodata_id,
                                assignment.title,
                                assignment.start_date,
                                assignment.end_date,
                                assignment.description
                            };
                            var json = Serial.Serialize(data);

                            assignment.biodata_id = entity.biodata_id;
                            assignment.title = entity.title;
                            assignment.start_date = entity.start_date;
                            assignment.end_date = entity.end_date;
                            assignment.description = entity.description;

                            assignment.modified_by = entity.UserId;
                            assignment.modified_on = DateTime.Now;
                            db.SaveChanges();
                            result.Entity = entity;


                            object data2 = new
                            {
                                assignment.biodata_id,
                                assignment.title,
                                assignment.start_date,
                                assignment.end_date,
                                assignment.description
                            };

                            t_audit_log log = new t_audit_log();
                            log.type = "MODIFY";
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
                            result.ErrorMessage = "Category not found";
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

        public static ResponseResult Delete(AssignmentViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_assignment assignment = db.t_assignment.Where(o => o.id == entity.id).FirstOrDefault();
                    if (assignment != null)
                    {
                        var Serial = new JavaScriptSerializer();
                        object data = new
                        {
                            assignment.biodata_id,
                            assignment.title,
                            assignment.start_date,
                            assignment.end_date,
                            assignment.description,
                            assignment.realization_date,
                            assignment.notes
                        };
                        var json = Serial.Serialize(data);

                        assignment.is_delete = true;

                        assignment.deleted_by = entity.UserId;
                        assignment.modified_on = DateTime.Now;
                        db.SaveChanges();

                        result.Entity = entity;
                        db.SaveChanges();


                        t_audit_log log = new t_audit_log();
                        log.type = "MODIFY";
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
                        result.ErrorMessage = "Category not found";
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

        public static ResponseResult MarkAsDone(AssignmentViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_assignment assignment = db.t_assignment.Where(o => o.id == entity.id).FirstOrDefault();
                    if (assignment != null)
                    {
                        var Serial = new JavaScriptSerializer();
                        object data = new
                        {
                            assignment.biodata_id,
                            assignment.title,
                            assignment.start_date,
                            assignment.end_date,
                            assignment.description,
                            assignment.realization_date,
                            assignment.notes
                        };
                        var json = Serial.Serialize(data);

                        assignment.realization_date = entity.realization_date;
                        assignment.notes = entity.notes;
                        assignment.is_done = true;

                        assignment.deleted_by = entity.UserId;
                        assignment.modified_on = DateTime.Now;
                        db.SaveChanges();

                        result.Entity = entity;
                        db.SaveChanges();


                        t_audit_log log = new t_audit_log();
                        log.type = "MODIFY";
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
                        result.ErrorMessage = "Category not found";
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

        public static ResponseResult Hold(AssignmentViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_assignment assignment = db.t_assignment.Where(o => o.id == entity.id).FirstOrDefault();
                    if (assignment != null)
                    {
                        var Serial = new JavaScriptSerializer();
                        object data = new
                        {
                            assignment.biodata_id,
                            assignment.title,
                            assignment.start_date,
                            assignment.end_date,
                            assignment.description,
                            assignment.realization_date,
                            assignment.notes
                        };
                        var json = Serial.Serialize(data);

                        assignment.is_hold = true;

                        assignment.deleted_by = entity.UserId;
                        assignment.modified_on = DateTime.Now;
                        db.SaveChanges();

                        result.Entity = entity;
                        db.SaveChanges();


                        t_audit_log log = new t_audit_log();
                        log.type = "MODIFY";
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
                        result.ErrorMessage = "Category not found";
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
