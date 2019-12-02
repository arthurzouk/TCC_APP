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
    public partial class NovoProdutoDoSupermercadoPage : ContentPage
    {
        ProdutosViewModel viewModel;
        string idSupermercado = string.Empty;

        public NovoProdutoDoSupermercadoPage(string idSupermercado)
        {
            InitializeComponent();

            this.idSupermercado = idSupermercado;
            BindingContext = this.viewModel = new ProdutosViewModel();
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        void OnTextChanged(object sender, EventArgs args)
        {
            SearchBar searchBar = (SearchBar)sender;

            BindingContext = viewModel = new ProdutosViewModel(searchBar.Text);

            viewModel.LoadItemsCommand.Execute(null);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            try
            {
                var item = args.SelectedItem as Produto;
                if (item == null)
                    return;


                ProdutoDoSupermercado novoProdutoDoSupermercado = new ProdutoDoSupermercado
                {
                    NomeSupermercado = idSupermercado,
                    NomeProduto = item.Id
                };

                using (var dados = new AcessoDB())
                {
                    dados.inserirProdutoDoSupermercado(novoProdutoDoSupermercado);
                }

                //MessagingCenter.Send(this, "AddItem", novoProduto);
                await Navigation.PopModalAsync();

                // Manually deselect item.
                //ItemsListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}