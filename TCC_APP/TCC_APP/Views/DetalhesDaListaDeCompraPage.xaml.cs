using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TCC_APP.Models;
using TCC_APP.ViewModels;

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

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //var item = args.SelectedItem as ListaDeCompra;
            //if (item == null)
            //    return;

            //await Navigation.PushAsync(new DetalhesDaListaDeCompraPage(new ItemDetailViewModel(item)));

            //// Manually deselect item.
            //ItemsListView.SelectedItem = null;
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