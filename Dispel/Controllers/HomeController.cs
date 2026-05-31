using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dispel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Models.UsuarioValido.ValidUser())
                return View();
            else
                return RedirectToAction("Login", "Authentication");
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}