using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC_APP.Models;

namespace TCC_APP.Services
{
    class ProdutoDaListaMockDataStore : IDataStore<ProdutosDaLista_Result>
    {
        List<ProdutosDaLista_Result> ProdutoDaLista;

        public ProdutoDaListaMockDataStore(string id)
        {
            using (var dados = new AcessoDB())
            {
                ProdutoDaLista = dados.GetAllProdutoDaLista(id);
            }

            //ProdutoDaLista = new List<Produto>();
            //var mockItems = new List<Produto>
            //{
            //    new Produto { Id = Guid.NewGuid().ToString(), Nome = "Detergente", Quantidade="6" },
            //    new Produto { Id = Guid.NewGuid().ToString(), Nome = "Sabão em pó", Quantidade="3" },
            //};

            //foreach (var item in mockItems)
            //{
            //    ProdutoDaLista.Add(item);
            //}
        }

        public async Task<bool> AddItemAsync(ProdutosDaLista_Result item)
        {
            ProdutoDaLista.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ProdutosDaLista_Result item)
        {
            var oldItem = ProdutoDaLista.Where((ProdutosDaLista_Result arg) => arg._nomeProduto == item._nomeProduto).FirstOrDefault();
            ProdutoDaLista.Remove(oldItem);
            ProdutoDaLista.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string nome)
        {
            var oldItem = ProdutoDaLista.Where((ProdutosDaLista_Result arg) => arg._nomeProduto == nome).FirstOrDefault();
            ProdutoDaLista.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ProdutosDaLista_Result> GetItemAsync(string nome)
        {
            return await Task.FromResult(ProdutoDaLista.FirstOrDefault(s => s._nomeProduto == nome));
        }

        public async Task<IEnumerable<ProdutosDaLista_Result>> GetItemsAsync(string id)//(bool forceRefresh = false)
        {
            return await Task.FromResult(ProdutoDaLista);
        }
    }
}
