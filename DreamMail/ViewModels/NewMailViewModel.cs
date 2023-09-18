using MimeKit;
using MimeKit.Utils;
using System;
using System.Linq;
using System.Windows.Input;
using DreamMail.Commands;
using MailKit;
namespace DreamMail.ViewModels;

public class NewMailViewModel : ViewModelBase
{
    private string[] _attachments = Array.Empty<string>();
    private string _body = string.Empty;
    private string _recipients = string.Empty;
    private string _sender = string.Empty;
    private string _subject = string.Empty;

    public ICommand SendCommand { get; }

    public NewMailViewModel(IMailTransport smtpClient)
    {
        Mail.Date = DateTimeOffset.Now;
        Mail.MessageId = MimeUtils.GenerateMessageId();
        SendCommand = new SendCommand(smtpClient, this);
    }

    public MimeMessage Mail { get; } = new();

    public string Sender
    {
        get => _sender;
        set
        {
            _sender = value;
            Mail.Sender = MailboxAddress.Parse(_sender);
            OnPropertyChanged(nameof(Sender));
        }
    }
    public string Recipients
    {
        get => _recipients;
        set
        {
            _recipients = value;
            Mail.To.AddRange(_recipients.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(MailboxAddress.Parse));
            OnPropertyChanged(nameof(Recipients));
        }
    }
    public string Subject
    {
        get => _subject;
        set
        {
            _subject = value;
            Mail.Subject = _subject;
            OnPropertyChanged(nameof(Subject));
        }
    }
    public string Body
    {
        get => _body;
        set
        {
            _body = value;
            Mail.Body = new TextPart("plain")
            {
                Text = _body
            };
            OnPropertyChanged(nameof(Body));
        }
    }
    public string[] Attachments
    {
        get => _attachments;
        set
        {
            _attachments = value;
            OnPropertyChanged(nameof(Attachments));
        }
    }
    public bool CanSend
    {
        get => !string.IsNullOrWhiteSpace(_sender) && _recipients.Any() && !string.IsNullOrWhiteSpace(_subject) && !string.IsNullOrWhiteSpace(_body);
    }

}