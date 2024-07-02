using CommunityToolkit.Mvvm.Input;

namespace BeFit.ViewModels;

public partial class GoalsPageViewModel(INavigator navigator) : ViewModelBase
{
    private readonly INavigator navigator = navigator;

    public required OneDayGoalsViewModel Today { get; set; }

    [RelayCommand]
    private void Back()
        => navigator.NavigateBackward();

    [RelayCommand]
    private void Done()
        => Back();
}
