using System.Windows.Input;
using DreamMail.Commands;
using DreamMail.Stores;
using MailKit;

namespace DreamMail.ViewModels;

public class MailboxViewModel : ViewModelBase
{

    public ICommand LoadFoldersCommand;
    public ICommand LoadMailsCommand;

    public MailboxViewModel(MailsStore mailsStore, FoldersStore foldersStore, SelectedFolderStore selectedFolderStore, SelectedMailStore selectedMailStore, NavigationStore navigationStore)
    {
        FoldersViewModel = new FoldersViewModel(foldersStore, selectedFolderStore);
        MailsViewModel = new MailsViewModel(mailsStore, selectedMailStore);
        MailDetailsViewModel = new MailDetailsViewModel(selectedMailStore, navigationStore);

        LoadFoldersCommand = new LoadFoldersCommand(FoldersViewModel, foldersStore);
        LoadMailsCommand = new LoadMailsCommand(MailsViewModel, mailsStore, selectedFolderStore);
    }
    public FoldersViewModel FoldersViewModel { get; }
    public MailsViewModel MailsViewModel { get; }
    public MailDetailsViewModel MailDetailsViewModel { get; }
    public static MailboxViewModel LoadViewModel(MailsStore mailsStore, FoldersStore foldersStore, SelectedFolderStore selectedFolderStore, SelectedMailStore selectedMailStore, NavigationStore navigationStore)
    {
        MailboxViewModel mailboxViewModel = new(mailsStore, foldersStore, selectedFolderStore, selectedMailStore, navigationStore);
        mailboxViewModel.LoadFoldersCommand.Execute(null);      
        mailboxViewModel.LoadMailsCommand.Execute(null);
        return mailboxViewModel;
    }
}
