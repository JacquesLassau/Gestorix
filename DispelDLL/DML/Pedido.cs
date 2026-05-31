using System;
using System.Collections.Generic;

namespace DispelDLL.DML
{
    public class Pedido
    {
        public Pedido() { }

        public int IdPedido { get; set; }
        public DateTime DataHoraPedidoFinalizado { get; set; }
        public DateTime DataHoraPedidoExcluido { get; set; }
        public ClientePedido ClientePedido { get; set; }
        public VeiculoPedido VeiculoPedido { get; set; }
        public List<ItemPedido> ItensPedido { get; set; }  
        public string DescricaoItens { get; set; }
        public string TotalCusto { get; set; }
        public string TotalLucro { get; set; }
        public string TotalPedido { get; set; }
        public string Situacao { get; set; }
        public int TotalPedidosOrcados { get; set; }

        public Pedido(int idPedido, DateTime dataHoraPedidoFinalizado, DateTime dataHoraPedidoExcluido, string descricaoItens, string totalCusto, string totalLucro, string totalPreco, string situacao)
        {
            this.IdPedido = idPedido;
            this.DataHoraPedidoFinalizado = dataHoraPedidoFinalizado;
            this.DataHoraPedidoExcluido = dataHoraPedidoExcluido;
            this.DescricaoItens = descricaoItens;
            this.TotalCusto = totalCusto;
            this.TotalLucro = totalLucro;
            this.TotalPedido = totalPreco;
            this.Situacao = situacao;
        }
    }

    public class PedidoViewModelDML
    {
        public List<Pedido> Pedido { get; set; }

        public PedidoViewModelDML()
        {
            this.Pedido = new List<Pedido>();
        }

        public PedidoViewModelDML ConvertToListPedidos(List<Pedido> listaPedido)
        {
            PedidoViewModelDML pedidoViewModel = new PedidoViewModelDML();
            if (listaPedido != null)
            {
                // pedidoViewModel.Pedido = listaPedido;
                // foreach está sendo usado para CASO deseje incluir validação no carregamento dos registros via conversão
                foreach (var pedido in listaPedido)
                {
                    pedidoViewModel.Pedido.Add(pedido);
                }
            }

            return pedidoViewModel;
        }
    }
    public class ItemPedido
    {
        public int IdPedido { get; set; }
        public string Tipo { get; set; }
        public string Referencia { get; set; }
        public string Descricao { get; set; }
        public string Custo { get; set; }
        public string Preco { get; set; }
        public string Lucro { get; set; }
        public string Quantidade { get; set; }        

    }

    public class ClientePedido
    {
        public int IdPedido { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }        
        public string Cep { get; set; }
    }

    public class VeiculoPedido
    {
        public int IdPedido { get; set; }
        public string Descricao { get; set; }
        public string Placa { get; set; }
        public string QuilometrosRodados { get; set; }       
    }

    public class ItemPedidoViewModelDML
    {
        public List<ItemPedido> ItemPedido { get; set; }

        public ItemPedidoViewModelDML()
        {
            this.ItemPedido = new List<ItemPedido>();
        }

        public ItemPedidoViewModelDML ConvertToListItensPedido(List<ItemPedido> listaItensPedido)
        {
            ItemPedidoViewModelDML itemPedidoViewModel = new ItemPedidoViewModelDML();
            if (listaItensPedido != null)
            {
                // pedidoViewModel.Pedido = listaPedido;
                // foreach está sendo usado para CASO deseje incluir validação no carregamento dos registros via conversão
                foreach (var itemPedido in listaItensPedido)
                {
                    itemPedidoViewModel.ItemPedido.Add(itemPedido);
                }
            }

            return itemPedidoViewModel;
        }
    }
}