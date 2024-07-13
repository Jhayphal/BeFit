using System;
using System.Collections.Generic;
using System.Linq;
using BeFit.Models;

using CommunityToolkit.Mvvm.Input;

namespace BeFit.ViewModels;

public partial class MenuPageViewModel(
    INavigator navigator,
    TestGoalsStorage storage) : ViewModelBase
{
    private readonly INavigator navigator = navigator;

    [RelayCommand]
    private void ViewHistory()
    {
        var history = GetHistory();

        navigator.Navigate(new GoalsHistoryPageViewModel(navigator, history));
    }

    [RelayCommand]
    private void UpdateTodayAchievements()
    {
        var now = DateTime.Now;
        var todayGoalsState = storage.GetGoalsStateOn(now)
            .Select(s => new DoneActionViewModel(s.Goal)
            {
                Done = s.Done
            });

        OneDayGoalsViewModel target = new(navigator, now, todayGoalsState);

        navigator.Navigate(new GoalsPageViewModel(navigator)
        {
            Today = target
        });
    }

    [RelayCommand]
    private void EditGoals()
    {
        IEnumerable<GoalViewModel> availableGoals = storage.GetGoals()
            .Select(g => new GoalViewModel(g));

        navigator.Navigate(new GoalsEditorPageViewModel(navigator, availableGoals, storage));
    }

    [RelayCommand]
    private void ViewTrends()
    {
        var history = GetHistory();

        navigator.Navigate(new TrendsViewModel(navigator, history));
    }

    private IEnumerable<OneDayGoalsViewModel> GetHistory()
        => storage.GetHistory(new DateTime(), DateTime.Now)
            .Select(day => new OneDayGoalsViewModel(
                navigator,
                day.Min(x => x.Created),
                day.Select(s => new DoneActionViewModel(s.Goal)
                {
                    Done = s.Done
                })))
            .ToArray();
}
