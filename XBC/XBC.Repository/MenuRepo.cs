using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
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
            try
            {
                using (var db = new XBC_Context())
                {
                    result = (from m in db.t_menu
                              where m.is_delete == false
                              select new MenuViewModel

                              {
                                  id = m.id,
                                  code = m.code,
                                  title = m.title,
                                  descriptoin = m.descriptoin,
                                  image_url = m.image_url,
                                  menu_order = m.menu_order,
                                  menu_parent = m.menu_parent,
                                  menu_url = m.menu_url
                              }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return result == null ? new List<MenuViewModel>() : result;
        }

        public static List<MenuViewModel> Search(string search)
        {

            List<MenuViewModel> result = new List<MenuViewModel>();
            try
            {
                using (var db = new XBC_Context())
                {
                    result = (from m in db.t_menu
                              where m.is_delete == false && m.title.Contains(search)
                              select new MenuViewModel

                              {
                                  id = m.id,
                                  code = m.code,
                                  title = m.title,
                                  descriptoin = m.descriptoin,
                                  image_url = m.image_url,
                                  menu_order = m.menu_order,
                                  menu_parent = m.menu_parent,
                                  menu_url = m.menu_url
                              }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return result == null ? new List<MenuViewModel>() : result;
        }

        public static string getParentName(long id)
        {
            MenuViewModel result1 = new MenuViewModel();
            string nama = "";
            try { 
            
            
            using (var db1 = new XBC_Context())
            {
                result1 = (from m in db1.t_menu
                           where m.is_delete == false && m.id == id
                           select new MenuViewModel
                           {
                               title = m.title,
                           }).FirstOrDefault();
            }
            nama = result1.title;
            } catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return result1 == null ? "Master" : nama;
        }

        public static MenuViewModel ById(long id)
        {
            MenuViewModel result1 = new MenuViewModel();
            string nama = "";
            try
            {


                using (var db1 = new XBC_Context())
                {
                    result1 = (from m in db1.t_menu
                               where m.is_delete == false && m.id == id
                               select new MenuViewModel
                               {
                                   id = m.id,
                                   code = m.code,
                                   title = m.title,
                                   descriptoin = m.descriptoin,
                                   image_url = m.image_url,
                                   menu_order = m.menu_order,
                                   menu_parent = m.menu_parent,
                                   menu_url = m.menu_url

                               }).FirstOrDefault();
                }
                nama = result1.title;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return result1 == null ? new MenuViewModel() : result1;
        }

        public static List<MenuViewModel> sortChild(long parent)
        {
            List<MenuViewModel> result = new List<MenuViewModel>();
            try
            {
                using (var db = new XBC_Context())
                {
                    result = (from m in db.t_menu
                              where m.is_delete == false && m.menu_parent == parent
                              select new MenuViewModel
                              {
                                  id = m.id,
                                  code = m.code,
                                  title = m.title,
                                  descriptoin = m.descriptoin,
                                  image_url = m.image_url,
                                  menu_order = m.menu_order,
                                  menu_parent = m.menu_parent,
                                  menu_url = m.menu_url

                              }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
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
                        menu.created_by = entity.UserId;
                        menu.created_on = DateTime.Now;
                        menu.is_delete = false;

                        db.t_menu.Add(menu);
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
                        t_menu menu = db.t_menu.Where(o => o.id == entity.id).FirstOrDefault();
                        if (menu != null)
                        {
                            var Serial = new JavaScriptSerializer();
                            object data = new
                            {
                                menu.title,
                                menu.descriptoin,
                                menu.image_url,
                                menu.menu_url,
                                menu.menu_order,
                                menu.menu_parent
                            };
                            var json = Serial.Serialize(data);

                            menu.title = entity.title;
                            menu.descriptoin = entity.descriptoin;
                            menu.image_url = entity.image_url;
                            menu.menu_url = entity.menu_url;
                            menu.menu_order = entity.menu_order;
                            menu.menu_parent = entity.menu_parent;

                            menu.modified_by = entity.UserId;
                            menu.modified_on = DateTime.Now;
                            db.SaveChanges();
                            result.Entity = entity;


                            object data2 = new
                            {
                                menu.title,
                                menu.descriptoin,
                                menu.image_url,
                                menu.menu_url,
                                menu.menu_order,
                                menu.menu_parent
                            };

                            t_audit_log log = new t_audit_log();
                            log.type = "Edit";
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
                    string code = result.code.Substring(1);
                    newCode += (int.Parse(code) + 1).ToString("D4");
                }
                else
                {
                    newCode += "0001";
                }
            }
            return newCode;
        }

        public static ResponseResult Delete(MenuViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_menu menu = db.t_menu.Where(o => o.id == entity.id).FirstOrDefault();
                    if (menu != null)
                    {
                        var Serial = new JavaScriptSerializer();
                        object data = new
                        {
                            menu.id,
                            menu.image_url,
                            menu.title,
                            menu.code
                        };
                        var json = Serial.Serialize(data);

                        menu.is_delete = true;

                        menu.deleted_by = entity.UserId;
                        menu.modified_on = DateTime.Now;
                        db.SaveChanges();

                        result.Entity = entity;
                        db.SaveChanges();


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
