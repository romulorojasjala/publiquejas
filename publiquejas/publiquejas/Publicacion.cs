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
        List<Categoria> _categorias = new List<Categoria>();

        public string Titulo { get { return _titulo; } }
        public string Contenido { get { return _contenido; } }
        public Ciudadano Ciudadano { get { return _ciudadano; } }               
        public IList<Categoria> Categorias => _categorias.AsReadOnly();

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
            _categorias.Add(categoria);
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

        public void agregarCategoria(Categoria categoria)
        {
            _categorias.Add(categoria);
        }

        public void eliminarCategoria(String nombreCategoria)
        {
            var indexCategoria = _categorias.FindIndex(cat => cat.Nombre.Equals(nombreCategoria));
            _categorias.RemoveAt(indexCategoria);
        }
    }
}
