using MailKit;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamMail.Stores
{
    public class SelectedMailStore
    {
        private MimeMessage _selectedMail;
        public MimeMessage SelectedMail
        {
            get
            {
                return _selectedMail;
            }
            set
            {
                _selectedMail = value;
                SelectedMailChanged?.Invoke();
            }
        }

        public event Action? SelectedMailChanged;
    }
}
