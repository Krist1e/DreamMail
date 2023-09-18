using DreamMail.Stores;
using DreamMail.ViewModels;
using MailKit;
namespace DreamMail.Commands;

public class OpenNewMailCommand : CommandBase
{
    private readonly NavigationStore _navigationStore;
    private readonly IMailTransport _smtpClient;

    public OpenNewMailCommand(NavigationStore navigationStore, IMailTransport smtpClient)
    {
        _navigationStore = navigationStore;
        _smtpClient = smtpClient;
    }

    public override void Execute(object parameter)
    {

        var newMailViewModel = new NewMailViewModel(_smtpClient);
        _navigationStore.CurrentViewModel = newMailViewModel;
    }
}
