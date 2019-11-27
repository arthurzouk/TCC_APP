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
	public partial class Register : ContentPage
	{
		public Register ()
		{
            InitializeComponent();

            btnCadastrar.Clicked += BtnCadastrar_Clicked;
        }

        async void BtnCadastrar_Clicked(object sender, EventArgs e)
        {
            var usuario = new Usuario()
            {
                LoginUsuario = LoginUsuario.Text,
                Senha = Senha.Text,
                Email = email.Text
            };

            var cadastrarSucesso = DadosValidos(usuario);
            if (cadastrarSucesso)
            {
                using (var dados = new AcessoDB())
                {
                    dados.InserirUsuario(usuario);
                }

                var rootPage = Navigation.NavigationStack.FirstOrDefault();
                if (rootPage != null)
                {
                    App.IsUserLoggedIn = true;
                    Navigation.InsertPageBefore(new MainPage(), Navigation.NavigationStack.First());
                    await Navigation.PopToRootAsync();
                }

            }
        }

        bool DadosValidos(Usuario usuario)
        {
            return (!string.IsNullOrWhiteSpace(usuario.LoginUsuario) &&
                !string.IsNullOrWhiteSpace(usuario.Senha) &&
                !string.IsNullOrWhiteSpace(usuario.Email) &&
                usuario.Email.Contains("@"));
        }
    }
}