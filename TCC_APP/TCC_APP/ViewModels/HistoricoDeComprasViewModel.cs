using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TCC_APP.Models;
using Xamarin.Forms;

namespace TCC_APP.ViewModels
{
    class HistoricoDeComprasViewModel : BaseViewModel
    {
        public ObservableCollection<Compra> _compras { get; set; }
        public Command LoadItemsCommand { get; set; }

        public HistoricoDeComprasViewModel()
        {
            Title = "Histórico de Compras";
            _compras = new ObservableCollection<Compra>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                _compras.Clear();

                List<Compra> items = null;

                using (var dados = new AcessoDB())
                {
                    items = dados.GetAllCompra();
                }

                foreach (var item in items)
                {
                    _compras.Add(item);
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
