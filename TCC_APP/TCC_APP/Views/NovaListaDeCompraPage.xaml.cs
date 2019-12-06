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
        public NovaListaDeCompraPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {

            if (NomeEntry.Text == "Informe o nome da lista"
                || string.IsNullOrEmpty(NomeEntry.Text)
                || string.IsNullOrWhiteSpace(NomeEntry.Text))
            {
                await DisplayAlert("Erro ao salvar nova lista.", "Por favor informe um nome para a lista", "OK");
                return;
            }

            ListaDeCompra novaLista = new ListaDeCompra
            {
                Id = Guid.NewGuid().ToString(),
                Nome = NomeEntry.Text,
                Descricao = descricaoEntry.Text
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