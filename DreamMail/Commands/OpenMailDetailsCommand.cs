using DreamMail.Stores;
using DreamMail.ViewModels;
namespace DreamMail.Commands;

public class OpenMailDetailsCommand : CommandBase
{
    private readonly NavigationStore _navigationStore;
    private readonly SelectedMailStore _selectedFolderStore;
    
    public OpenMailDetailsCommand(NavigationStore navigationStore, SelectedMailStore selectedFolderStore)
    {
        _navigationStore = navigationStore;
        _selectedFolderStore = selectedFolderStore;
    }

    public override void Execute(object parameter)
    {
        var messageDetailsViewModel = new MailDetailsViewModel(_selectedFolderStore);
        _navigationStore.CurrentViewModel = messageDetailsViewModel;
    }
}
