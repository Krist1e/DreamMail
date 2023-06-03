using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DreamMail.ViewModels
{
    public class NewMailViewModel : ViewModelBase
    {
        private string _sender;
        public string Sender
        {
            get
            {
                return _sender;
            }
            set
            {
                _sender = value;
                OnPropertyChanged(nameof(Sender));
            }
        }

        private string _recipient;
        public string Recipient
        {
            get
            {
                return _recipient;
            }
            set
            {
                _recipient = value;
                OnPropertyChanged(nameof(Recipient));
            }
        }

        private string _subject;
        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
                OnPropertyChanged(nameof(Subject));
            }
        }

        private string _body;
        public string Body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
                OnPropertyChanged(nameof(Body));
            }
        }

        private string[] _attachments;
        public string[] Attachments
        {
            get
            {
                return _attachments;
            }
            set
            {
                _attachments = value;
                OnPropertyChanged(nameof(Attachments));
            }
        }
        public bool CanSend { get; set; }

        public ICommand SendMailCommand;
        public ICommand DiscardMailCommand;
        public ICommand AddAttachments;

        public NewMailViewModel(ICommand sendMailCommand, ICommand discardMailCommand, ICommand addAttachments)
        {
            SendMailCommand = sendMailCommand;
            DiscardMailCommand = discardMailCommand;
            AddAttachments = addAttachments;
        }
    }
}
