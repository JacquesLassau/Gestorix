using GestorixDLL.DML;

namespace GestorixDLL.DAL
{
    public interface IDALUsuario
    {
        Usuario AcessoUsuarioDb(Usuario usuario);
        bool VerificarEmailUsuarioDb(string tipo, string email);
        void AlterarSenhaUsuarioDb(Usuario usuario);
    }
}
