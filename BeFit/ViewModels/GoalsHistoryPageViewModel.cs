using System.Collections.Generic;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.Input;

namespace BeFit.ViewModels;

public partial class GoalsHistoryPageViewModel(
    INavigator navigator,
    IEnumerable<OneDayGoalsViewModel> days) : ViewModelBase
{
    private readonly INavigator navigator = navigator;

    public ObservableCollection<OneDayGoalsViewModel> Days { get; } = new(days);

    [RelayCommand]
    private void Back()
        => navigator.NavigateBackward();
}