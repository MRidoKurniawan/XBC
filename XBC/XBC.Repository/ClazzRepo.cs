using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

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
                              biodataId = bd.id,
                          }).Take(10).ToList();
            }
            return result == null ? new List<ClazzViewModel>() : result;
        }
    }
}
