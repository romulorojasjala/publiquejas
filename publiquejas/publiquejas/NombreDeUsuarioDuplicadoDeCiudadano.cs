using System;

namespace publiquejas
{
    public class NombreDeUsuarioDuplicado : Exception
    {
        private const string MensajeDeError = "El nombre de usuario {0} ya existe";

        public NombreDeUsuarioDuplicado(string nombreDeUsuario): base(string.Format(MensajeDeError, nombreDeUsuario))
        {

        }
    }
}