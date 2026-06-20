using GestorixDLL.Infrastructure;
using GestorixDLL.DML;
using GestorixDLL.BLL;
using System;
using System.Web.Mvc;

namespace Gestorix.Controllers
{
    public class AuthenticationController : Controller
    {
        #region Construtor  
        private BoUsuario _boUsuario;
        private BoToken _boToken;
        private Token _token;

        public AuthenticationController()
        {
            _boUsuario = new BoUsuario();
            _boToken = new BoToken();
            _token = new Token();
        }
        #endregion

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario usuario)
        {
            try
            {
                Usuario usuarioAutenticado = _boUsuario.AcessoUsuario(usuario);

                if (usuarioAutenticado != null)
                {
                    Session["sessaoId"] = Utilidades.StrTokenMD5(Convert.ToString(usuarioAutenticado.IdUsuario));
                    Session["sessaoType"] = Utilidades.StrTokenMD5(usuarioAutenticado.Tipo);
                    Session["sessaoEmail"] = usuarioAutenticado.Email;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesAlerta.NAO_VALIDO;
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ConstantesAlerta.FALHA_LOGIN;
                    return RedirectToAction("Login");
                }
            }
            catch
            {
                TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesAlerta.NAO_VALIDO;
                TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ConstantesAlerta.FALHA_LOGIN;
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult PasswordRecouver()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordRecouver(Usuario usuario)
        {
            try
            {
                if (!_boUsuario.VerificarEmailUsuario(ConstantesUsuario.SUPERVISOR, usuario.Email))
                {
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesAlerta.EMAIL_NAO_ENCONTRADO;
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ConstantesAlerta.EMAIL_NAO_ENCONTRADO_DESCRICAO;
                }
                else
                {
                    string token = Utilidades.StrTokenMD5(usuario.Email);

                    //Condição incluída para que sejam realizados testes locais. 
                    //Propriedade "Host" incluí a porta do servidor local.
                    string dominio = (Request.IsLocal) ? Request.Url.Authority : Request.Url.Host;
                    // ---------------------------------------------------------------------------

                    string url = dominio + "/Authentication/PasswordEdit?StrToken=" + token;

                    _token.Email = usuario.Email;
                    _token.UrlBase = url;
                    _token.StrToken = token;
                    _boToken.IncluirToken(_token);

                    //Utilidades.EnviaEmailRecuperaSenha(usuario.Email, url);

                    ViewBag.LinkAccess = url;

                    return View("LinkAccess");
                }
            }
            catch (Exception e)
            {
                TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesAlerta.ERRO_NAO_TRATADO;
                TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = e.Message;
                return RedirectToAction("Login");
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult PasswordEdit()
        {
            try
            {
                string token = Request["StrToken"];
                Session["StrToken"] = token;

                if (string.IsNullOrEmpty(_boToken.BuscarValidadeToken(token)))
                {
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesAlerta.LINK_EXPIRADO;
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ConstantesAlerta.LINK_EXPIRADO_DESCRICAO;
                    return RedirectToAction("Login");
                }
                else
                {
                    _boToken.GravarAcessoToken(token);

                    //Ajuste pontual para uso da hospedagem com protocolo HTTP.
                    UriBuilder builder = new UriBuilder();
                    builder.Scheme = "http";
                    // --------------------------------------------------------

                    return View();

                }
            }
            catch (Exception e)
            {
                TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesAlerta.ERRO_NAO_TRATADO;
                TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = e.Message;
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordEdit(Usuario usuario)
        {
            try
            {
                string token = Session["StrToken"].ToString();
                usuario.Email = _boToken.BuscarEmailToken(token);

                if (!string.IsNullOrEmpty(usuario.Email))
                {
                    usuario.Tipo = ConstantesUsuario.SUPERVISOR;
                    _boUsuario.AlterarSenhaUsuario(usuario);
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesAlerta.NOVA_SENHA_SUCESSO;
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ConstantesAlerta.NOVA_SENHA_SUCESSO_DESCRICAO;
                }
                else
                {
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesAlerta.NAO_VALIDO;
                    TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = ConstantesAlerta.ERRO_NAO_TRATADO;
                }

            }
            catch (Exception e)
            {
                TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_TITULO] = ConstantesAlerta.ERRO_NAO_TRATADO;
                TempData[ConstantesAlerta.MENSAGEM_TEMP_DATA_DESCRICAO] = e.Message;
            }

            return RedirectToAction("Login");
        }
    }
}