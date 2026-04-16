using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using FlowersShop.ViewModels;

namespace FlowersShop.ViewModels
{
    public partial class SettingsViewModel : ViewModelBase
    {
        [ObservableProperty]
        private bool _isDarkTheme;

        [ObservableProperty]
        private string _selectedLanguage = "Українська";
        
        public List<string> Languages { get; } = new List<string> { "Українська", "English" };

        public SettingsViewModel()
        {
            if (Application.Current != null)
            {
                IsDarkTheme = Application.Current.RequestedThemeVariant == ThemeVariant.Dark;
            }
        }

        partial void OnIsDarkThemeChanged(bool value)
        {
            if (Application.Current != null)
            {
                Application.Current.RequestedThemeVariant = value ? ThemeVariant.Dark : ThemeVariant.Light;
            }
        }
        partial void OnSelectedLanguageChanged(string value)
        {
            if (Application.Current == null) return;
            
            string dictionaryPath = value == "English" 
                ? "avares://FlowersShop/Resources/en-US.axaml" 
                : "avares://FlowersShop/Resources/uk-UA.axaml";
            
            var newDictionary = new ResourceInclude(new Uri("avares://FlowersShop/App.axaml"))
            {
                Source = new Uri(dictionaryPath)
            };
            
            Application.Current.Resources.MergedDictionaries[0] = newDictionary;
        }
    }
}