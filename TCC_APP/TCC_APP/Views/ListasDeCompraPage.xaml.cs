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
using System.Diagnostics;

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

        void OnTextChanged(object sender, EventArgs args)
        {
            SearchBar searchBar = (SearchBar)sender;

            BindingContext = viewModel = new ListasViewModel(searchBar.Text);

            viewModel.LoadItemsCommand.Execute(null);
        }

        async void AddItem_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NovaListaDeCompraPage()));
        }

        async void Remove_Clicked(object sender, EventArgs args)
        {
            try
            {
                var button = sender as Button;
                var listaDeCompra = button.BindingContext as ListaDeCompra;
                var vm = BindingContext as ListasViewModel;
                vm.RemoveCommand.Execute(listaDeCompra);

                using (var dados = new AcessoDB())
                {
                    dados.DeletarListaDeCompra(listaDeCompra.Id);
                }

                await DisplayAlert("Lista de compras deletada", "A lista " + listaDeCompra.Nome + " foi deletada.", "OK");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}