using System;

namespace Ecoporto.AgendamentoCS.Extensions
{
    public static class BooleanExtensions
    {
        public static int ToInt(this bool valor)
            => Convert.ToInt32(valor);

        public static bool ToBoolean(this int valor)
            => Convert.ToInt32(valor) > 0;
    }
}
