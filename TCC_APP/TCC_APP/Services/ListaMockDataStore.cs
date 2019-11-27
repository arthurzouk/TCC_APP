using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC_APP.Models;

namespace TCC_APP.Services
{
    public class ListaMockDataStore : IDataStore<ListaDeCompra>
    {
        List<ListaDeCompra> Listas;

        public ListaMockDataStore()
        {
            using (var dados = new AcessoDB())
            {
                Listas = dados.GetAllListaDeCompra();
            }

            //Listas = new List<ListaDeCompra>();

            //    //var mockItems = new List<ListaDeCompra>
            //    //{
            //    //    new ListaDeCompra { Id = Guid.NewGuid().ToString(), Nome = "Compra semanal", Descricao="Compras para a semana" },
            //    //    new ListaDeCompra { Id = Guid.NewGuid().ToString(), Nome = "Compra mensal", Descricao="Compras para o mês" },
            //    //    new ListaDeCompra { Id = Guid.NewGuid().ToString(), Nome = "Churrasco", Descricao="Reuna os amigos para o churrasco" },
            //    //    new ListaDeCompra { Id = Guid.NewGuid().ToString(), Nome = "Aniversário", Descricao="Festinha, urru.." },
            //    //    new ListaDeCompra { Id = Guid.NewGuid().ToString(), Nome = "Sextou", Descricao="É hoje!" },
            //    //    new ListaDeCompra { Id = Guid.NewGuid().ToString(), Nome = "Outros", Descricao="Compras extras" },
            //    //};

            //    foreach (var item in mockItems)
            //{
            //    Listas.Add(item);
            //}
        }

        public async Task<bool> AddItemAsync(ListaDeCompra item)
        {
            Listas.Add(item);

            using (var dados = new AcessoDB())
            {
                dados.Inserir(item);
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ListaDeCompra item)
        {
            var oldItem = Listas.Where((ListaDeCompra arg) => arg.Id == item.Id).FirstOrDefault();
            Listas.Remove(oldItem);
            Listas.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = Listas.Where((ListaDeCompra arg) => arg.Id == id).FirstOrDefault();
            Listas.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ListaDeCompra> GetItemAsync(string id)
        {
            return await Task.FromResult(Listas.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ListaDeCompra>> GetItemsAsync(string id)//(bool forceRefresh = false)
        {
            return await Task.FromResult(Listas);
        }
    }
}