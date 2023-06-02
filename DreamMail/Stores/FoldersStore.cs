using MailKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamMail.Stores
{
    public class FoldersStore
    {
        private IMailStore _client;
        private readonly List<IMailFolder> _folders;
        public IEnumerable<IMailFolder> Folders => _folders;

        public event Action? FoldersLoaded;

        public FoldersStore(IMailStore client)
        {
            _client = client;
            _folders = new List<IMailFolder>();
        }
        
        public async Task Load()
        {                 
            IEnumerable<IMailFolder>? folders = await _client.GetFoldersAsync(_client.PersonalNamespaces[0]);

            _folders.Clear();
            _folders.AddRange(folders);

            FoldersLoaded?.Invoke();
        }
    }
}
