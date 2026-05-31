using DispelDLL.DAL;
using DispelDLL.DML;
using DispelDLL.Infrastructure;

namespace DispelDLL.BLL
{
    public class BoUsuario
    {
        DALUsuario _daoUsuario;

        public BoUsuario()
        {
            _daoUsuario = new DALUsuario();
        }
        /// <summary>
        /// Cadastra um novo usuario.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public string IncluirUsuario(Usuario usuario)
        {
           return _daoUsuario.IncluirUsuarioDb(usuario);
        }

        /// <summary>
        /// Acesso do usuário no sistema.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>Instancia do usuario</returns>
        public Usuario AcessoUsuario(Usuario usuario)
        {
            // Não há gestão de usuários: todos são supervisores até o momento.
            usuario.Tipo = ConstantesUsuario.SUPERVISOR;
            return _daoUsuario.AcessoUsuarioDb(usuario);
        }

        /// <summary>
        /// Verifica o email de um usuário.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Booleano se existe ou não o email.</returns>
        public bool VerificarEmailUsuario(string tipo, string email)
        {
            if (_daoUsuario.VerificarEmailUsuarioDb(tipo, email))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Atualiza a senha d usuário.
        /// </summary>
        /// <param></param>
        /// <param name="usuario"></param>
        /// <param name="novaSenhaUsuario"></param>
        /// <returns></returns>
        public void AlterarSenhaUsuario(Usuario usuario)
        {
            _daoUsuario.AlterarSenhaUsuarioDb(usuario);
        }
    }
}

