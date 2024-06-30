using System;
using System.Collections.Generic;
using System.Linq;

using BeFit.Models;
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

    public static IEnumerable<DayOfWeek> GetDays()
        => Enumerable
            .Range(0, Random.Shared.Next(7))
            .Select(x => (DayOfWeek)Random.Shared.Next(7))
            .Distinct();

    public static IEnumerable<DoneActionViewModel> GetGoals(DateTime date) => goalNames
        .Select((g, i) => new DoneActionViewModel(new Goal(i, g, new Schedule(i, GetDays())))
    {
        Done = Random.Shared.Next(1000) < 500,
        When = date
    })
        .Where(a => a.Schedule.Days.Contains(a.When!.Value.DayOfWeek));

    public static OneDayGoalsViewModel? CreateDayGoals(MainViewModel owner, DateTime date)
    {
        var result = new OneDayGoalsViewModel(owner);

        foreach (var goal in GetGoals(date))
        {
            result.Goals.Add(goal);
        }

        if (result.Goals.Count == 0)
        {
            return null;
        }

        result.Load();

        return result;
    }

    public static GoalsHistoryPageViewModel CreateHistory(MainViewModel owner, DateTime downToDay)
    {
        var targetDay = downToDay.Date;
        var currentDay = DateTime.Now.Date;

        List<OneDayGoalsViewModel> days = [];

        while ((currentDay - targetDay).TotalDays >= 0)
        {
            var day = CreateDayGoals(owner, currentDay);
            if (day is not null)
            {
                days.Add(day);
            }

            currentDay = currentDay.AddDays(-1);
        }

        return new(owner, days);
    }
}
