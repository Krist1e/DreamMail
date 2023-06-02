using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamMail.ViewModels
{
    public class MailboxViewModel : BaseViewModel
    {
        public FoldersViewModel FoldersViewModel { get; }
        public MailsViewModel MailsViewModel { get; }
        public MailDetailsViewModel MailDetailsViewModel { get; }
    }
}
