using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamMail.ViewModels
{
    public class MailItemViewModel : BaseViewModel
    {
        public string SenderName { get; set; }
        public string Subject { get; set; }
        public string BodyPart { get; set; }
        public string Date { get; set; }
        public string ProfileImage { get; set; }      
        public bool HasAttachments { get; set; }

        public MailItemViewModel(MimeMessage mail)
        {
            SenderName = mail.From.Mailboxes.FirstOrDefault()?.Name;
            Subject = mail.Subject;
            BodyPart = mail.TextBody;
            Date = mail.Date.ToString();
            ProfileImage = "https://picsum.photos/50/50";
            HasAttachments = mail.Attachments.Any();
            
        }

    }
}
