using System;
using System.Collections.Generic;

using CommunityToolkit.Mvvm.ComponentModel;

namespace BeFit.ViewModels;

public sealed record class Schedule(int Id, IEnumerable<DayOfWeek> Days);

public record struct Goal(int Id, string Description, Schedule Schedule);

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