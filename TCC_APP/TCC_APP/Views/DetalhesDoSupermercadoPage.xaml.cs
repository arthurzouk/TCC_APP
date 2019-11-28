using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC_APP.Models;
using TCC_APP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TCC_APP.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalhesDoSupermercadoPage : ContentPage
	{
        ItemDetailViewModel viewModel;

        public DetalhesDoSupermercadoPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        public DetalhesDoSupermercadoPage()
        {
            InitializeComponent();

            var item = new Supermercado
            {
                Nome = "Novo Supermercado"
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}