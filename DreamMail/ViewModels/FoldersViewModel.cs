using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DreamMail.ViewModels
{
    public class FoldersViewModel : BaseViewModel
    {
        private ObservableCollection<FolderItemViewModel> _folderItemViewModels;
        public IEnumerable<FolderItemViewModel> FolderItemViewModels { get; }
        public FolderItemViewModel SelectedFolderItemViewModel { get; }

        public ICommand OpenFolder;

        public FoldersViewModel(string folderName)
        {
            FolderName = folderName;
        }
    }
}
