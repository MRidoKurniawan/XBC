using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repository
{
    public class FeedbackRepo
    {
        public static List<FeedbackViewModel> All()
        {
            List<FeedbackViewModel> result = new List<FeedbackViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from t in db.t_test
                          where t.is_delete == false && t.is_bootcamp_test == false
                          select new FeedbackViewModel
                          {
                              id = t.id,
                              Nama = t.name
                          }).ToList();
                if (result == null)
                    result = new List<FeedbackViewModel>();
            }
            return result;
        }

        public static List<Document_TestViewModel> ListDT(long  id)
        {
            List<Document_TestViewModel> result = new List<Document_TestViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from dt in db.t_document_test
                          join t in db.t_test
                          on dt.test_id equals t.id
                          where dt.is_delete == false && t.id == id
                          select new Document_TestViewModel
                          {
                              id = dt.id

                          }).ToList();
            }

            return result == null ? new List<Document_TestViewModel>() : result;
        }

        public static List<Document_Test_DetailViewModel> ListDTD(long id)
        {
            List<Document_Test_DetailViewModel> result = new List<Document_Test_DetailViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from dtd in db.t_document_test_detail
                          join dt in db.t_document_test
                          on dtd.document_test_id equals dt.id
                          join q in db.t_question
                          on dtd.question_id equals q.id
                          where  dt.id == id
                          select new Document_Test_DetailViewModel
                          {
                              id = dtd.id,
                              document_test_id=dt.id,
                              question_id = q.id,
                              question = q.question,


                          }).ToList();
            }

            return result == null ? new List<Document_Test_DetailViewModel>() : result;
        }
    }
}
