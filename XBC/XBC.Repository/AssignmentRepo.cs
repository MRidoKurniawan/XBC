using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.ViewModel;
using XBC.DataModel;

namespace XBC.Repository
{
    public class AssignmentRepo
    {
        public static List<AssignmentViewModel> All()
        {

            List<AssignmentViewModel> result = new List<AssignmentViewModel>();
            try
            {
                using (var db = new XBC_Context())
                {
                    result = (from m in db.t_assignment
                              where m.is_delete == false
                              select new AssignmentViewModel

                              {
                                  id = m.id,
                                  biodata_id = m.biodata_id,
                                  title = m.title,
                                  start_date = m.start_date,
                                  end_date = m.end_date,
                                  description = m.description,
                                  realization_date = m.realization_date,
                                  notes = m.notes
                              }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return result == null ? new List<AssignmentViewModel>() : result;
        }
    }
}
