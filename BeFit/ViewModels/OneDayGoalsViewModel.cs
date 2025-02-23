﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

using BeFit.Models;

using CommunityToolkit.Mvvm.Input;

namespace BeFit.ViewModels;

public partial class OneDayGoalsViewModel : ViewModelBase
{
    private readonly INavigator navigator;

    public ObservableCollection<DoneActionViewModel> Goals { get; }

    public DateTime Day { get; private set; }

    public string DayName { get; private set; } = string.Empty;

    public double Progress => Goals.Count(g => g.Done) / (double)Goals.Count * 100d;

    public OneDayGoalsViewModel(INavigator navigator, DateTime day, IEnumerable<DoneActionViewModel> actual)
    {
        this.navigator = navigator;

        Goals = new(actual);
        Day = day;
        DayName = GetDayName(Day);
    }

    private static string GetDayName(DateTime day) => (DateTime.Today.Date - day).TotalDays switch
    {
        0 => $"Today, {day.ToString("dd MMM", CultureInfo.InvariantCulture)}",
        1 => $"Yesterday, {day.ToString("dd MMM", CultureInfo.InvariantCulture)}",
        _ => $"{day.DayOfWeek}, {day.ToString("dd MMM", CultureInfo.InvariantCulture)}",
    };

    [RelayCommand]
    private void Show()
        => navigator.Navigate(new GoalsPageViewModel(navigator) { Today = this });
}