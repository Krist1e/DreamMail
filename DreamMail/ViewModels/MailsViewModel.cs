using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DreamMail.Stores;
using MimeKit;
namespace DreamMail.ViewModels;

public class MailsViewModel : ViewModelBase
{

    private readonly ObservableCollection<MailItemViewModel> _mailItemViewModels;
    private readonly SelectedMailStore _selectedMailStore;

    public MailsViewModel(MailsStore mailsStore, SelectedMailStore selectedMailStore)
    {
        _mailItemViewModels = new ObservableCollection<MailItemViewModel>();

        _selectedMailStore = selectedMailStore;
        _selectedMailStore.SelectedMailChanged += OnSelectedMailChanged;

        mailsStore.MailAdded += OnMailAdded;
        // mailsStore.MailUpdated += OnMailUpdated;
    }
    public IEnumerable<MailItemViewModel> MailItemViewModels => _mailItemViewModels;

    public MailItemViewModel SelectedMailItemViewModel
    {
        get
        {
            return _mailItemViewModels
                .FirstOrDefault(mail => mail.Mail?.MessageId == _selectedMailStore.SelectedMail?.MessageId);
        }
        set => _selectedMailStore.SelectedMail = value.Mail;
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
