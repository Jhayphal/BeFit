using BeFit.Models;

using CommunityToolkit.Mvvm.Input;

namespace BeFit.ViewModels
{
    public partial class GoalEditorPageViewModel(
        INavigator navigator,
        GoalsEditorPageViewModel goals) : ViewModelBase
    {
        public GoalViewModel Goal { get; } = new(new Goal(0, string.Empty, Schedule.CreateNew()));

        [RelayCommand]
        private void Cancel()
            => navigator.NavigateBackward();

        [RelayCommand]
        private void Add()
        {
            goals.Goals.Add(Goal);

            navigator.NavigateBackward();
        }
    }
}
