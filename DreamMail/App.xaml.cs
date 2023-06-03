using DreamMail.Stores;
using DreamMail.ViewModels;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DreamMail
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MailsStore _mailsStore;
        private FoldersStore _foldersStore;
        private SelectedFolderStore _selectedFolderStore;
        private SelectedMailStore _selectedMailStore;
        private NavigationStore _navigationStore;
        private IMailStore _imapClient;
        private IMailTransport _smtpClient;
        private MailboxViewModel _mailboxViewModel;
        
        public App()
        {
            MainWindow = new MainWindow { DataContext = _mailboxViewModel };
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            _imapClient = new ImapClient();
            _smtpClient = new SmtpClient();
            _mailsStore = new();
            _foldersStore = new(_imapClient);
            _selectedFolderStore = new();
            _selectedMailStore = new(_mailsStore);
            _navigationStore = new NavigationStore();
            _mailboxViewModel = MailboxViewModel.LoadViewModel(_mailsStore, _foldersStore, _selectedFolderStore, _selectedMailStore, _navigationStore);
            base.OnStartup(e);
        }
    }
}
