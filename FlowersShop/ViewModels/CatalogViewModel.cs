using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlowersShop.Models;
using System.Linq;
using FlowersShop.ViewModels;

namespace FlowersShop.ViewModels
{
    public partial class CatalogViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Flower> _allFlowers;

        [ObservableProperty]
        private ObservableCollection<Flower> _flowers;

        [ObservableProperty]
        private string _searchText = string.Empty;
        
        [ObservableProperty]
        private Flower _selectedFlower;
        
        [ObservableProperty]
        private Flower _editingFlower;
        
        [ObservableProperty]
        private bool _isFormVisible;
        
        private bool _isCreatingNew;

        public CatalogViewModel()
        {
            _allFlowers = new ObservableCollection<Flower>
            {
                new Flower { Name = "Червона троянда", Category = "Зрізані квіти", Price = 150, Color = "Червоний", StockQuantity = 50 },
                new Flower { Name = "Орхідея Фаленопсис", Category = "Вазони", Price = 800, Color = "Білий", StockQuantity = 12 },
                new Flower { Name = "Тюльпан", Category = "Зрізані квіти", Price = 80, Color = "Жовтий", StockQuantity = 100 },
                new Flower { Name = "Біла троянда", Category = "Зрізані квіти", Price = 160, Color = "Білий", StockQuantity = 30 }
            };
            Flowers = new ObservableCollection<Flower>(_allFlowers);
        }

        partial void OnSearchTextChanged(string value)
        {
            FilterFlowers();
        }

        private void FilterFlowers()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
                Flowers = new ObservableCollection<Flower>(_allFlowers);
            else
                Flowers = new ObservableCollection<Flower>(_allFlowers.Where(f => f.Name.ToLower().Contains(SearchText.ToLower())));
        }
        
        partial void OnSelectedFlowerChanged(Flower value)
        {
            if (value != null)
            {
                EditingFlower = new Flower
                {
                    Id = value.Id,
                    Name = value.Name,
                    Category = value.Category,
                    Price = value.Price,
                    Color = value.Color,
                    StockQuantity = value.StockQuantity
                };
                _isCreatingNew = false;
                IsFormVisible = true;
            }
        }

        [RelayCommand]
        private void AddNewFlower()
        {
            SelectedFlower = null;
            EditingFlower = new Flower { Name = "", Category = "Зрізані квіти", Price = 0, Color = "", StockQuantity = 1 };
            _isCreatingNew = true;
            IsFormVisible = true;
        }

        [RelayCommand]
        private void DeleteSelected()
        {
            if (SelectedFlower != null)
            {
                _allFlowers.Remove(SelectedFlower);
                FilterFlowers();
                IsFormVisible = false; // Ховаємо форму, бо товар видалено
            }
        }

        [RelayCommand]
        private void SaveFlower()
        {
            if (string.IsNullOrWhiteSpace(EditingFlower.Name) || EditingFlower.Price < 0)
            {
                return;
            }

            if (_isCreatingNew)
            {
                _allFlowers.Add(EditingFlower);
            }
            else
            {
                var original = _allFlowers.FirstOrDefault(f => f.Id == EditingFlower.Id);
                if (original != null)
                {
                    original.Name = EditingFlower.Name;
                    original.Category = EditingFlower.Category;
                    original.Price = EditingFlower.Price;
                    original.Color = EditingFlower.Color;
                    original.StockQuantity = EditingFlower.StockQuantity;
                }
            }

            FilterFlowers();
            IsFormVisible = false;
            SelectedFlower = null; 
        }

        [RelayCommand]
        private void CancelEdit()
        {
            IsFormVisible = false;
            SelectedFlower = null;
        }
    }
}