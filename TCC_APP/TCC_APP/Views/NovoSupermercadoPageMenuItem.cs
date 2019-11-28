using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC_APP.Views
{

    public class NovoSupermercadoPageMenuItem
    {
        public NovoSupermercadoPageMenuItem()
        {
            TargetType = typeof(NovoSupermercadoPageDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}