using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repository
{
    public class HomeRepo
    {
       public static ResponseResult ForgotPassword(UserViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("ateambatch183@gmail.com");
                    mail.To.Add(entity.email);
                    mail.Subject = "percobaan";
                    mail.Body = "<a href='http://localhost:1216/Home/resetpassword/" + entity.id+ "'>http://localhost:1216/Home/resetpassword/" + entity.id + "</a>";
                    mail.IsBodyHtml = true;
                    //Way to add attachment
                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    using (SmtpClient smtp = new SmtpClient("Smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("ateambatch183@gmail.com", "Ateam1234");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (SmtpFailedRecipientException exception)
            {
                result.Success = false;
                result.ErrorMessage = exception.Message;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static List<MenuViewModel> getMenu(long id)
        {
            List<MenuViewModel> result = new List<MenuViewModel>();

            using (var db = new XBC_Context())
            {
                result = (from m in db.t_menu
                          join ma in db.t_menu_access on m.id equals ma.menu_id
                          where ma.role_id == id
                          select new MenuViewModel
                          {
                              title=m.title,
                              image_url = m.image_url,
                              menu_url = m.menu_url

                          }).ToList();
            }

            return result == null ? new List<MenuViewModel>() : result;
        }

    }
}
