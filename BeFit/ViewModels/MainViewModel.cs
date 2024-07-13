using System.Collections.Generic;

namespace BeFit.ViewModels;

public partial class MainViewModel : ViewModelBase, INavigator
{
    private readonly Stack<ViewModelBase> pagesNavigationHistory = [];

    private ViewModelBase? page;

    public ViewModelBase? Page
    {
        get => page;
        private set
        {
            if (!ReferenceEquals(value, page))
            {
                page = value;

                OnPropertyChanged(nameof(Page));
            }
        }
    }

    public MenuPageViewModel MenuPage { get; }

    public MainViewModel(TestGoalsStorage goals)
    {
        MenuPage = new(this, goals);

        Navigate(MenuPage);
    }

    public void Navigate(ViewModelBase target)
    {
        if (Page is not null)
        {
            pagesNavigationHistory.Push(Page);
        }

        Page = target;
    }

    public void NavigateBackward()
    {
        if (CanNavigateBackward())
        {
            Page = pagesNavigationHistory.Pop();
        }
    }

    public bool CanNavigateBackward()
        => pagesNavigationHistory.Count > 0;
}