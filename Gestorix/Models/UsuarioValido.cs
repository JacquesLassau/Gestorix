using System.Web;

namespace Gestorix.Models
{
    public static class UsuarioValido
    {
        public static bool ValidUser()
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["sessaoId"] != null)
                return true;
            else
                return false;
        }
    }
}