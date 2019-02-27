using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;
using System.Web.Script.Serialization;

namespace XBC.Repository
{
    public class IdleNewsRepo
    {
        //getall
        public static List<IdleNewsViewModel> All()
        {
            List<IdleNewsViewModel> result = new List<IdleNewsViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from idn in db.t_idle_news
                          join cat in db.t_category on idn.category_id equals cat.id
                          where idn.is_delete == false
                          select new IdleNewsViewModel
                          {
                              id = idn.id,
                              categoryId = idn.category_id,
                              categoryName = cat.name,
                              title = idn.title,
                              content = idn.content,
                              isPublish = idn.is_publish,
                              isDelete = idn.is_delete
                          }).ToList();
                if (result == null)
                    result = new List<IdleNewsViewModel>();
            }
            return result;
        }
        //getbyid
        public static IdleNewsViewModel ById(long id)
        {
            IdleNewsViewModel result = new IdleNewsViewModel();
            using (var db = new XBC_Context())
            {
                result = (from idn in db.t_idle_news
                          join cat in db.t_category on idn.category_id equals cat.id
                          where idn.id == id && idn.is_delete == false
                          select new IdleNewsViewModel
                          {
                              id = idn.id,
                              categoryId = idn.category_id,
                              categoryName = cat.name,
                              title = idn.title,
                              content = idn.content,
                              isPublish = idn.is_publish,
                              isDelete = idn.is_delete
                          }).FirstOrDefault();
                if (result == null)
                    result = new IdleNewsViewModel();
            }
            return result;
        }
        public static List<IdleNewsViewModel> GetBySearch(string search)
        {
            List<IdleNewsViewModel> result = new List<IdleNewsViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from idn in db.t_idle_news
                          join cat in db.t_category on idn.category_id equals cat.id
                          where idn.title.Contains(search) && idn.is_delete == false
                          select new IdleNewsViewModel
                          {
                              id = idn.id,
                              categoryId = idn.category_id,
                              categoryName = cat.name,
                              title = idn.title,
                              content = idn.content,
                              isPublish = idn.is_publish,
                              isDelete = idn.is_delete
                          }).ToList();
                if (result == null)
                    result = new List<IdleNewsViewModel>();
            }
            return result;
        }
        public static ResponseResult Update(IdleNewsViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new XBC_Context())
            {
                if (entity.id == 0)
                {
                    t_idle_news idn = new t_idle_news();
                    idn.category_id = entity.categoryId;
                    idn.title = entity.title;
                    idn.content = entity.content;
                    idn.is_publish = entity.isPublish;

                    idn.created_by = entity.UserId;
                    idn.created_on = DateTime.Now;
                    idn.is_delete = false;

                    db.t_idle_news.Add(idn);
                    db.SaveChanges();

                    var json = new JavaScriptSerializer().Serialize(idn);
                    t_audit_log log = new t_audit_log();
                    log.type = "Insert";
                    log.json_insert = json;

                    log.created_by = entity.UserId;
                    log.created_on = DateTime.Now;
                    db.t_audit_log.Add(log);

                    db.SaveChanges();

                    entity.id = idn.id;
                    result.Entity = entity;
                }
                else //edit
                {
                    t_idle_news idn = db.t_idle_news.Where(idns => idns.id == entity.id).FirstOrDefault();
                    if (idn != null)
                    {
                        var Serial = new JavaScriptSerializer();
                        object data = new
                        {
                            idn.category_id,
                            idn.title,
                            idn.content,
                            idn.is_publish
                        };
                        var json = Serial.Serialize(data);
                        idn.category_id = entity.categoryId;
                        idn.title = entity.title;
                        idn.content = entity.content;
                        idn.is_publish = entity.isPublish;

                        idn.modified_by = entity.UserId;
                        idn.modified_on = DateTime.Now;
                        db.SaveChanges();
                        result.Entity = entity;
                        db.SaveChanges();

                        object data2 = new
                        {
                            idn.category_id,
                            idn.title,
                            idn.content,
                            idn.is_publish
                        };

                        t_audit_log log = new t_audit_log();
                        log.type = "Modify";
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
                        result.ErrorMessage = "Idle News not found!";
                    }
                }
            }
            return result;
        }
        public static ResponseResult Publish(IdleNewsViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new XBC_Context())
            {
                t_idle_news idn = db.t_idle_news.Where(o => o.id == entity.id).FirstOrDefault();
                if (idn != null)
                {
                    var Serial = new JavaScriptSerializer();
                    object data = new
                    {
                        idn.category_id,
                        idn.title,
                        idn.content,
                        idn.is_publish
                    };
                    var json = Serial.Serialize(data);
                    idn.is_publish = true;
                    idn.modified_by = entity.UserId;
                    idn.modified_on = DateTime.Now;
                    db.SaveChanges();
                    result.Entity = entity;
                    db.SaveChanges();

                    t_audit_log log = new t_audit_log();
                    log.type = "Modify";
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
                    result.ErrorMessage = "Idle News not found!";
                }
            }
            return result;
        }

        public static ResponseResult Delete(IdleNewsViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new XBC_Context())
            {
                t_idle_news idn = db.t_idle_news.Where(o => o.id == entity.id).FirstOrDefault();
                if (idn != null)
                {
                    var Serial = new JavaScriptSerializer();
                    object data = new
                    {
                        idn.category_id,
                        idn.title,
                        idn.content,
                        idn.is_publish
                    };
                    var json = Serial.Serialize(data);

                    idn.is_delete = true;

                    idn.deleted_by = entity.UserId;
                    idn.deleted_on = DateTime.Now;
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
                    result.ErrorMessage = "Idle News not found!";
                }
            }
            return result;
        }
    }
}
