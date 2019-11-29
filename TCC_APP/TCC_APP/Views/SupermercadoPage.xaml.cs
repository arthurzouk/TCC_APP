using System;
using System.Collections.Generic;
using System.Diagnostics;
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
	public partial class SupermercadoPage : ContentPage
	{
        SupermercadosViewModel viewModel;

        public SupermercadoPage ()
		{
            InitializeComponent();
            BindingContext = viewModel = new SupermercadosViewModel();
        }

        void OnTextChanged(object sender, EventArgs args)
        {
            SearchBar searchBar = (SearchBar)sender;

            BindingContext = viewModel = new SupermercadosViewModel(searchBar.Text);

            viewModel.LoadItemsCommand.Execute(null);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Supermercado;
            if (item == null)
                return;

            await Navigation.PushAsync(new DetalhesDoSupermercadoPage(new ItemDetailViewModel(item), item.Nome, item.Id));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NovoSupermercadoPage()));
        }

        async void Remove_Clicked(object sender, EventArgs args)
        {
            try
            {
                var button = sender as Button;
                var listaDeCompra = button.BindingContext as Supermercado;
                var vm = BindingContext as SupermercadosViewModel;
                vm.RemoveCommand.Execute(listaDeCompra);

                using (var dados = new AcessoDB())
                {
                    dados.DeletarSupermercado(listaDeCompra.Id);
                }

                await DisplayAlert("Supermercado deletado", "A supermercado " + listaDeCompra.Nome + " foi deletado.", "OK");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}