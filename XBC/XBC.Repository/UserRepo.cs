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
    public class UserRepo
    {
        public static List<UserViewModel> All()
        {

            List<UserViewModel> result = new List<UserViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from u in db.t_user
                          join
      r in db.t_role on u.role_id equals r.id
                          select new UserViewModel
                          {
                              id = u.id,
                              username = u.username,
                              email = u.email,
                              role_id = u.role_id,
                              role_name = r.name,
                              password = u.password,
                              mobile_flag = u.mobile_flag,
                              mobile_token = u.mobile_token

                          }).ToList();
            }
            return result == null ? new List<UserViewModel>() : result;
        }
        public static List<UserViewModel> Search(string cari)
        {

            List<UserViewModel> result = new List<UserViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from u in db.t_user
                          join
                          r in db.t_role on u.role_id equals r.id
                          where u.username.Contains(cari) || u.email.Contains(cari)
                          select new UserViewModel
                          {
                              id = u.id,
                              username = u.username,
                              email = u.email,
                              role_id = u.role_id,
                              role_name = r.name,
                              password = u.password,
                              mobile_flag = u.mobile_flag,
                              mobile_token = u.mobile_token

                          }).ToList();
            }
            return result == null ? new List<UserViewModel>() : result;
        }
        public static UserViewModel ById(long id)
        {

            UserViewModel result = new UserViewModel();
            using (var db = new XBC_Context())
            {
                result = (from u in db.t_user
                          where u.id == id
                          select new UserViewModel
                          {
                              id = u.id,
                              username = u.username,
                              email = u.email,
                              role_id = u.role_id,
                              password = u.password,
                              mobile_flag = u.mobile_flag,
                              mobile_token = u.mobile_token

                          }).FirstOrDefault();
            }
            return result == null ? new UserViewModel() : result;
        }

        public static bool GetMobile_flag(long id)
        {

            UserViewModel result = new UserViewModel();
            using (var db = new XBC_Context())
            {
                result = (from u in db.t_user
                          where u.id == id
                          select new UserViewModel
                          {
                              mobile_flag = u.mobile_flag
                          }).FirstOrDefault();
            }
            return result == null ? false : result.mobile_flag;
        }

        public static ResponseResult Update(UserViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    if (entity.id == 0)
                    {
                        t_user user = new t_user();

                        user.username = entity.username;
                        user.email = entity.email;
                        user.password = entity.password;
                        user.role_id = entity.role_id;
                        user.mobile_flag = false;
                        user.mobile_token = null;
                        user.is_delete = false;

                        user.created_by = 1;
                        user.created_on = DateTime.Now;

                        db.t_user.Add(user);
                        db.SaveChanges();

                        var json = new JavaScriptSerializer().Serialize(user);

                        t_audit_log log = new t_audit_log();
                        log.type = "Insert";
                        log.json_insert = json;

                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        db.t_audit_log.Add(log);

                        db.SaveChanges();

                        entity.id = user.id;
                        result.Entity = entity;
                    }
                    else
                    {
                        t_user user = db.t_user.Where(o => o.id == entity.id).FirstOrDefault();
                        if (user != null)
                        {
                            object data = entity;
                            var json = new JavaScriptSerializer().Serialize(data);
                            user.username = entity.username;
                            user.email = entity.email;
                            user.role_id = entity.role_id;
                            user.mobile_flag = entity.mobile_flag;
                            user.mobile_token = entity.mobile_token;

                            user.modified_by = 1;
                            user.modified_on = DateTime.Now;
                            db.SaveChanges();
                            result.Entity = entity;
                            db.SaveChanges();


                            object data2 = user;

                            t_audit_log log = new t_audit_log();
                            log.type = "Edit";
                            log.json_before = json;
                            json = new JavaScriptSerializer().Serialize(data2);
                            //log.json_after = json2;
                            

                            log.created_by = 1;
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


    }
}
