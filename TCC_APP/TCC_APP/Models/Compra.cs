using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TCC_APP.Models
{
    [Table("Compra")]
    class Compra
    {
        [MaxLength(50)]
        public string Id { get; set; }
        [MaxLength(50)]
        public string NomeListaDeCompra { get; set; }
        [MaxLength(50)]
        public string ValorTotalProdutos { get; set; }
        [MaxLength(50)]
        public string ValorTotalEntrega { get; set; }
        public DateTime Data { get; set; }
    }
}
