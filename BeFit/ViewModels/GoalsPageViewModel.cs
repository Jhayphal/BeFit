using CommunityToolkit.Mvvm.Input;

namespace BeFit.ViewModels;

public partial class GoalsPageViewModel(MainViewModel owner) : ViewModelBase
{
    private readonly MainViewModel owner = owner;

    public required OneDayGoalsViewModel Today { get; set; }

    [RelayCommand]
    private void Back()
        => owner.NavigateBackward();

    [RelayCommand]
    private void Done() => throw new System.NotImplementedException();
}
