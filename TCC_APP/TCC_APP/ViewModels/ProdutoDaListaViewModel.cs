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
    class ProdutoDaListaViewModel : BaseViewModel
    {
        public ObservableCollection<ProdutosDaLista_Result> ProdutoDaLista { get; set; }
        public Command LoadItemsCommand { get; set; }
        public string id;

        public ProdutoDaListaViewModel(string idDeBusca)
        {
            Title = "Produtos da Lista";
            this.id = idDeBusca;
            ProdutoDaLista = new ObservableCollection<ProdutosDaLista_Result>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<DetalhesDaListaDeCompraPage, ProdutosDaLista_Result>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as ProdutosDaLista_Result;
                ProdutoDaLista.Add(newItem);
                await ProdutoDaListaDataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ProdutoDaLista.Clear();

                _stringDeBusca = id;
                var items = await ProdutoDaListaDataStore.GetItemsAsync(_stringDeBusca);
                foreach (var item in items)
                {
                    ProdutoDaLista.Add(item);
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
    }
}
