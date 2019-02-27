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
    public class MenuAccessRepo
    {
        //getall
        public static List<MenuAccessViewModel> All()
        {
            List<MenuAccessViewModel> result = new List<MenuAccessViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from ma in db.t_menu_access
                          join m in db.t_menu on ma.menu_id equals m.id
                          join r in db.t_role on ma.role_id equals r.id
                          select new MenuAccessViewModel
                          {
                              id = ma.id,
                              role_id = ma.role_id,
                              menu_id = ma.menu_id,
                              menu_name = m.title,
                              role_name = r.name
                          }).ToList();
                if (result == null)
                    result = new List<MenuAccessViewModel>();
            }
            return result;
        }
        public static MenuAccessViewModel ById(long id)
        {
            MenuAccessViewModel result = new MenuAccessViewModel();
            using (var db = new XBC_Context())
            {
                result = (from ma in db.t_menu_access
                          join m in db.t_menu on ma.menu_id equals m.id
                          join r in db.t_role on ma.role_id equals r.id
                          select new MenuAccessViewModel
                          {
                              id = ma.id,
                              role_id = ma.role_id,
                              menu_id = ma.menu_id,
                              menu_name = m.title,
                              role_name = r.name
                          }).FirstOrDefault();
                if (result == null)
                    result = new MenuAccessViewModel();
            }
            return result;
        }
        public static List<MenuAccessViewModel> GetBySearch(long id)
        {
            List<MenuAccessViewModel> result = new List<MenuAccessViewModel>();
            using (var db = new XBC_Context())
            {
                if (id > 0)
                {
                    result = (from ma in db.t_menu_access
                              join m in db.t_menu on ma.menu_id equals m.id
                              join r in db.t_role on ma.role_id equals r.id
                              where ma.role_id == id
                              select new MenuAccessViewModel
                              {
                                  id = ma.id,
                                  role_id = ma.role_id,
                                  menu_id = ma.menu_id,
                                  menu_name = m.title,
                                  role_name = r.name
                              }).ToList();
                }
                else
                {
                    result = (from ma in db.t_menu_access
                              join m in db.t_menu on ma.menu_id equals m.id
                              join r in db.t_role on ma.role_id equals r.id
                              select new MenuAccessViewModel
                              {
                                  id = ma.id,
                                  role_id = ma.role_id,
                                  menu_id = ma.menu_id,
                                  menu_name = m.title,
                                  role_name = r.name
                              }).ToList();
                }
                if (result == null)
                    result = new List<MenuAccessViewModel>();
            }
            return result;
        }

        public static ResponseResult Create(MenuAccessViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new XBC_Context())
            {
                t_menu_access ma = new t_menu_access();
                ma.role_id = entity.role_id;
                ma.menu_id = entity.menu_id;

                ma.created_by = entity.UserId;
                ma.created_on = DateTime.Now;

                db.t_menu_access.Add(ma);
                db.SaveChanges();

                var json = new JavaScriptSerializer().Serialize(ma);
                t_audit_log log = new t_audit_log();
                log.type = "Insert";
                log.json_insert = json;

                log.created_by = entity.UserId;
                log.created_on = DateTime.Now;
                db.t_audit_log.Add(log);

                db.SaveChanges();

                entity.id = ma.id;
                result.Entity = entity;
            }
            return result;
        }
        public static ResponseResult Delete(MenuAccessViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new XBC_Context())
            {
                t_menu_access ma = db.t_menu_access.Where(o => o.id == entity.id).FirstOrDefault();
                db.t_menu_access.Remove(ma);
                if (ma != null)
                {
                    object data = new
                    {
                        ma.id,
                        ma.menu_id,
                        ma.role_id
                    };
                    var json = new JavaScriptSerializer().Serialize(data);
                    t_audit_log log = new t_audit_log();
                    log.type = "DELETE";
                    log.json_before = json;

                    log.created_by = entity.UserId;
                    log.created_on = DateTime.Now;

                    var json2 = new JavaScriptSerializer().Serialize(ma);
                    log.json_after = json2;
                    db.t_audit_log.Add(log);
                    db.SaveChanges();

                    result.Entity = entity;
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = " not found! ";
                }
            }
            return result;
        }
    }
}
