using Ecoporto.Portal.Enums;

namespace Ecoporto.Portal.Models
{
    public class Usuario
    {
        public string Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public int EmpresaLoginId { get; set; }

        public int EmpresaId { get; set; }

        public string EmpresaDescricao { get; set; }

        public string EmpresaDescricaoCurta { get; set; }

        public string EmpresaCnpj { get; set; }

        public bool Ativo { get; set; }

        public string CPF { get; set; }

        public bool Administrador { get; set; }

        public Perfil Perfil { get; set; }
    }
}