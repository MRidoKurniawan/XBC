using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repository
{
    public class Test_TypeRepo
    {
        //GetAll
        public static List<Test_TypeViewModel> All(string search)
        {
            List<Test_TypeViewModel> result = new List<Test_TypeViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from t in db.t_test_type
                          where (t.is_delete == false && t.name.Contains(search))
                          select new Test_TypeViewModel
                          {
                              id = t.id,
                              name = t.name
                          }).ToList();
            }

            return result == null ? new List<Test_TypeViewModel>() : result;
        }
    }
}
