using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Ecoporto.AgendamentoDEPOT.Extensions
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string valor)
        {
            return (T)Enum.Parse(typeof(T), valor);
        }

        public static string ToName(this Enum value)
        {
            if (value == null)
                return string.Empty;

            var member = value.GetType()
                    .GetMember(value.ToString());

            var enumerador = member.FirstOrDefault();

            if (enumerador == null)
                return string.Empty;

            return enumerador.GetCustomAttribute<DisplayAttribute>().Name;
        }

        public static int ToValue(this Enum value)
        {
            return Convert.ToInt32(value);
        }
    }
}