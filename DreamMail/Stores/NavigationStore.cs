﻿using DreamMail.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamMail.Stores
{
    public class NavigationStore
    {
        private ViewModelBase? _currentViewModel;
        public ViewModelBase? CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }
        public bool IsOpen => CurrentViewModel != null;

        public event Action? CurrentViewModelChanged;

        public void Close()
        {
            CurrentViewModel = null;
        }
    }
}
