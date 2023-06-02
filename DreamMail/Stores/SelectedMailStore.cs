using MailKit;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamMail.Stores
{
    public class SelectedMailStore
    {
        private MailsStore _mailsStore;
        private MimeMessage _selectedMail;
        public MimeMessage SelectedMail
        {
            get
            {
                return _selectedMail;
            }
            set
            {
                _selectedMail = value;
                SelectedMailChanged?.Invoke();
            }
        }

        public event Action? SelectedMailChanged;

        public SelectedMailStore(MailsStore mailsStore)
        {
            _mailsStore = mailsStore;

            _mailsStore.MailAdded += OnMailAdded;
            _mailsStore.MailUpdated += OnMailUpdated;
        }
        
        private void OnMailAdded(MimeMessage mail)
        {
            SelectedMail = mail;
        }

        private void OnMailUpdated(MimeMessage mail)
        {
            if (mail.MessageId == SelectedMail?.MessageId)
            {
                SelectedMail = mail;
            }
        }
    }
}
