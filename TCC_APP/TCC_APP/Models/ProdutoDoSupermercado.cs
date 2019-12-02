using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TCC_APP.Models
{
    [Table("ProdutoDoSupermercado")]
    public class ProdutoDoSupermercado
    {
        [MaxLength(50)]
        public string idProduto { get; set; }
        [MaxLength(50)]
        public string NomeProduto { get; set; }
        [MaxLength(50)]
        public string NomeSupermercado { get; set; }
        [MaxLength(100)]
        public string MarcaProduto { get; set; }
        [MaxLength(100)]
        public string Preco { get; set; }
        [MaxLength(100)]
        public string _imgProduto { get; set; }
        [MaxLength(10)]
        public int distancia { get; set; }
    }
}
