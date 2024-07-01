using System;
using System.Collections.Generic;
using System.Linq;

using BeFit.Models;

namespace BeFit.ViewModels
{
    public partial class ScheduleViewModel(Schedule schedule) : ViewModelBase
    {
        public IEnumerable<DayOfWeekViewModel> Schedule { get; } = Enum
            .GetValues<DayOfWeek>()
            .Select(d => new DayOfWeekViewModel(d)
            {
                Active = schedule.Days.Contains(d)
            }).ToList();
    }
}
