using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public class Publicacion : Buscable
    {
        private string _titulo;
        private string _contenido;
        private Ciudadano _ciudadano;
        private Categoria _categoria;
        private int _likes;

        public string Titulo { get { return _titulo; } }
        public string Contenido { get { return _contenido; } }
        public Ciudadano Ciudadano { get { return _ciudadano; } }
        public Categoria Categoria { get { return _categoria; } }

        public int Likes { get { return _likes; } set { _likes = value; } }

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
            var property = GetType().GetProperties().ToList()
                .Find((prop) => propertyName.ToLower().Equals(prop.Name.ToLower()));

            if (property != null)
            {
                return property.GetValue(this);
            }

            return null;
        }
    }
}
