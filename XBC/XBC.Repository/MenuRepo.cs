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
    public class MenuRepo
    {
        public static List<MenuViewModel> All()
        {

            List<MenuViewModel> result = new List<MenuViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from m in db.t_menu
                          where m.is_delete == false
                          select new MenuViewModel
                          {
                              id = m.id,
                              
                          }).ToList();
            }
            return result == null ? new List<MenuViewModel>() : result;
        }
        public static ResponseResult Update(MenuViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    if (entity.id == 0)
                    {
                        t_menu menu = new t_menu();
                        menu.code = getCode();
                        menu.title = entity.title;
                        menu.descriptoin = entity.descriptoin;
                        menu.image_url = entity.image_url;
                        menu.menu_parent = entity.menu_parent;
                        menu.menu_url = entity.menu_url;

                        db.SaveChanges();

                        var json = new JavaScriptSerializer().Serialize(menu);

                        t_audit_log log = new t_audit_log();
                        log.type = "Insert";
                        log.json_insert = json;

                        log.created_by = entity.UserId;
                        log.created_on = DateTime.Now;

                        db.t_audit_log.Add(log);

                        db.SaveChanges();

                        entity.id = menu.id;
                        result.Entity = entity;
                    }
                    else
                    {
                //        t_user user = db.t_user.Where(o => o.id == entity.id).FirstOrDefault();
                //        if (user != null)
                //        {
                //            var Serial = new JavaScriptSerializer();
                //            object data = new
                //            {
                //                user.username,
                //                user.email,
                //                user.role_id,
                //                user.mobile_flag,
                //                user.mobile_token
                //            };
                //            var json = Serial.Serialize(data);

                //            user.username = entity.username;
                //            user.email = entity.email;
                //            user.role_id = entity.role_id;
                //            user.mobile_flag = entity.mobile_flag;
                //            user.mobile_token = entity.mobile_token;

                //            user.modified_by = entity.UserId;
                //            user.modified_on = DateTime.Now;
                //            db.SaveChanges();
                //            result.Entity = entity;
                //            db.SaveChanges();


                //            object data2 = new
                //            {
                //                user.username,
                //                user.email,
                //                user.role_id,
                //                user.mobile_flag,
                //                user.mobile_token
                //            };

                //            t_audit_log log = new t_audit_log();
                //            log.type = "Edit";
                //            log.json_before = json;
                //            json = Serial.Serialize(data2);
                //            log.json_after = json;


                //            log.created_by = entity.UserId;
                //            log.created_on = DateTime.Now;

                //            db.t_audit_log.Add(log);

                //            db.SaveChanges();

                //            result.Entity = entity;
                //        }
                //        else
                //        {
                //            result.Success = false;
                //            result.ErrorMessage = "Category not found";
                //        }
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
        public static string getCode()
        {
            string newCode = "M";
            using (var db = new XBC_Context())
            {
                var result = (from oh in db.t_menu
                              select new { code = oh.code })
                              .OrderByDescending(o => o.code)
                              .FirstOrDefault();
                if (result != null)
                {
                    newCode += (int.Parse(result.code) + 1).ToString("D4");
                }
                else
                {
                    newCode += "0001";
                }
            }
            return newCode;
        }
    }
}
