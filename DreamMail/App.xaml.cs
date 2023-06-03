using System.Windows;
using DreamMail.Stores;
using DreamMail.ViewModels;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
namespace DreamMail;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private FoldersStore _foldersStore;
    private IMailStore _imapClient;
    private MailboxViewModel _mailboxViewModel;
    private MailsStore _mailsStore;
    private NavigationStore _navigationStore;
    private SelectedFolderStore _selectedFolderStore;
    private SelectedMailStore _selectedMailStore;
    private IMailTransport _smtpClient;

    protected override void OnStartup(StartupEventArgs e)
    {
        _imapClient = new ImapClient();
        _smtpClient = new SmtpClient();
        _mailsStore = new MailsStore();
        _foldersStore = new FoldersStore(_imapClient);
        _selectedFolderStore = new SelectedFolderStore();
        _selectedMailStore = new SelectedMailStore(_mailsStore);
        _navigationStore = new NavigationStore();
        _mailboxViewModel = MailboxViewModel.LoadViewModel(_mailsStore, _foldersStore, _selectedFolderStore, _selectedMailStore, _navigationStore);

        MainWindow = new MainWindow
        {
            DataContext = _mailboxViewModel
        };
        MainWindow.Show();

        base.OnStartup(e);
    }
}
