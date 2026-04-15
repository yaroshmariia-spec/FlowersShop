using System;

namespace FlowersShop.ViewModels;

public class DashboardViewModel : ViewModelBase
{
    public string WelcomeMessage { get; set; } = "Вітаємо в системі управління Квітковим магазином!";
        
    public string CurrentDate { get; set; } = DateTime.Now.ToString("dd MMMM yyyy");
        
    public int TotalFlowers { get; set; } = 42; 
        
    public int ActiveOrders { get; set; } = 7;
}