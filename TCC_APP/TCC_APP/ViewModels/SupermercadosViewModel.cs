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
    class SupermercadosViewModel : BaseViewModel
    {
        public ObservableCollection<Supermercado> Supermercados { get; set; }
        public Command LoadItemsCommand { get; set; }

        public SupermercadosViewModel(string palavraDebusca = null)
        {
            Title = "Supermercados";
            Supermercados = new ObservableCollection<Supermercado>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(palavraDebusca));
        }

        async Task ExecuteLoadItemsCommand(string palavraDebusca)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Supermercados.Clear();

                List<Supermercado> items = null;

                using (var dados = new AcessoDB())
                {
                    items = string.IsNullOrEmpty(palavraDebusca) ? dados.GetAllSupermercado() : dados.BuscaSupermercado(palavraDebusca);
                }

                foreach (var item in items)
                {
                    Supermercados.Add(item);
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
