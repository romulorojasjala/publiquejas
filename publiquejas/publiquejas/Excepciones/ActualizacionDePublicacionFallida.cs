using System;

namespace publiquejas.Excepciones
{
    public class ActualizacionDePublicacionFallida : Exception
    {
        public readonly string mensaje = "No se pudo actualizar la publicacion porq tiene comentarios o esta en un ranking";
    }
}
