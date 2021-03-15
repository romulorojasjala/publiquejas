﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    class Publicacion
    {
        string _titulo;
        string _contenido;
        Ciudadano _ciudadano;
        ListaDeComentarios _listaDeComentarios;

        public string TituloPublicacion() { return _titulo; }

        public Publicacion(string titulo, string contenido, Ciudadano ciudadano)
        {
            _titulo = titulo;
            _contenido = contenido;
            _ciudadano = ciudadano;
        }
    }
}
