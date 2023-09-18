using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using MailKit.Search;
using MimeKit;
namespace DreamMail.Stores;

public class MailsStore
{
    private readonly List<MimeMessage> _mails;

    private readonly object _lock = new();
    private bool _isLoading;
    private Task? _loadingTask;

    public MailsStore()
    {
        _mails = new List<MimeMessage>();
    }
    public IEnumerable<MimeMessage> Mails => _mails;

    public event Action? StartLoading;
    public event Action? EndLoading;
    public event Action<MimeMessage>? MailAdded;
    public event Action<MimeMessage>? MailUpdated;
    public event Action<string>? MailDeleted;

    public async Task Load(IMailFolder folder)
    {
        if (_isLoading)
        {
            _isLoading = false;
            await _loadingTask!;
        }

        _isLoading = true;
        _mails.Clear();
        _loadingTask = LoadMails(folder);
        await _loadingTask;
    }

    private async Task LoadMails(IMailFolder folder)
    {
        await folder.OpenAsync(FolderAccess.ReadOnly);
        var uids = await folder.SearchAsync(SearchQuery.All);         
        StartLoading?.Invoke();
        var descendingUids = uids.OrderByDescending(uid => uid);
        foreach (var uid in descendingUids)
        {
            if (!_isLoading)
                break;
            var message = await folder.GetMessageAsync(uid);
            _mails.Add(message);
            MailAdded?.Invoke(message);
        }
        EndLoading?.Invoke();
        await folder.CloseAsync();
        _isLoading = false;
    }

    public async Task Add(MimeMessage mail)
    {
        // TODO: Add command for creating mail 
        // await _createMailCommand.Execute(mail);

        _mails.Add(mail);

        MailAdded?.Invoke(mail);
    }

    public async Task Update(MimeMessage mail)
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
    }

    public async Task Delete(string id)
    {
        // TODO: Add command for deleting mail
        // await _deleteMailCommand.Execute(id);

        _mails.RemoveAll(other => other.MessageId == id);

        MailDeleted?.Invoke(id);
    }
}
