using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using DispelDLL.DML;
using DispelDLL.BLL;
using DispelDLL.Infrastructure;
using System.Globalization;

namespace DispelDLL.DAL
{
    public class DALRelatorio
    {
        public Conexao Conexao { get; set; }

        public DALRelatorio()
        {
            Conexao = new Conexao();
        }

        public DataSet RelatorioAnaliticoPedidos(DateTime dataInicial, DateTime dataFinal)
        {
            using (SqlConnection conexao = Conexao.ConexaoDatabase())
            {
                conexao.Open();

                SqlDataAdapter oSqlDataAdapter = new SqlDataAdapter("SP_RelatorioAnaliticoPedidos", Conexao.stringConn);
                oSqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                oSqlDataAdapter.SelectCommand.Parameters.Add("@DataInicial", SqlDbType.DateTime).Value = dataInicial;
                oSqlDataAdapter.SelectCommand.Parameters.Add("@DataFinal", SqlDbType.DateTime).Value = dataFinal;

                DataSet oDataSet = new DataSet();
                oSqlDataAdapter.Fill(oDataSet);                

                conexao.Close();
                return oDataSet;
            }
        }
    }
}

