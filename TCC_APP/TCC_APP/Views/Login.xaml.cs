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
	public partial class Login : ContentPage
    {
		public Login ()
		{
			InitializeComponent ();

            BtnLogin.Clicked += BtnLogin_Clicked;
            BtnCadastrar.Clicked += BtnCadastrar_Clicked;
        }

        async void BtnCadastrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register());
        }

        async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            var usuario = new Usuario
            {
                LoginUsuario = LoginUsuario.Text,
                Senha = Senha.Text
            };

            var isvalid = CredenciasCorretas(usuario);
            if (isvalid)
            {
                App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
                messageLabel.Text = "Usuário ou senha incorretos!";
                Senha.Text = string.Empty;
            }
        }

        bool CredenciasCorretas(Usuario usuario)
        {
            using (var dados = new AcessoDB())
            {
                var retorno = dados.GetUsuario(usuario.LoginUsuario);

                if (retorno != null)
                {
                    return usuario.Senha == retorno.Senha;
                }

                return false;
            }
        }
    }
}