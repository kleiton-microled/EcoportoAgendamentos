﻿using System.Web.Mvc;

namespace Ecoporto.AgendamentoConteiner
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
