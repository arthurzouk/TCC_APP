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
        public Supermercado Item { get; set; }

        public NovoSupermercadoPage()
        {
            InitializeComponent();
            Item = new Supermercado
            {
                Nome = "Novo Supermercado",
                Distancia = 0
            };

            BindingContext = this;
        }

        void Save_Clicked(object sender, EventArgs e)
        {
            Supermercado novoSupermercado = new Supermercado
            {
                Id = Guid.NewGuid().ToString(),
                Nome = Item.Nome,
                Distancia = Item.Distancia
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