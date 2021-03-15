using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public class Comentario
    {
        private string _comentario;
        private Ciudadano _ciudadano;

        public Comentario(Ciudadano ciudadano, string comentario)
        {
            _ciudadano = ciudadano;
            _comentario = comentario;
        }
    }
}
