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

        public ProdutosViewModel(string palavraDebusca = null, string distancia = null, string idLista = null, string nomeProduto = null, string marcaProduto = null, double precoUnidade = 0)
        {
            Title = "Produtos";
            _produtos = new ObservableCollection<ProdutoDoSupermercado>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(palavraDebusca, distancia, idLista, nomeProduto, marcaProduto, precoUnidade));
        }

        async Task ExecuteLoadItemsCommand(string palavraDebusca, string distancia, string idLista, string nomeProduto, string marcaProduto, double precoUnidade)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                _produtos.Clear();

                bool produtoDentroDoRange;
                bool produtoPertenceALista;
                List<Produto> produtos = null;
                List<Produto> produtosMesmaMarca = null;
                List<Supermercado> supermercados = null;
                ProdutoDoSupermercado item = null;
                List<ProdutoDaLista> produtosDeListas = null;

                using (var dados = new AcessoDB())
                {
                    if (!string.IsNullOrEmpty(palavraDebusca))
                    {
                        produtos = dados.BuscaProduto(palavraDebusca);
                    }
                    else if (!string.IsNullOrEmpty(nomeProduto))
                    {
                        produtos = dados.GetAllEqualProduto(nomeProduto);
                        produtosMesmaMarca = dados.GetALLMesmaMarcaProduto(marcaProduto);

                        bool jaContido;

                        foreach (var p in produtosMesmaMarca)
                        {
                            jaContido = false;

                            foreach (var pr in produtos)
                            {
                                if (p.Id == pr.Id)
                                {
                                    jaContido = true;
                                }    
                            }

                            if (!jaContido)
                            {
                                produtos.Add(p);
                            }
                        }
                    }
                    else
                    {
                        produtos = dados.GetAllProduto();
                    }
                    //produtos = string.IsNullOrEmpty(palavraDebusca) ? dados.GetAllProduto() : dados.BuscaProduto(palavraDebusca);
                    supermercados = dados.GetAllSupermercado();
                    produtosDeListas = dados.GetAllProdutoDaLista(idLista);
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
                                    using (var dados = new AcessoDB())
                                    {
                                        var auxProduto = dados.GetProduto(prodLista.IdProduto);

                                        if (
                                            //Excluir da lista produto do mesmo ID ou produtos do mesmo nome se o nome produto for nulo
                                            (prodLista.IdListaDeCompra == idLista
                                        && prodLista.IdProduto == prod.Id)
                                        || (prod.Nome.ToUpper() == auxProduto.Nome.ToUpper()
                                        && nomeProduto == null)
                                        )
                                        {
                                            produtoPertenceALista = true;
                                        }
                                    }
                                }
                            }

                            if (produtoDentroDoRange && !produtoPertenceALista)
                            {
                                double diferenca = 0;
                                string corDiferenca = string.Empty;

                                if (distancia != null
                                    && precoUnidade != 0)
                                {
                                    diferenca = precoUnidade - prod.Preco;

                                    corDiferenca = diferenca == 0 ? "black" : diferenca > 0 ? "green" : "red";
                                }


                                item = new ProdutoDoSupermercado()
                                {
                                    idProduto = prod.Id,
                                    NomeProduto = prod.Nome,
                                    NomeSupermercado = sup.Nome,
                                    MarcaProduto = prod.Marca,
                                    Preco = prod.Preco.ToString("C", CultureInfo.CurrentCulture),
                                    _imgProduto = prod._img != null ? prod._img : string.Empty,
                                    distancia = sup.Distancia,
                                    Diferenca = diferenca == 0 ? "Mesmo valor!" : diferenca > 0 ? "Economize: " + diferenca.ToString("C", CultureInfo.CurrentCulture) : "Prejuizo: " + diferenca.ToString("C", CultureInfo.CurrentCulture),
                                    CorDiferenca = string.IsNullOrEmpty(corDiferenca) ? "black" : corDiferenca
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
                return new Command<ProdutoDoSupermercado>((Product) =>
                {
                    _produtos.Remove(Product);
                });
            }
        }
    }
}
