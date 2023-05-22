using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class ModuloBL
    {
        /// <summary>
        /// Listar Modulo
        /// </summary>
        /// <returns></returns>
        public List<ModuloCLS> listarModulo()
        {
            //instanciar  el objeto
            List<ModuloCLS> lista = new List<ModuloCLS>();
            ModuloDAL oModuloDAL = new ModuloDAL();
            return oModuloDAL.listarModulo();
        }

        /// <summary>
        /// Recuperar Modulo
        /// </summary>
        /// <param name="iidmoduloR"></param>
        /// <returns></returns>
        public ModuloCLS recuperarModulo(int iidmoduloR)
        {
            ModuloDAL oModuloDAL = new ModuloDAL();
            return oModuloDAL.recuperarModulo(iidmoduloR);
        }

        /// <summary>
        /// Guardar Modulo
        /// </summary>
        public int GuardarModulo(ModuloCLS oModuloCLS)
        {
            ModuloDAL oModuloDAL = new ModuloDAL();
            return oModuloDAL.GuardarModulo(oModuloCLS);
        }

        //------------------Modulo Menu---------------------------////

        /// <summary>
        /// lista Modulo Rol
        /// </summary>
        /// <returns></returns>
        public ModuloUsuarioCLS listarModuloRol()
        {
            ModuloDAL oModulo = new ModuloDAL();
    
            RolDAL oRol = new RolDAL();

            ModuloUsuarioCLS oModuloUsuario = new ModuloUsuarioCLS();

            //Listado Modulo 
            oModuloUsuario.listaModulo = oModulo.listarModulo();

            //Listado Modulo Menu
            oModuloUsuario.listaModuloMenu = oModulo.ListarModuloMenu();

            //Listado Rol
            oModuloUsuario.listaRol = oRol.listarRol();

            return oModuloUsuario;
        }

        /// <summary>
        /// Listar Modulo Menu
        /// </summary>
        /// <returns></returns>
        public List<ModuloMenuCLS> ListarModuloMenu()
        {
            //instanciar  el objeto
            List<ModuloMenuCLS> lista = new List<ModuloMenuCLS>();
            ModuloDAL oModuloDAL = new ModuloDAL();
            return oModuloDAL.ListarModuloMenu();
        }

        /// <summary>
        /// Recuperar Modulo Menu
        /// </summary>
        /// <returns></returns>
        /// 
        public ModuloMenuCLS RecuperarModuloMenu(int iidmenuR)
        {
            ModuloDAL oTH = new ModuloDAL();
            return oTH.RecuperarModuloMenu(iidmenuR);
        }

        /// <summary>
        /// Guardar Modulo Menu 
        /// </summary>
        public int GuardarModuloMenu(ModuloMenuCLS moduloCLS)
        {
            ModuloDAL oTH = new ModuloDAL();
            return oTH.GuardarModuloMenu(moduloCLS);
        }

        /// <summary>
        /// Deshabilitar Modulo Menu
        /// </summary>
        /// <param name="iidmenuD"></param>
        /// <returns></returns>
        public int DeshabilitarModuloMenu(int iidmenuD)
        {
            ModuloDAL oTH = new ModuloDAL();
            return oTH.DesHabilitarModuloMenu(iidmenuD);
        }

 

      

       
    }
}
