using System;
using System.Linq;
using BeFit.Models;

using CommunityToolkit.Mvvm.Input;

namespace BeFit.ViewModels;

public partial class MenuPageViewModel(INavigator navigator) : ViewModelBase
{
    private readonly INavigator navigator = navigator;
    private readonly GoalsHistoryPageViewModel history = TestDataProvider.CreateHistory(
        navigator,
        new DateTime(2024, 06, 01));

    [RelayCommand]
    private void ViewHistory()
    {
        navigator.Navigate(history);
    }

    [RelayCommand]
    private void UpdateTodayAchievements()
    {
        OneDayGoalsViewModel? target;
        
        var today = DateTime.Now.Date;
        var latestDay = history.Days
            .OrderByDescending(d => d.Day)
            .FirstOrDefault();
        
        if (latestDay is null || latestDay.Day.Date != today)
        {
            target = TestDataProvider.CreateDayGoals(navigator, today);
            target ??= new OneDayGoalsViewModel(navigator);
            history.Days.Add(target);
        }
        else
        {
            target = latestDay;
        }

        navigator.Navigate(new GoalsPageViewModel(navigator)
        {
            Today = target
        });
    }

    [RelayCommand]
    private void EditGoals()
    {
        var goals = history.Days
            .SelectMany(x => x.Goals)
            .Select((g, i) => new Goal(i, g.Description, g.Schedule))
            .GroupBy(x => x.Description)
            .Select(x => x.First());

        navigator.Navigate(new GoalsEditorPageViewModel(navigator, goals));
    }

    [RelayCommand]
    private void ViewTrends()
        => navigator.Navigate(new TrendsViewModel(navigator, history.Days));
}
