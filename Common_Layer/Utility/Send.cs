using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Common_Layer.Utility
{
    public class Send
    {
        public string SendMail(string ToEmail, string Token)
        {
            string FromEmail = "pk3476@srmist.edu.in";
            // string resetPasswordLink = $"BookStore Reset Password: <a href=http://localhost:4200/resetpassword/{Token}> Click Here</a>";
            MailMessage Message = new MailMessage(FromEmail, ToEmail);
            string MailBody = "Token Generated : " + Token;
            Message.Subject = "Token Generated For Resetting Password";
            //Message.Body = resetPasswordLink;
            //Message.Body = $"Click here <a href=\"{resetPasswordLink}\">reset your password</a>";
            Message.Body = MailBody.ToString();
            Message.BodyEncoding = Encoding.UTF8;
            Message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential credential
                = new NetworkCredential(FromEmail, "wnql alvr imhs znru");

            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = credential;

            smtp.Send(Message);
            return ToEmail;
        }

    }
}

