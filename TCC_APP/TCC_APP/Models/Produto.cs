﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TCC_APP.Models
{
    [Table("Produto")]
    public class Produto
    {
        //[PrimaryKey, AutoIncrement]
        [MaxLength(50)]
        public string Id { get; set; }
        [MaxLength(100)]
        public string IdSupermercado { get; set; }
        [MaxLength(100)]
        public string Nome { get; set; }
        [MaxLength(100)]
        public string Marca { get; set; }
        [MaxLength(100)]
        public double Preco { get; set; }
        [MaxLength(100)]
        public string _img { get; set; }


        public override string ToString()
        {
            return string.Format("{0}", Nome);
        }
    }
}
