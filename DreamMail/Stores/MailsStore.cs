using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using MailKit.Search;
using MimeKit;
namespace DreamMail.Stores;

public class MailsStore
{
    private readonly List<MimeMessage> _mails;

    public MailsStore()
    {
        _mails = new List<MimeMessage>();
    }
    public IEnumerable<MimeMessage> Mails => _mails;

    public event Action? MailsLoaded;
    public event Action<MimeMessage>? MailAdded;
    // public event Action<MimeMessage>? MailUpdated;
    public event Action<string>? MailDeleted;

    public async Task Load(IMailFolder folder)
    {
        List<MimeMessage>? mails = new();
        var message = new MimeMessage()
        {
            Subject = "Test",
            Body = new TextPart("plain") { Text = "Test" },
            Date = DateTime.Now,
            MessageId = "1",
            Sender = new MailboxAddress("Kris1", "kris.lex1e3@gmail.com"),
            To = { new MailboxAddress("Kris2", "kris_lexie@mail.ru"), new MailboxAddress("Kris3", "kris.com605@gmail.com") },
        };
        mails.Add(message);
        message = new MimeMessage()
        {
            Subject = "Test2",
            Body = new TextPart("plain") { Text = "Test" },
            Date = DateTime.Now,
            MessageId = "1",
            Sender = new MailboxAddress("Kris3", "kris.lex1e3@gmail.com"),
            To = { new MailboxAddress("Kris4", "kris_lexie@mail.ru"), new MailboxAddress("Kris3", "kris.com605@gmail.com") },
        };
        mails.Add(message);
        /*var uids = folder.Search(SearchQuery.HasGMailLabel(folder.ToString()));

        foreach (var uid in uids)
        {
            var message = await folder.GetMessageAsync(uid);
            mails.Add(message);
        }*/

        _mails.Clear();
        _mails.AddRange(mails);        
        // add mails to folder        
        MailsLoaded?.Invoke();
    }

    public async Task Add(MimeMessage mail)
    {
        // TODO: Add command for creating mail 
        // await _createMailCommand.Execute(mail);

        _mails.Add(mail);

        MailAdded?.Invoke(mail);
    }

    /*public async Task Update(MimeMessage mail)
    {
        // TODO: Add command for updating mail
        //await _updateMailCommand.Execute(mail);
        
        int currentIndex = _mails.FindIndex(other => other.MessageId == mail.MessageId);

        if (currentIndex != -1)
        {
            _mails[currentIndex] = mail;
        }
        else
        {
            _mails.Add(mail);
        }

        MailUpdated?.Invoke(mail);
    }*/

    public async Task Delete(string id)
    {
        // TODO: Add command for deleting mail
        // await _deleteMailCommand.Execute(id);

        _mails.RemoveAll(other => other.MessageId == id);

        MailDeleted?.Invoke(id);
    }
}
