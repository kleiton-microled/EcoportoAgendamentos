namespace Ecoporto.Portal.Models
{
    public class UsuarioAutenticado
    {
        public string Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string EmpresaCnpj { get; set; }

        public int EmpresaId { get; set; }

        public bool Ativo { get; set; }

        public string CPF { get; set; }
    }
}