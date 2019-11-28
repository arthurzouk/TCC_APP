using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TCC_APP.Models
{
    [Table("Supermercado")]
    public class Supermercado
    {
        [MaxLength(50)]
        public string Id { get; set; }
        [MaxLength(100)]
        public string Nome { get; set; }
        [MaxLength(10)]
        public int Distancia { get; set; }
    }
}
