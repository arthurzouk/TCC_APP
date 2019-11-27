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
    class ProdutosViewModel : BaseViewModel
    {
        public ObservableCollection<Produto> Produtos { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ProdutosViewModel()
        {
            Title = "Produtos";
            Produtos = new ObservableCollection<Produto>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NovoProdutoPage, Produto>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Produto;
                Produtos.Add(newItem);
                await ProdutoDataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Produtos.Clear();
                var items = await ProdutoDataStore.GetItemsAsync("teste");
                foreach (var item in items)
                {
                    Produtos.Add(item);
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
