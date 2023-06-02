using MailKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamMail.Stores
{
    public class SelectedFolderStore
    {             
        private IMailFolder _selectedFolder;
        public IMailFolder SelectedFolder
        {
            get
            {
                return _selectedFolder;
            }
            set
            {
                _selectedFolder = value;
                SelectedFolderChanged?.Invoke();
            }
        }

        public event Action? SelectedFolderChanged;
    }
}
