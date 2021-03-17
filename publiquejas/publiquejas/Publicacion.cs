using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public class Publicacion
    {
        private string _titulo;
        private string _contenido;
        private Ciudadano _ciudadano;
        private int _likes;

        public string Titulo { get { return _titulo; } }
        public string Contenido { get { return _contenido; } }
        public Ciudadano Ciudadano { get { return _ciudadano; } }

        public int Likes { get { return _likes; } set { _likes = value; } }

        public Publicacion(string titulo, string contenido, Ciudadano ciudadano)
        {
            _titulo = titulo;
            _contenido = contenido;
            _ciudadano = ciudadano;
        }


    }
}
