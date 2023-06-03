using DreamMail.Stores;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamMail.ViewModels
{
    public class MailsViewModel : ViewModelBase
    {
        private SelectedMailStore _selectedMailStore;
                
        private readonly ObservableCollection<MailItemViewModel> _mailItemViewModels;
        public IEnumerable<MailItemViewModel> MailItemViewModels => _mailItemViewModels;

        public MailItemViewModel SelectedMailItemViewModel {
            get
            {
                return _mailItemViewModels
                    .FirstOrDefault(mail => mail.Mail?.MessageId == _selectedMailStore.SelectedMail?.MessageId);
            }
            set
            {
                _selectedMailStore.SelectedMail = value.Mail;
            }
        }

        public MailsViewModel(MailsStore mailsStore, SelectedMailStore selectedMailStore)
        {
            _mailItemViewModels = new ObservableCollection<MailItemViewModel>();

            _selectedMailStore = selectedMailStore;
            _selectedMailStore.SelectedMailChanged += OnSelectedMailChanged;

            mailsStore.MailAdded += OnMailAdded;
            // mailsStore.MailUpdated += OnMailUpdated;
        }
        
        /*private void OnMailUpdated(MimeMessage mail)
        {
            MailItemViewModel mailItemViewModel =
                _mailItemViewModels.FirstOrDefault(other => other.Mail?.MessageId == mail?.MessageId);

            if (mailItemViewModel != null)
            {
                mailItemViewModel.Update(mail);
            }
        }*/

        private void OnMailAdded(MimeMessage mail)
        {
            _mailItemViewModels.Add(new MailItemViewModel(mail));
        }

        private void OnSelectedMailChanged()
        {
            OnPropertyChanged(nameof(SelectedMailItemViewModel));
        }
    }    
}
