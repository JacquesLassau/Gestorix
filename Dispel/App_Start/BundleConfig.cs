using System.Web;
using System.Web.Optimization;

namespace Dispel
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
                        
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));            

            bundles.Add(new ScriptBundle("~/bundles/Produto").Include(
                        "~/Scripts/Produto/BuscarProduto.js",
                        "~/Scripts/Produto/ListarProdutos.js",
                        "~/Scripts/Produto/ControleEstoque.js"));

            bundles.Add(new ScriptBundle("~/bundles/Servico").Include(
                        "~/Scripts/Servico/BuscarServico.js",
                        "~/Scripts/Servico/ListarServicos.js"));

            bundles.Add(new ScriptBundle("~/bundles/Pedido").Include(
                        "~/Scripts/Pedido/CadastrarPedido.js",
                        "~/Scripts/Pedido/EditarPedido.js",
                        "~/Scripts/Pedido/ExcluirPedido.js",
                        "~/Scripts/Pedido/BuscarPedido.js"));

            bundles.Add(new ScriptBundle("~/bundles/Relatorio").Include(
                        "~/Scripts//Relatorio/BuscarRelatorio.js"));

            bundles.Add(new ScriptBundle("~/bundles/Genericos").Include(
                        "~/Scripts/Genericos/ChamadasExternasIframes.js",
                        "~/Scripts/Genericos/Utils.js"));
        }
    }
}
