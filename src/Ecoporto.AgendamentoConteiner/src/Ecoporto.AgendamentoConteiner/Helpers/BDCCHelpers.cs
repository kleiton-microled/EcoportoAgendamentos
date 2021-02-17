namespace Ecoporto.AgendamentoConteiner.Helpers
{
    public static class BDCCHelpers
    {
        public static bool ErroBDCC(string retornoBDCC)
        {
            return (retornoBDCC == "2" || retornoBDCC == "4" || retornoBDCC == "5");
        }
    }
}