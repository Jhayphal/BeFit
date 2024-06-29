using System;
using System.Collections.Generic;
using System.Linq;
using BeFit.ViewModels;

namespace BeFit;

internal static class TestDataProvider
{
    private static readonly string[] goalNames = [
        "Приготовить еду",
        "Учить английский",
        "Не есть сладкое",
        "Не есть жаренное",
        "Читать книгу",
        "Выключить компьютер до 22:00"];

    public static IEnumerable<DoneActionViewModel> GetGoals(DateTime date) => goalNames.Select(g => new DoneActionViewModel(g)
    {
        Done = Random.Shared.Next(1000) < 250,
        When = date
    });

    public static OneDayGoalsViewModel CreateDayGoals(MainViewModel owner, DateTime date)
    {
        var result = new OneDayGoalsViewModel(owner);

        foreach (var goal in GetGoals(date))
        {
            result.Goals.Add(goal);
        }

        result.SetDay();

        return result;
    }

    public static GoalsHistoryPageViewModel CreateHistory(MainViewModel owner, DateTime downToDay)
    {
        var targetDay = downToDay.Date;
        var currentDay = DateTime.Now.Date;

        List<OneDayGoalsViewModel> days = [];

        while ((currentDay - targetDay).TotalDays >= 0)
        {
            days.Add(CreateDayGoals(owner, currentDay));

            currentDay = currentDay.AddDays(-1);
        }

        return new(owner, days);
    }
}
