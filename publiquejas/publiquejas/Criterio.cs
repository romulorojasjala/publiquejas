using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public enum AccionCriterio
    {
        Mayor,
        Menor
    }

    public enum TipoCriterio
    {
        Cantidad,
        Longitud
    }
    public enum TransformacionCriterio
    {
        Concatenacion,
        Suma
    }

    public class Criterio<T> : IComparer<T> where T : IRankeable
    {
        public string Propiedad {
            get;
            private set;
        }

        public AccionCriterio Accion {
            get;
            private set;
        }

        public TipoCriterio Tipo
        {
            get;
            private set;
        }

        public TransformacionCriterio Transformacion
        {
            get;
            private set;
        }


        public Criterio<T> AgregarPropiedad(string v)
        {
            Propiedad = v;
            return this;
        }

        public Criterio<T> AgregarAccion(AccionCriterio v)
        {
            Accion = v;
            return this;
        }

        public Criterio<T> AgregarTipo(TipoCriterio v)
        {
            Tipo = v;
            return this;
        }

        public Criterio<T> AgregarTransformacionPropiedad(TransformacionCriterio concatenacion)
        {
            Transformacion = concatenacion;
            return this;
        }

        public int Compare(T x, T y)
        {
            Type tipoPropiedad = x.GetPropertyType(Propiedad);
            var propertyValueX = x.getPropertyValue(Propiedad);
            var propertyValueY = y.getPropertyValue(Propiedad);

            if (ReferenceEquals(propertyValueX, propertyValueY))
                return 0;

            if (x == null)
                return -1;

            if (y == null)
                return 1;

            if (tipoPropiedad.Equals(typeof(string)))
            {
                return Accion == AccionCriterio.Mayor && Tipo == TipoCriterio.Cantidad ?
                    string.Compare(propertyValueY as string, propertyValueX as string)
                    : Accion == AccionCriterio.Menor && Tipo == TipoCriterio.Cantidad ?
                        string.Compare(propertyValueX as string, propertyValueY as string)
                            : Accion == AccionCriterio.Mayor && Tipo == TipoCriterio.Longitud ?
                             (propertyValueY as string).Length.CompareTo((propertyValueX as string).Length)
                                : (propertyValueX as string).Length.CompareTo((propertyValueY as string).Length);
            }
            else if (tipoPropiedad.Equals(typeof(Int32)))
            {
                return Accion == AccionCriterio.Mayor && Tipo == TipoCriterio.Cantidad ?
                    Convert.ToInt32(propertyValueY).CompareTo(Convert.ToInt32(propertyValueX))
                    : Convert.ToInt32(propertyValueX).CompareTo(Convert.ToInt32(propertyValueY));
            }
            else
            {
                throw new Exception("Type not implemented");
            }
        }
    }
}
