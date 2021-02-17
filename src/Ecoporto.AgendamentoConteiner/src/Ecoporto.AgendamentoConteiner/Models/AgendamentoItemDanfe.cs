namespace Ecoporto.AgendamentoConteiner.Models
{
    public class AgendamentoItemDanfe
    {
        public AgendamentoItemDanfe(int item, string dANFE)
        {
            Item = item;
            DANFE = dANFE;
        }

        public int Item { get; set; }

        public string DANFE { get; set; }
    }
}