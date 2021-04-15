using publiquejas.Votos;
using System;
using System.Collections.Generic;
using System.Linq;
using publiquejas.Excepciones;

namespace publiquejas
{
    public class Publicacion : Buscable, Votable, IEditable<DatosEditablesPublicacion>
    {
        string _titulo;
        string _contenido;
        Ciudadano _ciudadano; 
        List<ICategoria> _categorias = new List<ICategoria>();
        List<Comentario> _comentarios;

        public string Titulo { get { return _titulo; } }
        public string Contenido { get { return _contenido; } }
        public Ciudadano Ciudadano { get { return _ciudadano; } }               
        public IList<ICategoria> Categorias => _categorias.AsReadOnly();
        public List<Comentario> Comentarios { get { return _comentarios; } }

        public List<Voto> Votos { get; set; } = new List<Voto>();

        public Publicacion(string titulo, string contenido, Ciudadano ciudadano)
        {
            _titulo = titulo;
            _contenido = contenido;
            _ciudadano = ciudadano;
            _comentarios = new List<Comentario>();
        }

      

        public Publicacion(string titulo, string contenido, Ciudadano ciudadano, ICategoria categoria)
        {
            _titulo = titulo;
            _contenido = contenido;
            _ciudadano = ciudadano;            
            _categorias.Add(categoria);
            _comentarios = new List<Comentario>();
        }

        public void Editar(DatosEditablesPublicacion nuevosDatos, Ciudadano ciudadanoAutorizado)
        {
            if (ciudadanoAutorizado == null || ciudadanoAutorizado.UserName != _ciudadano.UserName)
            {
                string username = ciudadanoAutorizado != null ? ciudadanoAutorizado.UserName : "ciudadano vacio";
                throw new AutorizacionDenegada(username);
            }
            if (nuevosDatos.Titulo == String.Empty || nuevosDatos.Contenido == String.Empty)
                throw new DatosVaciosPublicacion(this.Titulo);
            //if (this.Comentarios.Count > 0)
            //    throw new ActualizacionDePublicacionFallida();
            this._titulo = nuevosDatos.Titulo  != null ? nuevosDatos.Titulo : this.Titulo;
            this._contenido = nuevosDatos.Contenido != null ? nuevosDatos.Contenido : this.Contenido;
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

        public void agregarCategoria(Categoria categoria)
        {
            _categorias.Add(categoria);
        }

        public void eliminarCategoria(String nombreCategoria)
        {
            var indexCategoria = _categorias.FindIndex(cat => cat.Nombre.Equals(nombreCategoria));
            _categorias.RemoveAt(indexCategoria);
        }

        public void Votar(Ciudadano ciudadano, TipoVoto tipoVoto)
        {
            var voteFound = Votos.FirstOrDefault(v => v.Ciudadano.UserName == ciudadano.UserName && v.Ciudadano.NombreCompleto == ciudadano.NombreCompleto); // TODO: Add comparador para ciudadano
            if(voteFound != null)
            { 
                Votos.Remove(voteFound);
                if (voteFound.TipoVoto == tipoVoto)
                    return;
            }
            Votos.Add(new Voto(ciudadano, tipoVoto));
        }

        public IEnumerable<Voto> GetVotos(TipoVoto tipoVoto)
        {
            var votosFiltrados = Votos.Where(v => v.TipoVoto == tipoVoto);

            return votosFiltrados;
        }

        public IEnumerable<Voto> GetVotos()
        {
            return Votos;
        }

        public void AgregarComentario(Ciudadano ciudadano, string contenidoComentario)
        {
            Comentario comentario = new Comentario(ciudadano, contenidoComentario);
            _comentarios.Add(comentario);
        }
    }
    public struct DatosEditablesPublicacion : INuevosDatos
    {
        public string Titulo { get; set; }
        public string Contenido { get; set; }
    }
}
