using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using BeFit.Models;
using CommunityToolkit.Mvvm.Input;

namespace BeFit.ViewModels
{
    public partial class GoalsEditorPageViewModel(MainViewModel owner, IEnumerable<Goal> goals) : ViewModelBase
    {
        public ObservableCollection<GoalViewModel> Goals { get; } = new(goals.Select(g => new GoalViewModel(g)));

        [RelayCommand]
        private void Back()
            => owner.NavigateBackward();

        [RelayCommand]
        private void Save()
        {

        }
    }
}
