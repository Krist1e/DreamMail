using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DreamMail.Stores;
using MimeKit;
namespace DreamMail.ViewModels;

public class MailsViewModel : ViewModelBase
{
    private readonly ObservableCollection<MailItemViewModel> _mailItemViewModels;
    private readonly SelectedMailStore _selectedMailStore;
    private readonly MailsStore _mailsStore;
    
    private readonly ICommand _openMailDetailsCommand;

    public MailsViewModel(MailsStore mailsStore, SelectedMailStore selectedMailStore, ICommand openMailDetailsCommand)
    {
        _mailItemViewModels = new ObservableCollection<MailItemViewModel>();

        _openMailDetailsCommand = openMailDetailsCommand;
        _mailsStore = mailsStore;
        _selectedMailStore = selectedMailStore;
        _selectedMailStore.SelectedMailChanged += OnSelectedMailChanged;

        _mailsStore.MailAdded += OnMailAdded;
        _mailsStore.StartLoading += OnMailsStartLoading;
        _mailsStore.MailUpdated += OnMailUpdated;
    }
    public IEnumerable<MailItemViewModel> MailItemViewModels => _mailItemViewModels;

    public MailItemViewModel? SelectedMailItemViewModel
    {
        get
        {
            return _mailItemViewModels
                .FirstOrDefault(mail => mail.Mail?.MessageId == _selectedMailStore.SelectedMail?.MessageId);
        }
        set
        {
            _selectedMailStore.SelectedMail = value?.Mail;
            _openMailDetailsCommand.Execute(null);
        }
    }

    private void OnMailUpdated(MimeMessage mail)
    {
        MailItemViewModel? mailItemViewModel =
            _mailItemViewModels.FirstOrDefault(other => other.Mail?.MessageId == mail?.MessageId);

        mailItemViewModel?.Update(mail);
    }

    private void OnMailsStartLoading()
    {
        _mailItemViewModels.Clear();

        /*foreach (var mail in _mailsStore.Mails)
        {
            _mailItemViewModels.Add(new MailItemViewModel(mail));
        }*/
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
