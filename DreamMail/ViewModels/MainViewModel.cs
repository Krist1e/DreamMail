using DreamMail.Stores;
namespace DreamMail.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;

    public MainViewModel(NavigationStore navigationStore, MailboxViewModel mailboxViewModel)
    {
        _navigationStore = navigationStore;
        MailboxViewModel = mailboxViewModel;

        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }
    public ViewModelBase? CurrentViewModel => _navigationStore.CurrentViewModel;
    public bool IsOpen => _navigationStore.IsOpen;

    public MailboxViewModel MailboxViewModel { get; }

    protected override void Dispose()
    {
        _navigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;

        base.Dispose();
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
        OnPropertyChanged(nameof(IsOpen));
    }
}
