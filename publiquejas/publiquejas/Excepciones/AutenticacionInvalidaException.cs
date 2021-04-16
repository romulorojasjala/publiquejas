using System;

namespace publiquejas.Excepciones
{
    [Serializable]
    public class AutenticacionInvalidaException : Exception
    {
        public const string GetMessage = "El UserName o Password es incorrecto";
        public AutenticacionInvalidaException() : base(GetMessage)
        {
            
        }
    }
}

