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
                    mail.Subject = "Reset Password";
                    mail.Body = "<div style = 'width: 96%; margin-left: 2%; padding-top:20px; background-color: #FFFFFF; float: left;'><div style='float:right;'><img src = 'https://i.imgur.com/Qt0o9W9.png' style = 'height: 100px; margin-right: 3rem;'></div><div style='width:100%; margin-top:150px;'><h3><b> Atur ulang kata sandi anda?</b></h3><br><p> Jika anda meminta pengaturan ulang kata sandi untuk " + entity.username + ", klik tombol dibawah. Jika anda tidak melakukannya, abaikan email ini.</p><br><a href = 'http://localhost:1216/Home/resetpassword/" + entity.id + "' target = '_blank'><button style = 'background-color: #008CBA; border: none; color: white; padding: 15px 32px; text-align: center; text-decoration: none; display: inline-block; font-size: 16px; border-radius: 8px;'> Atur ulang kata sandi </button></a></div></div> ";
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
                          where ma.role_id == id && m.is_delete == false
                          select new MenuViewModel
                          {
                              title = m.title,
                              image_url = m.image_url,
                              menu_url = m.menu_url

                          }).ToList();
            }

            return result == null ? new List<MenuViewModel>() : result;
        }

    }
}
