using System;

using CommunityToolkit.Mvvm.ComponentModel;

namespace BeFit.ViewModels
{
    public partial class DayOfWeekViewModel(DayOfWeek dayOfWeek) : ViewModelBase
    {
        [ObservableProperty]
        private bool active;

        public DayOfWeek DayOfWeek { get; } = dayOfWeek;

        public string Caption { get; } = dayOfWeek.ToString()[..2];
    }
}
