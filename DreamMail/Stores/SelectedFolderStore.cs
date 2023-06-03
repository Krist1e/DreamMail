using System;
using MailKit;
namespace DreamMail.Stores;

public class SelectedFolderStore
{
    private IMailFolder _selectedFolder;
    public IMailFolder SelectedFolder
    {
        get => _selectedFolder;
        set
        {
            _selectedFolder = value;
            SelectedFolderChanged?.Invoke();
        }
    }

    public event Action? SelectedFolderChanged;
}
