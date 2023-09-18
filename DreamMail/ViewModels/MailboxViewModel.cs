using System.Windows.Input;
using DreamMail.Commands;
using DreamMail.Stores;
namespace DreamMail.ViewModels;

public class MailboxViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

    public readonly ICommand LoadFoldersCommand;
    public readonly ICommand LoadMailsCommand;
    public readonly ICommand AuthenticateCommand;
    public ICommand SwitchAccountCommand { get; }
    public ICommand OpenNewMailCommand { get; }

    public MailboxViewModel(MailsStore mailsStore, FoldersStore foldersStore, SelectedFolderStore selectedFolderStore, 
        SelectedMailStore selectedMailStore, NavigationStore navigationStore,
        ICommand loadFoldersCommand, ICommand loadMailsCommand, ICommand openNewMailCommand, ICommand openMailDetailsCommand, ICommand authenticateCommand, ICommand switchAccountCommand)
    {
        _navigationStore = navigationStore;
        LoadFoldersCommand = loadFoldersCommand;
        LoadMailsCommand = loadMailsCommand;
        OpenNewMailCommand = openNewMailCommand;
        AuthenticateCommand = authenticateCommand;
        SwitchAccountCommand = switchAccountCommand;

        FoldersViewModel = new FoldersViewModel(foldersStore, selectedFolderStore, loadMailsCommand);
        MailsViewModel = new MailsViewModel(mailsStore, selectedMailStore, openMailDetailsCommand);

        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }
    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
    public FoldersViewModel FoldersViewModel { get; }
    public MailsViewModel MailsViewModel { get; }
    public static MailboxViewModel LoadViewModel(MailsStore mailsStore, FoldersStore foldersStore, 
        SelectedFolderStore selectedFolderStore, SelectedMailStore selectedMailStore, NavigationStore navigationStore,
        ICommand loadFoldersCommand, ICommand loadMailsCommand, ICommand openNewMailCommand, ICommand openMailDetailsCommand, ICommand authenticateCommand, ICommand switchAccountCommand)
    {
        MailboxViewModel mailboxViewModel = new(mailsStore, foldersStore, selectedFolderStore, selectedMailStore,
            navigationStore, loadFoldersCommand, loadMailsCommand, openNewMailCommand, openMailDetailsCommand, authenticateCommand, switchAccountCommand);
        mailboxViewModel.AuthenticateCommand.Execute(null);
        mailboxViewModel.LoadFoldersCommand.Execute(null);      
        mailboxViewModel.LoadMailsCommand.Execute(null);        
        return mailboxViewModel;
    }
}