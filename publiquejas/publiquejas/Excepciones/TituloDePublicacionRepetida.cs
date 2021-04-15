using System;

namespace publiquejas.Excepciones
{
    public class TituloDePublicacionRepetida : Exception
    {
        public const string Mensaje = "El titulo de la publicacion ya existe.";
        private string _titulo;
        public string ObtenerTitulo => _titulo;
        public TituloDePublicacionRepetida() : base(Mensaje)
        {

        }

        public TituloDePublicacionRepetida(string titulo) : base(Mensaje)
        {
            _titulo = titulo;
        }
    }
}
