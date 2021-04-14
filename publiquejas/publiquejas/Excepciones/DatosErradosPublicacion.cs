using System;

namespace publiquejas.Excepciones
{
    public class DatosErradosPublicacion : Exception
    {
        public readonly string mensaje = "Uno de los datos de publicacion esta vacio";
    }
}
