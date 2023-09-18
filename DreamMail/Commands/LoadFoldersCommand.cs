using System;
using System.Threading.Tasks;
using DreamMail.Stores;
namespace DreamMail.Commands;

public class LoadFoldersCommand : AsyncCommandBase
{
    private readonly FoldersStore _foldersStore;

    public LoadFoldersCommand(FoldersStore foldersStore)
    {
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
