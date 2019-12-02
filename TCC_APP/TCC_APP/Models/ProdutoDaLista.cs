using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TCC_APP.Models
{
    [Table("ProdutoDaLista")]
    public class ProdutoDaLista
    {
        [MaxLength(50)]
        public string Id { get; set; }
        [MaxLength(50)]
        public string IdProduto { get; set; }
        [MaxLength(50)]
        public string IdListaDeCompra { get; set; }
        [MaxLength(2)]
        public string qtdProduto { get; set; }
    }
}
