using System;
using CommunityToolkit.Mvvm.ComponentModel;
using FlowersShop.Services;

namespace FlowersShop.ViewModels;

public partial class DashboardViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _currentDate = DateTime.Now.ToString("dd MMMM yyyy");
    
    [ObservableProperty]
    private int _totalFlowers;
    
    [ObservableProperty]
    private int _activeOrders;

    public DashboardViewModel()
    {
        var fileService = new FileService("flowers_data.json");
        
        var flowers = fileService.LoadData();
        
        TotalFlowers = flowers.Count;
        
        ActiveOrders = 5; 
    }
}