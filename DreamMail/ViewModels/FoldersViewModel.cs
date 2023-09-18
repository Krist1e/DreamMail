using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using DreamMail.Commands;
using DreamMail.Stores;
namespace DreamMail.ViewModels;

public class FoldersViewModel : ViewModelBase
{

    private readonly ObservableCollection<FolderItemViewModel> _folderItemViewModels;
    private readonly FoldersStore _foldersStore;
    private readonly SelectedFolderStore _selectedFolderStore;

    public FoldersViewModel(FoldersStore foldersStore, SelectedFolderStore selectedFolderStore, ICommand loadMailsCommand)
    {
        LoadMailsCommand = loadMailsCommand;
        _foldersStore = foldersStore;
        _selectedFolderStore = selectedFolderStore;
        _folderItemViewModels = new ObservableCollection<FolderItemViewModel>();

        _selectedFolderStore.SelectedFolderChanged += OnSelectedFolderChanged;

        _foldersStore.FoldersLoaded += OnFoldersLoaded;

        _folderItemViewModels.CollectionChanged += OnCollectionChanged;
    }
    public IEnumerable<FolderItemViewModel> FolderItemViewModels => _folderItemViewModels;

    // public ICommand OpenFolder;
    public ICommand LoadMailsCommand;

    public FolderItemViewModel? SelectedFolderItemViewModel
    {
        get
        {
            return _folderItemViewModels
                .FirstOrDefault(folder => folder.Folder?.Id == _selectedFolderStore.SelectedFolder?.Id);
        }
        set
        {
            _selectedFolderStore.SelectedFolder = value?.Folder;
            LoadMailsCommand.Execute(null);
        }
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
        SelectedFolderItemViewModel = _folderItemViewModels[0] ?? null;
    }

    private void OnSelectedFolderChanged()
    {
        OnPropertyChanged(nameof(SelectedFolderItemViewModel));
    }
}
