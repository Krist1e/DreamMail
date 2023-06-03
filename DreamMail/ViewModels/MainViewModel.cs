using DreamMail.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamMail.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ViewModelBase? CurrentViewModel => _navigationStore.CurrentViewModel;
        public bool IsOpen => _navigationStore.IsOpen;

        public MailsViewModel MailsViewModel { get; }

        public MainViewModel(NavigationStore navigationStore, MailsViewModel mailsViewModel)
        {
            _navigationStore = navigationStore;
            MailsViewModel = mailsViewModel;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        protected override void Dispose()
        {
            _navigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;

            base.Dispose();
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
            OnPropertyChanged(nameof(IsOpen));
        }

    }
}
