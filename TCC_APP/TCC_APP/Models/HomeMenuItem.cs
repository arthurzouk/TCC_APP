using System;
using System.Collections.Generic;
using System.Text;

namespace TCC_APP.Models
{
    public enum MenuItemType
    {
        Listas,
        Produtos,
        Supermercados,
        Relatorios
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
