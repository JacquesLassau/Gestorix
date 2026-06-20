using GestorixDLL.DAL;
using GestorixDLL.DML;

namespace GestorixDLL.BLL
{
    public class BoToken
    {
        DALToken _dalToken;
        public BoToken()
        {
            _dalToken = new DALToken();
        }

        /// <summary>
        /// Cadastra um novo token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public void IncluirToken(Token token)
        {
            _dalToken.IncluirTokenDb(token);
        }

        /// <summary>
        /// Acesso do usuário ao link enviado por e-mail.
        /// </summary>
        /// <param name="token"></param>        
        /// <returns></returns>
        public void GravarAcessoToken(string token)
        {
            _dalToken.GravarAcessoTokenDb(token);
        }

        /// <summary>
        /// Verifica e-mail vinculado ao token.
        /// </summary>
        /// <param name="token"></param>        
        /// <returns>Retorna e-mail do usuário vinculado ao token.</returns>
        public string BuscarEmailToken(string token)
        {            
            return _dalToken.BuscarEmailTokenDb(token);
        }

        /// <summary>
        /// Verifica e-mail vinculado ao token.
        /// </summary>
        /// <param name="token"></param>        
        /// <returns>Retorna se o token foi usado ou não.</returns>
        public string BuscarValidadeToken(string token)
        {            
            return _dalToken.BuscarValidadeTokenDb(token);
        }

    }
}

