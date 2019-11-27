using System.Collections.Generic;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TCC_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RelatorioLista : ContentPage
    {
        public RelatorioLista()
        {
            InitializeComponent();

            Grafico.Chart = new Microcharts.LineChart() { Entries = entries };
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


    }
}
