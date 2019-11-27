using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TCC_APP.Models;
using TCC_APP.Views;
using TCC_APP.ViewModels;

namespace TCC_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListasDeCompraPage : ContentPage
    {
        ListasViewModel viewModel;

        public ListasDeCompraPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ListasViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as ListaDeCompra;
            if (item == null)
                return;

            await Navigation.PushAsync(new DetalhesDaListaDeCompraPage(new ItemDetailViewModel(item), item.Id));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NovaListaDeCompraPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}