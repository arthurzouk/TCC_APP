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

        public ProdutoDaListaViewModel(string idDeBusca, string palavraDebusca = null)
        {
            Title = "Produtos da Lista";
            this.idDeBusca = idDeBusca;
            _produtoDaLista = new ObservableCollection<DetalhesProdutoDaLista>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(palavraDebusca));
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
                            QtdProduto = item.qtdProduto != null && int.Parse(item.qtdProduto) != 0 ? item.qtdProduto : "1",
                            nomeSupermercado = supermercado.Nome,
                            preco = produto.Preco.ToString("C", CultureInfo.CurrentCulture)
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
