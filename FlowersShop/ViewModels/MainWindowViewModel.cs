using CommunityToolkit.Mvvm.ComponentModel;
using FlowersShop.ViewModels;

namespace FlowerShop.ViewModels 
{
    public class MainWindowViewModel : ViewModelBase 
    {
        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value); 
        }

        public MainWindowViewModel()
        {
            CurrentView = new DashboardViewModel();
        }

        public void NavigateTo(string viewName)
        {
            switch (viewName)
            {
                case "Dashboard":
                    CurrentView = new DashboardViewModel();
                    break;
            }
        }
    }
}