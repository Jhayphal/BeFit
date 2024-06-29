using System;
using System.Linq;
using CommunityToolkit.Mvvm.Input;

namespace BeFit.ViewModels;

public partial class MenuPageViewModel(MainViewModel owner) : ViewModelBase
{
    private readonly MainViewModel owner = owner;
    private readonly GoalsHistoryPageViewModel history = TestDataProvider.CreateHistory(
        owner,
        new DateTime(2024, 06, 24));

    [RelayCommand]
    private void ViewHistory()
    {
        owner.Navigate(history);
    }

    [RelayCommand]
    private void UpdateTodayAchievements()
    {
        OneDayGoalsViewModel target;
        
        var today = DateTime.Now.Date;
        var latestDay = history.Days
            .OrderByDescending(d => d.Day)
            .FirstOrDefault();
        
        if (latestDay is null || latestDay.Day.Date != today)
        {
            target = TestDataProvider.CreateDayGoals(owner, today);
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
        throw new NotImplementedException();
    }

    [RelayCommand]
    private void ViewTrends()
    {
        throw new NotImplementedException();
    }
}
