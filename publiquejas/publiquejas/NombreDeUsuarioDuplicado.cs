using System;

namespace publiquejas
{
    public class NombreDeUsuarioDuplicado : Exception
    {
        public const string MensajeDeError = "El nombre de usuario ya existe";
        public string NombreDeUsuario { get; }

        public NombreDeUsuarioDuplicado(string nombreDeUsuario): base(MensajeDeError)
        {
            NombreDeUsuario = nombreDeUsuario;
        }
    }
}