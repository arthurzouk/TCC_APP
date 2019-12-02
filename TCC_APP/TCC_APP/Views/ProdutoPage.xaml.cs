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
	public partial class ProdutoPage : ContentPage
	{
        ProdutosViewModel viewModel;

        public ProdutoPage()
		{
			InitializeComponent ();
            BindingContext = viewModel = new ProdutosViewModel();
        }

        void OnTextChanged(object sender, EventArgs args)
        {
            SearchBar searchBar = (SearchBar)sender;

            BindingContext = viewModel = new ProdutosViewModel(searchBar.Text);

            viewModel.LoadItemsCommand.Execute(null);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Produto;
            if (item == null)
                return;

            await Navigation.PushAsync(new DetalhesDoProdutoPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void Remove_Clicked(object sender, EventArgs args)
        {
            try
            {
                var button = sender as Button;
                var produto = button.BindingContext as ProdutoDoSupermercado;
                var vm = BindingContext as ProdutosViewModel;
                vm.RemoveCommand.Execute(produto);

                using (var dados = new AcessoDB())
                {
                    dados.DeletarProduto(produto.idProduto);
                }

                await DisplayAlert("Produto removido", "O produto " + produto.NomeProduto + " foi removido do " + produto.NomeSupermercado + ".", "OK");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            List<Supermercado> supermercados = null;

            using (var dados = new AcessoDB())
            {
                supermercados = dados.GetAllSupermercado();
            }

            if (supermercados.Count == 0)
            {
                await DisplayAlert("Nenhum supermercado encontrado.", "Por favor adicione mais supermercados", "OK");
            }
            else
            {
                await Navigation.PushModalAsync(new NavigationPage(new NovoProdutoPage()));
            }            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}