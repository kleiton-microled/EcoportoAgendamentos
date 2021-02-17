using Ecoporto.AgendamentoCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecoporto.AgendamentoCS.Helpers
{
    public class GerenciadorDeEstado<T> where T : Entidade<T>
    {
        private static readonly string CHAVE = "";

        static GerenciadorDeEstado()
        {
            Type instancia = typeof(T);

            if (instancia == typeof(ReservaItem))
            {
                CHAVE =  $"{HttpContext.Current.Session.SessionID}{instancia.Name}";
            }

            if (instancia == typeof(NotaFiscal))
            {
                CHAVE = $"{HttpContext.Current.Session.SessionID}{instancia.Name}";
            }

            if (instancia == typeof(Upload))
            {
                CHAVE = $"{HttpContext.Current.Session.SessionID}{instancia.Name}";
            }
        }

        internal static void Armazenar(T item)
        {
            item.Id = RetornarTodos()?.Count + 1 ?? 1;

            List<T> registros = RetornarTodos() != null ?
                RetornarTodos() : new List<T>();

            registros.Add(item);

            HttpContext.Current.Session[CHAVE] = registros;
        }

        internal static void ArmazenarLista(List<T> lista)
        {
            List<T> registros = RetornarTodos() != null ?
                RetornarTodos() : new List<T>();

            registros.AddRange(lista);
            HttpContext.Current.Session[CHAVE] = registros;
        }

        internal static void Remover(int id)
        {
            List<T> registros = RetornarTodos();

            var registroBusca = registros.Where(c => c.Id == id).FirstOrDefault();

            if (registroBusca != null)
            {
                registros.Remove(registroBusca);
            }

            HttpContext.Current.Session[CHAVE] = registros;
        }

        internal static void RemoverTodos()
        {
            HttpContext.Current.Session[CHAVE] = new List<T>();
        }

        internal static List<T> RetornarTodos()
        {
            return HttpContext.Current.Session[CHAVE] != null ?
                (List<T>)HttpContext.Current.Session[CHAVE] : null;
        }
    }
}