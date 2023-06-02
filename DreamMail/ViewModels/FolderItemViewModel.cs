using DreamMail.Stores;
using MailKit;

namespace DreamMail.ViewModels
{
    public class FolderItemViewModel : BaseViewModel
    {
        public IMailFolder Folder { get; private set; }
        public string FolderName => Folder.Name;
        //private readonly FoldersStore _foldersStore;

        public FolderItemViewModel(IMailFolder folder)
        {
            Folder = folder;
        }

        public void Update(IMailFolder folder)
        {
            Folder = folder;
            OnPropertyChanged(nameof(FolderName));
        }
    }
}