using System;
using System.Collections.Generic;
using SkiaSharp;
using TCC_APP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TCC_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RelatorioLista : ContentPage
    {
        HistoricoDeComprasViewModel viewModel;

        public RelatorioLista()
        {
            InitializeComponent();

            //Grafico.Chart = new Microcharts.LineChart() { Entries = entries };
        }

        List<Microcharts.Entry> entries = new List<Microcharts.Entry>
        {
            new Microcharts.Entry(200)
            {
                Label = "Janeiro",
                ValueLabel = "R$ 205,00",
                Color = SKColor.Parse("#266489")
            },
            new Microcharts.Entry(250)
            {
                Label = "Fevereiro",
                ValueLabel = "R$ 235,00",
                Color = SKColor.Parse("#68B9C0")
            },
            new Microcharts.Entry(100)
            {
                Label = "Março",
                ValueLabel = "R$ 215,00",
                Color = SKColor.Parse("#90D585")
            },
            new Microcharts.Entry(150)
            {
                Label = "Abril",
                ValueLabel = "R$ 195,00",
                Color = SKColor.Parse("#e77e23")
            }
        };

        //void OnViewCellTapped(object sender, EventArgs e)
        //{
        //    _target.IsVisible = !_target.IsVisible;
        //    _viewCell.ForceUpdateSize();
        //}

        private void PckTipoRelatorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = pckTipoRelatorio.SelectedItem;

            if (picker.ToString() == "Histórico de compras")
            {
                tblHistoricoCompras.IsVisible = true;

                BindingContext = viewModel = new HistoricoDeComprasViewModel();

                viewModel.LoadItemsCommand.Execute(null);
            }
            else if (picker.ToString() == "Variação de preço de produto")
            {
                tblHistoricoCompras.IsVisible = false;
            }
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //var item = args.SelectedItem as ListaDeCompra;
            //if (item == null)
            //    return;

            //await Navigation.PushAsync(new DetalhesDaListaDeCompraPage(new ItemDetailViewModel(item), item.Id));

            //// Manually deselect item.
            //ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel != null)
            {
                viewModel.LoadItemsCommand.Execute(null);
            }
        }
    }
}
