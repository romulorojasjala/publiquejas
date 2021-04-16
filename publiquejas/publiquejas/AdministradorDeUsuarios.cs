using System;
using System.Collections.Generic;
using System.Linq;
using publiquejas.Excepciones;
using publiquejas.Exceptions;

namespace publiquejas
{
    public class AdministradorDeUsuarios
    {
        private List<Ciudadano> _ciudadanos;
        private int contadorUsuariosEliminados;

        public AdministradorDeUsuarios()
        {
            _ciudadanos = new List<Ciudadano>();
        }

        // public IList<Ciudadano> Ciudadanos => _ciudadanos.AsReadOnly();

        public void AgregarCiudadano(string nombreDeUsuario, string nombre, string apellido, DateTime fechaDeNacimiento, string ubicacion)
        {
            Ciudadano ciudadanoDuplicado = _ciudadanos.Find((ciudadanoBuscado) => ciudadanoBuscado.UserName == nombreDeUsuario);

            if (ciudadanoDuplicado != null)
            {
                throw new NombreDeUsuarioDuplicado(nombreDeUsuario);
            }

            var ciudadano = new Ciudadano(nombreDeUsuario, nombre, apellido, fechaDeNacimiento, new Ubicacion(ubicacion));
            _ciudadanos.Add(ciudadano);
        }

        public void ActualizarUbicacionCiudadano(string userName, string nuevaUbicacion)
        {
            Ciudadano ciudadano = BuscarCiudadano(userName);

            if (ciudadano == null)
            {
                throw new ActualizacionUbicacionUserNameCiudadanoException(userName);
            }

            if (string.IsNullOrEmpty(nuevaUbicacion))
            {
                throw new ActualizacionUbicacionNuevaUbicacionException(nuevaUbicacion);
            }

            ciudadano.ActualizarUbicacion(ubicacion: nuevaUbicacion);
        }
        public Ciudadano BuscarCiudadano(string userNameDeCiudadano)
        {
            return _ciudadanos.Where(ciudadano => ciudadano.UserName.Equals(userNameDeCiudadano)).FirstOrDefault();
        }
        public Ciudadano BuscarCiudadano(Ciudadano ciudadano)
        {
            return _ciudadanos.FirstOrDefault(c => c == ciudadano);
        }
        public List<Ciudadano> BuscarCiudadanos(List<TerminoDeBusqueda<Ciudadano>> terminosDeBusqueda)
        {
            var ciudadanos = _ciudadanos;

            terminosDeBusqueda.ForEach(termino =>
            {
                ciudadanos = termino.filtrar(ciudadanos);
            });

            return ciudadanos;
        }

        public void EliminarCiudadano(string nombreCiudadano)
        {
            Ciudadano ciudadano = BuscarCiudadano(nombreCiudadano);
            if (ciudadano == null)
            {
                throw new CiudadanoInexistente();
            }
            contadorUsuariosEliminados++;
            ciudadano.Anonimizar(contadorUsuariosEliminados);
        }

        public bool ExisteCiudadano(string nombreCiudadano)
        {
            bool existe = false;
            Ciudadano ciudadano = BuscarCiudadano(nombreCiudadano);

            if (ciudadano != null)
            {
                existe = true;
            }
            return existe;
        }

        public int ContarCiudadanos()
        {
            return _ciudadanos.Count();
        }

        public Ciudadano GetCiudadano(int index)
        {
            return _ciudadanos[index];
        }

        public void AutenticarCiudadano (string nombreUsuario, string password)
        {
            Ciudadano usuario = BuscarCiudadano(nombreUsuario); 
            if (usuario == null)
            {
                throw new AutenticacionInvalidaException();
            }

            usuario.Autenticar(password);
                        
        }


    }
}
