using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using GestorixDLL.DML;
using GestorixDLL.BLL;
using GestorixDLL.Infrastructure;
using System.Globalization;

namespace GestorixDLL.DAL
{
    public class DALPedido
    {
        public Conexao Conexao { get; set; }

        public DALPedido()
        {
            Conexao = new Conexao();
        }

        public ItemPedido ConsultarItemParaPedidoDb(string referencia)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_SelecionarItemParaPedido", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@Referencia", SqlDbType.VarChar, 50);
                comandoDML.Parameters["@Referencia"].Value = referencia;

                SqlDataReader dr = comandoDML.ExecuteReader();
                bool consultarExistenciaItem = dr.HasRows;

                ItemPedido itemPedido = new ItemPedido();
                if (consultarExistenciaItem)
                {
                    while (dr.Read())
                    {
                        itemPedido.Tipo = Convert.ToString(dr["TIPO"]);
                        itemPedido.Descricao = Convert.ToString(dr["DESCRICAO"]);
                        itemPedido.Custo = dr["CUSTO"] != DBNull.Value ? ((decimal)dr["CUSTO"]).ToString(CultureInfo.InvariantCulture) : null;

                        if (itemPedido.Tipo == ConstantesProduto.PRODUTO)
                            itemPedido.Preco = dr["PRECO"] != DBNull.Value ? ((decimal)dr["PRECO"]).ToString(CultureInfo.InvariantCulture) : null;
                    }
                }

