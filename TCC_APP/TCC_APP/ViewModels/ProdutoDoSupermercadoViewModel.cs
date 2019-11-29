using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TCC_APP.Models;
using TCC_APP.Views;
using Xamarin.Forms;

namespace TCC_APP.ViewModels
{
    class ProdutoDoSupermercadoViewModel : BaseViewModel
    {
        public ObservableCollection<Produto> ProdutoDoSupermercado { get; set; }
        public Command LoadItemsCommand { get; set; }
        public string idDeBusca;

        public ProdutoDoSupermercadoViewModel(string supermercado, string idDeBusca, string palavraDebusca = null)
        {
            Title = supermercado + " - Produtos";
            this.idDeBusca = idDeBusca;
            ProdutoDoSupermercado = new ObservableCollection<Produto>();
            LoadItemsCommand = new Command(async () => ExecuteLoadItemsCommand(palavraDebusca));
        }

        void ExecuteLoadItemsCommand(string palavraDebusca = null)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ProdutoDoSupermercado.Clear();

                List<ProdutoDoSupermercado> tabelaRelacional = null;
                Produto produto = null;


                using (var dados = new AcessoDB())
                {
                    tabelaRelacional = dados.GetAllProdutoDoSupermercado(idDeBusca);
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
                        ProdutoDoSupermercado.Add(produto);
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

        public Command<Produto> RemoveCommand
        {
            get
            {
                return new Command<Produto>((Product) =>
                {
                    ProdutoDoSupermercado.Remove(Product);
                });
            }
        }
    }
}
