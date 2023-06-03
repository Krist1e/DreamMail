using DreamMail.Stores;
using DreamMail.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamMail.Commands
{
    public class LoadFoldersCommand : AsyncCommandBase
    {
        private readonly FoldersViewModel _foldersViewModel;
        private readonly FoldersStore _foldersStore;

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
}
