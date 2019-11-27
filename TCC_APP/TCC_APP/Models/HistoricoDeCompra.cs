using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TCC_APP.Models
{
    [Table("HistoricoDeCompra")]
    class HistoricoDeCompra
    {
        [MaxLength(50)]
        public string Id { get; set; }
        [MaxLength(50)]
        public string IdListaDeCompra { get; set; }
        [MaxLength(50)]
        public string IdProduto { get; set; }
        [MaxLength(50)]
        public string Preco { get; set; }
        [MaxLength(50)]
        public string Supermercado { get; set; }
        [MaxLength(50)]
        public string Bairro { get; set; }
        [MaxLength(50)]
        public string Cidade { get; set; }
        [MaxLength(50)]
        public DateTime Data { get; set; }
    }
}
