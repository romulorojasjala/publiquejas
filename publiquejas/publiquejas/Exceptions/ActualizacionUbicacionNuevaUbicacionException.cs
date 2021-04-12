using System;

namespace publiquejas.Exceptions
{
    public class ActualizacionUbicacionNuevaUbicacionException : Exception
    {
        public const string GetMessage = "El nombre de la nueva Ubicacion, esta vacia o es nula.";
        private string _ubicacion;
        public string GetUbicacion => _ubicacion;

        public ActualizacionUbicacionNuevaUbicacionException() : base()
        {

        }

        public ActualizacionUbicacionNuevaUbicacionException(string ubicacion) : base(GetMessage)
        {
            _ubicacion = ubicacion;
        }

        public ActualizacionUbicacionNuevaUbicacionException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}
