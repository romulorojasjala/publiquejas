using System;

namespace publiquejas.Excepciones
{
    public class PublicacionConComentarios : Exception
    {
        public const string MensajeDeError = "La publicacion no se puede eliminar, debido a que tiene comentarios";
        public string TituloDeDePublicacion { get; }

        public PublicacionConComentarios(string tituloDeDePublicacion) : base(MensajeDeError)
        {
            TituloDeDePublicacion = tituloDeDePublicacion;
        }

    }
}
