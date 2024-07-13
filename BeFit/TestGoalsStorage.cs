using System;
using System.Collections.Generic;
using System.Linq;

using BeFit.Models;

namespace BeFit
{
    public sealed class TestGoalsStorage
    {
        private readonly List<Goal> goals;
        private readonly List<GoalState> goalsState;

        public TestGoalsStorage()
        {
            goals = TestDataProvider.GoalNames
                .Select((g, i) => new Goal(i + 1, g, new Schedule(i + 1, TestDataProvider.GetDays())))
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

        public void UpdateGoalState(GoalState state)
        {
            var targetIndex = goalsState.FindIndex(s => s.Goal.Id == state.Goal.Id);
            if (targetIndex < 0)
            {
                throw new ArgumentException("Unknown state", nameof(state));
            }

            goalsState[targetIndex] = state;
        }

        public void AddState(GoalState state)
        {
            if (state.Goal.Id != 0)
            {
                throw new ArgumentException("State already exists", nameof(state));
            }

            var lastScheduleId = goalsState.Count == 0
                ? 0
                : goalsState.Max(g => g.Goal.Schedule.Id);

            var newSchedule = state.Goal.Schedule with
            {
                Id = lastScheduleId + 1
            };

            var lastGoalId = goalsState.Count == 0
                ? 0
                : goalsState.Max(g => g.Goal.Id);

            var newGoal = state.Goal with
            {
                Id = lastGoalId + 1
            };

            GoalState newState = state with
            {
                Goal = newGoal
            };

            goalsState.Add(state);
        }
    }
}
