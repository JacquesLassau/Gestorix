using GestorixDLL.BLL;
using GestorixDLL.DAL;
using GestorixDLL.DML;
using GestorixDLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace GestorixDLL.DAL
{
    public class DALProduto
    {
        public Conexao Conexao { get; set; }

        public DALProduto()
        {
            Conexao = new Conexao();
        }

        public string IncluirProdutoDb(Produto produto)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_CadastrarProduto", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@ReferenciaProduto", SqlDbType.VarChar, 50);
                comandoDML.Parameters["@ReferenciaProduto"].Value = produto.ReferenciaProduto;

                comandoDML.Parameters.Add("@Tipo", SqlDbType.VarChar, 10);
                comandoDML.Parameters["@Tipo"].Value = produto.TipoProduto;

                comandoDML.Parameters.Add("@Descricao", SqlDbType.VarChar, 255);
                comandoDML.Parameters["@Descricao"].Value = string.IsNullOrWhiteSpace(produto.Descricao) ? DBNull.Value.ToString() : produto.Descricao;

                comandoDML.Parameters.Add("@Fabricante", SqlDbType.VarChar, 255);
                comandoDML.Parameters["@Fabricante"].Value = string.IsNullOrWhiteSpace(produto.Fabricante) ? DBNull.Value.ToString() : produto.Fabricante;

                comandoDML.Parameters.Add("@CustoProduto", SqlDbType.Decimal);
                if (string.IsNullOrWhiteSpace(produto.CustoProduto))
                    comandoDML.Parameters["@CustoProduto"].Value = null;
                else
                    comandoDML.Parameters["@CustoProduto"].Value = Convert.ToDecimal(produto.CustoProduto.Replace(".", ""), new CultureInfo("pt-BR"));

                comandoDML.Parameters.Add("@PrecoProduto", SqlDbType.Decimal);
                if (string.IsNullOrWhiteSpace(produto.PrecoProduto))
                    comandoDML.Parameters["@PrecoProduto"].Value = null;
                else
                    comandoDML.Parameters["@PrecoProduto"].Value = Convert.ToDecimal(produto.PrecoProduto.Replace(".", ""), new CultureInfo("pt-BR"));

                comandoDML.Parameters.Add("@Situacao", SqlDbType.Char);
                comandoDML.Parameters["@Situacao"].Value = produto.Situacao;

                comandoDML.ExecuteNonQuery();

                conexao.Close();
                return ConstantesProduto.PRODUTO_GRAVADO;
            }
        }

        public string EditarProdutoDb(Produto produto)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_EditarProduto", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@ReferenciaProduto", SqlDbType.VarChar, 50);
                comandoDML.Parameters["@ReferenciaProduto"].Value = produto.ReferenciaProduto;

                comandoDML.Parameters.Add("@Tipo", SqlDbType.VarChar, 10);
                comandoDML.Parameters["@Tipo"].Value = produto.TipoProduto;

                comandoDML.Parameters.Add("@Descricao", SqlDbType.VarChar, 255);
                comandoDML.Parameters["@Descricao"].Value = string.IsNullOrWhiteSpace(produto.Descricao) ? DBNull.Value.ToString() : produto.Descricao;

                comandoDML.Parameters.Add("@Fabricante", SqlDbType.VarChar, 255);
                comandoDML.Parameters["@Fabricante"].Value = string.IsNullOrWhiteSpace(produto.Fabricante) ? DBNull.Value.ToString() : produto.Fabricante;

                comandoDML.Parameters.Add("@CustoProduto", SqlDbType.Decimal);
                if (string.IsNullOrWhiteSpace(produto.CustoProduto))
                    comandoDML.Parameters["@CustoProduto"].Value = null;
                else
                    comandoDML.Parameters["@CustoProduto"].Value = Convert.ToDecimal(produto.CustoProduto.Replace(".", ""), new CultureInfo("pt-BR"));

                comandoDML.Parameters.Add("@PrecoProduto", SqlDbType.Decimal);
                if (string.IsNullOrWhiteSpace(produto.PrecoProduto))
                    comandoDML.Parameters["@PrecoProduto"].Value = null;
                else
                    comandoDML.Parameters["@PrecoProduto"].Value = Convert.ToDecimal(produto.PrecoProduto.Replace(".", ""), new CultureInfo("pt-BR"));

                comandoDML.ExecuteNonQuery();

                conexao.Close();
                return ConstantesProduto.PRODUTO_ATUALIZADO;
            }
        }

        public string ExcluirProdutoDb(Produto produto)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_ExcluirProduto", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@ReferenciaProduto", SqlDbType.VarChar, 50);
                comandoDML.Parameters["@ReferenciaProduto"].Value = produto.ReferenciaProduto;

                comandoDML.Parameters.Add("@Situacao", SqlDbType.Char);
                comandoDML.Parameters["@Situacao"].Value = produto.Situacao;

                comandoDML.ExecuteNonQuery();

                conexao.Close();
                return ConstantesProduto.PRODUTO_INATIVADO;
            }
        }

        public Produto SelecionarProdutoDb(string referenciaProduto)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_SelecionarProduto", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@ReferenciaProduto", SqlDbType.VarChar, 50);
                comandoDML.Parameters["@ReferenciaProduto"].Value = referenciaProduto;

                SqlDataReader dr = comandoDML.ExecuteReader();
                bool consultarExistenciaProduto = dr.HasRows;

                Produto produto = new Produto();
                if (consultarExistenciaProduto)
                {
                    while (dr.Read())
                    {
                        produto.ReferenciaProduto = Convert.ToString(dr["IdREFPRODUTO"]);
                        produto.TipoProduto = Convert.ToString(dr["TIPO"]);
                        produto.Descricao = Convert.ToString(dr["DESCRICAO"]);
                        produto.Fabricante = Convert.ToString(dr["FABRICANTE"]);
                        produto.CustoProduto = Convert.ToString(dr["CUSTO"]).Replace(".", string.Empty);
                        produto.PrecoProduto = Convert.ToString(dr["PRECO"]).Replace(".", string.Empty);
                        produto.Quantidade = Convert.ToString(dr["QUANTIDADE"]);
                    }
                }

                conexao.Close();
                return produto;
            }
        }

        public List<Produto> ListarProdutosDb()
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                List<Produto> produtos = new List<Produto>();

                SqlCommand comandoDML = new SqlCommand("SP_ListarProdutos", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = comandoDML.ExecuteReader();

                while (dr.Read())
                {
                    string referenciaProduto = Convert.ToString(dr["IdREFPRODUTO"]);
                    string tipoProduto = Convert.ToString(dr["TIPO"]);
                    string descricao = Convert.ToString(dr["DESCRICAO"]);
                    string fabricante = Convert.ToString(dr["FABRICANTE"]);
                    string custoProduto = Convert.ToString(dr["CUSTO"]).Replace(".", string.Empty);
                    string precoProduto = Convert.ToString(dr["PRECO"]).Replace(".", string.Empty);

                    produtos.Add(new Produto(referenciaProduto, tipoProduto, descricao, fabricante, custoProduto, precoProduto));
                }

                conexao.Close();
                return produtos;
            }
        }

        public string AtualizarProdutoEstoqueDb(string referencia, string quantidade, string controle)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_AtualizarProdutoEstoque", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@ReferenciaProduto", SqlDbType.VarChar, 50);
                comandoDML.Parameters["@ReferenciaProduto"].Value = referencia;

                comandoDML.Parameters.Add("@Quantidade", SqlDbType.Int);
                comandoDML.Parameters["@Quantidade"].Value = Convert.ToInt32(quantidade);

                comandoDML.Parameters.Add("@Controle", SqlDbType.VarChar, 3);
                comandoDML.Parameters["@Controle"].Value = controle;

                comandoDML.ExecuteNonQuery();

                conexao.Close();
                return ConstantesProduto.PRODUTO_ATUALIZADO_EM_ESTOQUE;
            }
        }
    }
}
