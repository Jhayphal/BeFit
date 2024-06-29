using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using CommunityToolkit.Mvvm.Input;

namespace BeFit.ViewModels;

public partial class GoalsHistoryPageViewModel(MainViewModel owner, IEnumerable<OneDayGoalsViewModel> days) : ViewModelBase
{
    private readonly MainViewModel owner = owner;

    public ObservableCollection<OneDayGoalsViewModel> Days { get; } = new(days.OrderBy(d => d.Day));

    [RelayCommand]
    private void Back()
        => owner.NavigateBackward();
}