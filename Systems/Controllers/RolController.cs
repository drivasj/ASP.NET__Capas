using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oesia_Arequipa.Controllers
{
    public class RolController : Controller
    {
        // GET: Rol
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Listar Rol
        /// </summary>
        /// <returns></returns>
        public JsonResult listarRol()
        {
            RolBL oTPUBL = new RolBL();
            return Json(oTPUBL.listarRol(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Recuperar Rol
        /// </summary>
        /// <param name="iidrolR"></param>
        /// <returns></returns>
        public JsonResult RecuperarRol(int iidrolR)
        {
            RolBL oCamaBL = new RolBL();
            return Json(oCamaBL.RecuperarRol(iidrolR), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Guardar Rol
        /// </summary>
        /// <param name="oRol"></param>
        /// <returns></returns>
        public int GuardarRol(RolCLS oRol)
        {
            //oTipoUsuarioCLS.idpaginaTipousuarios = idpaginas;
            RolBL oTPUBL = new RolBL();
            return oTPUBL.GuardarRol(oRol);
        }

        /////--------------------Rol Pagina Menu---------------------///////
        
        public ActionResult RolPaginaMenu()
        {
            return View();
        }

        /// <summary>
        /// Recuperar Rol Menu
        /// </summary>
        /// <param name="iidrolMenu"></param>
        /// <returns></returns>
        public JsonResult RecuperarRolMenu(int iidrolMenu)
        {
            RolBL oTUPBL = new RolBL();
            return Json(oTUPBL.RecuperarRolMenu(iidrolMenu), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Guardar Rol Menu
        /// </summary>
        /// <param name="oTipoUsuario"></param>
        /// <returns></returns>
        public int GuardarRolMenu(RolCLS oRolCLS)
        {
            //oTipoUsuarioCLS.idpaginaTipousuarios = idpaginas;
            RolBL oTPUBL = new RolBL();
            return oTPUBL.GuardarRolMenu(oRolCLS);
        }


        /////--------------------Asignar Menu---------------------///////
        
        /// <summary>
        /// Index Asignar menu
        /// </summary>
        /// <returns></returns>
        public ActionResult AsignarMenu()
        {
            return View();
        }

        /// <summary>
        /// ListarTMenu
        /// </summary>
        /// <returns></returns>
        public JsonResult listarTMenu()
        {
            RolBL oMenuBL = new RolBL();

            return Json(oMenuBL.listarTMenu(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Listar Menu Lista
        /// </summary>
        /// <returns></returns>
        public JsonResult ListarMenuLista()
        {
            RolBL oMenuBL = new RolBL();

            return Json(oMenuBL.ListarMenuLista(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// RecuperarTMenu
        /// </summary>
        /// <returns></returns>
        public JsonResult recuperarTMenu(int iidmenuR)
        {
            RolBL menuBL = new RolBL();
            return Json(menuBL.recuperarTMenu(iidmenuR), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// /Asignar Módulos a las páginas del Menu
        /// </summary>
        /// <param name="oMenuCLS"></param>
        /// <returns></returns>
        public int GuardarDatos(MenuCLS oMenuCLS)
        {
            RolBL obj = new RolBL();
            return obj.GuardarTmenu(oMenuCLS);
        }

    }
}