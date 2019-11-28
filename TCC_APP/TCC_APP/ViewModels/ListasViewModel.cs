using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using TCC_APP.Models;
using TCC_APP.Views;
using System.Collections.Generic;

namespace TCC_APP.ViewModels
{
    public class ListasViewModel : BaseViewModel
    {
        public ObservableCollection<ListaDeCompra> Listas { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ListasViewModel()
        {
            Title = "Listas de compra";
            Listas = new ObservableCollection<ListaDeCompra>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Listas.Clear();
                //var items = await ListaDataStore.GetItemsAsync("teste");

                List<ListaDeCompra> items = null;

                using (var dados = new AcessoDB())
                {
                    items = dados.GetAllListaDeCompra();
                }

                foreach (var item in items)
                {
                    Listas.Add(item);
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

        public Command<ListaDeCompra> RemoveCommand
        {
            get
            {
                return new Command<ListaDeCompra>((Product) => {
                    Listas.Remove(Product);
                });
            }
        }
    }
}