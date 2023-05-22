using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class RolBL
    {
        /// <summary>
        /// Listar Rol
        /// </summary>
        /// <returns></returns>
        public List<RolCLS> listarRol()
        {
            List<RolCLS> lista = new List<RolCLS>();
            RolDAL oPersonaDAL = new RolDAL();
            return oPersonaDAL.listarRol();
        }

        /// <summary>
        /// Recuperar Rol
        /// </summary>
        /// <param name="iidrol"></param>
        /// <returns></returns>
        public RolCLS RecuperarRol(int iidrolR)
        {
            RolDAL oTH = new RolDAL();
            return oTH.RecuperarRol(iidrolR);
        }

        /// <summary>
        /// Guardar Rol
        /// </summary>
        /// <param name="oRol"></param>
        /// <returns></returns>
        public int GuardarRol(RolCLS oRol)
        {
            RolDAL oTH = new RolDAL();
            return oTH.GuardarRol(oRol);
        }

        /////--------------------Rol Pagina Menu---------------------///////
        
        /// <summary>
        /// Recuperar Rol Menu
        /// </summary>
        /// <param name="iidrolMenu"></param>
        /// <returns></returns>
        public RolCLS RecuperarRolMenu(int iidrolMenu)
        {
            RolDAL oTH = new RolDAL();
            return oTH.RecuperarRolMenu(iidrolMenu);
        }

        /// <summary>
        /// Guardar Rol Menu
        /// </summary>
        /// <param name="oTipoUsuario"></param>
        /// <returns></returns>
        public int GuardarRolMenu(RolCLS oRolCLS)
        {
            RolDAL obj = new RolDAL();
            return obj.GuardarRolMenu(oRolCLS);
        }

        /////--------------------Asignar Menu---------------------///////

        /// <summary>
        /// Listar Tabla Menu
        /// </summary>
        /// <returns></returns>
        public List<MenuCLS> listarTMenu()
        {
            RolDAL oMenuDAL = new RolDAL();
            return oMenuDAL.listarTMenu();
        }

        /// <summary>
        /// ListarMenuLista
        /// </summary>
        /// <returns></returns>
        public ListarMenuListaCLS ListarMenuLista()
        {
            //   ModuloMenuDAL oModuloMDAL = new ModuloMenuDAL();

            ModuloDAL oModuloMDAL = new ModuloDAL();

            RolDAL oMenuDAL = new RolDAL();

            ListarMenuListaCLS oListarMenuListaCLS = new ListarMenuListaCLS();


            //Listado Modulo
            oListarMenuListaCLS.listaModulo = oModuloMDAL.listarModulo();

            //  oListarMenuListaCLS.listaModulo = oModuloMDAL.listarModuloMenu();

            //Listado Menu
            oListarMenuListaCLS.listaMenu = oMenuDAL.listarTMenu();



            return oListarMenuListaCLS;
        }

        /// <summary>
        /// Recuperar TMenu
        /// </summary>
        /// <returns></returns>
        public MenuCLS recuperarTMenu(int iidmenuR)
        {
            RolDAL oMenuDAL = new RolDAL();
            return oMenuDAL.recuperarTMenu(iidmenuR);
        }

        /// <summary>
        /// Asignar Módulos a las páginas del Menu
        /// </summary>
        /// <param name="oMenuCLS"></param>
        /// <returns></returns>
        public int GuardarTmenu(MenuCLS oMenuCLS)
        {
            RolDAL oMenuDAL = new RolDAL();
            return oMenuDAL.GuardarTmenu(oMenuCLS);
        }

    }
}

