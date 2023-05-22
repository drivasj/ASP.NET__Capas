using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using Capa_Negocio;


namespace Oesia_Arequipa.Controllers
{
    public class ModuloController : Controller
    {
        // GET: Modulo
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Listar Modulo
        /// </summary>
        /// <param name="iidmoduloR"></param>
        /// <returns></returns>
        public JsonResult listarModulos()
        {
            ModuloBL oModuloBL = new ModuloBL();

            return Json(oModuloBL.listarModulo(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Recuperar Modulo
        /// </summary>
        /// <param name="iidmoduloR"></param>
        /// <returns></returns>
        public JsonResult recuperarModulo(int iidmoduloR)
        {
            ModuloBL oModuloBL = new ModuloBL();
            return Json(oModuloBL.recuperarModulo(iidmoduloR), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Guardar Modulo
        /// </summary>
        public int GuardarModulo(ModuloCLS oModuloCLS)
        {
            ModuloBL oModuloBL = new ModuloBL();
            return oModuloBL.GuardarModulo(oModuloCLS);
        }

        //------------------ModuloMenu---------------------------////

        /// <summary>
        /// Index ModuloMenu
        /// </summary>
        /// <returns></returns>
        public ActionResult ModuloMenu()
        {
            return View();
        }


        /// <summary>
        ///  Listado Modulo, Modulo Menu, Rol
        /// </summary>
        /// <returns></returns>
        public JsonResult listarModuloRol()
        {
            ModuloBL oModuloBL = new ModuloBL();

            return Json(oModuloBL.listarModuloRol(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Listar Modulo Menu
        /// </summary>
        /// <returns></returns>
        public JsonResult ListarModuloMenu()
        {
            ModuloBL oModuloBL = new ModuloBL();

            return Json(oModuloBL.ListarModuloMenu(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Recuperar Modulo Menu
        /// </summary>
        /// <param name="iidmenuR"></param>
        /// <returns></returns>
        public JsonResult RecuperarModuloMenu(int iidmenuR)
        {
            ModuloBL oModulo = new ModuloBL();
            return Json(oModulo.RecuperarModuloMenu(iidmenuR), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Guardar Modulo Menu 
        /// </summary>
        /// <param name="moduloCLS"></param>
        /// <returns></returns>
        public int GuardarModuloMenu(ModuloMenuCLS moduloCLS)
        {
            //oTipoUsuarioCLS.idpaginaTipousuarios = idpaginas;
            ModuloBL oTPUBL = new ModuloBL();
            return oTPUBL.GuardarModuloMenu(moduloCLS);
        }

        /// <summary>
        /// Deshabilitar Modulo Menu
        /// </summary>
        /// <param name="iidmenuD"></param>
        /// <returns></returns>   
        public int DeshabilitarModuloMenu(int iidmenuD)
        {
            ModuloBL oModuloBL = new ModuloBL();
            return oModuloBL.DeshabilitarModuloMenu(iidmenuD);
        }
    }
}