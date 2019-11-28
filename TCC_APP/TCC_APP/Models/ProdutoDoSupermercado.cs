﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TCC_APP.Models
{
    [Table("ProdutoDoSupermercado")]
    public class ProdutoDoSupermercado
    {
        [MaxLength(50)]
        public string Id { get; set; }
        [MaxLength(50)]
        public string IdProduto { get; set; }
        [MaxLength(50)]
        public string IdSupermercado { get; set; }
        [MaxLength(50)]
        public string Quantidade { get; set; }
    }
}