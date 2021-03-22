using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public class Publicacion : Buscable
    {
        string _titulo;
        string _contenido;
        Ciudadano _ciudadano;
        Categoria _categoria;

        public string Titulo { get { return _titulo; } }
        public string Contenido { get { return _contenido; } }
        public Ciudadano Ciudadano { get { return _ciudadano; } }
        public Categoria Categoria { get { return _categoria; } }

        public Publicacion(string titulo, string contenido, Ciudadano ciudadano)
        {
            _titulo = titulo;
            _contenido = contenido;
            _ciudadano = ciudadano;
        }

        public Publicacion(string titulo, string contenido, Ciudadano ciudadano, Categoria categoria)
        {
            _titulo = titulo;
            _contenido = contenido;
            _ciudadano = ciudadano;
            _categoria = categoria;
        }

        public object getPropertyValue(string propertyName)
        {
            switch(propertyName)
            {
                case "categoria": return _categoria;
            }

            return null;
        }
    }
}
