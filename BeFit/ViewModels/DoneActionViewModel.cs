using System;
using BeFit.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeFit.ViewModels;

public sealed partial class DoneActionViewModel(Goal goal) : ViewModelBase
{
    [ObservableProperty]
    private bool done;

    [ObservableProperty]
    private string description = string.IsNullOrWhiteSpace(goal.Description)
        ? throw new ArgumentOutOfRangeException(nameof(goal))
        : goal.Description;

    public DateTime? When { get; set; }

    public Schedule Schedule { get; } = goal.Schedule;

    partial void OnDoneChanged(bool value) => When = value
            ? DateTime.Now
            : null;
}