using DispelDLL.DML;

namespace DispelDLL.DAL
{
    public interface IDALToken
    {
        void IncluirTokenDb(Token token);
        void GravarAcessoTokenDb(string token);
        string BuscarEmailTokenDb(string token);
        string BuscarValidadeTokenDb(string pToken);
    }
}
