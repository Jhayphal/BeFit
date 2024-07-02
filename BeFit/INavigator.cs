using BeFit.ViewModels;

namespace BeFit
{
    public interface INavigator
    {
        bool CanNavigateBackward();

        void Navigate(ViewModelBase target);

        void NavigateBackward();
    }
}