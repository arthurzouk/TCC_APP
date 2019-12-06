using System;
using System.Collections.Generic;
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
	public partial class OpcoesProdutoPage : ContentPage
	{
        ProdutosViewModel viewModel;
        string idLista = string.Empty;
        string distancia, idProdutoDaLista, nomeProduto, marcaProduto;
        double precoUnidade;

        public OpcoesProdutoPage (string idLista, string idProdutoDaLista, string nomeProduto, string marcaProduto, double precoUnidade, string distancia)
		{
			InitializeComponent ();

            this.idLista = idLista;
            this.idProdutoDaLista = idProdutoDaLista;
            this.nomeProduto = nomeProduto;
            this.marcaProduto = marcaProduto;
            this.precoUnidade = precoUnidade;
            this.distancia = distancia;
            BindingContext = this.viewModel = new ProdutosViewModel(null, distancia, idLista, nomeProduto, marcaProduto, precoUnidade);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as ProdutoDoSupermercado;
            if (item == null)
                return;

            //Trocar para atualizar produto da lista

            ProdutoDaLista attProdutoDaLista = new ProdutoDaLista
            {
                Id = idProdutoDaLista, //pega o idProdutoDaLista e substitui apenas o idProduto
                IdListaDeCompra = idLista,
                IdProduto = item.idProduto,
                qtdProduto = "1"
            };

            using (var dados = new AcessoDB())
            {
                dados.AtualizarProdutoDaLista(attProdutoDaLista);
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