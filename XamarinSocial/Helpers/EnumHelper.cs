using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace XamarinSocial.Helpers
{
    public static class EnumHelper
    {
        public static Dictionary<string, string> GetCollectionByType(Type type)
        {
            var values = Enum.GetNames(type);
            var resultGenders = new Dictionary<string, string>();
            foreach (var value in values)
            {
                if (value.Equals("None") || value.Equals("Default"))
                {
                    continue;
                }
                string description = GetEnumDescription(Enum.Parse(type, value));
                resultGenders.Add(value, description);
            }
            return resultGenders;
        }

        public static string GetEnumDescription<T>(this T source)
        {
            FieldInfo fieldInfo = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return source.ToString();
        }
    }
}
