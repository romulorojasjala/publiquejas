using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public class Ciudadano : Buscable
    {
        private string _userName;
        private string _nombre;
        private string _apellido;
        private DateTime _fechaDeNacimiento;
        private Ubicacion _ubicacion;

        public string UserName { get { return _userName; } }
        public string NombreCompleto { get { return _nombre + " " + _apellido; } }
        public DateTime FechaDeNacimiento { get { return _fechaDeNacimiento; } }

        public Ciudadano(string userName, string nombre, string apellido, DateTime fechaDeNacimiento, Ubicacion ubicacion)
        {
            _userName = userName;
            _nombre = nombre;
            _apellido = apellido;
            _fechaDeNacimiento = fechaDeNacimiento;
            _ubicacion = ubicacion;
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

        public void Anonimizar(int num)
        {
            _userName = "Anonimo" + num;
            _nombre = "Usuario" ;
            _apellido = "Inactivo";
            _fechaDeNacimiento = DateTime.Now;
            _ubicacion = null;
        }
    }
}
