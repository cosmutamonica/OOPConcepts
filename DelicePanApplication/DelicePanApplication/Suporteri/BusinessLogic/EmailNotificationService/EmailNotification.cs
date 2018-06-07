using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BusinessLayer.EmailNotificationService
{
    public class EmailNotification : IEmailNotification
    {
        private IEmailSender _sender;

        public EmailNotification(IEmailSender sender)
        {
            _sender = sender;
        }

        public void NotifystudentWhenHomeworkIsCreated(string contact, string studentName,DateTime EndDate)
        {
            string subject = String.Format("Another homework was assigned to you");
            string body = String.Format("<br><br>Dear {0},<br><br>Please note that new homework was now created and will expire in {1}. To view it please log in to <a href=\"http://localhost:54335/User/Login\">Check</a>.<br><br>Thank you!<br>",
                studentName,               
                EndDate
                );
            _sender.SendEmail(contact, subject, body);
        }

        public void SendValidationEmail(string contact, string studentName, string studentId)
        {
            string subject = String.Format("Thank you for enrolling in our school ");
            string body = String.Format("<br><br>Dear {0},<br><br>Please note that your registration was not valide.To activate please <a href=\"http://"+ HttpContext.Current.Request.Url.Authority+"/Account/ActivateAccount/{2}\">click here!!!!!!</a>. To view it please log in !!! <br><br>Thank you!<br>",
                studentName,
                studentId,
                studentId
                );
            _sender.SendEmail(contact, subject, body);
        }

        public void NotifystudentWhenHomeworkIsRejected(string contact, string studentName, int homeworkId, DateTime EndDate)
        {
            string subject = String.Format("Rejected homework");
            string body = String.Format("<br><br>Dear {0},<br><br>Please note that homework with Id={1} was expired in {2}. To view it please log in to <a href=\"http://localhost:54335/\">Check</a>.<br><br>Thank you!<br>",
                studentName,
                homeworkId,
                EndDate
                );
            _sender.SendEmail(contact, subject, body);
        }

        public void NotifystudentWhenHomeworkIsAccepted(string contact, string studentName, int homeworkId)
        {
            string subject = String.Format("Accepted homework");
            string body = String.Format("<br><br>Dear {0},<br><br>Please note that homework with Id={1} was accepted! To view it please log in to <a href=\"http://localhost:54335/\">Check</a>.<br><br>Congrats!!!<br>",
                studentName,
                homeworkId
                );
            _sender.SendEmail(contact, subject, body);
        }

        public void NotifystudentWhenHomeworkIsCommented(string contact, string studentName, int homeworkId)
        {
            string subject = String.Format("Commented homework");
            string body = String.Format("<br><br>Dear {0},<br><br>Please note that homework with Id={1} was commented. To view your homework please log in to <a href=\"http://localhost:54335/\">Check</a>.<br><br>Thank you!<br>",
                studentName,
                homeworkId                
                );
            _sender.SendEmail(contact, subject, body);
        }
    }
}