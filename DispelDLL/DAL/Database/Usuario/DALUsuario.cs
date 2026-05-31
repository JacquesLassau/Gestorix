using System.Data.SqlClient;
using System.Data;
using System;
using DispelDLL.DML;
using DispelDLL.Infrastructure;

namespace DispelDLL.DAL
{
    public class DALUsuario : IDALUsuario
    {
        public Conexao Conexao { get; set; }

        public DALUsuario()
        {
            Conexao = new Conexao();
        }

        public string IncluirUsuarioDb(Usuario usuario)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_CadastrarUsuario", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;
                                
                comandoDML.Parameters.Add("@Email", SqlDbType.VarChar, 255);
                comandoDML.Parameters.Add("@Senha", SqlDbType.VarChar, 255);
                comandoDML.Parameters.Add("@Tipo", SqlDbType.VarChar, 255);
                comandoDML.Parameters.Add("@Situacao", SqlDbType.Char, 1);
                                
                comandoDML.Parameters["@Email"].Value = usuario.Email;
                comandoDML.Parameters["@Senha"].Value = usuario.Senha;
                comandoDML.Parameters["@Tipo"].Value = usuario.Tipo;
                comandoDML.Parameters["@Situacao"].Value = Constantes.ATIVO;

                SqlDataReader dr = comandoDML.ExecuteReader();
                bool existeMsgErro = dr.HasRows;
                string mensagemErro = string.Empty;

                if (existeMsgErro)                
                    while (dr.Read())
                        mensagemErro = Convert.ToString(dr["MSgERRO"]);                

                conexao.Close();
                return mensagemErro;
            }
        }

        public Usuario AcessoUsuarioDb(Usuario usuario)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_AcessoUsuario", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@Email", SqlDbType.VarChar, 255);
                comandoDML.Parameters["@Email"].Value = usuario.Email;

                comandoDML.Parameters.Add("@Senha", SqlDbType.VarChar, 255);
                comandoDML.Parameters["@Senha"].Value = usuario.Senha;

                comandoDML.Parameters.Add("@Tipo", SqlDbType.Char, 1);
                comandoDML.Parameters["@Tipo"].Value = usuario.Tipo;

                SqlDataReader dr = comandoDML.ExecuteReader();

                bool verificarUsuario = dr.HasRows;

                if (!verificarUsuario)
                    usuario = null;
                else
                {
                    while (dr.Read())
                    {
                        int id = Convert.ToInt32(dr["IdUSUARIO"]);
                        string email = Convert.ToString(dr["EMAIL"]);
                        string tipo = Convert.ToString(dr["TIPO"]);
                        usuario = new Usuario(id, email, tipo);
                    }
                }

                conexao.Close();
                return usuario;
            }
        }

        public bool VerificarEmailUsuarioDb(string tipo, string email)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_VerificarEmailUsuario", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@Tipo", SqlDbType.Char, 1);
                comandoDML.Parameters["@Tipo"].Value = tipo;

                comandoDML.Parameters.Add("@Email", SqlDbType.VarChar, 255);
                comandoDML.Parameters["@Email"].Value = email;

                SqlDataReader dr = comandoDML.ExecuteReader();
                bool consultarEmailUsuario = dr.HasRows;

                conexao.Close();
                return consultarEmailUsuario;
            }
        }

        public void AlterarSenhaUsuarioDb(Usuario usuario)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_AlterarSenhaUsuario", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@Email", SqlDbType.VarChar, 200);
                comandoDML.Parameters["@Email"].Value = usuario.Email;

                comandoDML.Parameters.Add("@NovaSenha", SqlDbType.VarChar, 200);
                comandoDML.Parameters["@NovaSenha"].Value = usuario.Senha;

                comandoDML.Parameters.Add("@Tipo", SqlDbType.Char, 1);
                comandoDML.Parameters["@Tipo"].Value = usuario.Tipo;

                comandoDML.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public bool VerificarEmailUsuarioDb(int tipo, string email)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlCommand comandoDML = new SqlCommand("SP_VerificarEmailUsuario", conexao);
                comandoDML.CommandType = CommandType.StoredProcedure;

                comandoDML.Parameters.Add("@Email", SqlDbType.VarChar, 255);
                comandoDML.Parameters["@Email"].Value = email;

                SqlDataReader dr = comandoDML.ExecuteReader();
                bool consultarEmailUsuario = dr.HasRows;

                conexao.Close();
                return consultarEmailUsuario;
            }
        }
    }
}
