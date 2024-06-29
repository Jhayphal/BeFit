using System;

using CommunityToolkit.Mvvm.ComponentModel;

namespace BeFit.ViewModels;

public partial class DoneActionViewModel(string description) : ViewModelBase
{
    [ObservableProperty]
    private bool done;

    [ObservableProperty]
    private string description = string.IsNullOrWhiteSpace(description)
        ? throw new ArgumentOutOfRangeException(nameof(description))
        : description;

    public DateTime? When { get; set; }

    partial void OnDoneChanged(bool value) => When = value
            ? DateTime.Now
            : null;
}