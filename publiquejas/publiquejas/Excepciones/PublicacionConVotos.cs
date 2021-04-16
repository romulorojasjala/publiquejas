using System;

namespace publiquejas.Excepciones
{
    public class PublicacionConVotos : Exception
    {
        public const string MensajeDeError = "La publicacion no se puede eliminar, debido a que tiene votos";
        public string TituloDeDePublicacion { get; }

        public PublicacionConVotos(string tituloDeDePublicacion) : base(MensajeDeError)
        {
            TituloDeDePublicacion = tituloDeDePublicacion;
        }
    }
}