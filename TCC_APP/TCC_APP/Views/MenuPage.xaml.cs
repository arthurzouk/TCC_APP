using TCC_APP.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TCC_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Listas, Title="Listas de Compra" },
                new HomeMenuItem {Id = MenuItemType.Produtos, Title="Produtos" },
                new HomeMenuItem {Id = MenuItemType.Relatorios, Title="Relatórios" }
            };

            ListViewMenu.ItemsSource = menuItems;

            //ListViewMenu.SelectedItem = menuItems[0];
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {

            if (args.SelectedItem != null)
            {
                switch (args.SelectedItemIndex)
                {
                    case 0:
                        await Navigation.PushAsync(new MainPage());
                        break;
                    case 1:
                        await Navigation.PushAsync(new ProdutoPage());
                        break;
                    case 2:
                        await Navigation.PushAsync(new RelatorioLista());
                        break;
                    default:
                        break;
                }

                // Manually deselect item.
                ListViewMenu.SelectedItem = null;
            }
        }
    }
}