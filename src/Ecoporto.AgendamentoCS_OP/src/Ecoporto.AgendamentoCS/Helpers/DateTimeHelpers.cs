using System;
using System.Data.SqlTypes;

namespace Ecoporto.AgendamentoCS.Helpers
{
    public static class DateTimeHelpers
    {
        public static bool IsDate(object data)
        {
            return DateTime.TryParse(data?.ToString(), out _);
        }

        public static bool IsNotDefaultDate(DateTime data)
        {            
            return (data != default(DateTime) && data != SqlDateTime.MinValue.Value);
        }
    }
}