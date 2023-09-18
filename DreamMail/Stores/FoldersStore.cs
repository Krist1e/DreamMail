using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MailKit;

namespace DreamMail.Stores;

public class FoldersStore
{
    private readonly List<IMailFolder> _folders;
    private IMailStore _client;

    public FoldersStore(IMailStore client)
    {
        _client = client;
        _folders = new List<IMailFolder>();
    }
    public IEnumerable<IMailFolder> Folders => _folders;

    public event Action? FoldersLoaded;

    public async Task Load()
    {
        IEnumerable<IMailFolder>? folders = await _client.GetFoldersAsync(_client.PersonalNamespaces[0]);
        _folders.Clear();
        foreach (var folder in folders)
        {
            if (folder.Name != "[Gmail]")
            {
                _folders.Add(folder);
            }
        }

        FoldersLoaded?.Invoke();
    }
}