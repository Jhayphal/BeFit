using System;
using System.Collections.Generic;
using System.Linq;

using BeFit.Models;

namespace BeFit
{
    public sealed class TestGoalsStorage : IGoalsStorage
    {
        private readonly List<Goal> goals;
        private readonly List<GoalState> goalsState;

        public TestGoalsStorage()
        {
            goals = TestDataProvider.GoalNames
                .Select((g, i) => new Goal(i, g, new Schedule(i, TestDataProvider.GetDays())))
                .ToList();

            goalsState = [];

            var today = DateTime.Now.Date;
            var current = today.AddDays(-10);
            while (current <= today)
            {
                foreach (var goal in goals)
                {
                    if (goal.Schedule.Days.Contains(current.DayOfWeek))
                    {
                        goalsState.Add(new GoalState(goal, Random.Shared.Next(1000) < 500, current));
                    }
                }

                current = current.AddDays(1);
            }
        }

        public IEnumerable<Goal> GetGoals() => goals;

        public IEnumerable<GoalState> GetGoalsStateOn(DateTime date)
            => goalsState
                .OrderBy(s => s.Created)
                .Where(s => s.Created.Date == date.Date);

        public IEnumerable<IEnumerable<GoalState>> GetHistory(DateTime from, DateTime to)
            => goalsState
                .OrderByDescending(s => s.Created)
                .Where(s => from.Date <= s.Created.Date && to.Date >= s.Created.Date)
                .GroupBy(x => x.Created.Date);
    }
}
