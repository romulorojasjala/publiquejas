using System;

namespace publiquejas.Excepciones
{
    [Serializable]
    class PublicacionNoEncontradaExcepcion : Exception
    {
        public PublicacionNoEncontradaExcepcion()
        : base("Publicacion no encontrada")
        {

        }
    }
}
