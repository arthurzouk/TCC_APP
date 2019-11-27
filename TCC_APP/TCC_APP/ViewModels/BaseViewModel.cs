using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using TCC_APP.Models;
using TCC_APP.Services;

namespace TCC_APP.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public string _stringDeBusca;

        public IDataStore<ListaDeCompra> ListaDataStore => DependencyService.Get<IDataStore<ListaDeCompra>>() ?? new ListaMockDataStore();
        public IDataStore<Produto> ProdutoDataStore => DependencyService.Get<IDataStore<Produto>>() ?? new ProdutoMockDataStore();
        public IDataStore<ProdutosDaLista_Result> ProdutoDaListaDataStore => DependencyService.Get<IDataStore<ProdutosDaLista_Result>>() ?? new ProdutoDaListaMockDataStore(_stringDeBusca);

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
