using System;

namespace publiquejas.Excepciones
{
    public class AutorizacionDenegada : Exception
    {
        public const string Mensaje = "El ciudadano no esta autorizado para realizar esta operacion.";
        private string _username;
        public string ObtenerUsername => _username;
        public AutorizacionDenegada() : base(Mensaje)
        {

        }

        public AutorizacionDenegada(string username) : base(Mensaje)
        {
            _username = username;
        }
    }
}
