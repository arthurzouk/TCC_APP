using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TCC_APP.Models;

namespace TCC_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NovaListaDeCompraPage : ContentPage
    {
        public ListaDeCompra Item { get; set; }

        public NovaListaDeCompraPage()
        {
            InitializeComponent();

            Item = new ListaDeCompra
            {
                Nome = "Nova Lista",
                Descricao = "Descrição"
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            ListaDeCompra novaLista = new ListaDeCompra
            {
                Id = Guid.NewGuid().ToString(),
                Nome = Item.Nome,
                Descricao = Item.Descricao
            };

            using (var dados = new AcessoDB())
            {
                dados.InserirListaDeCompra(novaLista);
            }

            //MessagingCenter.Send(this, "AddItem", novaLista);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}