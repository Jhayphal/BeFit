using BeFit.Models;

using CommunityToolkit.Mvvm.Input;

namespace BeFit.ViewModels
{
    public partial class GoalEditorPageViewModel(MainViewModel owner, GoalsEditorPageViewModel goals) : ViewModelBase
    {
        public GoalViewModel Goal { get; } = new GoalViewModel(
            new Goal(
                0,
                string.Empty,
                Schedule.CreateNew()));

        [RelayCommand]
        private void Cancel()
            => owner.NavigateBackward();

        [RelayCommand]
        private void Add()
        {
            goals.Goals.Add(Goal);

            owner.NavigateBackward();
        }
    }
}
