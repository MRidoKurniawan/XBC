using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using XBC.DataModel;
using System.Net.Mail;
using System.Net;

namespace XBC.Tests
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        //public void InsertVariant()
        //{
        //    Trace.WriteLine("--- Start Test Variant ---");
        //    using (var db = new XBC_Context())
        //    {
        //        t_audit_log variant = new t_audit_log();
        //        variant.type = "sda";
        //        variant.json_insert = "TSdsT";
        //        variant.json_before = "Tesssting";
        //        variant.json_after = "hhddh";
        //        variant.json_delete = "Deaaar";
        //        variant.created_by = 1;
        //        variant.created_on = DateTime.Now;
        //        db.t_audit_log.Add(variant);
        //        db.SaveChanges();
        //    }
        //    Trace.WriteLine("--- End Test Variant ---");
        //}

        [TestMethod]
        public void SendMail()
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("ateambatch183@gmail.com");
                    mail.To.Add("ashararbain.aa@gmail.com");
                    mail.Subject = "percobaan";
                    mail.Body = "rizqi";
                    mail.IsBodyHtml = true;
                    //Way to add attachment
                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    using (SmtpClient smtp = new SmtpClient("Smtp.gmail.com", 25))
                    {
                        smtp.Credentials = new NetworkCredential("ateambatch183@gmail.com", "Ateam1234");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (SmtpFailedRecipientException exception)
            {
                Console.Write(exception.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

    }
}
