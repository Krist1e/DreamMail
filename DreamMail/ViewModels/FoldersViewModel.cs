using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using DreamMail.Stores;
namespace DreamMail.ViewModels;

public class FoldersViewModel : ViewModelBase
{

    private readonly ObservableCollection<FolderItemViewModel> _folderItemViewModels;
    private readonly FoldersStore _foldersStore;
    private readonly SelectedFolderStore _selectedFolderStore;

    public FoldersViewModel(FoldersStore foldersStore, SelectedFolderStore selectedFolderStore)
    {
        _foldersStore = foldersStore;
        _selectedFolderStore = selectedFolderStore;
        _folderItemViewModels = new ObservableCollection<FolderItemViewModel>();

        _selectedFolderStore.SelectedFolderChanged += OnSelectedFolderChanged;

        _foldersStore.FoldersLoaded += OnFoldersLoaded;

        _folderItemViewModels.CollectionChanged += OnCollectionChanged;
    }
    public IEnumerable<FolderItemViewModel> FolderItemViewModels => _folderItemViewModels;

    // public ICommand OpenFolder;

    public FolderItemViewModel SelectedFolderItemViewModel
    {
        get
        {
            return _folderItemViewModels
                .FirstOrDefault(folder => folder.Folder?.Id == _selectedFolderStore.SelectedFolder?.Id);
        }
        set => _selectedFolderStore.SelectedFolder = value?.Folder;
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(SelectedFolderItemViewModel));
    }

    private void OnFoldersLoaded()
    {
        _folderItemViewModels.Clear();

        foreach (var folder in _foldersStore.Folders)
        {
            _folderItemViewModels.Add(new FolderItemViewModel(folder));
        }
    }

    private void OnSelectedFolderChanged()
    {
        OnPropertyChanged(nameof(SelectedFolderItemViewModel));
    }
}
