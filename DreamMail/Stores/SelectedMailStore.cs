using System;
using MimeKit;
namespace DreamMail.Stores;

public class SelectedMailStore
{
    private readonly MailsStore _mailsStore;
    private MimeMessage _selectedMail;

    public SelectedMailStore(MailsStore mailsStore)
    {
        _mailsStore = mailsStore;

        //_mailsStore.MailAdded += OnMailAdded;
        _mailsStore.MailUpdated += OnMailUpdated;
    }
    public MimeMessage SelectedMail
    {
        get => _selectedMail;
        set
        {
            _selectedMail = value;
            SelectedMailChanged?.Invoke();
        }
    }

    public event Action? SelectedMailChanged;

    /*private void OnMailAdded(MimeMessage mail)
    {
        SelectedMail = mail;
    }*/

    private void OnMailUpdated(MimeMessage mail)
    {
        if (mail.MessageId == SelectedMail?.MessageId)
        {
            SelectedMail = mail;
        }
    }
}
