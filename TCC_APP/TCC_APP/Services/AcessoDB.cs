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
            conexaoSQLite.CreateTable<Supermercado>();
            conexaoSQLite.CreateTable<ListaDeCompra>();
            conexaoSQLite.CreateTable<ProdutoDaLista>();
            conexaoSQLite.CreateTable<Compra>();
            conexaoSQLite.CreateTable<ProdutoDaCompra>();
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
        internal void DeletarProduto(string idProduto)
        {
            conexaoSQLite.Table<Produto>().Delete(x => x.Id == idProduto);
        }
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
        internal List<Produto> GetAllEqualProduto(string nomeProduto)
        {
            return conexaoSQLite.Table<Produto>().Where(c => c.Nome.ToUpper() == nomeProduto.ToUpper()).ToList();
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
        internal void DeletarSupermercado(string id)
        {
            conexaoSQLite.Table<Supermercado>().Delete(x => x.Id == id);
        }
        #endregion
        #region Gets Supermercado
        public Supermercado GetSupermercado(string id)
        {
            return conexaoSQLite.Table<Supermercado>().FirstOrDefault(c => c.Id == id);
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


        #region CRUD ProdutoDaLista

        internal void InserirProdutoDaLista(ProdutoDaLista novoProdutoDaLista)
        {
            conexaoSQLite.Insert(novoProdutoDaLista);
        }
        public void AtualizarProdutoDaLista(ProdutoDaLista produtoDaLista)
        {
            conexaoSQLite.Update(produtoDaLista);
        }
        public void DeletarProdutoDaLista(string idProdutoDaLista)
        {
            conexaoSQLite.Table<ProdutoDaLista>().Delete(x => x.Id == idProdutoDaLista);
        }

        #endregion
        #region Gets ProdutoDaLista
        public ProdutoDaLista GetProdutoDaLista(string id)
        {
            //modificar criterio de busca
            return conexaoSQLite.Table<ProdutoDaLista>().FirstOrDefault(c => c.Id == id);
        }
        public List<ProdutoDaLista> GetAllProdutoDaLista(string idLista = null)
        {
            return idLista != null ? conexaoSQLite.Table<ProdutoDaLista>().Where(c => c.IdListaDeCompra == idLista).ToList() :
                conexaoSQLite.Table<ProdutoDaLista>().OrderBy(c => c.Id).ToList();
        }
        #endregion


        #region CRUD ProdutoDoSupermercado
        internal void inserirProdutoDoSupermercado(ProdutoDoSupermercado novoProdutoDoSupermercado)
        {
            conexaoSQLite.Insert(novoProdutoDoSupermercado);
        }
        public void DeletarProdutoDoSupermercado(string idSupermercado, string idProduto)
        {
            return;
            //conexaoSQLite.Table<ProdutoDoSupermercado>().Delete(x => x.IdSupermercado == idSupermercado && x.IdProduto == idProduto);
        }
        #endregion
        #region GET ProdutoDoSupermercado
        internal List<ProdutoDoSupermercado> GetAllProdutoDoSupermercado(string idSupermercado)
        {
            return null;
            //return conexaoSQLite.Table<ProdutoDoSupermercado>().Where(c => c.IdSupermercado == idSupermercado).ToList();
        }
        #endregion

        #region CRUD Compra
        internal void InserirCompra(Compra novaCompra)
        {
            conexaoSQLite.Insert(novaCompra);
        }
        #endregion
        #region GET Compra
        internal List<Compra> GetAllCompra()
        {
            return conexaoSQLite.Table<Compra>().OrderBy(c => c.Id).ToList();
        }
        #endregion

        #region CRUD ProdutoDaCompra
        internal void InserirProdutoDaCompra(ProdutoDaCompra novoProdutoDaCompra)
        {
            conexaoSQLite.Insert(novoProdutoDaCompra);
        }
        #endregion
        #region GET ProdutoDaCompra
        internal List<ProdutoDaCompra> GetAllProdutoDaCompra(/*string idCompra*/)
        {
            return conexaoSQLite.Table<ProdutoDaCompra>().ToList();
                //Where(c => c.IdCompra == idCompra).ToList();
        }
        #endregion
        public void Dispose()
        {
            conexaoSQLite.Dispose();
        }
    }
}
