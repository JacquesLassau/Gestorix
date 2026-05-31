using System.Web.Mvc;
using DispelDLL.DML;
using DispelDLL.BLL;
using DispelDLL.Infrastructure;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Dispel.Controllers
{
    public class PedidoController : Controller
    {
        #region Construtor
        private Pedido _pedido;
        private ItemPedido _itemPedido;
        private ClientePedido _clientePedido;
        private VeiculoPedido _veiculoPedido;
        private BoPedido _boPedido;
        private PedidoViewModelDML _pedidoViewModelDML;

        private BoProduto _boProduto;

        public PedidoController()
        {
            _pedido = new Pedido();
            _itemPedido = new ItemPedido();
            _clientePedido = new ClientePedido();
            _veiculoPedido = new VeiculoPedido();
            _boPedido = new BoPedido();
            _pedidoViewModelDML = new PedidoViewModelDML();

            _boProduto = new BoProduto();
        }
        #endregion

        [HttpGet]
        public ActionResult MonitorUI()
        {
            if (Models.UsuarioValido.ValidUser())
            {
                PedidoViewModelDML pedidos = _pedidoViewModelDML.ConvertToListPedidos(_boPedido.ListarPedidos());
                return View(pedidos);
            }
            else
                return RedirectToAction("Login", "Authentication");

        }

        [HttpPost]
        public JsonResult CadastrarPedido(Pedido pedido)
        {
            if (Models.UsuarioValido.ValidUser())
            {
                try
                {
                    List<string> itensCriticados = new List<string>();
                    if (pedido.ItensPedido != null && pedido.ItensPedido.Count > 0)
                    {
                        foreach (var item in pedido.ItensPedido)
                        {
                            if (item.Tipo == ConstantesProduto.PRODUTO)
                            {
                                Produto produto = _boProduto.SelecionaProduto(item.Referencia);
                                if (produto.ReferenciaProduto == null)
                                    itensCriticados.Add(string.Concat(item.Referencia, " - ", item.Descricao));
                            }
                        }
                    }
                    else
                    {
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesPedido.PEDIDO_ITENS_NAO_ENVIADOS;
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ConstantesAlerta.ERRO_NAO_TRATADO;
                        return Json("", JsonRequestBehavior.AllowGet);
                    }

                    if (pedido.ClientePedido != null &&
                        string.IsNullOrWhiteSpace(pedido.ClientePedido.Nome) && 
                        string.IsNullOrWhiteSpace(pedido.ClientePedido.Documento))
                    {
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesPedido.PEDIDO_CLIENTE_NAO_ENVIADO;
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ConstantesAlerta.ERRO_NAO_TRATADO;
                        return Json("", JsonRequestBehavior.AllowGet);
                    }

                    if (pedido.VeiculoPedido != null &&
                        string.IsNullOrWhiteSpace(pedido.VeiculoPedido.Descricao) && 
                        string.IsNullOrWhiteSpace(pedido.VeiculoPedido.Placa) &&
                        string.IsNullOrWhiteSpace(pedido.VeiculoPedido.Placa))
                    {
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesPedido.PEDIDO_VEICULO_NAO_ENVIADO;
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ConstantesAlerta.ERRO_NAO_TRATADO;
                        return Json("", JsonRequestBehavior.AllowGet);
                    }

                    if (itensCriticados.ToList().Count() == 0)
                    {
                        if (_boPedido.CadastrarPedido(pedido))
                        {
                            TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesPedido.PEDIDO_ATUALIZADO;
                            TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ConstantesAlerta.SUCESSO;
                        }
                    }
                    else
                    {
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesPedido.PEDIDO_ITENS_INEXISTENTES;
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ListaItensCriicados(itensCriticados);
                    }

                }
                catch (Exception ex)
                {
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesAlerta.ERRO_NAO_TRATADO;
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ex.Message;
                }
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AtualizarPedido(Pedido pedido)
        {
            if (Models.UsuarioValido.ValidUser())
            {

                try
                {
                    if (_boPedido.AtualizarPedido(pedido))
                    {
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesPedido.PEDIDO_ATUALIZADO;
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ConstantesAlerta.SUCESSO;
                    }
                    else
                    {
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesPedido.PEDIDO_NAO_ATUALIZADO;
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ConstantesAlerta.ERRO_NAO_TRATADO;
                    }
                }
                catch (Exception ex)
                {
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesAlerta.ERRO_NAO_TRATADO;
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ex.Message;
                }
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ExcluirPedido(int IdPedido)
        {
            if (Models.UsuarioValido.ValidUser())
            {

                try
                {
                    if (_boPedido.InativarPedido(IdPedido))
                    {
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesPedido.PEDIDO_INATIVADO;
                        TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ConstantesAlerta.SUCESSO;
                    }
                }
                catch (Exception ex)
                {
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesAlerta.ERRO_NAO_TRATADO;
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ex.Message;
                }
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult BuscarItemParaPedido(string referencia)
        {
            if (Models.UsuarioValido.ValidUser())
            {
                _itemPedido = _boPedido.ConsultarItemParaPedido(referencia);
                var item = _itemPedido;

                return Json(item, JsonRequestBehavior.AllowGet);
            }
            else
                return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObterPedido(int idPedido)
        {
            if (Models.UsuarioValido.ValidUser())
            {
                _pedido = _boPedido.SelecionarItensPedido(idPedido);

                if (_pedido == null)
                    return Json(null, JsonRequestBehavior.AllowGet);

                var itensPedido = _pedido.ItensPedido.Select(i => new
                {
                    IdPedido = i.IdPedido,
                    Tipo = i.Tipo,
                    Referencia = i.Referencia,
                    Descricao = i.Descricao,
                    Custo = Convert.ToDecimal(i.Custo),
                    Preco = Convert.ToDecimal(i.Preco),
                    Lucro = Convert.ToDecimal(i.Lucro),
                    Quantidade = i.Quantidade
                }).ToList();
                return Json(new { itensPedido }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { string.Empty }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult BuscarClientePedido(string nrOrcamento)
        {
            if (Models.UsuarioValido.ValidUser())
            {
                _clientePedido = _boPedido.BuscarClientePedido(nrOrcamento);
                var item = _clientePedido;

                return Json(item, JsonRequestBehavior.AllowGet);
            }
            else
                return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult BuscarVeiculoPedido(string nrOrcamento)
        {
            if (Models.UsuarioValido.ValidUser())
            {
                _veiculoPedido = _boPedido.BuscarVeiculoPedido(nrOrcamento);
                var item = _veiculoPedido;

                return Json(item, JsonRequestBehavior.AllowGet);
            }
            else
                return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CadastrarClientePedido(ClientePedido clientePedido)
        {
            if (Models.UsuarioValido.ValidUser())
            {
                

                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CadastrarVeiculoPedido(VeiculoPedido pClientePedido)
        {
            if (Models.UsuarioValido.ValidUser())
            {


                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("", JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public PartialViewResult _OrcamentoPedido()
        {
            if (Models.UsuarioValido.ValidUser())
                return PartialView("~/Views/Pedido/_OrcamentoPedido.cshtml");

            return null;
        }

        private string ListaItensCriicados(List<string> itens)
        {
            string corpoHtml = "<ul>";

            foreach (var item in itens)
                corpoHtml = string.Concat(corpoHtml, "<li>", item, "</li>");

            corpoHtml = string.Concat(corpoHtml, "</ul>");

            return corpoHtml;
        }
    }
}