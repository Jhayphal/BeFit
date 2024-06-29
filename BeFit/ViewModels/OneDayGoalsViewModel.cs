using System;
using System.Collections.ObjectModel;
using System.Linq;

using CommunityToolkit.Mvvm.Input;

namespace BeFit.ViewModels;

public partial class OneDayGoalsViewModel(MainViewModel owner) : ViewModelBase
{
    private readonly MainViewModel owner = owner;

    public ObservableCollection<DoneActionViewModel> Goals { get; } = [];

    public DateTime Day { get; private set; }

    public string DayName { get; private set; } = string.Empty;

    public void SetDay()
    {
        Day = Goals
            .Where(g => g.When.HasValue)
            .Min(g => g.When!.Value)
            .Date;

        DayName = GetDayName(Day);
    }

    private static string GetDayName(DateTime day) => (DateTime.Today.Date - day).TotalDays switch
    {
        0 => $"Today, {day:dd MMM}",
        1 => $"Yesterday, {day:dd MMM}",
        _ => $"{day.DayOfWeek}, {day:dd MMM}",
    };

    [RelayCommand]
    private void Show()
        => owner.Navigate(new GoalsPageViewModel(owner) { Today = this });
}