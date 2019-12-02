using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using TCC_APP.Models;
using TCC_APP.Views;
using Xamarin.Forms;

namespace TCC_APP.ViewModels
{
    class ProdutosViewModel : BaseViewModel
    {
        public ObservableCollection<ProdutoDoSupermercado> _produtos { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ProdutosViewModel(string palavraDebusca = null, string distancia = null, string idLista = null)
        {
            Title = "Produtos";
            _produtos = new ObservableCollection<ProdutoDoSupermercado>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(palavraDebusca, distancia, idLista));
        }

        async Task ExecuteLoadItemsCommand(string palavraDebusca, string distancia, string idLista)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                _produtos.Clear();
                //var items = await ProdutoDataStore.GetItemsAsync("teste");

                bool produtoDentroDoRange;
                bool produtoPertenceALista;
                List<Produto> produtos = null;
                List<Supermercado> supermercados = null;
                ProdutoDoSupermercado item = null;
                List<ProdutoDaLista> produtosDeListas = null;

                using (var dados = new AcessoDB())
                {
                    produtos = string.IsNullOrEmpty(palavraDebusca) ? dados.GetAllProduto() : dados.BuscaProduto(palavraDebusca);
                    supermercados = dados.GetAllSupermercado();
                    produtosDeListas = dados.GetAllProdutoDaLista();
                }
                
                foreach (var prod in produtos)
                {
                    item = null;
                    produtoDentroDoRange = true;
                    produtoPertenceALista = false;

                    foreach (var sup in supermercados)
                    {
                        if (prod.IdSupermercado == sup.Id)
                        {
                            if (distancia != null
                                && int.Parse(distancia) < sup.Distancia)
                            {
                                produtoDentroDoRange = false;
                            }

                            if (idLista != null)
                            {
                                foreach (var prodLista in produtosDeListas)
                                {
                                    if (prodLista.IdListaDeCompra == idLista
                                        && prodLista.IdProduto == prod.Id)
                                    {
                                        produtoPertenceALista = true;
                                    }
                                }
                            }

                            if (produtoDentroDoRange && !produtoPertenceALista)
                            {
                                item = new ProdutoDoSupermercado()
                                {
                                    idProduto = prod.Id,
                                    NomeProduto = prod.Nome,
                                    NomeSupermercado = sup.Nome,
                                    MarcaProduto = prod.Marca,
                                    Preco = prod.Preco.ToString("C", CultureInfo.CurrentCulture),
                                    _imgProduto = prod._img != null ? prod._img : string.Empty,
                                    distancia = sup.Distancia
                                };
                            }

                            break;
                        }
                    }

                    if (item != null)
                    {
                        _produtos.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Command<ProdutoDoSupermercado> RemoveCommand
        {
            get
            {
                return new Command<ProdutoDoSupermercado>((Product) => {
                    _produtos.Remove(Product);
                });
            }
        }
    }
}
