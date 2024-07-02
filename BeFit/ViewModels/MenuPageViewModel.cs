using System;
using System.Collections.Generic;
using System.Linq;
using BeFit.Models;

using CommunityToolkit.Mvvm.Input;

namespace BeFit.ViewModels;

public partial class MenuPageViewModel(
    INavigator navigator,
    IGoalsStorage goals) : ViewModelBase
{
    private readonly INavigator navigator = navigator;

    [RelayCommand]
    private void ViewHistory()
    {
        var now = DateTime.Now;
        var history = goals.GetHistory(new DateTime(), now)
            .Select(day => new OneDayGoalsViewModel(
                navigator,
                now,
                day.Select(s => new DoneActionViewModel(s.Goal)
                {
                    Done = s.Done
                })));

        navigator.Navigate(new GoalsHistoryPageViewModel(navigator, history));
    }

    [RelayCommand]
    private void UpdateTodayAchievements()
    {
        var now = DateTime.Now;
        var todayGoalsState = goals.GetGoalsStateOn(now)
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
        IEnumerable<GoalViewModel> availableGoals = goals.GetGoals()
            .Select(g => new GoalViewModel(g));

        navigator.Navigate(new GoalsEditorPageViewModel(navigator, availableGoals));
    }

    [RelayCommand]
    private void ViewTrends()
    {
        var now = DateTime.Now;
        var history = goals.GetHistory(new DateTime(), DateTime.Now)
            .Select(day => new OneDayGoalsViewModel(
                navigator,
                now,
                day.Select(s => new DoneActionViewModel(s.Goal)
                {
                    Done = s.Done
                })))
            .ToArray();

        navigator.Navigate(new TrendsViewModel(navigator, history));
    }
}
