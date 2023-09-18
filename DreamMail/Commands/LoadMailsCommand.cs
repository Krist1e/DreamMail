using System;
using System.Threading.Tasks;
using DreamMail.Stores;
using MailKit;

namespace DreamMail.Commands;

public class LoadMailsCommand : AsyncCommandBase
{
    private readonly MailsStore _mailsStore;
    private readonly SelectedFolderStore _selectedFolderStore;

    public LoadMailsCommand(MailsStore mailsStore, SelectedFolderStore selectedFolderStore)
    {
        _mailsStore = mailsStore;
        _selectedFolderStore = selectedFolderStore;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        var folder = _selectedFolderStore.SelectedFolder;
        if (folder == null)
            return;

        await _mailsStore.Load(folder);
    }


}
