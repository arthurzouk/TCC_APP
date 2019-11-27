﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TCC_APP.Models;
using TCC_APP.Views;
using Xamarin.Forms;

namespace TCC_APP.ViewModels
{
    class ProdutoDaListaViewModel : BaseViewModel
    {
        public ObservableCollection<Produto> ProdutoDaLista { get; set; }
        public Command LoadItemsCommand { get; set; }
        public string idDeBusca;

        public ProdutoDaListaViewModel(string idDeBusca)
        {
            Title = "Produtos da Lista";
            this.idDeBusca = idDeBusca;
            ProdutoDaLista = new ObservableCollection<Produto>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ProdutoDaLista.Clear();

                List<ProdutoDaLista> tabelaRelacional = null;
                Produto produto = null;


                using (var dados = new AcessoDB())
                {
                    tabelaRelacional = dados.GetAllProdutoDaLista(idDeBusca);
                }

                foreach (var item in tabelaRelacional)
                {
                    using (var dados = new AcessoDB())
                    {
                        produto = dados.GetProduto(item.IdProduto);
                    }

                    if (produto != null)
                    {
                        ProdutoDaLista.Add(produto);
                    }                    

                    produto = null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
