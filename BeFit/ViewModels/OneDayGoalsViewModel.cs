using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

using CommunityToolkit.Mvvm.Input;

namespace BeFit.ViewModels;

public partial class OneDayGoalsViewModel(MainViewModel owner) : ViewModelBase
{
    private readonly MainViewModel owner = owner;

    public ObservableCollection<DoneActionViewModel> Goals { get; } = [];

    public DateTime Day { get; private set; }

    public string DayName { get; private set; } = string.Empty;

    public void Load()
    {
        Day = Goals
            .Where(g => g.When.HasValue)
            .Min(g => g.When!.Value)
            .Date;

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
        => owner.Navigate(new GoalsPageViewModel(owner) { Today = this });
}