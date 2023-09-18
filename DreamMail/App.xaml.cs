using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using DreamMail.Commands;
using DreamMail.Services;
using DreamMail.Stores;
using DreamMail.ViewModels;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MimeKit;

namespace DreamMail;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IMailTransport _smtpClient;
    private readonly IMailStore _imapClient;

    private readonly FoldersStore _foldersStore;
    private readonly MailsStore _mailsStore;
    private readonly NavigationStore _navigationStore;
    private readonly SelectedFolderStore _selectedFolderStore;
    private readonly SelectedMailStore _selectedMailStore;

    private MainViewModel _mainViewModel;
    private MailboxViewModel _mailboxViewModel;

    private readonly LoadMailsCommand _loadMailsCommand;
    private readonly LoadFoldersCommand _loadFoldersCommand;
    private readonly OpenNewMailCommand _openNewMailCommand;
    private readonly OpenMailDetailsCommand _openMailDetailsCommand;
    private readonly AuthenticateCommand _authenticateCommand;
    private readonly SwitchAccountCommand _switchAccountCommand;
    private readonly SendCommand _sendCommand;

    public App()
    {
        /*_imapClient = new FakeMailStore();*/
        _imapClient = new ImapClient();
        _smtpClient = new SmtpClient();
        _mailsStore = new MailsStore();
        _foldersStore = new FoldersStore(_imapClient);
        _selectedFolderStore = new SelectedFolderStore();
        _selectedMailStore = new SelectedMailStore(_mailsStore);
        _navigationStore = new NavigationStore();
        
        _authenticateCommand = new AuthenticateCommand(_imapClient, _smtpClient);
        _loadMailsCommand = new LoadMailsCommand(_mailsStore, _selectedFolderStore);
        _loadFoldersCommand = new LoadFoldersCommand(_foldersStore);
        _openNewMailCommand = new OpenNewMailCommand(_navigationStore, _smtpClient);
        _openMailDetailsCommand = new OpenMailDetailsCommand(_navigationStore, _selectedMailStore);
        _switchAccountCommand = new SwitchAccountCommand(_imapClient, _smtpClient, _authenticateCommand, _loadFoldersCommand, _loadMailsCommand);        
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        var authTask = _authenticateCommand.ExecuteAsync(null);

        _mailboxViewModel = new MailboxViewModel(_mailsStore, _foldersStore, _selectedFolderStore, _selectedMailStore,
            _navigationStore, _loadFoldersCommand, _loadMailsCommand, _openNewMailCommand, _openMailDetailsCommand, _authenticateCommand, _switchAccountCommand);

        _mainViewModel = new MainViewModel(_navigationStore, _mailboxViewModel)
        {
            MailboxViewModel = _mailboxViewModel
        };

        MainWindow = new MainWindow
        {
            DataContext = _mailboxViewModel
        };
        MainWindow.Show();

        base.OnStartup(e);

        await authTask;
        await _loadFoldersCommand.ExecuteAsync(null);
        await _loadMailsCommand.ExecuteAsync(null);
    }
}
