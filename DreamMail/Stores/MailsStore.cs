
using MailKit;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MimeKit;
using MailKit.Search;

namespace DreamMail.Stores
{
    public class MailsStore
    {
        private readonly List<MimeMessage> _mails;
        public IEnumerable<MimeMessage> Mails => _mails;

        public event Action? MailsLoaded;
        public event Action<MimeMessage>? MailAdded;
        // public event Action<MimeMessage>? MailUpdated;
        public event Action<string>? MailDeleted;


        public MailsStore()
        {
            _mails = new List<MimeMessage>();
        }
        
        public async Task Load(IMailFolder folder)
        {
            List<MimeMessage>? mails = new();
            
            IList<UniqueId> uids = folder.Search(SearchQuery.All);
            foreach (var uid in uids)
            {
                MimeMessage message = await folder.GetMessageAsync(uid);
                mails.Add(message);
            }                            

            _mails.Clear();
            _mails.AddRange(mails);

            MailsLoaded?.Invoke();
        }

        public async Task Add(MimeMessage mail)
        {
            // TODO: Add command for creating mail 
            // await _createMailCommand.Execute(mail);

            _mails.Add(mail);

            MailAdded?.Invoke(mail);
        }

        /*public async Task Update(MimeMessage mail)
        {
            // TODO: Add command for updating mail
            //await _updateMailCommand.Execute(mail);
            
            int currentIndex = _mails.FindIndex(other => other.MessageId == mail.MessageId);

            if (currentIndex != -1)
            {
                _mails[currentIndex] = mail;
            }
            else
            {
                _mails.Add(mail);
            }

            MailUpdated?.Invoke(mail);
        }*/

        public async Task Delete(string id)
        {
            // TODO: Add command for deleting mail
            // await _deleteMailCommand.Execute(id);

            _mails.RemoveAll(other => other.MessageId == id);

            MailDeleted?.Invoke(id);
        }
    }
}
