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
        ICategoria _categoria;

        public string Titulo { get { return _titulo; } }
        public string Contenido { get { return _contenido; } }
        public Ciudadano Ciudadano { get { return _ciudadano; } }
        public ICategoria Categoria { get { return _categoria; } }

        public Publicacion(string titulo, string contenido, Ciudadano ciudadano)
        {
            _titulo = titulo;
            _contenido = contenido;
            _ciudadano = ciudadano;
        }

        public Publicacion(string titulo, string contenido, Ciudadano ciudadano, ICategoria categoria)
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
