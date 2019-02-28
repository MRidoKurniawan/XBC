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
    public class ClazzRepo
    {
        public static List<ClazzViewModel> BySearch(string search)
        {
            List<ClazzViewModel> result = new List<ClazzViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from c in db.t_clazz
                          join b in db.t_batch on c.batch_id equals b.id
                          join bd in db.t_biodata on c.biodata_id equals bd.id
                          where (b.name.Contains(search))
                          select new ClazzViewModel
                          {
                              id = c.id,
                              batchId = b.id,
                              BatchName = b.name,
                              biodataId = bd.id,
                              BiodataName = bd.name
                          }).Take(10).ToList();
            }
            return result == null ? new List<ClazzViewModel>() : result;
        }
        public static ClazzViewModel ById(long id)
        {
            ClazzViewModel result = new ClazzViewModel();
            using (var db = new XBC_Context())
            {
                result = (from cl in db.t_clazz
                          where cl.id == id
                          select new ClazzViewModel
                          {
                              id = cl.id,
                              batchId = cl.batch_id,
                              biodataId = cl.biodata_id,
                          }).FirstOrDefault();
                if (result == null)
                {
                    result = new ClazzViewModel();
                }
            }
            return result;
        }
        public static ResponseResult Delete(ClazzViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_clazz cl = db.t_clazz.Where(o => o.id == entity.id).FirstOrDefault();
                    if (cl != null)
                    {
                        db.t_clazz.Remove(cl);
                        db.SaveChanges();

                        //insert AuditLog
                        var json = new JavaScriptSerializer().Serialize(cl);
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
                        result.ErrorMessage = "Class Not Found!";
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
