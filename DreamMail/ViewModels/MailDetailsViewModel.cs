using System.Linq;
using DreamMail.Stores;
using MimeKit;
namespace DreamMail.ViewModels;

public class MailDetailsViewModel : ViewModelBase
{
    private NavigationStore _navigationStore;
    private readonly SelectedMailStore _selectedMailStore;

    public MailDetailsViewModel(SelectedMailStore selectedMailStore, NavigationStore navigationStore)
    {
        _selectedMailStore = selectedMailStore;
        _navigationStore = navigationStore;

        _selectedMailStore.SelectedMailChanged += OnSelectedMailChanged;
    }
    public MimeMessage SelectedMail => _selectedMailStore.SelectedMail;
    public bool HasSelectedMail => SelectedMail != null;
    public string SenderName => SelectedMail?.Sender.Name;
    public string Subject => SelectedMail?.Subject;
    public string Body => SelectedMail?.TextBody;
    public string Date => SelectedMail?.Date.ToString();
    public string ProfileImage { get; set; }
    public bool HasAttachments => SelectedMail?.Attachments.Any() ?? false;
    public string[] Attachments => SelectedMail?.Attachments.Select(a => a.ContentDisposition?.FileName ?? "No name").ToArray() ?? new string[0];

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
