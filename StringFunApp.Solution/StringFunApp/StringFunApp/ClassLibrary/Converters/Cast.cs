using System;

namespace StringFunApp.ClassLibrary.Converters
{
    class Cast<T> where T : class
    {
        public static T perform(Object obj)
        {
            System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty("Instance");
            return propertyInfo == null ? null : propertyInfo.GetValue(obj, null) as T;
        }
    }
}
