using publiquejas.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace publiquejas
{
    class ValidadorCategoria : IValidadorCategoria
    {
        public void Validar(ICategoria categoria)
        {
            this.ValidarNombreVacio(categoria.Nombre);
            this.ValidarNombreCaracteres(categoria.Nombre);
        }

        private void ValidarNombreVacio(string nombre)
        {
            if (nombre.Length <= 0)
            {
                throw new CategoriaConNombreVacio();
            }
        }

        private void ValidarNombreCaracteres(string nombre)
        {
            Regex regex = new Regex("^[a-zA-Z0-9:]*$");
            if (!regex.IsMatch(nombre))
            {
                throw new CategoriaConNombreTieneCaracteresInvalidos();
            }
        }

        public void VerificarDuplicados(ICategoria categoria, IList<ICategoria> categorias)
        {
            ICategoria duplicado = AdministradorDePublicaciones.BuscarCategoria(categoria.Nombre, categorias);
            if (duplicado != null)
            {
                throw new CategoriaConNombreDuplicado();
            }
        }
    }
}
