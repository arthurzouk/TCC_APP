using SQLite;
using System;

namespace TCC_APP.Models
{
    public class ListaDeCompra
    {
        //[PrimaryKey, AutoIncrement]
        [MaxLength(50)]
        public string Id { get; set; }
        [MaxLength(100)]
        public string Nome { get; set; }
        //[MaxLength(50)]
        //public string Categoria { get; set; }
        [MaxLength(50)]
        public string Descricao { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", Nome, Descricao);
        }
    }
}