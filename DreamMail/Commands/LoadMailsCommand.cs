using System;
using System.Threading.Tasks;
using DreamMail.Stores;
using DreamMail.ViewModels;
using MailKit;

namespace DreamMail.Commands;

public class LoadMailsCommand : AsyncCommandBase
{
    private readonly MailsStore _mailsStore;
    private readonly MailsViewModel _mailsViewModel;
    private readonly SelectedFolderStore _selectedFolderStore;

    public LoadMailsCommand(MailsViewModel mailsViewModel, MailsStore mailsStore, SelectedFolderStore selectedFolderStore)
    {
        _mailsViewModel = mailsViewModel;
        _mailsStore = mailsStore;
        _selectedFolderStore = selectedFolderStore;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        try
        {
            await _mailsStore.Load(parameter as IMailFolder);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
