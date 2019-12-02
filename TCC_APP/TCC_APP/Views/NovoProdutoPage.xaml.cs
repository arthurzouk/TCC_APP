using System;
using System.Collections.Generic;
using System.Globalization;
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
        //public ProdutoDoSupermercado Item { get; set; }

        public NovoProdutoPage()
        {
            InitializeComponent();
            //Item = new ProdutoDoSupermercado
            //{
            //    NomeProduto = "Informe o produto",
            //    MarcaProduto = "Informe a Marca",
            //    Preco = 0
            //    //Quantidade = "2"
            //};

            using (var dados = new AcessoDB())
            {
                supPicker.ItemsSource = dados.GetAllSupermercado();
            }

            int preco = 0;

            precoEntry.Text = preco.ToString("C", CultureInfo.CurrentCulture);

            BindingContext = this;
        }

        void Save_Clicked(object sender, EventArgs e)
        {
            Supermercado supermercado = null;

            try
            {
                supermercado = supPicker.SelectedItem as Supermercado;

                if (supermercado == null
                    || string.IsNullOrEmpty(supermercado.Id)
                    || string.IsNullOrWhiteSpace(supermercado.Id))
                {
                    DisplayAlert("Erro ao salvar novo produto.", "Por favor escolha um supermercado", "OK");
                    return;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro ao salvar novo produto.", "Por favor escolha um supermercado", "OK");
                return;
            }

            if (nomeEntry.Text == "Informe o produto"
                || string.IsNullOrEmpty(nomeEntry.Text)
                || string.IsNullOrWhiteSpace(nomeEntry.Text))
            {
                DisplayAlert("Erro ao salvar novo produto.", "Por favor informe um produto", "OK");
                return;
            }
            else if (marcaEntry.Text == "Informe a Marca"
                || string.IsNullOrEmpty(marcaEntry.Text)
                || string.IsNullOrWhiteSpace(marcaEntry.Text))
            {
                DisplayAlert("Erro ao salvar novo produto.", "Por favor informe a marca do produto", "OK");
                return;
            }
            else if (precoEntry.Text == "R$ 0,00")
            {
                DisplayAlert("Erro ao salvar novo produto", "Por favor informe o preço do produto", "OK");
                return;
            }


            Produto novoProduto = new Produto
            {
                Id = Guid.NewGuid().ToString(),
                Marca = marcaEntry.Text,
                Nome = nomeEntry.Text,
                Preco = double.Parse(precoEntry.Text.Replace("R$", "")),
                IdSupermercado = supermercado.Id,
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

        private void OnTextPriceChanged(object sender, EventArgs e)
        {
            double valor = 0;
            Entry entry = (Entry)sender;

            string[] aux = entry.Text.Replace("R$", "").Split(',');

            if (aux.Length > 1)
            {
                if (aux[1].Length > 2 && aux[1][aux.Length - 1] != 0)
                {
                    string centavos = aux[1];

                    if (centavos == "0")
                    {
                        aux[0] = aux[0] + centavos[0].ToString();
                        aux[1] = centavos.Remove(0, 1);
                    }
                    //if (centavos[0] == '0')
                    //{
                    //    aux[1] = centavos.Remove(0,1);
                    //}
                    else
                    {
                        aux[0] = aux[0] + centavos[0];
                        aux[1] = centavos.Remove(0, 1);
                    }
                }
                else if (aux[1].Length == 1)
                {
                    string reais = aux[0];

                    aux[1] = reais[reais.Length - 1].ToString() + aux[1];
                    aux[0] = aux[0].Remove(aux[0].Length - 1, 1);
                }

                valor = double.Parse(aux[0] + "," + aux[1]);
            }
            else
            {
                valor = double.Parse(entry.Text.Replace("R$", ""));
            }            

            entry.Text = valor.ToString("C", CultureInfo.CurrentCulture);
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}