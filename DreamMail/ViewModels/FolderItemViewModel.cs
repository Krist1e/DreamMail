using MailKit;
namespace DreamMail.ViewModels;

public class FolderItemViewModel : ViewModelBase
{
    //private readonly FoldersStore _foldersStore;

    public FolderItemViewModel(IMailFolder folder)
    {
        Folder = folder;
    }
    public IMailFolder Folder { get; private set; }
    public string FolderName => Folder.Name;

    public void Update(IMailFolder folder)
    {
        Folder = folder;
        OnPropertyChanged(nameof(FolderName));
    }
}
