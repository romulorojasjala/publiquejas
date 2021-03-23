using CsvHelper;
using publiquejas;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace XUnitTestProject
{
    public static class Utilitarios
    {
        public static ModeloCiudadano[] ObtenerListaModeloCiudadano()
        {
            return ReadCsv(@"Recursos/FakeNameGenerator.csv");
        }

        public static ModeloCiudadano[] ObtenerListaModeloCiudadano(string csvPath)
        {
            return ReadCsv(csvPath);
        }

        private static ModeloCiudadano[] ReadCsv(string path)
        {
            ModeloCiudadano[] ciudadanos;
            using (StreamReader reader = new StreamReader(path))
            using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                ciudadanos = csv.GetRecords<ModeloCiudadano>().ToArray();
            }

            return ciudadanos;
        }

        public static AdministradorDePublicaciones GeneradorCiudadanos(AdministradorDePublicaciones administrador, ModeloCiudadano[] modeloCiudadanos, 
            uint startIndex, uint endIndex)
        {
            int count = modeloCiudadanos.Count();
            
            if (startIndex < count - 1 && endIndex < count - 1 && startIndex < endIndex)
            {
                var ciudadanos = modeloCiudadanos[new Range((int) startIndex, new Index((int) endIndex, fromEnd: false))];

                foreach (var ciudadano in ciudadanos)
                {
                    administrador.AgregarCiudadano(userName: ciudadano.UserName, nombre: ciudadano.Nombre, apellido: ciudadano.Apellido,
                        fechaDeNacimiento: ciudadano.FechaDeNacimiento, ubicacion: ciudadano.Ubicacion);
                }
            }

            return administrador;
        }
    }
}
