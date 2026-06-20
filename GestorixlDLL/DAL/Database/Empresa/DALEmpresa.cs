using GestorixDLL.DML;
using GestorixDLL.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;

namespace GestorixDLL.DAL
{
    public class DALEmpresa
    {
        public Conexao Conexao { get; set; }

        public DALEmpresa()
        {
            Conexao = new Conexao();
        }

        public Empresa ConsultarEmpresaDb(string cnpjEmpresa)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_SelecionarEmpresa", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@cnpjEmpresa", SqlDbType.VarChar);
                comandoDML.Parameters["@cnpjEmpresa"].Value = cnpjEmpresa;

                SqlDataReader dr = comandoDML.ExecuteReader();
                bool consultarEmpresa = dr.HasRows;

                Empresa empresa = new Empresa();
                if (consultarEmpresa)
                {
                    while (dr.Read())
                    {
                        empresa.Cnpj = Convert.ToString(dr["CNPJ"]);
                        empresa.Cep = Convert.ToString(dr["CEP"]);
                        empresa.Logradouro = Convert.ToString(dr["LOGRADOURO"]);
                        empresa.Numero = Convert.ToString(dr["NUMERO"]);
                        empresa.Bairro = Convert.ToString(dr["BAIRRO"]);
                        empresa.Cidade = Convert.ToString(dr["CIDADE"]);
                        empresa.Estado = Convert.ToString(dr["ESTADO"]);
                        empresa.Ddd = Convert.ToString(dr["DDD"]);
                        empresa.Telefone = Convert.ToString(dr["TELEFONE"]);
                        empresa.Email = Convert.ToString(dr["EMAIL"]);
                        empresa.UrlRedeSocial = Convert.ToString(dr["URlREDeSOCIAL"]);
                        empresa.DddChat = Convert.ToString(dr["DDdCHAT"]);
                        empresa.TelefoneChat = Convert.ToString(dr["TELEFONeCHAT"]);
                    }
                }

                conexao.Close();
                return empresa;
            }
        }

        public string EditarEmpresaDb(Empresa empresa)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_EditarEmpresa", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@Cnpj", SqlDbType.VarChar, 20);
                comandoDML.Parameters["@Cnpj"].Value = empresa.Cnpj;

                comandoDML.Parameters.Add("@Cep", SqlDbType.VarChar, 10);
                comandoDML.Parameters["@Cep"].Value = empresa.Cep;

                comandoDML.Parameters.Add("@Logradouro", SqlDbType.VarChar, 255);
                comandoDML.Parameters["@Logradouro"].Value = empresa.Logradouro;

                comandoDML.Parameters.Add("@Numero", SqlDbType.VarChar, 10);
                comandoDML.Parameters["@Numero"].Value = empresa.Numero;

                comandoDML.Parameters.Add("@Bairro", SqlDbType.VarChar, 10);
                comandoDML.Parameters["@Bairro"].Value = empresa.Numero;

                comandoDML.Parameters.Add("@Cidade", SqlDbType.VarChar, 255);
                comandoDML.Parameters["@Cidade"].Value = empresa.Cidade;

                comandoDML.Parameters.Add("@Estado", SqlDbType.VarChar, 25);
                comandoDML.Parameters["@Estado"].Value = empresa.Estado;

                comandoDML.Parameters.Add("@Ddd", SqlDbType.VarChar, 3);
                comandoDML.Parameters["@Ddd"].Value = empresa.Ddd;

                comandoDML.Parameters.Add("@Telefone", SqlDbType.VarChar, 10);
                comandoDML.Parameters["@Telefone"].Value = empresa.Telefone;

                comandoDML.Parameters.Add("@Email", SqlDbType.VarChar, 255);
                comandoDML.Parameters["@Email"].Value = empresa.Email;

                comandoDML.Parameters.Add("@UrlRedeSocial", SqlDbType.VarChar, 255);
                comandoDML.Parameters["@UrlRedeSocial"].Value = empresa.UrlRedeSocial;

                comandoDML.Parameters.Add("@DddChat", SqlDbType.VarChar, 3);
                comandoDML.Parameters["@DddChat"].Value = empresa.DddChat;

                comandoDML.Parameters.Add("@TelefoneChat", SqlDbType.VarChar, 10);
                comandoDML.Parameters["@TelefoneChat"].Value = empresa.TelefoneChat;

                comandoDML.ExecuteNonQuery();

                conexao.Close();
                return ConstantesEmpresa.EMPRESA_ATUALIZADA;
            }
        }
    }
}
