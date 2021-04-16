using System;

namespace publiquejas.Excepciones
{
    public class CiudadanoConPermisosInsuficientes : Exception
    {
        public const string MensajeDeError = "El ciudadano no tiene los permisos suficientes para realizar esta accion";
        public string NombreDelCiudadano { get; }

        public CiudadanoConPermisosInsuficientes(string nombreDelCiudadano) : base(MensajeDeError)
        {
            NombreDelCiudadano = nombreDelCiudadano;
        }
    }
}
