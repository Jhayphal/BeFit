using System;
using System.Linq;

using BeFit.Models;

using CommunityToolkit.Mvvm.Input;

namespace BeFit.ViewModels;

public partial class MenuPageViewModel(MainViewModel owner) : ViewModelBase
{
    private readonly MainViewModel owner = owner;
    private readonly GoalsHistoryPageViewModel history = TestDataProvider.CreateHistory(
        owner,
        new DateTime(2024, 06, 01));

    [RelayCommand]
    private void ViewHistory()
    {
        owner.Navigate(history);
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
            target = TestDataProvider.CreateDayGoals(owner, today);
            target ??= new OneDayGoalsViewModel(owner);
            history.Days.Add(target);
        }
        else
        {
            target = latestDay;
        }

        owner.Navigate(new GoalsPageViewModel(owner)
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

        owner.Navigate(new GoalsEditorPageViewModel(owner, goals));
    }

    [RelayCommand]
    private void ViewTrends()
        => owner.Navigate(new TrendsViewModel(owner, history.Days));
}
