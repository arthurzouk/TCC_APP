using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TCC_APP.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Nome { get; set; }
        [MaxLength(50)]
        public string LoginUsuario { get; set; }
        [MaxLength(50)]
        public string Senha { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", LoginUsuario, Email);
        }
    }
}
