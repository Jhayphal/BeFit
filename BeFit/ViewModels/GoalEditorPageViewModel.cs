using System;
using System.Linq;

using BeFit.Models;

using CommunityToolkit.Mvvm.Input;

namespace BeFit.ViewModels
{
    public partial class GoalEditorPageViewModel(
        INavigator navigator,
        GoalsEditorPageViewModel goals,
        TestGoalsStorage storage) : ViewModelBase
    {
        public GoalViewModel Goal { get; } = new(new Goal(0, string.Empty, Schedule.CreateNew()));

        [RelayCommand]
        private void Cancel()
            => navigator.NavigateBackward();

        [RelayCommand]
        private void Add()
        {
            goals.Goals.Add(Goal);

            var schedule = Goal.Schedule.Schedule
                .Where(x => x.Active)
                .Select(x => x.DayOfWeek);

            var state = new GoalState(
                new Goal(
                    0,
                    Goal.Description,
                    new Schedule(0, schedule)),
                false,
                DateTime.Now);

            storage.AddState(state);

            navigator.NavigateBackward();
        }
    }
}
