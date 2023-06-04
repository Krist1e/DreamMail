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
    private readonly MailsStore _mailsStore;

    public MailsViewModel(MailsStore mailsStore, SelectedMailStore selectedMailStore)
    {
        _mailItemViewModels = new ObservableCollection<MailItemViewModel>();

        _mailsStore = mailsStore;
        _selectedMailStore = selectedMailStore;
        _selectedMailStore.SelectedMailChanged += OnSelectedMailChanged;

        _mailsStore.MailAdded += OnMailAdded;
        _mailsStore.MailsLoaded += OnMailsLoaded;
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

    private void OnMailsLoaded()
    {
        _mailItemViewModels.Clear();

        foreach (var mail in _mailsStore.Mails)
        {
            _mailItemViewModels.Add(new MailItemViewModel(mail));
        }
    }

    private void OnMailAdded(MimeMessage mail)
    {
        _mailItemViewModels.Add(new MailItemViewModel(mail));
    }

    private void OnSelectedMailChanged()
    {
        OnPropertyChanged(nameof(SelectedMailItemViewModel));
    }
}