                conexao.Close();
                return itemPedido;
            }
        }

        public Pedido SelecionarItensPedidoDb(int idPedido)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_SelecionarItensPedido", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@IdPedido", SqlDbType.Int).Value = idPedido;

                SqlDataReader dr = comandoDML.ExecuteReader();

                Pedido pedido = new Pedido
                {
                    ItensPedido = new List<ItemPedido>()
                };

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ItemPedido item = new ItemPedido
                        {
                            IdPedido = Convert.ToInt32(dr["IdPEDIDO"]),
                            Tipo = Convert.ToString(dr["TIPoITEM"]),
                            Referencia = Convert.ToString(dr["IdREFERENCIA"]),
                            Descricao = Convert.ToString(dr["DESCRICAO"]),
                            Custo = Convert.ToString(dr["CUSTO"]),
                            Preco = Convert.ToString(dr["PRECO"]),
                            Lucro = Convert.ToString(dr["LUCRO"]),
                            Quantidade = Convert.ToString(dr["QUANTIDADE"])
                        };

                        pedido.ItensPedido.Add(item);
                    }
                }

                conexao.Close();
                return pedido;
            }
        }


        public List<Pedido> ListarPedidosDb()
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                List<Pedido> pedidos = new List<Pedido>();

                SqlCommand comandoDML = new SqlCommand("SP_ListarPedidos", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = comandoDML.ExecuteReader();

                int count = 0;
                while (dr.Read())
                {
                    if (dr["SITUACAO"].ToString() == Constantes.ATIVO)
                    {
                        int codigo = Convert.ToInt32(dr["IdPEDIDO"]);
                        DateTime datahoraFim = Convert.ToDateTime(dr["DATaHORaFINALIZADO"]);

                        DateTime datahoraExcluido = DateTime.MinValue;
                        if (dr["DATaHORaEXCLUIDO"] != DBNull.Value)
                            datahoraExcluido = Convert.ToDateTime(dr["DATaHORaEXCLUIDO"]);

                        string descricao = Convert.ToString(dr["DESCRICAO"]);
                        string custo = Convert.ToString(dr["TOTAlCUSTO"]);
                        string recebimento = Convert.ToString(dr["TOTAlLUCRO"]);
                        string precoTotal = Convert.ToString(dr["TOTAlPEDIDO"]);
                        string situacao = Convert.ToString(dr["SITUACAO"]);

                        pedidos.Add(new Pedido(codigo, datahoraFim, datahoraExcluido, descricao, custo, recebimento, precoTotal, situacao));
                        pedidos[count].TotalPedidosOrcados = Convert.ToInt32(dr["TOTAL_PEDIDOS_ORCADOS"]);
                    }
                }

                conexao.Close();
                return pedidos;
            }
        }

        public Pedido ConsultarPedidoDb(int idPedido)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_ConsultarPedido", conexao);

                comandoDML.Parameters.Add("@IdPedido", SqlDbType.Int);
                comandoDML.Parameters["@IdPedido"].Value = idPedido;

                comandoDML.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = comandoDML.ExecuteReader();
                bool consultaPedido = dr.HasRows;

                Pedido pedido = new Pedido();
                if (consultaPedido)
                {
                    while (dr.Read())
                    {
                        int codigo = Convert.ToInt32(dr["IdPEDIDO"]);
                        DateTime datahoraFim = Convert.ToDateTime(dr["DATaHORaFINALIZADO"]);

                        DateTime datahoraExcluido = DateTime.MinValue;
                        if (dr["DATaHORaEXCLUIDO"] != DBNull.Value)
                            datahoraExcluido = Convert.ToDateTime(dr["DATaHORaEXCLUIDO"]);

                        string descricao = Convert.ToString(dr["DESCRICAO"]);
                        string custo = Convert.ToString(dr["TOTAlCUSTO"]);
                        string recebimento = Convert.ToString(dr["TOTAlLUCRO"]);
                        string precoTotal = Convert.ToString(dr["TOTAlPEDIDO"]);
                        string situacao = Convert.ToString(dr["SITUACAO"]);

                        pedido = new Pedido(codigo, datahoraFim, datahoraExcluido, descricao, custo, recebimento, precoTotal, situacao);
                    }
                }
                else
                    pedido = null;

                conexao.Close();
                return pedido;
            }
        }

        public bool CadastrarPedidoDb(Pedido pedido)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDMLpedido = new SqlCommand("SP_CadastrarPedido", conexao);
                comandoDMLpedido.CommandType = CommandType.StoredProcedure;

                comandoDMLpedido.Parameters.Add("@DescricaoItensPedido", SqlDbType.VarChar, 2000);
                comandoDMLpedido.Parameters.Add("@CustoPedido", SqlDbType.Decimal);
                comandoDMLpedido.Parameters.Add("@PrecoPedido", SqlDbType.Decimal);
                comandoDMLpedido.Parameters.Add("@LucroPedido", SqlDbType.Decimal);

                comandoDMLpedido.Parameters["@DescricaoItensPedido"].Value = pedido.DescricaoItens;
                comandoDMLpedido.Parameters["@CustoPedido"].Value = Convert.ToDecimal(pedido.TotalCusto, new CultureInfo("en-US"));
                comandoDMLpedido.Parameters["@PrecoPedido"].Value = Convert.ToDecimal(pedido.TotalPedido, new CultureInfo("en-US"));
                comandoDMLpedido.Parameters["@LucroPedido"].Value = Convert.ToDecimal(pedido.TotalLucro, new CultureInfo("en-US"));

                object pedidoCadastrado = comandoDMLpedido.ExecuteScalar();
                int idPedido = 0;

                if (pedidoCadastrado != null && int.TryParse(pedidoCadastrado.ToString(), out idPedido))
                {
                    SqlCommand comandoDMLitens = new SqlCommand("SP_CadastrarItemPedido", conexao);
                    comandoDMLitens.CommandType = CommandType.StoredProcedure;

                    comandoDMLitens.Parameters.Add("@Pedido", SqlDbType.Int);
                    comandoDMLitens.Parameters.Add("@Referencia", SqlDbType.VarChar, 50);
                    comandoDMLitens.Parameters.Add("@Tipo", SqlDbType.VarChar, 10);
                    comandoDMLitens.Parameters.Add("@Descricao", SqlDbType.VarChar, 255);
                    comandoDMLitens.Parameters.Add("@Custo", SqlDbType.Decimal);
                    comandoDMLitens.Parameters.Add("@Preco", SqlDbType.Decimal);
                    comandoDMLitens.Parameters.Add("@Lucro", SqlDbType.Decimal);
                    comandoDMLitens.Parameters.Add("@Quantidade", SqlDbType.Int);

                    foreach (ItemPedido item in pedido.ItensPedido)
                    {
                        comandoDMLitens.Parameters["@Pedido"].Value = idPedido;
                        comandoDMLitens.Parameters["@Referencia"].Value = item.Referencia;
                        comandoDMLitens.Parameters["@Tipo"].Value = item.Tipo;
                        comandoDMLitens.Parameters["@Descricao"].Value = item.Descricao;
                        comandoDMLitens.Parameters["@Custo"].Value = Convert.ToDecimal(item.Custo, new CultureInfo("en-US"));
                        comandoDMLitens.Parameters["@Preco"].Value = Convert.ToDecimal(item.Preco, new CultureInfo("en-US"));
                        comandoDMLitens.Parameters["@Lucro"].Value = Convert.ToDecimal(item.Lucro, new CultureInfo("en-US"));
                        comandoDMLitens.Parameters["@Quantidade"].Value = item.Quantidade;

                        comandoDMLitens.ExecuteNonQuery();
                    }

                    SqlCommand comandoDMLcliente = new SqlCommand("SP_CadastrarClientePedido", conexao);
                    comandoDMLcliente.CommandType = CommandType.StoredProcedure;

                    comandoDMLcliente.Parameters.Add("@Pedido", SqlDbType.Int);
                    comandoDMLcliente.Parameters.Add("@Nome", SqlDbType.VarChar, 255);
                    comandoDMLcliente.Parameters.Add("@Documento", SqlDbType.VarChar, 20);
                    comandoDMLcliente.Parameters.Add("@Endereco", SqlDbType.VarChar, 255);
                    comandoDMLcliente.Parameters.Add("@Bairro", SqlDbType.VarChar, 255);
                    comandoDMLcliente.Parameters.Add("@Cidade", SqlDbType.VarChar, 255);
                    comandoDMLcliente.Parameters.Add("@Uf", SqlDbType.VarChar, 255);
                    comandoDMLcliente.Parameters.Add("@Cep", SqlDbType.VarChar, 255);

                    comandoDMLcliente.Parameters["@Pedido"].Value = idPedido;
                    comandoDMLcliente.Parameters["@Nome"].Value = pedido.ClientePedido.Nome;
                    comandoDMLcliente.Parameters["@Documento"].Value = pedido.ClientePedido.Documento;
                    comandoDMLcliente.Parameters["@Endereco"].Value = pedido.ClientePedido.Endereco;
                    comandoDMLcliente.Parameters["@Bairro"].Value = pedido.ClientePedido.Bairro;
                    comandoDMLcliente.Parameters["@Cidade"].Value = pedido.ClientePedido.Cidade;
                    comandoDMLcliente.Parameters["@Uf"].Value = pedido.ClientePedido.Uf;
                    comandoDMLcliente.Parameters["@Cep"].Value = pedido.ClientePedido.Cep;

                    comandoDMLcliente.ExecuteNonQuery();

                    SqlCommand comandoDMLveiculo = new SqlCommand("SP_CadastrarVeiculoPedido", conexao);
                    comandoDMLveiculo.CommandType = CommandType.StoredProcedure;

                    comandoDMLveiculo.Parameters.Add("@Pedido", SqlDbType.Int);
                    comandoDMLveiculo.Parameters.Add("@Descricao", SqlDbType.VarChar, 255);
                    comandoDMLveiculo.Parameters.Add("@Placa", SqlDbType.VarChar, 20);
                    comandoDMLveiculo.Parameters.Add("@QuilometrosRodados", SqlDbType.VarChar, 255);

                    comandoDMLveiculo.Parameters["@Pedido"].Value = idPedido;
                    comandoDMLveiculo.Parameters["@Descricao"].Value = pedido.VeiculoPedido.Descricao;
                    comandoDMLveiculo.Parameters["@Placa"].Value = pedido.VeiculoPedido.Placa;
                    comandoDMLveiculo.Parameters["@QuilometrosRodados"].Value = pedido.VeiculoPedido.QuilometrosRodados;

                    comandoDMLveiculo.ExecuteNonQuery();
                }

                conexao.Close();
                return true;
            }
        }

        public bool AtualizarPedidoDb(Pedido pedido)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                #region Atualizar Dados Gerais do Pedido
                SqlCommand comandoDMLpedido = new SqlCommand("SP_AtualizarPedido", conexao);
                comandoDMLpedido.CommandType = CommandType.StoredProcedure;

                comandoDMLpedido.Parameters.Add("@IdPedido", SqlDbType.Int);
                comandoDMLpedido.Parameters.Add("@DescricaoItensPedido", SqlDbType.VarChar, 2000);
                comandoDMLpedido.Parameters.Add("@CustoPedido", SqlDbType.Decimal);
                comandoDMLpedido.Parameters.Add("@PrecoPedido", SqlDbType.Decimal);
                comandoDMLpedido.Parameters.Add("@LucroPedido", SqlDbType.Decimal);

                comandoDMLpedido.Parameters["@IdPedido"].Value = pedido.IdPedido;
                comandoDMLpedido.Parameters["@DescricaoItensPedido"].Value = pedido.DescricaoItens;
                comandoDMLpedido.Parameters["@CustoPedido"].Value = Convert.ToDecimal(pedido.TotalCusto, new CultureInfo("en-US"));
                comandoDMLpedido.Parameters["@PrecoPedido"].Value = Convert.ToDecimal(pedido.TotalPedido, new CultureInfo("en-US"));
                comandoDMLpedido.Parameters["@LucroPedido"].Value = Convert.ToDecimal(pedido.TotalLucro, new CultureInfo("en-US"));
                comandoDMLpedido.ExecuteNonQuery();
                #endregion

                #region Excluir Itens do Pedido
                SqlCommand comandoDMLlimparItens = new SqlCommand("SP_LimparItensPedido", conexao);
                comandoDMLlimparItens.CommandType = CommandType.StoredProcedure;

                comandoDMLlimparItens.Parameters.Add("@Pedido", SqlDbType.Int);
                comandoDMLlimparItens.Parameters["@Pedido"].Value = pedido.IdPedido;

                comandoDMLlimparItens.ExecuteNonQuery();
                #endregion

                #region Inserir Itens do Pedido
                SqlCommand comandoDMLitens = new SqlCommand("SP_AtualizarItemPedido", conexao);
                comandoDMLitens.CommandType = CommandType.StoredProcedure;

                comandoDMLitens.Parameters.Add("@Pedido", SqlDbType.Int);
                comandoDMLitens.Parameters.Add("@Referencia", SqlDbType.VarChar, 50);
                comandoDMLitens.Parameters.Add("@Tipo", SqlDbType.VarChar, 10);
                comandoDMLitens.Parameters.Add("@Descricao", SqlDbType.VarChar, 255);
                comandoDMLitens.Parameters.Add("@Custo", SqlDbType.Decimal);
                comandoDMLitens.Parameters.Add("@Preco", SqlDbType.Decimal);
                comandoDMLitens.Parameters.Add("@Lucro", SqlDbType.Decimal);
                comandoDMLitens.Parameters.Add("@Quantidade", SqlDbType.Int);

                foreach (ItemPedido item in pedido.ItensPedido)
                {
                    comandoDMLitens.Parameters["@Pedido"].Value = pedido.IdPedido;
                    comandoDMLitens.Parameters["@Referencia"].Value = item.Referencia;
                    comandoDMLitens.Parameters["@Tipo"].Value = item.Tipo;
                    comandoDMLitens.Parameters["@Descricao"].Value = item.Descricao;
                    comandoDMLitens.Parameters["@Custo"].Value = Convert.ToDecimal(item.Custo, new CultureInfo("en-US"));
                    comandoDMLitens.Parameters["@Preco"].Value = Convert.ToDecimal(item.Preco, new CultureInfo("en-US"));
                    comandoDMLitens.Parameters["@Lucro"].Value = Convert.ToDecimal(item.Lucro, new CultureInfo("en-US"));
                    comandoDMLitens.Parameters["@Quantidade"].Value = item.Quantidade;

                    comandoDMLitens.ExecuteNonQuery();
                }
                #endregion

                conexao.Close();
                return true;
            }
        }

        public bool InativarPedidoDb(int idPedido)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDMLpedido = new SqlCommand("SP_ExcluirPedido", conexao);
                comandoDMLpedido.CommandType = CommandType.StoredProcedure;

                comandoDMLpedido.Parameters.Add("@IdPedido", SqlDbType.Int);
                comandoDMLpedido.Parameters["@IdPedido"].Value = idPedido;

                comandoDMLpedido.ExecuteNonQuery();

                conexao.Close();
                return true;
            }
        }

        public ClientePedido ConsultarClientePedido(string nrPedido)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_ConsultarClientePedido", conexao);

                comandoDML.Parameters.Add("@IdPedido", SqlDbType.Int);
                comandoDML.Parameters["@IdPedido"].Value = Convert.ToInt32(nrPedido);

                comandoDML.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = comandoDML.ExecuteReader();
                bool consultaPedido = dr.HasRows;

                ClientePedido clientePedido = new ClientePedido();
                if (consultaPedido)
                {
                    while (dr.Read())
                    {
                        clientePedido.IdPedido = Convert.ToInt32(dr["IdPEDIDO"]);
                        clientePedido.Nome = Convert.ToString(dr["NOME"]);
                        clientePedido.Documento = Convert.ToString(dr["DOCUMENTO"]);
                        clientePedido.Endereco = Convert.ToString(dr["ENDERECO"]);
                        clientePedido.Bairro = Convert.ToString(dr["BAIRRO"]);
                        clientePedido.Cidade = Convert.ToString(dr["CIDADE"]);
                        clientePedido.Uf = Convert.ToString(dr["UF"]);
                        clientePedido.Cep = Convert.ToString(dr["CEP"]);
                    }
                }

                conexao.Close();
                return clientePedido;
            }
        }

        public VeiculoPedido ConsultarVeiculoPedido(string nrPedido)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_ConsultarVeiculoPedido", conexao);

                comandoDML.Parameters.Add("@IdPedido", SqlDbType.Int);
                comandoDML.Parameters["@IdPedido"].Value = Convert.ToInt32(nrPedido);

                comandoDML.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = comandoDML.ExecuteReader();
                bool consultaPedido = dr.HasRows;

                VeiculoPedido veiculoPedido = new VeiculoPedido();
                if (consultaPedido)
                {
                    while (dr.Read())
                    {
                        veiculoPedido.IdPedido = Convert.ToInt32(dr["IdPEDIDO"]);
                        veiculoPedido.Descricao = Convert.ToString(dr["DESCRICAO"]);
                        veiculoPedido.Placa = Convert.ToString(dr["PLACA"]);
                        veiculoPedido.QuilometrosRodados = Convert.ToString(dr["QUILOMETROsRODADOS"]);
                    }
                }

                conexao.Close();
                return veiculoPedido;
            }
        }
    }
}

