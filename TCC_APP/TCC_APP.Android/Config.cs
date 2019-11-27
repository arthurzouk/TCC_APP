using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: Dependency(typeof(TCC_APP.Droid.Config))]
namespace TCC_APP.Droid
{
    public class Config : IConfig
    {
        private string _diretorioSQLite;
        public string DiretorioSQLite
        {
            get
            {
                if (string.IsNullOrEmpty(_diretorioSQLite))
                {
                    _diretorioSQLite = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }
                return _diretorioSQLite;
            }
        }
    }
}