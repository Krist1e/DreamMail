using System.Windows.Input;
namespace DreamMail.ViewModels;

public class NewMailViewModel : ViewModelBase
{

    private string[] _attachments;

    private string _body;

    private string _recipient;
    private string _sender;

    private string _subject;
    public ICommand AddAttachments;
    public ICommand DiscardMailCommand;

    public ICommand SendMailCommand;

    public NewMailViewModel(ICommand sendMailCommand, ICommand discardMailCommand, ICommand addAttachments)
    {
        SendMailCommand = sendMailCommand;
        DiscardMailCommand = discardMailCommand;
        AddAttachments = addAttachments;
    }
    public string Sender
    {
        get => _sender;
        set
        {
            _sender = value;
            OnPropertyChanged(nameof(Sender));
        }
    }
    public string Recipient
    {
        get => _recipient;
        set
        {
            _recipient = value;
            OnPropertyChanged(nameof(Recipient));
        }
    }
    public string Subject
    {
        get => _subject;
        set
        {
            _subject = value;
            OnPropertyChanged(nameof(Subject));
        }
    }
    public string Body
    {
        get => _body;
        set
        {
            _body = value;
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
    public bool CanSend { get; set; }
}
