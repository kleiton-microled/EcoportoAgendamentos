using System;

namespace Ecoporto.AgendamentoTRA.Models
{
    /// <summary>
    /// Class that encapsulates most common parameters sent by DataTables plugin
    /// </summary>
    public class JQueryDataTablesParamViewModel
    {
        /// <summary>
        /// Request sequence number sent by DataTable,
        /// same value must be returned in response
        /// </summary>
        public string sEcho { get; set; }

        /// <summary>
        /// Text used for filtering
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        public int iSortingCols { get; set; }

        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        public string sColumns { get; set; }

        /// <summary>
        /// Which column is ordering the result
        /// </summary>
        public int iSortCol_0 { get; set; }

        /// <summary>
        /// Is the sort ASC or DESC?
        /// </summary>
        public string sSortDir_0 { get; set; }

        public string OrderByColumn => sColumns.Split(',')[iSortCol_0];

        public string OrderBy => iSortingCols > 0 ? (OrderByColumn != string.Empty ? (" ORDER BY " + OrderByColumn + " " + sSortDir_0) : string.Empty) : string.Empty;

        public int Pagina => (int)(Math.Ceiling((decimal)iDisplayStart / (decimal)iDisplayLength) + 1);
    }
}
