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
    class ProdutoDaListaViewModel : BaseViewModel
    {
        public ObservableCollection<DetalhesProdutoDaLista> _produtoDaLista { get; set; }
        public Command LoadItemsCommand { get; set; }
        public string idDeBusca;

        public Command DecrementCounterCommand { get; set; }
        public Command IncrementCounterCommand { get; set; }

        public ProdutoDaListaViewModel(string idDeBusca, string palavraDebusca = null)
        {
            Title = "Produtos da Lista";
            this.idDeBusca = idDeBusca;
            _produtoDaLista = new ObservableCollection<DetalhesProdutoDaLista>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(palavraDebusca));

            DecrementCounterCommand = new Command<DetalhesProdutoDaLista>(async (key) =>
            {
                if (int.Parse(key.QtdProduto) > 0)
                {
                    DetalhesProdutoDaLista prodList = null;

                    foreach (var item in _produtoDaLista)
                    {
                        if (item.idProdutoDaLista == key.idProdutoDaLista)
                        {
                            prodList = item;
                        }
                    }

                    //var item = _produtoDaLista.(x => x.Id == key.Id);
                    int idx = _produtoDaLista.IndexOf(prodList);
                    int.TryParse(key.QtdProduto, out int counter);
                    counter--;
                    //                key.item.Text = counter.ToString();
                    prodList = key;
                    _produtoDaLista[idx].QtdProduto = counter.ToString();

                    using (var dados = new AcessoDB())
                    {
                        var aux = dados.GetProdutoDaLista(prodList.idProdutoDaLista);

                        ProdutoDaLista attProdList = new ProdutoDaLista
                        {
                            Id = aux.Id,
                            IdListaDeCompra = aux.IdListaDeCompra,
                            IdProduto = aux.IdProduto,
                            qtdProduto = key.QtdProduto
                        };

                        dados.AtualizarProdutoDaLista(attProdList);

                        await ExecuteLoadItemsCommand();
                    }
                    
                }
            });

            IncrementCounterCommand = new Command<DetalhesProdutoDaLista>(async (key) =>
            {
                if (int.Parse(key.QtdProduto) < 100)
                {
                    DetalhesProdutoDaLista prodList = null;

                    foreach (var item in _produtoDaLista)
                    {
                        if (item.idProdutoDaLista == key.idProdutoDaLista)
                        {
                            prodList = item;
                        }
                    }

                    //var item = _produtoDaLista.FirstOrDefault(x => x.item.Id == key.item.Id);
                    int idx = _produtoDaLista.IndexOf(prodList);
                    int.TryParse(key.QtdProduto, out int counter);
                    counter++;
                    //                key.item.Text = counter.ToString();
                    prodList = key;
                    _produtoDaLista[idx].QtdProduto = counter.ToString();

                    using (var dados = new AcessoDB())
                    {
                        var aux = dados.GetProdutoDaLista(prodList.idProdutoDaLista);

                        ProdutoDaLista attProdList = new ProdutoDaLista
                        {
                            Id = aux.Id,
                            IdListaDeCompra = aux.IdListaDeCompra,
                            IdProduto = aux.IdProduto,
                            qtdProduto = key.QtdProduto
                        };

                        dados.AtualizarProdutoDaLista(attProdList);

                        await ExecuteLoadItemsCommand();
                    }
                }
            });
        }

        async Task ExecuteLoadItemsCommand(string palavraDebusca = null)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                _produtoDaLista.Clear();

                List<ProdutoDaLista> tabelaRelacional = null;
                Produto produto = null;
                Supermercado supermercado = null;


                using (var dados = new AcessoDB())
                {
                    tabelaRelacional = dados.GetAllProdutoDaLista(idDeBusca);
                }

                foreach (var item in tabelaRelacional)
                {
                    using (var dados = new AcessoDB())
                    {
                        produto = dados.GetProduto(item.IdProduto);
                    }

                    if (produto != null && (string.IsNullOrEmpty(palavraDebusca)
                       || produto.Nome.ToUpper().Contains(palavraDebusca.ToUpper())))
                    {
                        using (var dados = new AcessoDB())
                        {
                            supermercado = dados.GetSupermercado(produto.IdSupermercado);
                        }

                        DetalhesProdutoDaLista produtoDaLista = new DetalhesProdutoDaLista()
                        {
                            idProdutoDaLista = item.Id,
                            nomeProduto = produto.Nome,
                            marcaProduto = produto.Marca,
                            QtdProduto = item.qtdProduto != null ? item.qtdProduto : "1",
                            nomeSupermercado = supermercado.Nome,
                            valorTotalProduto = (produto.Preco * int.Parse(item.qtdProduto)).ToString("C", CultureInfo.CurrentCulture)
                        };

                        _produtoDaLista.Add(produtoDaLista);
                    }

                    produto = null;
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

        public Command<DetalhesProdutoDaLista> RemoveCommand
        {
            get
            {
                return new Command<DetalhesProdutoDaLista>((Product) =>
                {
                    _produtoDaLista.Remove(Product);
                });
            }
        }
    }
}
