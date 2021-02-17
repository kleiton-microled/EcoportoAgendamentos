using Ecoporto.Portal.Extensions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Ecoporto.Portal.Helpers
{
    public static class Validation
    {
        public static MvcHtmlString ValidarFormulario(this HtmlHelper helper, ModelStateDictionary modelState, string titulo, bool update)
        {
            var erros = modelState.Values
                .SelectMany(x => x.Errors)
                .ToList();

            var sucesso = helper.ViewContext.TempData["Sucesso"]?
                .ToString()
                .ToBoolean();

            var acao = update ? "atualizado" : "cadastrado";

            var msgSucesso = $@"<div 
                                    class='alert alert-success' 
                                    role='alert'>{titulo} <strong>{ acao }</strong> com sucesso!
                                    <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>
                                </div>";

            var itens = string.Empty;

            foreach (var erro in erros)
            {
                itens += $"<li>{ erro.ErrorMessage }</li>";
            }

            var msgErro = $@"<div class='alert alert-danger' role='alert'>
                                <ul>
                                    {itens}
                                </ul>
                             </div> ";

            var devolve = string.Empty;

            if (erros.Count() == 0 && sucesso.GetValueOrDefault())
                return MvcHtmlString.Create(msgSucesso);

            if (erros.ToArray().Length > 0 && !sucesso.GetValueOrDefault())
                return MvcHtmlString.Create(msgErro);

            return MvcHtmlString.Create(string.Empty);

        }

        public static bool EmailValido(string email)
        {
            if (!string.IsNullOrEmpty(email))
                return new Regex("^[A-Za-z0-9](([_.-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([.-]?[a-zA-Z0-9]+)*)([.][A-Za-z]{2,4})$").Match(email.Trim()).Success;

            return false;
        }

        public static IEnumerable<ValidationFailure> ValidarListaDeEmails(string entrada)
        {
            var lista = new List<ValidationFailure>();

            if (!string.IsNullOrEmpty(entrada))
            {
                var emails = entrada.Split(';');

                foreach (var email in emails)
                {
                    if (!string.IsNullOrEmpty(email) && !EmailValido(email))
                        lista.Add(new ValidationFailure(email, $"{email} é um email inválido"));
                }
            }

            return lista;
        }

        public static MvcHtmlString ValidarFormulario(this HtmlHelper helper, ModelStateDictionary modelState, bool update)
        {
            var erros = modelState.Values
                .SelectMany(x => x.Errors)
                .ToList();

            var sucesso = helper.ViewContext.TempData["Sucesso"]?
                .ToString()
                .ToBoolean();

            var acao = update ? "atualizado" : "cadastrado";

            var msgSucesso = $@"<div 
                                    class='alert alert-success' 
                                    role='alert'>Registro <strong>{ acao }</strong> com sucesso!
                                    <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>
                                </div>";

            var itens = string.Empty;

            foreach (var erro in erros)
            {
                itens += $"<li>{ erro.ErrorMessage }</li>";
            }

            var msgErro = $@"<div class='alert alert-danger' role='alert'>
                                <ul>
                                    {itens}
                                </ul>
                             </div> ";

            var devolve = string.Empty;

            if (erros.Count() == 0 && sucesso.GetValueOrDefault())
                return MvcHtmlString.Create(msgSucesso);

            if (erros.ToArray().Length > 0 && !sucesso.GetValueOrDefault())
                return MvcHtmlString.Create(msgErro);

            return MvcHtmlString.Create(string.Empty);
        }

        public static bool CPFValido(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            string[] dadosArray = new[] { "11111111111", "22222222222", "33333333333", "44444444444", "55555555555", "66666666666", "77777777777", "88888888888", "99999999999" };

            for (int i = 0; i <= dadosArray.Length - 1; i++)
            {
                if (cpf.Length != 11 | dadosArray[i].Equals(cpf))
                    return false;
            }

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static bool CNHValida(string valor)
        {
            bool isValid = false;

            if (string.IsNullOrEmpty(valor))
                return false;

            var cnh = valor;

            if (cnh.Length == 11 && cnh != new string('1', 11))
            {
                var dsc = 0;
                var v = 0;

                for (int i = 0, j = 9; i < 9; i++, j--)
                {
                    v += (Convert.ToInt32(cnh[i].ToString()) * j);
                }

                var vl1 = v % 11;

                if (vl1 >= 10)
                {
                    vl1 = 0;
                    dsc = 2;
                }

                v = 0;

                for (int i = 0, j = 1; i < 9; ++i, ++j)
                {
                    v += (Convert.ToInt32(cnh[i].ToString()) * j);
                }

                var x = v % 11;
                var vl2 = (x >= 10) ? 0 : x - dsc;

                isValid = vl1.ToString() + vl2.ToString() == cnh.Substring(cnh.Length - 2, 2);
            }

            return isValid;
        }

        public static bool RenavamValido(string renavam)
        {
            if (string.IsNullOrEmpty(renavam.Trim())) return false;

            int[] d = new int[11];
            string sequencia = "3298765432";
            string SoNumero = Regex.Replace(renavam, "[^0-9]", string.Empty);

            if (string.IsNullOrEmpty(SoNumero)) return false;

            if (new string(SoNumero[0], SoNumero.Length) == SoNumero) return false;
            SoNumero = Convert.ToInt64(SoNumero).ToString("00000000000");

            int v = 0;

            for (int i = 0; i < 11; i++)
                d[i] = Convert.ToInt32(SoNumero.Substring(i, 1));

            for (int i = 0; i < 10; i++)
                v += d[i] * Convert.ToInt32(sequencia.Substring(i, 1));

            v = (v * 10) % 11; v = (v != 10) ? v : 0;
            return (v == d[10]);
        }

        public static bool CNHEmDia(DateTime? validadeCNH)
        {
            if (validadeCNH != null)
                return (validadeCNH > DateTime.Now.AddDays(-30));

            return false;
        }
    }
}