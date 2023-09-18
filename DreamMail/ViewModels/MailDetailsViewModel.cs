using System.Linq;
using DreamMail.Stores;
using MimeKit;
namespace DreamMail.ViewModels;

public class MailDetailsViewModel : ViewModelBase
{
    private readonly SelectedMailStore _selectedMailStore;

    public MailDetailsViewModel(SelectedMailStore selectedMailStore)
    {
        _selectedMailStore = selectedMailStore;

        _selectedMailStore.SelectedMailChanged += OnSelectedMailChanged;
    }
    public MimeMessage SelectedMail => _selectedMailStore.SelectedMail;
    public bool HasSelectedMail => SelectedMail != null;
    public string? SenderName => SelectedMail?.From.ToString();
    public string? Subject => SelectedMail?.Subject;
    public string? Body => SelectedMail?.TextBody;
    public string? Date => SelectedMail?.Date.ToString("dd.MM.yyyy HH:mm");
    public string? Recipients => SelectedMail?.To.ToString();
    public bool HasAttachments => SelectedMail?.Attachments.Any() ?? false;
    public string? Attachments => SelectedMail?.Attachments.ToString();
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
        OnPropertyChanged(nameof(Recipients));
        
    }
}
