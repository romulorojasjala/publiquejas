using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public class CiudadanoInexistente : Exception
    {
        public const string mensaje = "El ciudadano no existe";
        public CiudadanoInexistente()
            : base(mensaje)
        { }

        public CiudadanoInexistente(string mensaje)
        { }

        public CiudadanoInexistente(string mensaje, Exception inner)
            : base(mensaje, inner)
        { }
    }
}
