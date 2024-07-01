using BeFit.Models;

using CommunityToolkit.Mvvm.ComponentModel;

namespace BeFit.ViewModels
{
    public partial class GoalViewModel(Goal goal) : ViewModelBase
    {
        [ObservableProperty]
        private string description = goal.Description;

        public ScheduleViewModel Schedule { get; } = new(goal.Schedule);
    }
}
