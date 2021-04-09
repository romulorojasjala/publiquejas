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
        List<Comentario> _comentarios;

        public string Titulo { get { return _titulo; } }
        public string Contenido { get { return _contenido; } }
        public Ciudadano Ciudadano { get { return _ciudadano; } }
        public Categoria Categoria { get { return _categoria; } }
        public List<Comentario> Comentarios { get { return _comentarios; } }

        public Publicacion(string titulo, string contenido, Ciudadano ciudadano)
        {
            _titulo = titulo;
            _contenido = contenido;
            _ciudadano = ciudadano;
            _comentarios = new List<Comentario>();
        }

        public Publicacion(string titulo, string contenido, Ciudadano ciudadano, Categoria categoria)
        {
            _titulo = titulo;
            _contenido = contenido;
            _ciudadano = ciudadano;
            _categoria = categoria;
            _comentarios = new List<Comentario>();
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

        internal void AgregarComentario(Ciudadano ciudadano, string contenidoComentario)
        {
            Comentario comentario = new Comentario(ciudadano, contenidoComentario);
            _comentarios.Add(comentario);
        }
    }
}
