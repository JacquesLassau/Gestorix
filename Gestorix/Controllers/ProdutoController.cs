using System;
using System.Web.Mvc;
using GestorixDLL.DML;
using GestorixDLL.BLL;
using GestorixDLL.Infrastructure;

namespace Gestorix.Controllers
{
    public class ProdutoController : Controller
    {
        #region Construtor
        private Produto _produto;
        private BoProduto _boProduto;
        private ProdutoViewModelDML _produtoViewModelDML;
        private const string produto = "Produto(s)";

        public ProdutoController()
        {
            _produto = new Produto();
            _boProduto = new BoProduto();
            _produtoViewModelDML = new ProdutoViewModelDML();
        }
        #endregion

        #region Requisições
        [HttpGet]
        public ActionResult CadastrarProdutoUI()
        {
            if (Models.UsuarioValido.ValidUser())
                return View();
            else
                return RedirectToAction("Login", "Authentication");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarProduto(Produto produto)
        {
            if (Models.UsuarioValido.ValidUser())
            {
                try
                {
                    string mensagem = string.Empty;
                    Produto produtoExiste = _boProduto.SelecionaProduto(produto.ReferenciaProduto);

                    if (produtoExiste != null && produtoExiste.ReferenciaProduto != null)
                    {
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = string.Format(ConstantesProduto.PRODUTO_EXISTE, produto.ReferenciaProduto);
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ConstantesAlerta.DESCRICAO_NAO_VALIDO;
                    }
                    else if (!string.IsNullOrWhiteSpace(produto.PrecoProduto) && !string.IsNullOrWhiteSpace(Utilidades.VerificarCustoPreco(produto.CustoProduto, produto.PrecoProduto, out string critica)))
                    {
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = critica;
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ConstantesAlerta.DESCRICAO_NAO_VALIDO;
                    }
                    else
                    {
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = _boProduto.IncluirProduto(produto);
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ConstantesAlerta.SUCESSO;
                    }
                }
                catch (Exception ex)
                {
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesAlerta.ERRO_NAO_TRATADO;
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ex.Message;
                }

                return RedirectToAction("CadastrarProdutoUI", "Produto");
            }                
            else
                return RedirectToAction("Login", "Authentication");            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarProduto(Produto produto)
        {
            if (Models.UsuarioValido.ValidUser())
            {
                try
                {
                    string mensagem = string.Empty;

                    if (!string.IsNullOrWhiteSpace(produto.PrecoProduto) && !string.IsNullOrWhiteSpace(Utilidades.VerificarCustoPreco(produto.CustoProduto, produto.PrecoProduto, out string critica)))
                    {
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = critica;
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ConstantesAlerta.DESCRICAO_NAO_VALIDO;
                        return RedirectToAction("EditarProdutoUI", "Produto");
                    }
                    else
                    {
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = _boProduto.EditarProduto(produto);
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ConstantesAlerta.SUCESSO;
                    }
                }
                catch (Exception ex)
                {
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesAlerta.ERRO_NAO_TRATADO;
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ex.Message;
                }

                return RedirectToAction("BuscarProdutoUI", "Produto");
            }                
            else
                return RedirectToAction("Login", "Authentication");            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirProduto(Produto produto)
        {
            if (Models.UsuarioValido.ValidUser())
            {
                try
                {
                    string mensagem = _boProduto.ExcluirProduto(produto);

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

                return RedirectToAction("BuscarProdutoUI", "Produto");
            }                
            else
                return RedirectToAction("Login", "Authentication");            
        }

        [HttpGet]
        public ActionResult IframeListaProdutoUI()
        {
            if (Models.UsuarioValido.ValidUser())
            {
                ProdutoViewModelDML produtos = _produtoViewModelDML.ConvertToListProduto(_boProduto.ListaProdutos());
                return View(produtos);
            }                
            else
                return RedirectToAction("Login", "Authentication");            
        }

        [HttpGet]
        public ActionResult BuscarProdutoUI()
        {

            return View();
        }

        [ChildActionOnly]
        public PartialViewResult _ModalListaProdutoUI()
        {
            return PartialView();
        }

        [HttpGet]
        public JsonResult SelecionarProduto(string referenciaProduto)
        {
            _produto = _boProduto.SelecionaProduto(referenciaProduto);
            return Json(_produto, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult BuscarProduto(string referenciaProduto)
        {
            try
            {
                string btnTipoAcao = Request["btnReferenciaProduto"];

                if (string.IsNullOrWhiteSpace(btnTipoAcao))
                {
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = string.Format(ConstantesAlerta.NAO_ENCONTRADO, produto);
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = string.Format(ConstantesAlerta.NAO_ENCONTRADO_DESCRICAO, produto);
                    return RedirectToAction("BuscarProdutoUI", "Produto");
                }

                if (string.IsNullOrWhiteSpace(referenciaProduto))
                {
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = string.Format(ConstantesAlerta.NAO_ENCONTRADO, produto);
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = string.Format(ConstantesAlerta.NAO_ENCONTRADO_DESCRICAO, produto);
                    return RedirectToAction("BuscarProdutoUI", "Produto");
                }

                _produto = _boProduto.SelecionaProduto(Convert.ToString(referenciaProduto));

                if (_produto != null && !string.IsNullOrWhiteSpace(_produto.ReferenciaProduto))
                {
                    switch (btnTipoAcao)
                    {
                        case "Detalhes":
                            return View("DetalhesProdutoUI", _produto);

                        case "Editar":
                            return View("EditarProdutoUI", _produto);

                        case "Excluir":
                            return View("ExcluirProdutoUI", _produto);

                        default:
                            TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = string.Format(ConstantesAlerta.NAO_ENCONTRADO, produto);
                            TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = string.Format(ConstantesAlerta.NAO_ENCONTRADO_DESCRICAO, produto);
                            return RedirectToAction("BuscarProdutoUI", "Produto");
                    }
                }
                else
                {
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = string.Format(ConstantesAlerta.NAO_ENCONTRADO, produto);
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = string.Format(ConstantesAlerta.NAO_ENCONTRADO_DESCRICAO, produto);
                    return RedirectToAction("BuscarProdutoUI", "Produto");
                }
            }
            catch (Exception e)
            {
                TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesAlerta.ERRO_NAO_TRATADO;
                TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = e.Message;
                return RedirectToAction("BuscarProdutoUI", "Produto");
            }
        }

        [HttpGet]
        public ActionResult DetalhesProdutoUI()
        {
            if (Models.UsuarioValido.ValidUser())
                return View();
            else
                return RedirectToAction("Login", "Authentication");
        }

        [HttpGet]
        public ActionResult EditarProdutoUI()
        {
            if (Models.UsuarioValido.ValidUser())
                return View();
            else
                return RedirectToAction("Login", "Authentication");
        }

        [HttpGet]
        public ActionResult ExcluirProdutoUI()
        {
            if (Models.UsuarioValido.ValidUser())
                return View();
            else
                return RedirectToAction("Login", "Authentication");
        }

        [HttpPost]
        public JsonResult AtualizarProdutoNoEstoque(string referenciaProduto, string quantidadeProduto, string controle)
        {
            if (Models.UsuarioValido.ValidUser())
                return Json(_boProduto.AtualizarProdutoEstoque(referenciaProduto, quantidadeProduto, controle));
            else 
                return Json(string.Empty);
        }

        [ChildActionOnly]
        public PartialViewResult _EstoqueAdicaoProduto()
        {
            if (Models.UsuarioValido.ValidUser())
                return PartialView("~/Views/Shared/_EstoqueAdicaoProduto.cshtml");
            else
                return null;
        }

        [ChildActionOnly]
        public PartialViewResult _EstoqueRemocaoProduto()
        {
            if (Models.UsuarioValido.ValidUser())
                return PartialView("~/Views/Shared/_EstoqueRemocaoProduto.cshtml");
            else
                return null;
        }

        [ChildActionOnly]
        public PartialViewResult _ConfirmarExclusao()
        {
            if (Models.UsuarioValido.ValidUser())
                return PartialView("~/Views/Shared/_ConfirmaExclusao.cshtml");
            else
                return null;
        }
        #endregion
    }
}