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
                          where idn.is_delete == false
                          select new IdleNewsViewModel
                          {
                              id = idn.id,
                              categoryId = idn.category_id,
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
                          where idn.id == id && idn.is_delete == false
                          select new IdleNewsViewModel
                          {
                              id = idn.id,
                              categoryId = idn.category_id,
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
    }
}
