using SQLite;

namespace TCC_APP.Models
{
    [Table("ProdutoDaCompra")]
    class ProdutoDaCompra
    {
        [MaxLength(50)]
        public string Id { get; set; }
        [MaxLength(50)]
        public string IdCompra { get; set; }
        [MaxLength(50)]
        public string IdProduto { get; set; }
        [MaxLength(50)]
        public string PrecoProduto { get; set; }
        public int qtdProduto { get; set; }
    }
}
