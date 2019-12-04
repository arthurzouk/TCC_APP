using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC_APP.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TCC_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NovoSupermercadoPage : ContentPage
    {
        public NovoSupermercadoPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        void Save_Clicked(object sender, EventArgs e)
        {
            if (NomeEntry.Text == "Novo Supermercado"
                || string.IsNullOrEmpty(NomeEntry.Text)
                || string.IsNullOrWhiteSpace(NomeEntry.Text))
            {
                DisplayAlert("Erro ao salvar novo supermercado.", "Por favor informe um supermercado", "OK");
                return;
            }
            else if (BairroEntry.Text == "Bairro"
                || string.IsNullOrEmpty(BairroEntry.Text)
                || string.IsNullOrWhiteSpace(BairroEntry.Text))
            {
                DisplayAlert("Erro ao salvar novo supermercado.", "Por favor informe um bairro", "OK");
                return;
            }
            else if (CidadeEntry.Text == "Cidade"
               || string.IsNullOrEmpty(CidadeEntry.Text)
               || string.IsNullOrWhiteSpace(CidadeEntry.Text))
            {
                DisplayAlert("Erro ao salvar novo supermercado.", "Por favor informe uma cidade", "OK");
                return;
            }
            else if (DistanciaEntry.Text == "0"
               || string.IsNullOrEmpty(DistanciaEntry.Text)
               || string.IsNullOrWhiteSpace(DistanciaEntry.Text))
            {
                DisplayAlert("Erro ao salvar novo supermercado.", "Por favor informe uma distancia", "OK");
                return;
            }


            Supermercado novoSupermercado = new Supermercado
            {
                Id = Guid.NewGuid().ToString(),
                Nome = NomeEntry.Text,
                Bairro = BairroEntry.Text,
                Cidade = CidadeEntry.Text,
                Distancia = int.Parse(DistanciaEntry.Text)
            };

            using (var dados = new AcessoDB())
            {
                dados.InserirSupermercado(novoSupermercado);
            }

            Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}