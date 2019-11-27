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

        }
        #region CRUD padrão usuário
        //public void InserirUsuario(Usuario usuario)
        //{
        //    conexaoSQLite.Insert(usuario);
        //}
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

        #region CRUD genérico
        public void Inserir(object objeto)
        {
            conexaoSQLite.Insert(objeto);
        }
        public void Atualizar(object objeto)
        {
            conexaoSQLite.Update(objeto);
        }
        public void Deletar(object objeto)
        {
            conexaoSQLite.Query<ListaDeCompra>("DELETE FROM [ListaDeCompra] WHERE [id] IS NULL ");
            //conexaoSQLite.Delete(objeto);
        }
        #endregion

        #region Gets ListaDeCompra
        public ListaDeCompra GetListaDeCompra(string id)
        {
            return conexaoSQLite.Table<ListaDeCompra>().FirstOrDefault(c => c.Id == id);
        }
        public List<ListaDeCompra> GetAllListaDeCompra()
        {
            return conexaoSQLite.Table<ListaDeCompra>().OrderBy(c => c.Id).ToList();
        }
        #endregion
        #region Gets Produto
        public Produto GetProduto(string id)
        {
            return conexaoSQLite.Table<Produto>().FirstOrDefault(c => c.Id == id);
        }
        public List<Produto> GetAllProduto()
        {
            return conexaoSQLite.Table<Produto>().OrderBy(c => c.Id).ToList();
        }
        #endregion

        #region Gets ProdutoDaLista
        public ProdutosDaLista_Result GetProdutoDaLista(string id)
        {
            //modificar criterio de busca
            return conexaoSQLite.Table<ProdutosDaLista_Result>().FirstOrDefault(c => c._quantidade == id);
        }
        public List<ProdutosDaLista_Result> GetAllProdutoDaLista(string idLista)
        {
            var q = conexaoSQLite.Query<ProdutosDaLista_Result>(
                "select p.Nome, p._img, pdl.Quantidade from ProdutoDaLista pdl"
                + " inner join Produto p"
                + " on pdl.IdProduto = p.Id where pdl.IdListaDeCompra = ?",
                idLista).ToList();
            return q; //.Select(x => new ProdutosDaLista_Result { _nomeProduto = x.Nome, _img = x._img, _quantidade = x._quantidade });
            //return conexaoSQLite.Table<ProdutoDaLista>().OrderBy(c => c.Id).ToList();
        }
        #endregion

        public void Dispose()
        {
            conexaoSQLite.Dispose();
        }
    }
}
