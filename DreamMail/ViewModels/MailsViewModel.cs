using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamMail.ViewModels
{
    public class MailsViewModel : BaseViewModel
    {
        private readonly ObservableCollection<MailItemViewModel> _mailItemViewModels;
        public IEnumerable<MailItemViewModel> MailItemViewModels => _mailItemViewModels;

        public MailItemViewModel SelectedMailItemViewModel {
            get
            {
                return _mailItemViewModels
                    .FirstOrDefault(mail => mail == _selectedMailStore.SelectedMail ?);
            }
            set
            {
                _selectedMailStore.SelectedMail = value;
            }
        }

        public MailsViewModel()
        {            
        }
    }    
}
