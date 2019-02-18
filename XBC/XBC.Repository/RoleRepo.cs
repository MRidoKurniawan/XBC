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
    public class RoleRepo
    {
        //Get All Id
        public static List<RoleViewModel> All()
        {
            List<RoleViewModel> result = new List<RoleViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from r in db.t_role
                          where r.is_deleted == false
                          select new RoleViewModel
                          {
                              id = r.id, //sesuai database
                              code = r.code,
                              name = r.name,
                              is_delete = r.is_deleted
                          }).ToList();

                if (result == null)
                {
                    result = new List<RoleViewModel>();
                }
            }
            return result;
        }

        //Get By Id
        public static RoleViewModel ById(long Id)
        {
            RoleViewModel result = new RoleViewModel();
            using (var db = new XBC_Context())
            {
                result = (from r in db.t_role
                          where r.id == Id && r.is_deleted == false
                          select new RoleViewModel
                          {
                              id = r.id,
                              code = r.code,
                              name = r.name,
                              description = r.description,
                              is_delete = r.is_deleted
                          }).FirstOrDefault();

                if (result == null)
                    result = new RoleViewModel();
            }
            return result;
        }

        public static ResponseResult Update(RoleViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    //CREATE
                    if (entity.id == 0)
                    {
                        t_role rol = new t_role();
                        rol.code = entity.code; //sesuai yg kita input di web
                        rol.name = entity.name;
                        rol.description = entity.description;
                        rol.is_deleted = entity.is_delete;

                        rol.created_by = 1;
                        rol.created_on = DateTime.Now;

                        db.t_role.Add(rol);
                        db.SaveChanges();

                        var json = new JavaScriptSerializer().Serialize(rol);

                        t_audit_log log = new t_audit_log();
                        log.type = "Insert";
                        log.json_insert = json;

                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        db.t_audit_log.Add(log);
                        db.SaveChanges();

                        entity.id = rol.id;
                        result.Entity = entity;
                    }
                    else //EDIT
                    {
                        t_role rol = db.t_role
                            .Where(o => o.id == entity.id)
                            .FirstOrDefault();

                        if (rol != null)
                        {
                            var json = new JavaScriptSerializer().Serialize(rol);
                            t_audit_log log = new t_audit_log();
                            log.type = "Modify";
                            log.json_before = json;
                            log.created_by = 1;
                            log.created_on = DateTime.Now;

                            rol.code = entity.code;
                            rol.name = entity.name;
                            rol.description = entity.description;

                            rol.modified_by = 1;
                            rol.modified_on = DateTime.Now;

                            var json2 = new JavaScriptSerializer().Serialize(rol);
                            log.json_after = json2;
                            db.t_audit_log.Add(log);

                            db.SaveChanges();

                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.ErrorMessage = "Role not found!";
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

        //DELETE
        public static ResponseResult Delete(RoleViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_role rol = db.t_role
                        .Where(o => o.id == entity.id)
                        .FirstOrDefault();

                    if (rol != null)
                    {
                        var json = new JavaScriptSerializer().Serialize(rol);
                        t_audit_log log = new t_audit_log();
                        log.type = "Modify";
                        log.json_before = json;
                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        rol.is_deleted = true;
                        rol.deleted_by = 1;
                        rol.deleted_on = DateTime.Now;

                        var json2 = new JavaScriptSerializer().Serialize(rol);
                        log.json_after = json2;
                        db.t_audit_log.Add(log);

                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Role not found!";
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
        public static List<RoleViewModel> GetBySearch(String search)
        {
            List<RoleViewModel> result = new List<RoleViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from rol in db.t_role
                          where rol.name.Contains(search) && rol.is_deleted == false
                          select new RoleViewModel
                          {
                              id = rol.id,
                              code = rol.code,
                              name = rol.name
                          }).ToList();

                if (result == null)
                {
                    result = new List<RoleViewModel>();
                }
            }
            return result;
        }
    }
}
