using System;
using System.Threading.Tasks;
using DreamMail.Stores;
using DreamMail.ViewModels;
namespace DreamMail.Commands;

public class LoadFoldersCommand : AsyncCommandBase
{
    private readonly FoldersStore _foldersStore;
    private readonly FoldersViewModel _foldersViewModel;

    public LoadFoldersCommand(FoldersViewModel foldersViewModel, FoldersStore foldersStore)
    {
        _foldersViewModel = foldersViewModel;
        _foldersStore = foldersStore;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        try
        {
            await _foldersStore.Load();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
