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
    public class BiodataRepo
    {
        //Get All Id
        public static List<BiodataViewModel> All()
        {
            List<BiodataViewModel> result = new List<BiodataViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from b in db.t_biodata
                          where b.is_deleted == false
                          select new BiodataViewModel
                          {
                              id = b.id,
                              name = b.name,
                              majors = b.majors,
                              gpa = b.gpa,
                              is_deleted = b.is_deleted
                          }).ToList();

                if (result == null)
                {
                    result = new List<BiodataViewModel>();
                }
            }
            return result;
        }

        //Get By Id
        public static BiodataViewModel ById(long Id)
        {
            BiodataViewModel result = new BiodataViewModel();
            using (var db = new XBC_Context())
            {
                result = (from b in db.t_biodata
                          where b.id == Id && b.is_deleted == false
                          select new BiodataViewModel
                          {
                              id = b.id,
                              name = b.name,
                              gender = b.gender,
                              last_education = b.last_education,
                              graduation_year = b.graduation_year,
                              educational_level = b.educational_level,
                              majors = b.majors,
                              gpa = b.gpa,
                              bootcamp_test_type = b.bootcamp_test_type,
                              iq = b.iq,
                              du = b.du,
                              arithmetic = b.arithmetic,
                              nested_logic = b.nested_logic,
                              join_table = b.join_table,
                              tro = b.tro,
                              notes = b.notes,
                              interviewer = b.interviewer
                          }).FirstOrDefault();

                if (result == null)
                    result = new BiodataViewModel();
            }
            return result;
        }

        public static ResponseResult Update(BiodataViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    //CREATE
                    if (entity.id == 0)
                    {
                        t_biodata bio = new t_biodata();
                        bio.name = entity.name; //sesuai yg kita input di web
                        bio.last_education = entity.last_education;
                        bio.educational_level = entity.educational_level;
                        bio.graduation_year = entity.graduation_year;
                        bio.majors = entity.majors;
                        bio.gpa = entity.gpa;
                        bio.is_deleted = entity.is_deleted;

                        bio.created_by = 1;
                        bio.created_on = DateTime.Now;

                        db.t_biodata.Add(bio);
                        db.SaveChanges();

                        var json = new JavaScriptSerializer().Serialize(bio);

                        t_audit_log log = new t_audit_log();
                        log.type = "Insert";
                        log.json_insert = json;

                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        db.t_audit_log.Add(log);
                        db.SaveChanges();

                        entity.id = bio.id;
                        result.Entity = entity;
                    }
                    else //EDIT
                    {
                        t_biodata bio = db.t_biodata
                            .Where(o => o.id == entity.id && o.is_deleted == false)
                            .FirstOrDefault();

                        if (bio != null)
                        {
                            object data = new
                            {
                                bio.id,
                                bio.name,
                                bio.gender,
                                bio.last_education,
                                bio.graduation_year,
                                bio.educational_level,
                                bio.majors,
                                bio.gpa,
                                bio.bootcamp_test_type,
                                bio.iq,
                                bio.du,
                                bio.arithmetic,
                                bio.nested_logic,
                                bio.join_table,
                                bio.tro,
                                bio.notes,
                                bio.interviewer
                            };

                            var json = new JavaScriptSerializer().Serialize(data);
                            t_audit_log log = new t_audit_log();
                            log.type = "Modify";
                            log.json_before = json;
                            log.created_by = 1;
                            log.created_on = DateTime.Now;

                            bio.name = entity.name;
                            bio.gender = entity.gender;
                            bio.last_education = entity.last_education;
                            bio.graduation_year = entity.graduation_year;
                            bio.educational_level = entity.educational_level;
                            bio.majors = entity.majors;
                            bio.gpa = entity.gpa;
                            bio.bootcamp_test_type = entity.bootcamp_test_type;
                            bio.iq = entity.iq;
                            bio.du = entity.du;
                            bio.arithmetic = entity.arithmetic;
                            bio.nested_logic = entity.nested_logic;
                            bio.join_table = entity.join_table;
                            bio.tro = entity.tro;
                            bio.notes = entity.notes;
                            bio.interviewer = entity.interviewer;

                            bio.modified_by = 1;
                            bio.modified_on = DateTime.Now;

                            object data2 = new
                            {
                                bio.id,
                                bio.name,
                                bio.gender,
                                bio.last_education,
                                bio.graduation_year,
                                bio.educational_level,
                                bio.majors,
                                bio.gpa,
                                bio.bootcamp_test_type,
                                bio.iq,
                                bio.du,
                                bio.arithmetic,
                                bio.nested_logic,
                                bio.join_table,
                                bio.tro,
                                bio.notes,
                                bio.interviewer
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
                            result.ErrorMessage = "Biodata not found!";
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

        //RADIO BUTTON
        public static string GetGender(long Id)
        {
            string gender = "";
            BiodataViewModel result = new BiodataViewModel();
            try
            {
                using (var db = new XBC_Context())
                {
                    result = (from b in db.t_biodata
                              where b.id == Id
                              select new BiodataViewModel
                              {
                                  gender = b.gender
                              }).FirstOrDefault();

                    if (result != null)
                        gender = result.gender==null?"" : result.gender;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return gender;

        }

        //DELETE
        public static ResponseResult Delete(BiodataViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_biodata bio = db.t_biodata
                        .Where(o => o.id == entity.id)
                        .FirstOrDefault();

                    if (bio != null)
                    {
                        var json = new JavaScriptSerializer().Serialize(bio);
                        t_audit_log log = new t_audit_log();
                        log.type = "Modify";
                        log.json_before = json;
                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        bio.is_deleted = true;
                        bio.deleted_by = 1;
                        bio.deleted_on = DateTime.Now;

                        var json2 = new JavaScriptSerializer().Serialize(bio);
                        log.json_after = json2;
                        db.t_audit_log.Add(log);

                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Biodata not found!";
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

        //Get By Search
        public static List<BiodataViewModel> GetBySearch(String search)
        {
            List<BiodataViewModel> result = new List<BiodataViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from bio in db.t_biodata
                          where bio.name.Contains(search) && bio.is_deleted == false
                          || bio.majors.Contains(search) && bio.is_deleted == false
                          select new BiodataViewModel
                          {
                              id = bio.id,
                              name = bio.name,
                              majors = bio.majors,
                              gpa = bio.gpa
                          }).ToList();

                if (result == null)
                {
                    result = new List<BiodataViewModel>();
                }
            }
            return result;
        }

        //Get By Bootcamp Test Type 
        public static List<BootcampTestTypeViewModel> ByBtt()
        {
            List<BootcampTestTypeViewModel> result = new List<BootcampTestTypeViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from btt in db.t_bootcamp_test_type
                          join b in db.t_biodata on btt.id equals b.bootcamp_test_type into ps
                          from m in ps.DefaultIfEmpty()
                          where btt.is_delete == false
                          select new BootcampTestTypeViewModel
                          {
                              id = btt.id,
                              name = btt.name,
                          }).ToList();

                if (result == null)
                {
                    result = new List<BootcampTestTypeViewModel>();
                }
            }
            return result;
        }
    }
}
