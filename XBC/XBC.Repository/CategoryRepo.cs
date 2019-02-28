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
    public class CategoryRepo
    {
        public static List<CategoryViewModel> GetBySearch(String cari)
        {
            List<CategoryViewModel> result = new List<CategoryViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from c in db.t_category
                          where c.is_delete== false 
                          && (c.code.Contains(cari) || c.name.Contains(cari))
                          select new CategoryViewModel
                          {
                              id = c.id,
                              code = c.code,
                              name = c.name,
                          }).ToList();

                if (result == null)
                {
                    result = new List<CategoryViewModel>();
                }
            }
            //return result == null? new List<CategoryViewModel>():result;
            return result;
        }

        //Post Update for Create and Edit
        public static ResponseResult Update(CategoryViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                entity.code = GetNewCode();
                using (var db = new XBC_Context())
                {
                    //create
                    if (entity.id == 0)
                    {
                        t_category cat = new t_category();
                        cat.code = entity.code;
                        cat.name = entity.name;
                        cat.description = entity.description;

                        cat.created_by = entity.UserId;
                        cat.created_on = DateTime.Now;

                        db.t_category.Add(cat);
                        
                        db.SaveChanges();

                        var json = new JavaScriptSerializer().Serialize(cat);
                        t_audit_log log = new t_audit_log();
                        log.type = "Insert";
                        log.json_insert = json;

                        log.created_by = entity.UserId;
                        log.created_on = DateTime.Now;

                        db.t_audit_log.Add(log);

                        db.SaveChanges();

                        entity.id = cat.id;
                        result.Entity = entity;
                    }
                    else //edit
                    {
                        t_category cat = db.t_category.Where(o => o.id == entity.id).FirstOrDefault();

                        if (cat != null)
                        {
                            var json = new JavaScriptSerializer().Serialize(cat);
                            t_audit_log log = new t_audit_log();
                            log.type = "Modify";
                            log.json_before = json;

                            log.created_by = entity.UserId;
                            log.created_on = DateTime.Now;



                            cat.name = entity.name;
                            cat.description = entity.description;


                            cat.modified_by = entity.UserId;
                            cat.modified_on = DateTime.Now;

                            var json2 = new JavaScriptSerializer().Serialize(cat);
                            log.json_after = json2;
                            db.t_audit_log.Add(log);
                            db.SaveChanges();

                            result.Entity = entity;
                        }

                        else
                        {
                            result.Success = false;
                            result.ErrorMessage = "Category Not Found";
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

        private static string GetNewCode()
        {
            string newRef = String.Format("C");
            using (var db = new XBC_Context())
            {
                var result = (from oh in db.t_category
                              where oh.code.Contains(newRef)
                              select new { Code = oh.code })
                              .OrderByDescending(o => o.Code)
                              .FirstOrDefault();
                if (result != null)
                {
                    string[] lastRef = result.Code.Split('C');
                    newRef += (int.Parse(lastRef[1]) + 1).ToString("D4");
                }
                else
                {
                    newRef += "0001";
                }
            }
            return newRef;
        }

        //Get Id for edit and delete
        public static CategoryViewModel ById(long id)
        {
            CategoryViewModel result = new CategoryViewModel();
            using (var db = new XBC_Context())
            {
                result = (from c in db.t_category
                          where c.id == id
                          select new CategoryViewModel
                          {
                              id = c.id,
                              code=c.code,
                              name=c.name,
                              description=c.description
                          }).FirstOrDefault();
                if (result == null)
                    result = new CategoryViewModel();
            }
            return result;
        }

        public static ResponseResult Delete(CategoryViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_category cat = db.t_category.Where(o => o.id == entity.id).FirstOrDefault();
                    if (cat != null)
                    {
                        var json = new JavaScriptSerializer().Serialize(cat);
                        t_audit_log log = new t_audit_log();
                        log.type = "Modify";
                        log.json_before = json;

                        log.created_by = entity.UserId;
                        log.created_on = DateTime.Now;

                        cat.is_delete = true;
     
                        cat.deleted_by = entity.UserId;
                        cat.deleted_on = DateTime.Now;

                        db.SaveChanges();

                        var json2 = new JavaScriptSerializer().Serialize(cat);
                        log.json_after = json2;
                        db.t_audit_log.Add(log);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Category Not Found!";
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

        public static CategoryViewModel Check(string name)
        {
            CategoryViewModel result = new CategoryViewModel();
            using (var db = new XBC_Context())
            {
                result = (from cat in db.t_category
                          where cat.name == name && cat.is_delete == false
                          select new CategoryViewModel
                          {
                              id = cat.id,
                              name = cat.name
                          }).FirstOrDefault();
            }
            return result == null ? new CategoryViewModel() : result;
        }

    }
}
