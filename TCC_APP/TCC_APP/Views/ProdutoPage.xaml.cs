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
	public partial class ProdutoPage : ContentPage
	{
        ProdutosViewModel viewModel;

        public ProdutoPage()
		{
			InitializeComponent ();
            BindingContext = viewModel = new ProdutosViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Produto;
            if (item == null)
                return;

            await Navigation.PushAsync(new DetalhesDoProdutoPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NovoProdutoPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Produtos.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}