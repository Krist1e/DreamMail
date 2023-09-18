﻿using System;
using DreamMail.ViewModels;
namespace DreamMail.Stores;

public class NavigationStore
{
    private ViewModelBase? _currentViewModel;
    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel?.Dispose();
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
