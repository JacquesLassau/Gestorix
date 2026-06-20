using GestorixDLL.Infrastructure;
using System;
using System.Collections.Generic;
using GestorixDLL.DML;
using GestorixDLL.DAL;

namespace GestorixDLL.BLL
{
    public class BoPedido
    {
        DALPedido _daoPedido;
        public BoPedido()
        {
            _daoPedido = new DALPedido();
        }

        /// <summary>
        /// Lista com pedidos na base de dados.
        /// </summary>
        /// <param></param>
        /// <returns>Propriedade do tipo List<Pedido></returns>
        public List<Pedido> ListarPedidos()
        {
            return _daoPedido.ListarPedidosDb();
        }

        /// <summary>
        /// Altera pedido
        /// </summary>
        /// <param></param>
        /// <returns>booleano que indica se a situação foi alterada</returns>
        public bool AtualizarPedido(Pedido pedido)
        {
            if (pedido.ItensPedido != null && pedido.ItensPedido.Count > 0)
                foreach (ItemPedido item in pedido.ItensPedido)
                    pedido.DescricaoItens = string.Concat(pedido.DescricaoItens, string.Concat(item.Descricao, " " + item.Quantidade + "x" + "<br/>"));

            return _daoPedido.AtualizarPedidoDb(pedido);
        }

        /// <summary>
        /// Cadastra um novo pedido
        /// </summary>
        /// <param></param>
        /// <returns>booleano que indica se a situação da ocorrência foi alterada</returns>
        public bool CadastrarPedido(Pedido pedido)
        {
            if (pedido.ItensPedido != null && pedido.ItensPedido.Count > 0)
                foreach (ItemPedido item in pedido.ItensPedido)
                    pedido.DescricaoItens = string.Concat(pedido.DescricaoItens, string.Concat(item.Descricao, " " + item.Quantidade + "x" + "<br/>"));            

            return _daoPedido.CadastrarPedidoDb(pedido);
        }

        /// <summary>
        /// Seleciona um pedido
        /// </summary>
        /// <param></param>
        /// <returns>Objeto do tipo Pedido</returns>
        public Pedido SelecionarItensPedido(int idPedido)
        {
            return _daoPedido.SelecionarItensPedidoDb(idPedido);
        }

        /// <summary>
        /// Consulta dados de um item para o pedido
        /// </summary>
        /// <param></param>
        /// <returns>Objeto do tipo Pedido</returns>
        public ItemPedido ConsultarItemParaPedido(string referencia)
        {
            return _daoPedido.ConsultarItemParaPedidoDb(referencia);
        }

        /// <summary>
        /// Inativa o pedido estornando os itens do estoque
        /// </summary>
        /// <param></param>
        /// <returns>Objeto do tipo Pedido</returns>
        public bool InativarPedido(int idPedido)
        {
            return _daoPedido.InativarPedidoDb(idPedido);
        }

        public ClientePedido BuscarClientePedido(string nrOrcammento)
        {
            return _daoPedido.ConsultarClientePedido(nrOrcammento);
        }

        public VeiculoPedido BuscarVeiculoPedido(string nrOrcammento)
        {
            return _daoPedido.ConsultarVeiculoPedido(nrOrcammento);
        }
    }
}
