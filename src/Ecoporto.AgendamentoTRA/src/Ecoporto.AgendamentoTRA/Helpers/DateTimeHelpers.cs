using System;
using System.Data.SqlTypes;

namespace Ecoporto.AgendamentoTRA.Helpers
{
    public static class DateTimeHelpers
    {
        public static bool IsDate(object data)
        {
            return DateTime.TryParse(data?.ToString(), out _);
        }

        public static bool IsNullDate(DateTime data)
        {
            if (data == null)
                return true;

            return (data == default(DateTime) || data == SqlDateTime.MinValue.Value);
        }

        public static bool IntervaloDeDatas(DateTime data, DateTime inicio, DateTime fim)
        {
            return data >= inicio && data <= fim;
        }
    }
}