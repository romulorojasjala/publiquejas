using System;

namespace publiquejas.Excepciones
{
    [Serializable]
    public class PublicacionNoEncontradaExcepcion : Exception
    {
        public const string Mensaje = "Publicacion no encontrada.";
        private string _titulo;
        public string ObtenerTitulo => _titulo;
        public PublicacionNoEncontradaExcepcion() : base(Mensaje)
        {

        }

        public PublicacionNoEncontradaExcepcion(string titulo) : base(Mensaje)
        {
            _titulo = titulo;
        }
    }
}
