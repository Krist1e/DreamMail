using MailKit;
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
        public MimeMessage Mail { get; private set; }
        public string SenderName => Mail.Sender.Name;
        public string Subject => Mail.Subject;
        public string BodyPart => Mail.TextBody;
        public string Date => Mail.Date.ToString();
        public string ProfileImage { get; set; }
        public bool HasAttachments => Mail.Attachments.Any();

        public MailItemViewModel(MimeMessage mail)
        {
            Mail = mail;
        }

        public void Update(MimeMessage mail)
        {
            Mail = mail;
            OnPropertyChanged(nameof(SenderName));
            OnPropertyChanged(nameof(Subject));
            OnPropertyChanged(nameof(BodyPart));
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(HasAttachments));            
        }

    }
}
