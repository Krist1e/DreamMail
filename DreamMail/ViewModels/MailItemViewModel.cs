using System;
using System.Linq;
using MimeKit;
namespace DreamMail.ViewModels;

public class MailItemViewModel : ViewModelBase
{

    public MailItemViewModel(MimeMessage mail)
    {
        Mail = mail;
    }
    public MimeMessage Mail { get; private set; }

    public string Id => Mail.MessageId;
    public string SenderName => Mail.Sender.Name;
    public string Subject => Mail.Subject;
    public string? BodyPart => Mail.TextBody?.Substring(0, Math.Min(100, Mail.TextBody.Length));    
    public string Date => Mail.Date.ToString("dd.MM.yyyy HH:mm");
    public string ProfileImage { get; set; }
    public bool HasAttachments => Mail.Attachments.Any();

    public void Update(MimeMessage mail)
    {
        Mail = mail;
        OnPropertyChanged(nameof(Id));
        OnPropertyChanged(nameof(SenderName));
        OnPropertyChanged(nameof(Subject));
        OnPropertyChanged(nameof(BodyPart));
        OnPropertyChanged(nameof(Date));
        OnPropertyChanged(nameof(HasAttachments));
    }
}
