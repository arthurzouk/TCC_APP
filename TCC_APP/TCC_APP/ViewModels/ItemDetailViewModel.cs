using System;

using TCC_APP.Models;

namespace TCC_APP.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ListaDeCompra ItemLista { get; set; }
        public Produto ItemProduto { get; set; }
        public Supermercado ItemSupermercado { get; set; }

        public ItemDetailViewModel(ListaDeCompra item = null)
        {
            Title = item?.Nome;
            ItemLista = item;
        }

        public ItemDetailViewModel(Produto item = null)
        {
            Title = item?.Nome;
            ItemProduto = item;
        }

        public ItemDetailViewModel(Supermercado item = null)
        {
            Title = item?.Nome;
            ItemSupermercado = item;
        }
    }
}
