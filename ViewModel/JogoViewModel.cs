using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoDeJogos.ViewModel
{
    public class JogoViewModel
    {
        public Guid idJogo { get; set; }
        public string nome { get; set; }
        public string produtora { get; set; }
        public double preco { get; set; }
    }
}
