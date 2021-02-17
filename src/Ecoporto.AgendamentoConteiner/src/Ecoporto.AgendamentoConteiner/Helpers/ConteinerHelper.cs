using System.Collections.Generic;

namespace Ecoporto.AgendamentoConteiner.Helpers
{
    public static class ConteinerHelper
    {
        private static readonly List<string> TiposReefer = new List<string>()
        {
            "HR", "RE", "HF"
        };

        private static readonly List<string> TiposExcesso = new List<string>()
        {
            "FR"
        };

        public static bool Reefer(string tipo)
        {
            return TiposReefer.Contains(tipo);
        }

        public static bool Excesso(string tipo)
        {
            return TiposExcesso.Contains(tipo);
        }
    }
}