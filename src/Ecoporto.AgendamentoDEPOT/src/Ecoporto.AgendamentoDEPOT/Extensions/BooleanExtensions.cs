using System;

namespace Ecoporto.AgendamentoDEPOT.Extensions
{
    public static class BooleanExtensions
    {
        public static int ToInt(this bool valor)
            => Convert.ToInt32(valor);

        public static bool ToBoolean(this int valor)
            => Convert.ToInt32(valor) > 0;

        public static string ToSimOuNao(this bool valor)
            => valor ? "Sim" : "Não";
    }
}
