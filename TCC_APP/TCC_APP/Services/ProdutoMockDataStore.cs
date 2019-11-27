using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC_APP.Models;

namespace TCC_APP.Services
{
    public class ProdutoMockDataStore : IDataStore<Produto>
    {
        List<Produto> Produtos;

        public ProdutoMockDataStore()
        {
            Produtos = new List<Produto>();
            var mockItems = new List<Produto>
            {
                new Produto { Id = Guid.NewGuid().ToString(), Nome = "Detergente" },
                new Produto { Id = Guid.NewGuid().ToString(), Nome = "Sabão em pó" },
                new Produto { Id = Guid.NewGuid().ToString(), Nome = "Carne bovina 300g" },
                new Produto { Id = Guid.NewGuid().ToString(), Nome = "Salmão 150g" },
                new Produto { Id = Guid.NewGuid().ToString(), Nome = "Frango 200g" },
                new Produto { Id = Guid.NewGuid().ToString(), Nome = "Outros" },
            };

            foreach (var item in mockItems)
            {
                Produtos.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Produto item)
        {
            Produtos.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Produto item)
        {
            var oldItem = Produtos.Where((Produto arg) => arg.Id == item.Id).FirstOrDefault();
            Produtos.Remove(oldItem);
            Produtos.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = Produtos.Where((Produto arg) => arg.Id == id).FirstOrDefault();
            Produtos.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Produto> GetItemAsync(string id)
        {
            return await Task.FromResult(Produtos.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Produto>> GetItemsAsync(string id)//(bool forceRefresh = false)
        {
            return await Task.FromResult(Produtos);
        }
    }
}
