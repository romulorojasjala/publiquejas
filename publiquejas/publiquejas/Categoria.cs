using System;
using System.Collections.Generic;

namespace publiquejas
{
    public class Categoria
    {
        private string _nombre;
        private List<Publicacion> _publicaciones;

        public string Nombre { get { return _nombre; } }
        public IList<Publicacion> Publicaciones { get { return _publicaciones.AsReadOnly(); } }


        public Categoria(string nombreCategoria)
        {
            _nombre = nombreCategoria;
            _publicaciones = new List<Publicacion>();
        }


        internal void AgregarPublicacion(Publicacion publicacion)
        {
            _publicaciones.Add(publicacion);
        }
    }
}
