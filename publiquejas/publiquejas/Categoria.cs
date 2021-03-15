using System;
using System.Collections.Generic;

namespace publiquejas
{
    public class Categoria
    {
        private string _nombre;
        private List<Publicacion> _publicaciones;

        public Categoria(string nombreCategoria)
        {
            _nombre = nombreCategoria;
            _publicaciones = new List<Publicacion>();
        }

        public string Nombre { get { return _nombre; } }
        //public List<Publicacion> Publicacion { get { return _publicaciones; } }

        public void AgregarPublicacion(Publicacion publicacion)
        {
            _publicaciones.Add(publicacion);
        }
    }
}
