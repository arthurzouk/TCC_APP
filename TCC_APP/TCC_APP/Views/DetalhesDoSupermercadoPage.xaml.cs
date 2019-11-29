using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC_APP.Models;
using TCC_APP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TCC_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalhesDoSupermercadoPage : ContentPage
    {
        ProdutoDoSupermercadoViewModel viewModel;
        string idSupermercado = string.Empty;


        public DetalhesDoSupermercadoPage(ItemDetailViewModel viewModel, string nome, string id)
        {
            InitializeComponent();

            idSupermercado = id;
            BindingContext = this.viewModel = new ProdutoDoSupermercadoViewModel(nome, idSupermercado);            
        }

        public DetalhesDoSupermercadoPage()
        {
            InitializeComponent();

            var item = new Produto
            {
                Nome = "Novo Produto"
            };
        }

        void OnTextChanged(object sender, EventArgs args)
        {
            SearchBar searchBar = (SearchBar)sender;

            BindingContext = viewModel = new ProdutoDoSupermercadoViewModel(idSupermercado, searchBar.Text);

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
                var vm = BindingContext as ProdutoDoSupermercadoViewModel;

                using (var dados = new AcessoDB())
                {
                    dados.DeletarProdutoDoSupermercado(idSupermercado, produto.Id);
                }

                vm.RemoveCommand.Execute(produto);

                await DisplayAlert("Produto removido", "O produto " + produto.Nome + " foi removido do supermercado.", "OK");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NovoProdutoDoSupermercadoPage(idSupermercado)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}