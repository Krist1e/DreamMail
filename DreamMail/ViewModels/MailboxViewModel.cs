using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DreamMail.ViewModels
{
    public class MailboxViewModel : ViewModelBase
    {
        public FoldersViewModel FoldersViewModel { get; }
        public MailsViewModel MailsViewModel { get; }
        public MailDetailsViewModel MailDetailsViewModel { get; }

        public ICommand LoadFoldersCommand;
        public ICommand LoadMailsCommand;




    }
}
