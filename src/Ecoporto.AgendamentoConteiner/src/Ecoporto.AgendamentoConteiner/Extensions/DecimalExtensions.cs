using System;

namespace Ecoporto.AgendamentoConteiner.Extensions
{
    public static class DecimalExtensions
    {
        public static string PtBr(this decimal valor)
        {
            if (Decimal.TryParse(valor.ToString(), out Decimal convertido))
                return convertido.ToString("N2");

            return "0";
        }
    }
}