using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TCC_APP.Models;
using TCC_APP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TCC_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NovoProdutoDaListaPage : ContentPage
    {
        ProdutosViewModel viewModel;
        string idLista = string.Empty;
        string distancia;

        public NovoProdutoDaListaPage(string idLista, string distancia = null)
        {
            InitializeComponent();

            this.idLista = idLista;
            this.distancia = distancia;
            BindingContext = this.viewModel = new ProdutosViewModel(null, distancia, idLista);
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
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

            BindingContext = viewModel = new ProdutosViewModel(searchBar.Text, distancia, idLista);

            viewModel.LoadItemsCommand.Execute(null);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as ProdutoDoSupermercado;
            if (item == null)
                return;


            ProdutoDaLista novoProdutoDaLista = new ProdutoDaLista
            {
                Id = Guid.NewGuid().ToString(),
                IdListaDeCompra = idLista,
                IdProduto = item.idProduto,
                qtdProduto = "0"
            };

            using (var dados = new AcessoDB())
            {
                dados.inserirProdutoDaLista(novoProdutoDaLista);
            }

            //MessagingCenter.Send(this, "AddItem", novoProduto);
            await Navigation.PopModalAsync();

            // Manually deselect item.
            //ItemsListView.SelectedItem = null;
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
