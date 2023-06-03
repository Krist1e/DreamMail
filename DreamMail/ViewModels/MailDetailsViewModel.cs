using DreamMail.Stores;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamMail.ViewModels
{
    public class MailDetailsViewModel : ViewModelBase
    {
        private SelectedMailStore _selectedMailStore;
        public MimeMessage SelectedMail => _selectedMailStore.SelectedMail;
        public bool HasSelectedMail => SelectedMail != null;
        public string SenderName => SelectedMail?.Sender.Name;
        public string Subject => SelectedMail?.Subject;
        public string Body => SelectedMail?.TextBody;
        public string Date => SelectedMail?.Date.ToString();
        public string ProfileImage { get; set; }
        public bool HasAttachments => SelectedMail?.Attachments.Any() ?? false;
        public string[] Attachments => SelectedMail?.Attachments.Select(a => a.ContentDisposition?.FileName ?? "No name").ToArray() ?? new string[0];

        public MailDetailsViewModel(SelectedMailStore selectedMailStore)
        {
            _selectedMailStore = selectedMailStore;

            _selectedMailStore.SelectedMailChanged += OnSelectedMailChanged;
        }

        private void OnSelectedMailChanged()
        {
            OnPropertyChanged(nameof(SelectedMail));
            OnPropertyChanged(nameof(HasSelectedMail));
            OnPropertyChanged(nameof(SenderName));
            OnPropertyChanged(nameof(Subject));
            OnPropertyChanged(nameof(Body));
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(HasAttachments));
            OnPropertyChanged(nameof(Attachments));
        }

    }
}
