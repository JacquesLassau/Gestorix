using System.Data.SqlClient;

namespace GestorixDLL.DAL
{
    public class Conexao
    {
        internal const string stringConn = @"Server=Lassau;Database=DB_GESTORIX;User Id=sa;Password=1234@qwer;";
        public SqlConnection ConexaoDatabase()
        {
            return new SqlConnection(stringConn);
        }
    }
}
