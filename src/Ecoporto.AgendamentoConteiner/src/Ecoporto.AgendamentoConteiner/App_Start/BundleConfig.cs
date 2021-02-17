using Ecoporto.AgendamentoConteiner.Extensions;
using System.Web;
using System.Web.Optimization;

namespace Ecoporto.AgendamentoConteiner
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Content/js/jquery-3.3.1.js",
                         "~/Content/js/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-url")
               .Include("~/Content/plugins/jquery-url/jquery-url.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui")
               .Include("~/Content/plugins/jquery-ui/js/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui-CSS")
                .Include("~/Content/plugins/jquery-ui/css/jquery-ui.css"));

            bundles.Add(new ScriptBundle("~/bundles/site")
               .Include("~/Content/plugins/easyAutocomplete/jquery.easy-autocomplete.js", 
                        "~/Content/js/default.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-validate")
                .Include("~/Content/plugins/jquery-validate/jquery.validate.min.js",
                         "~/Content/plugins/jquery-validate/language/messages_pt_BR.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-mask")
                .Include("~/Content/plugins/jquery-mask/jquery.mask.js"));

            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/Content/css/bootstrap.css",
                         "~/Content/css/estilos.css",
                         "~/Content/css/fontawesome-all.css",
                         "~/Content/plugins/toastr/toastr.css",
                         "~/Content/plugins/easyAutocomplete/easy-autocomplete.css"));

            bundles.Add(new StyleBundle("~/bundles/login")
               .Include("~/Content/css/bootstrap.css",
                        "~/Content/css/login.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Content/js/popper.min.js",
                         "~/Content/js/bootstrap.js"));

            bundles.Add(new StyleBundle("~/bundles/datatablesCSS")
                .Include("~/Content/plugins/datatables/css/dataTables.bootstrap4.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/datatables")
                .Include("~/Content/plugins/datatables/js/jquery.dataTables.min.js",
                         "~/Content/plugins/datatables/js/dataTables.bootstrap4.min.js"));
        
            bundles.Add(new ScriptBundle("~/bundles/agendamento")
                .Include("~/Content/js/agendamento.js"));

            bundles.Add(new ScriptBundle("~/bundles/toastr")
                .Include("~/Content/plugins/toastr/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment")
              .Include("~/Content/plugins/moment/moment-with-locales.js"));

            bundles.Add(new StyleBundle("~/bundles/tagsCSS")
               .Include("~/Content/plugins/tags/tagsinput.css"));

            bundles.Add(new ScriptBundle("~/bundles/tags")
                .Include("~/Content/plugins/tags/tagsinput.js"));

            bundles.Add(new StyleBundle("~/bundles/select2CSS")
                .Include("~/Content/plugins/select2/css/select2.css"));

            bundles.Add(new ScriptBundle("~/bundles/select2")
                .Include("~/Content/plugins/select2/js/select2.min.js"));

            bundles.Add(new StyleBundle("~/bundles/smartWizardCSS")
                .Include("~/Content/plugins/smartWizard/css/smart_wizard.min.css",
                         "~/Content/plugins/smartWizard/css/smart_wizard_theme_dots.min.css",
                         "~/Content/plugins/smartWizard/css/smart_wizard_theme_arrows.min.css",
                         "~/Content/plugins/smartWizard/css/smart_wizard_theme_circles.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/smartWizard")
                .Include("~/Content/plugins/smartWizard/js/jquery.smartWizard.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap4Toggle")
              .Include("~/Content/plugins/bootstrap4-toggle/bootstrap4-toggle.min.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap4ToggleCSS")
                .Include("~/Content/plugins/bootstrap4-toggle/bootstrap4-toggle.min.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
