using DreamMail.Stores;
using MailKit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace DreamMail.ViewModels
{
    public class FoldersViewModel : BaseViewModel
    {
        private SelectedFolderStore _selectedFolderStore;
        private FoldersStore _foldersStore;

        private ObservableCollection<FolderItemViewModel> _folderItemViewModels;
        public IEnumerable<FolderItemViewModel> FolderItemViewModels => _folderItemViewModels;

        // public ICommand OpenFolder;

        public FolderItemViewModel SelectedFolderItemViewModel
        {
            get
            {
                return _folderItemViewModels
                    .FirstOrDefault(folder => folder.Folder?.Id == _selectedFolderStore.SelectedFolder?.Id);
            }
            set
            {
                _selectedFolderStore.SelectedFolder = value?.Folder;
            }
        }

        public FoldersViewModel(FoldersStore foldersStore, SelectedFolderStore selectedFolderStore)
        {
            _foldersStore = foldersStore;
            _selectedFolderStore = selectedFolderStore;
            _folderItemViewModels = new ObservableCollection<FolderItemViewModel>();

            _selectedFolderStore.SelectedFolderChanged += OnSelectedFolderChanged;

            _foldersStore.FoldersLoaded += OnFoldersLoaded;

            _folderItemViewModels.CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(SelectedFolderItemViewModel));
        }

        private void OnFoldersLoaded()
        {
            _folderItemViewModels.Clear();

            foreach (IMailFolder folder in _foldersStore.Folders)
            {
                _folderItemViewModels.Add(new FolderItemViewModel(folder));
            }
        }

        private void OnSelectedFolderChanged()
        {
            OnPropertyChanged(nameof(SelectedFolderItemViewModel));
        }
    }
}
