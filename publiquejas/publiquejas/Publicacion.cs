using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public class Publicacion
    {
        string _titulo;
        string _contenido;
        Ciudadano _ciudadano;

        public string Titulo { get { return _titulo; } }
        public string Contenido { get { return _contenido; } }
        public Ciudadano Ciudadano { get { return _ciudadano; } }


        public Publicacion(string titulo, string contenido, Ciudadano ciudadano)
        {
            _titulo = titulo;
            _contenido = contenido;
            _ciudadano = ciudadano;
        }
    }
}
