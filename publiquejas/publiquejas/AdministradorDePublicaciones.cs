using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace publiquejas
{
    public class AdministradorDePublicaciones
    {
        private List<Ciudadano> _ciudadanos;
        private List<ICategoria> _categorias;
        private List<Publicacion> _publicaciones;
        private IValidadorCategoria _validadorCategoria;

        public AdministradorDePublicaciones()
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<IValidadorCategoria, ValidadorCategoria>()
                .BuildServiceProvider();
            
            _ciudadanos = new List<Ciudadano>();
            _categorias = new List<ICategoria>();
            _publicaciones = new List<Publicacion>();
            _validadorCategoria = serviceProvider.GetService<IValidadorCategoria>();
        }

        public IList<Ciudadano> Ciudadanos => _ciudadanos.AsReadOnly();
        public IList<Publicacion> Publicaciones => _publicaciones.AsReadOnly();
        public IList<ICategoria> Categorias => _categorias.AsReadOnly();

        public void AgregarCiudadano(string userName, string nombre, string apellido, DateTime fechaDeNacimiento, string ubicacion) 
        { 
            var ciudadano = new Ciudadano(userName, nombre, apellido, fechaDeNacimiento, new Ubicacion(ubicacion));
            _ciudadanos.Add(ciudadano);
        }

        public void AgregarCategoria(string nombreDeCategoria)
        {            
            Categoria categoria = new Categoria(nombreDeCategoria);
            _validadorCategoria.VerificarDuplicados(categoria, _categorias);
            _validadorCategoria.Validar(categoria);
            _categorias.Add(categoria);
        }

        public void AgregarPublicacion(string userNameDeCiudadano, string titulo, string contenido, string nombreDeCategoria)
        {
            Ciudadano ciudadano = BuscarCiudadano(userNameDeCiudadano);
            ICategoria categoria = AdministradorDePublicaciones.BuscarCategoria(nombreDeCategoria, _categorias);

            if (ciudadano != null && categoria != null) {
                Publicacion publicacion = new Publicacion(titulo, contenido, ciudadano, categoria);
                categoria.AgregarPublicacion(publicacion);
                _publicaciones.Add(publicacion);
            }
        }

        private Ciudadano BuscarCiudadano(string userNameDeCiudadano)
        {
            return _ciudadanos.Where(ciudadano => ciudadano.UserName.Equals(userNameDeCiudadano)).FirstOrDefault();
        }

        public static ICategoria BuscarCategoria(string nombreDeCategoria, IList<ICategoria> categorias)
        {
            return categorias.Where(categoria => categoria.Nombre.Equals(nombreDeCategoria)).FirstOrDefault();
        }

        public List<Publicacion> BuscarPublicacion(List<TerminoDeBusqueda<Publicacion>> terminosDeBusqueda)
        {
            var publicaciones = _publicaciones;

            terminosDeBusqueda.ForEach(termino =>
            {
                publicaciones = termino.filtrar(publicaciones);
            });

            return publicaciones;
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
    }
}
