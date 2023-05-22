using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Negocio;
using Capa_Entidad;

namespace Oesia_Arequipa.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Cerrar Sesion 
        /// </summary>
        /// <returns></returns>
        public ActionResult CerrarSesion()
        {
            Session["persona"] = null;
            return RedirectToAction("Index");
        }

        /// <summary>
        ///  Login
        /// </summary>
        /// <param name="usuario ,contra" "></param>
        /// <returns></returns>
        public JsonResult uspLogin(string usuario, string contra)
        {

            PersonaBL oPersonaBL = new PersonaBL();
            PaginaBL oPaginaBL = new PaginaBL();
           

            PersonaCLS oPersonaCLS = oPersonaBL.uspLogin(usuario, contra);

            if (oPersonaCLS.iidususario != 0)
            {
                Session["persona"] = oPersonaCLS;
                int iidrol = oPersonaCLS.iidrol;
                Session["ROL"] = iidrol;

             //   List<PaginaCLS> listapagina = oPaginaBL.listarMenu(iidrol); //Listar menu

                // Todas las opciones que el usuario puedes ver 
             //   Session["menu"] = listapagina;

            }
            else
            {
                Session["persona"] = null;
            }

            return Json(oPersonaCLS, JsonRequestBehavior.AllowGet);
        }
    }
}