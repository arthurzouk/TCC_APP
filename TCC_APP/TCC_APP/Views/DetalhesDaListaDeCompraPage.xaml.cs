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
using System.Globalization;

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
        }

        void OnTextChanged(object sender, EventArgs args)
        {
            SearchBar searchBar = (SearchBar)sender;

            BindingContext = viewModel = new ProdutoDaListaViewModel(idLista, searchBar.Text);

            viewModel.LoadItemsCommand.Execute(null);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as DetalhesProdutoDaLista;
            if (item == null)
                return;

            if (rangeEntry.Text == null)
            {
                await DisplayAlert("Erro.", "Por favor informe a DISTANCIA desejada", "OK");

                rangeEntry.Focus();
            }
            else
            {
                double precoUnidade = double.Parse(item.valorTotalProduto.Replace("R$", "")) / int.Parse(item.QtdProduto);

                await Navigation.PushModalAsync(new NavigationPage(new OpcoesProdutoPage(idLista, item.idProdutoDaLista, item.nomeProduto, item.marcaProduto, precoUnidade, rangeEntry.Text)));
            }

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        void OnQtdChanged(object sender, EventArgs args)
        {
            var produtos = ItemsListView.ItemsSource;
            double valorTotalProdutos = 0;
            double valorTotalEntrega = 0;
            string idSupermercados = string.Empty;
            ProdutoDaLista prodLista = null;
            Produto prod = null;
            Supermercado super = null;

            try
            {
                //Muda os valores totais da lista
                foreach (var item in produtos)
                {
                    var produto = item as DetalhesProdutoDaLista;

                    if (!string.IsNullOrEmpty(produto.QtdProduto)
                        && !string.IsNullOrWhiteSpace(produto.QtdProduto)
                        && int.Parse(produto.QtdProduto) != 0)
                    {
                        using (var dados = new AcessoDB())
                        {
                            prodLista = dados.GetProdutoDaLista(produto.idProdutoDaLista);
                            prod = dados.GetProduto(prodLista.IdProduto);
                            super = dados.GetSupermercado(prod.IdSupermercado);
                        }

                        ProdutoDaLista atualizarProdutoDaLista = new ProdutoDaLista
                        {
                            Id = prodLista.Id,
                            IdListaDeCompra = prodLista.IdListaDeCompra,
                            IdProduto = prodLista.IdProduto,
                            qtdProduto = string.IsNullOrEmpty(produto.QtdProduto) ? "0" : produto.QtdProduto
                        };

                        if (prodLista.qtdProduto != produto.QtdProduto)
                        {
                            using (var dados = new AcessoDB())
                            {
                                dados.AtualizarProdutoDaLista(atualizarProdutoDaLista);
                            }
                        }

                        valorTotalProdutos += double.Parse(produto.QtdProduto) * prod.Preco;

                        if (!idSupermercados.Contains(prod.IdSupermercado))
                        {
                            valorTotalEntrega += super.Distancia;
                            idSupermercados += prod.IdSupermercado + " | ";
                        }
                    }
                }

                lblTotalLista.Text = "Total produtos: " + valorTotalProdutos.ToString("C", CultureInfo.CurrentCulture);

                lblTotalEntrega.Text = "Total entrega: " + valorTotalEntrega.ToString("C", CultureInfo.CurrentCulture);

                lblTotalCompra.Text = "Total: " + (valorTotalProdutos + valorTotalEntrega).ToString("C", CultureInfo.CurrentCulture);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
        }

        async void Remove_Clicked(object sender, EventArgs args)
        {
            try
            {
                var button = sender as Button;
                var produto = button.BindingContext as DetalhesProdutoDaLista;
                var vm = BindingContext as ProdutoDaListaViewModel;

                using (var dados = new AcessoDB())
                {
                    dados.DeletarProdutoDaLista(produto.idProdutoDaLista);
                }

                vm.RemoveCommand.Execute(produto);

                await DisplayAlert("Produto removido", "O produto " + produto.nomeProduto + " foi removido da lista.", "OK");
                
                viewModel.LoadItemsCommand.Execute(null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            if (rangeEntry.Text == null)
            {
                await DisplayAlert("Erro ao tentar adicionar produto.", "Por favor informe a DISTANCIA desejada", "OK");

                rangeEntry.Focus();
                return;
            }

            await Navigation.PushModalAsync(new NavigationPage(new NovoProdutoDaListaPage(idLista, rangeEntry.Text)));
        }

        async void btnComprarClicked(object sender, EventArgs args)
        {
            ProdutoDaLista prodLista = null;

            if (lblTotalCompra.Text == null || lblTotalCompra.Text.Contains("R$ 0,00"))
            {
                await DisplayAlert("Erro ao efetuar compra", "Não é possível comprar uma lista vazia.", "OK");
            }
            else
            {
                try
                {
                    var produto1 = viewModel._produtoDaLista[0];
                    ListaDeCompra lista = null;

                    using (var dados = new AcessoDB())
                    {
                        prodLista = dados.GetProdutoDaLista(produto1.idProdutoDaLista);
                        lista = dados.GetListaDeCompra(prodLista.IdListaDeCompra);
                    }

                    string novoID = Guid.NewGuid().ToString();

                    Compra novaCompra = new Compra
                    {
                        Id = novoID,
                        NomeListaDeCompra = lista.Nome,
                        ValorTotalProdutos = lblTotalLista.Text,
                        ValorTotalEntrega = lblTotalEntrega.Text,
                        Data = DateTime.Now
                    };

                    using (var dados = new AcessoDB())
                    {
                        dados.InserirCompra(novaCompra);
                    }

                    foreach (var item in ItemsListView.ItemsSource)
                    {
                        var produto = item as DetalhesProdutoDaLista;

                        using (var dados = new AcessoDB())
                        {
                            prodLista = dados.GetProdutoDaLista(produto.idProdutoDaLista);
                        }

                        ProdutoDaCompra novoProdutoDaCompra = new ProdutoDaCompra()
                        {
                            Id = Guid.NewGuid().ToString(),
                            IdProduto = prodLista.IdProduto,
                            PrecoProduto = produto.valorTotalProduto,
                            qtdProduto = int.Parse(produto.QtdProduto)
                        };

                        using (var dados = new AcessoDB())
                        {
                            dados.InserirProdutoDaCompra(novoProdutoDaCompra);
                        }
                    }

                    await DisplayAlert("Compra realizada", "Você realizou uma compra no valor de " + lblTotalLista.Text + "\r\n A entrega ficou no valor de " + lblTotalEntrega.Text, "OK");

                    //Encaminhar usuário para tela de acompanhamento do pedido
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro ao realizar compra", "Exceção: " + ex.Message, "OK");
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}