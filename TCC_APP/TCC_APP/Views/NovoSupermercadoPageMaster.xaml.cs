using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TCC_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NovoSupermercadoPageMaster : ContentPage
    {
        public ListView ListView;

        public NovoSupermercadoPageMaster()
        {
            InitializeComponent();

            BindingContext = new NovoSupermercadoPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class NovoSupermercadoPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<NovoSupermercadoPageMenuItem> MenuItems { get; set; }
            
            public NovoSupermercadoPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<NovoSupermercadoPageMenuItem>(new[]
                {
                    new NovoSupermercadoPageMenuItem { Id = 0, Title = "Page 1" },
                    new NovoSupermercadoPageMenuItem { Id = 1, Title = "Page 2" },
                    new NovoSupermercadoPageMenuItem { Id = 2, Title = "Page 3" },
                    new NovoSupermercadoPageMenuItem { Id = 3, Title = "Page 4" },
                    new NovoSupermercadoPageMenuItem { Id = 4, Title = "Page 5" },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}