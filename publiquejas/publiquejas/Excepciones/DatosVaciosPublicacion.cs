using System;

namespace publiquejas.Excepciones
{
    public class DatosVaciosPublicacion : Exception
    {
        public const string Mensaje = "Uno de los datos de publicacion esta vacio.";
        private string _titulo;
        public string ObtenerTitulo => _titulo;
        public DatosVaciosPublicacion() : base(Mensaje)
        {

        }

        public DatosVaciosPublicacion(string titulo) : base(Mensaje)
        {
            _titulo = titulo;
        }
    }
}
