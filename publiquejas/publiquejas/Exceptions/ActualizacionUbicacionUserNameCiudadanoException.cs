using System;

namespace publiquejas.Exceptions
{
    public class ActualizacionUbicacionUserNameCiudadanoException : Exception
    {
        public const string GetMessage = "El User Name ingresado para la actualizacion de la ubicacion no existe en la aplicacion.";
        private string _userName;
        public string GetUserName => _userName;
        public ActualizacionUbicacionUserNameCiudadanoException() : base()
        {

        }

        public ActualizacionUbicacionUserNameCiudadanoException(string userName) : base(GetMessage)
        {
            _userName = userName;
        }

        public ActualizacionUbicacionUserNameCiudadanoException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
