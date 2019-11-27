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
    public partial class NovoProdutoPage : ContentPage
    {
        public Produto Item { get; set; }

        public NovoProdutoPage()
        {
            InitializeComponent();
            Item = new Produto
            {
                Nome = "Novo Produto",
                Marca = "qualquer um"
                //Preco = "R$ 00,00"
                //Quantidade = "2"
            };

            BindingContext = this;
        }

        void Save_Clicked(object sender, EventArgs e)
        {
            Produto novoProduto = new Produto
            {
                Id = Guid.NewGuid().ToString(),
                Marca = string.Empty,
                Nome = Item.Nome,
                _img = string.Empty
            };

            using (var dados = new AcessoDB())
            {
                dados.InserirProduto(novoProduto);
            }

            //MessagingCenter.Send(this, "AddItem", Item);
            //await Navigation.PopModalAsync();
            Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}