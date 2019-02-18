using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repository
{
    public class Document_Test_DetailRepo
    {

        public static QuestionViewModel ById(long id)
        {
            QuestionViewModel result = new QuestionViewModel();
            using (var db = new XBC_Context())
            {
                result = (from q in db.t_question
                          select new QuestionViewModel
                          {
                              id = q.id,
                              //question_id=q.id,
                              //document_test_id=dt.id,
                              question = q.question
                          }).FirstOrDefault();
                if (result == null)
                    result = new QuestionViewModel();
            }
            return result;
        }

        public static Document_Test_DetailViewModel ByQuestion(long id)
        {
            Document_Test_DetailViewModel result = new Document_Test_DetailViewModel();
            using (var db = new XBC_Context())
            {
                result = (from q in db.t_question
                          where q.id == id 
                          select new Document_Test_DetailViewModel
                          {
                              question_id = q.id,
                              question = q.question
                          }).FirstOrDefault();

                return result != null ? result : new Document_Test_DetailViewModel();

            }

        }

        public static List<Document_Test_DetailViewModel> ByID(long id)
        {
            List<Document_Test_DetailViewModel> result = new List<Document_Test_DetailViewModel>();
            using(var db = new XBC_Context())
            {
                result = (from dtd in db.t_document_test_detail
                          join q in db.t_question
                          on dtd.question_id equals q.id
                          join dt in db.t_document_test
                          on dtd.document_test_id equals dt.id
                          where dtd.document_test_id == id
                          select new Document_Test_DetailViewModel
                          {
                              id = dtd.id,
                              question = q.question,
                              question_id = q.id,
                              document_test_id = dt.id
                          }).ToList();
                if (result==null)
                {
                    result = new List<Document_Test_DetailViewModel>();
                }
            }
            return result;
        }
    }
}
