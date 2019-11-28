using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TCC_APP.Models;
using Xamarin.Forms;

namespace TCC_APP
{
    public class AcessoDB : IDisposable
    {
        private SQLiteConnection conexaoSQLite;
        public AcessoDB()
        {
            var config = DependencyService.Get<IConfig>();
            conexaoSQLite = new SQLiteConnection(Path.Combine(config.DiretorioSQLite, "TCC_APP.db3"));
            conexaoSQLite.CreateTable<Usuario>();
            conexaoSQLite.CreateTable<Produto>();
            conexaoSQLite.CreateTable<ListaDeCompra>();
            conexaoSQLite.CreateTable<ProdutoDaLista>();
            conexaoSQLite.CreateTable<HistoricoDeCompra>();
            conexaoSQLite.CreateTable<Supermercado>();
        }
        #region CRUD padrão usuário
        public void InserirUsuario(Usuario usuario)
        {
            conexaoSQLite.Insert(usuario);
        }
        //public void AtualizarUsuario(Usuario usuario)
        //{
        //    conexaoSQLite.Update(usuario);
        //}
        //public void DeletarUsuario(Usuario usuario)
        //{
        //    conexaoSQLite.Delete(usuario);
        //}
        public Usuario GetUsuario(string loginUsuario)
        {
            return conexaoSQLite.Table<Usuario>().FirstOrDefault(c => c.LoginUsuario == loginUsuario);
        }

        public List<Usuario> GetUsuarios()
        {
            return conexaoSQLite.Table<Usuario>().OrderBy(c => c.LoginUsuario).ToList();
        }
        #endregion

        #region CRUD produto

        public void InserirProduto(Produto produto)
        {
            conexaoSQLite.Insert(produto);
        }
        //public void AtualizarUsuario(Usuario usuario)
        //{
        //    conexaoSQLite.Update(usuario);
        //}
        //public void DeletarUsuario(Usuario usuario)
        //{
        //    conexaoSQLite.Delete(usuario);
        //}
        #endregion
        #region Gets Produto
        public Produto GetProduto(string id)
        {
            return conexaoSQLite.Table<Produto>().FirstOrDefault(c => c.Id == id);
        }
        internal List<Produto> BuscaProduto(string palavraDebusca)
        {
            return conexaoSQLite.Table<Produto>().Where(c => c.Nome.ToUpper().Contains(palavraDebusca.ToUpper())).ToList();
        }
        public List<Produto> GetAllProduto()
        {
            return conexaoSQLite.Table<Produto>().OrderBy(c => c.Id).ToList();
        }
        #endregion

        #region CRUD Supermercado

        public void InserirSupermercado(Supermercado supermercado)
        {
            conexaoSQLite.Insert(supermercado);
        }
        //public void AtualizarUsuario(Usuario usuario)
        //{
        //    conexaoSQLite.Update(usuario);
        //}
        //public void DeletarUsuario(Usuario usuario)
        //{
        //    conexaoSQLite.Delete(usuario);
        //}
        #endregion
        #region Gets Supermercado
        public Produto GetSupermercado(string id)
        {
            return conexaoSQLite.Table<Produto>().FirstOrDefault(c => c.Id == id);
        }
        internal List<Supermercado> BuscaSupermercado(string palavraDebusca)
        {
            return conexaoSQLite.Table<Supermercado>().Where(c => c.Nome.ToUpper().Contains(palavraDebusca.ToUpper())).ToList();
        }
        public List<Supermercado> GetAllSupermercado()
        {
            return conexaoSQLite.Table<Supermercado>().OrderBy(c => c.Id).ToList();
        }
        #endregion

        #region CRUD ProdutoDaLista

        internal void inserirProdutoDaLista(ProdutoDaLista novoProdutoDaLista)
        {
            conexaoSQLite.Insert(novoProdutoDaLista);
        }
        public void DeletarProdutoDaLista(string idLista, string idProduto)
        {
            conexaoSQLite.Table<ProdutoDaLista>().Delete(x => x.IdListaDeCompra == idLista && x.IdProduto == idProduto);
        }

        #endregion
        #region Gets ProdutoDaLista
        public ProdutoDaLista GetProdutoDaLista(string id)
        {
            //modificar criterio de busca
            return conexaoSQLite.Table<ProdutoDaLista>().FirstOrDefault(c => c.Id == id);
        }
        public List<ProdutoDaLista> GetAllProdutoDaLista(string idLista)
        {
            return conexaoSQLite.Table<ProdutoDaLista>().Where(c => c.IdListaDeCompra == idLista).ToList();
        }
        #endregion

        #region CRUD ListaDeCompra

        public void InserirListaDeCompra(ListaDeCompra lista)
        {
            conexaoSQLite.Insert(lista);
        }
        public void DeletarListaDeCompra(string idLista)
        {
            conexaoSQLite.Table<ListaDeCompra>().Delete(x => x.Id == idLista);
        }

        #endregion
        #region Gets ListaDeCompra
        public ListaDeCompra GetListaDeCompra(string id)
        {
            return conexaoSQLite.Table<ListaDeCompra>().FirstOrDefault(c => c.Id == id);
        }
        internal List<ListaDeCompra> BuscaListaDeCompra(string busca)
        {
            return conexaoSQLite.Table<ListaDeCompra>().Where(c => c.Nome.ToUpper().Contains(busca.ToUpper())).ToList();
        }
        public List<ListaDeCompra> GetAllListaDeCompra()
        {
            return conexaoSQLite.Table<ListaDeCompra>().OrderBy(c => c.Id).ToList();
        }
        #endregion

        public void Dispose()
        {
            conexaoSQLite.Dispose();
        }
    }
}
