using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class PaginaBL
    {
        /// <summary>
        /// Listar
        /// </summary>
        /// <returns></returns>
        public IList<PaginaCLS> listarPagina()
        {
            List<PaginaCLS> lista = new List<PaginaCLS>();
            PaginaDAL oPaginaDAL = new PaginaDAL();
            return oPaginaDAL.listarPagina();
        }

        /// <summary>
        /// Recuperar Página
        /// </summary>
        /// <returns></returns>
        /// 
        public PaginaCLS recuperarPagina(int iidpagina)
        {
            PaginaDAL oTH = new PaginaDAL();
            return oTH.recuperarPagina(iidpagina);
        }

        /// <summary>
        /// Guardar 
        /// </summary>
        public int guardarPagina(PaginaCLS oPaginaCLS)
        {
            PaginaDAL oTH = new PaginaDAL();
            return oTH.guardarPagina(oPaginaCLS);
        }

        /// <summary>
        /// Lista Menu izquierdo
        /// </summary>
        /// 
        public List<admin_menu> ListarMenuEF(int IDUsuario)
        {
            PaginaDAL oMenuDAL = new PaginaDAL();
            return oMenuDAL.ListarMenuEF(IDUsuario);
        }

        /// <summary>
        /// Listar Menu al iniciar sesion
        /// </summary>
        /// <param name="iidtipousuario"></param>
        /// <returns></returns>
        public List<PaginaCLS> listarMenu(int iidrol)
        {
            PaginaDAL oTH = new PaginaDAL();
            return oTH.listarMenu(iidrol);
        }

    }
}
