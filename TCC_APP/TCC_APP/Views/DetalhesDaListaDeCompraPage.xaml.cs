using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TCC_APP.Models;
using TCC_APP.ViewModels;
using System.Diagnostics;

namespace TCC_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalhesDaListaDeCompraPage : ContentPage
    {
        //ItemDetailViewModel viewModel;
        ProdutoDaListaViewModel viewModel;
        string idLista = string.Empty;

        public DetalhesDaListaDeCompraPage(ItemDetailViewModel viewModel, string id)
        {
            InitializeComponent();

            idLista = id;
            BindingContext = this.viewModel = new ProdutoDaListaViewModel(idLista);
        }

        public DetalhesDaListaDeCompraPage()
        {
            InitializeComponent();

            var item = new Produto
            {
                Nome = "Nova produto"
                //Quantidade = "1"
            };

            //viewModel = new ItemDetailViewModel(item);
            //BindingContext = viewModel;
        }

        void OnTextChanged(object sender, EventArgs args)
        {
            SearchBar searchBar = (SearchBar)sender;

            BindingContext = viewModel = new ProdutoDaListaViewModel(idLista, searchBar.Text);

            viewModel.LoadItemsCommand.Execute(null);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //var item = args.SelectedItem as ListaDeCompra;
            //if (item == null)
            //    return;

            //await Navigation.PushAsync(new DetalhesDaListaDeCompraPage(new ItemDetailViewModel(item)));

            //// Manually deselect item.
            //ItemsListView.SelectedItem = null;
        }

        async void Remove_Clicked(object sender, EventArgs args)
        {
            try
            {
                var button = sender as Button;
                var produto = button.BindingContext as Produto;
                var vm = BindingContext as ProdutoDaListaViewModel;

                using (var dados = new AcessoDB())
                {
                    dados.DeletarProdutoDaLista(idLista, produto.Id);
                }

                vm.RemoveCommand.Execute(produto);

                await DisplayAlert("Produto removido", "O produto " + produto.Nome + " foi removido da lista.", "OK");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NovoProdutoDaListaPage(idLista)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}