using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using publiquejas.Excepciones;

namespace publiquejas
{
    public class Ciudadano : Buscable
    {
        private string _userName;
        private string _nombre;
        private string _apellido;
        private string _password;

        private DateTime _fechaDeNacimiento;
        private Ubicacion _ubicacion;

        public string UserName { get { return _userName; } }
        public string NombreCompleto { get { return _nombre + " " + _apellido; } }
        public string Ubicacion => _ubicacion.UbicacionName;
        public DateTime FechaDeNacimiento { get { return _fechaDeNacimiento; } }

        public Ciudadano(string userName, string nombre, string apellido, DateTime fechaDeNacimiento, Ubicacion ubicacion)
        {
            _userName = userName;
            _nombre = nombre;
            _apellido = apellido;
            _fechaDeNacimiento = fechaDeNacimiento;
            _ubicacion = ubicacion;
        }

        public Ciudadano(string userName, string nombre, string apellido, DateTime fechaDeNacimiento, Ubicacion ubicacion, string password)
        {
            _userName = userName;
            _nombre = nombre;
            _apellido = apellido;
            _fechaDeNacimiento = fechaDeNacimiento;
            _ubicacion = ubicacion;
            _password = password;
        }

        public void Autenticar(string password)
        {
            if(!password.Equals(_password))
            {
                throw new AutenticacionInvalidaException();
            }
        }

        public object getPropertyValue(string propertyName)
        {
            var property = GetType().GetProperties().ToList()
                .Find((prop) => propertyName.ToLower().Equals(prop.Name.ToLower()));

            if (property != null)
            {
                return property.GetValue(this);
            }

            return null;
        }

        public void ActualizarUbicacion(string ubicacion)
        {
            this._ubicacion = null;
            _ubicacion = new Ubicacion(ubicacion);
        }

        public void Anonimizar(int num)
        {
            _userName = "Anonimo" + num;
            _nombre = "Usuario" + GetHash().Substring(0, 8);
            _apellido = "";
            _fechaDeNacimiento = DateTime.Now;
            _ubicacion = null;
        }
        private string GetHash()
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(NombreCompleto));
                return string.Join(string.Empty, data.Select(x => x.ToString("x2")));
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Ciudadano ciudadano))
            {
                return false;
            }
            
            return NombreCompleto == ciudadano.NombreCompleto && UserName == ciudadano.UserName;
        }
        
        public override int GetHashCode()
        {
            return UserName.GetHashCode();
        }
    }
}
