using System;

namespace publiquejas
{
    public interface IRankeable
    {
        object getPropertyValue(string propertyName);
        Type GetPropertyType(string propiedad);
    }
}