using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oesia_Arequipa.Controllers
{
    public class PaginaController : Controller
    {
        // GET: Pagina
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Listar
        /// </summary>
        /// <returns></returns>
        public JsonResult listarPaginas()
        {
            PaginaBL oPaginaBL = new PaginaBL();

            return Json(oPaginaBL.listarPagina(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Recuperar Página
        /// </summary>
        /// <returns></returns>
        /// 
        public JsonResult recuperarPagina(int iidpagina)
        {
            PaginaBL oCamaBL = new PaginaBL();
            return Json(oCamaBL.recuperarPagina(iidpagina), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Guardar 
        /// </summary>
        public int guardarPagina(PaginaCLS paginaCLS)
        {
            //oTipoUsuarioCLS.idpaginaTipousuarios = idpaginas;
            PaginaBL oTPUBL = new PaginaBL();
            return oTPUBL.guardarPagina(paginaCLS);
        }

        /// <summary>
        /// Listar Menu izquierdo
        /// </summary>
        /// <param name="oMenuCLS"></param>
        /// <returns></returns>
        public ActionResult ListarMenuEF()
        {

            //  int IDUsuario = (int)Session["ROL"];
            int IDUsuario = 1;
            PaginaBL oMenuBL = new PaginaBL();
            var listaMenu1 = oMenuBL.ListarMenuEF(IDUsuario);

            return PartialView("CelestialUI/_sidebar", listaMenu1);
        }


        /// <summary>
        /// Listar
        /// </summary>
        /// <returns></returns>
        public JsonResult ListarMenuEFJ()
        {
            int IDUsuario = 1;
            PaginaBL oPaginaBL = new PaginaBL();
            return Json(oPaginaBL.ListarMenuEF(IDUsuario), JsonRequestBehavior.AllowGet);
        }
    }
}