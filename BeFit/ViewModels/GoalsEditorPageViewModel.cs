using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BeFit.Models;

using CommunityToolkit.Mvvm.Input;

namespace BeFit.ViewModels
{
    public partial class GoalsEditorPageViewModel(INavigator navigator, IEnumerable<Goal> goals) : ViewModelBase
    {
        public ObservableCollection<GoalViewModel> Goals { get; } = new(goals.Select(g => new GoalViewModel(g)));

        [RelayCommand]
        private void Back()
            => navigator.NavigateBackward();

        [RelayCommand]
        private void Add()
        {
            navigator.Navigate(new GoalEditorPageViewModel(navigator, this));
        }
    }
}
