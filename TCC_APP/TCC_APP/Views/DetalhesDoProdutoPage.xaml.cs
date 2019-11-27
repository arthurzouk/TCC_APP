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
	public partial class DetalhesDoProdutoPage : ContentPage
	{
        ItemDetailViewModel viewModel;

        public DetalhesDoProdutoPage (ItemDetailViewModel viewModel)
		{
			InitializeComponent ();
            BindingContext = this.viewModel = viewModel;
        }

        public DetalhesDoProdutoPage()
        {
            InitializeComponent();

            var item = new Produto
            {
                Nome = "Novo Produto"
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}