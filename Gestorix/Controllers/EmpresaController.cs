using System;
using System.Web.Mvc;
using System.Web.Services.Description;
using GestorixDLL.BLL;
using GestorixDLL.DML;
using GestorixDLL.Infrastructure;

namespace Gestorix.Controllers
{
    public class EmpresaController : Controller
    {
        #region Construtor
        private Empresa _empresa;
        private BoEmpresa _boEmpresa;
        private EmpresaViewModelDML _empresaViewModelDML;
        private string cnpjDispel = "30.549.665/0001-10";

        public EmpresaController()
        {
            _empresa = new Empresa();
            _boEmpresa = new BoEmpresa();
            _empresaViewModelDML = new EmpresaViewModelDML();
        }
        #endregion

        [HttpGet]
        public ActionResult EditarEmpresaUI()
        {
            if (Models.UsuarioValido.ValidUser())
            {
                _empresa = _boEmpresa.SelecionaEmpresa(cnpjDispel);
                return View("EditarEmpresaUI", _empresa);
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        [HttpGet]
        public JsonResult SelecionarEmpresa()
        {
            _empresa = _boEmpresa.SelecionaEmpresa(cnpjDispel);
            return Json(_empresa, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarEmpresa(Empresa empresa)
        {
            if (Models.UsuarioValido.ValidUser())
            {
                try
                {
                    string mensagem = _boEmpresa.EditarEmpresa(empresa);

                    if (!string.IsNullOrWhiteSpace(mensagem))
                    {
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = mensagem;
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ConstantesAlerta.SUCESSO;
                    }
                }
                catch (Exception ex)
                {
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesAlerta.ERRO_NAO_TRATADO;
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ex.Message;
                }

                return RedirectToAction("EditarEmpresaUI", "Empresa");
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }

            
        }
    }
}