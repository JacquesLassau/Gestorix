using System.Web.Mvc;
using DispelDLL.DML;
using DispelDLL.BLL;
using DispelDLL.Infrastructure;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;

namespace Dispel.Controllers
{
    public class RelatorioController : Controller
    {
        #region Construtor

        private BoRelatorio _boRelatorio;

        public RelatorioController()
        {
            _boRelatorio = new BoRelatorio();
        }
        #endregion
        [HttpGet]
        public ActionResult RelatorioUI()
        {
            if (Models.UsuarioValido.ValidUser())
                return View();
            else
                return RedirectToAction("Login", "Authentication");
        }

        [HttpGet]
        public JsonResult BuscarRelatorioAnalitico(string dataInicio, string dataFim)
        {
            if (Models.UsuarioValido.ValidUser())
            {
                DataSet relatorioAnalitico = _boRelatorio.RelatorioAnaliticoPedidos(Convert.ToDateTime(dataInicio), Convert.ToDateTime(dataFim));

                var result = new Dictionary<string, object>();
                for (int i = 0; i < relatorioAnalitico.Tables.Count; i++)
                {
                    var tabela = relatorioAnalitico.Tables[i];
                    var linhas = new List<Dictionary<string, object>>();
                    foreach (DataRow row in tabela.Rows)
                    {
                        var linha = new Dictionary<string, object>();
                        foreach (DataColumn col in tabela.Columns)
                        {
                            linha[col.ColumnName] = row[col];
                        }
                        linhas.Add(linha);
                    }
                    result[$"Tabela{i + 1}"] = linhas;
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
    }
}