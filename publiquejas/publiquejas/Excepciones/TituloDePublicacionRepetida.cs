using System;

namespace publiquejas.Excepciones
{
    public class TituloDePublicacionRepetida : Exception
    {
        public readonly string mensaje = "El titulo de la publicacion ya existe";
    }
}
