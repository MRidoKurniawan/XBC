using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
