using System;
using System.Collections.Generic;
using System.Linq;

using BeFit.Models;

using CommunityToolkit.Mvvm.ComponentModel;

namespace BeFit.ViewModels
{
    public partial class GoalViewModel(Goal goal) : ViewModelBase
    {
        [ObservableProperty]
        private string description = goal.Description;

        public IEnumerable<DayOfWeekViewModel> Schedule { get; } = Enum
            .GetValues<DayOfWeek>()
            .Select(d => new DayOfWeekViewModel(d)
            {
                Active = goal.Schedule.Days.Contains(d)
            });
    }
}
