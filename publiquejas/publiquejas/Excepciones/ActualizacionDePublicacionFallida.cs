using System;

namespace publiquejas.Excepciones
{
    public class ActualizacionDePublicacionFallida : Exception
    {
        public const string Mensaje = "No se pudo actualizar la publicacion.";
        public ActualizacionDePublicacionFallida() : base(Mensaje)
        {

        }
    }
}
