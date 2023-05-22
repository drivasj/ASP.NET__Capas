using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class PaginaDAL:CadenaDAL
    {
        /// <summary>
        /// Listar Pagina
        /// </summary>
        /// <returns></returns>
        public List<PaginaCLS> listarPagina()
        {
            List<PaginaCLS> lista = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand
                        ("SELECT IIDPAGINA, NM_PAG,ACCION,CONTROLLER FROM admin_pagina WHERE BHABILITADO = 1", cn))
                    {
                      //  cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            lista = new List<PaginaCLS>();
                            PaginaCLS oPCLS;
                            int posIIDPAGINA = drd.GetOrdinal("IIDPAGINA");
                            int posNM_PAG = drd.GetOrdinal("NM_PAG");
                            int posACCION = drd.GetOrdinal("ACCION");
                            int posCONTROLLER = drd.GetOrdinal("CONTROLLER");



                            while (drd.Read())
                            {
                                oPCLS = new PaginaCLS();
                                oPCLS.iidpagina = drd.IsDBNull(posIIDPAGINA) ? 0 : drd.GetInt32(posIIDPAGINA);
                                oPCLS.nm_pagina = drd.IsDBNull(posNM_PAG) ? "" : drd.GetString(posNM_PAG);
                                oPCLS.actionP = drd.IsDBNull(posACCION) ? "" : drd.GetString(posACCION);
                                oPCLS.controllerP = drd.IsDBNull(posCONTROLLER) ? "" : drd.GetString(posCONTROLLER);

                                lista.Add(oPCLS);
                            }

                        }
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    cn.Close();
                }
            }
            return lista;
        }

        /// <summary>
        /// Recuperar Página
        /// </summary>
        /// <returns></returns>
        /// 
        public PaginaCLS recuperarPagina(int iidpagina)
        {
            PaginaCLS paginaCLS = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT IIDPAGINA,NM_PAG,ACCION,CONTROLLER FROM admin_pagina WHERE IIDPAGINA = @idpagina", cn))
                    {
                      //  cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idpagina", iidpagina);
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            int posIIDPAGINA = drd.GetOrdinal("IIDPAGINA");
                            int posNM_PAG = drd.GetOrdinal("NM_PAG");
                            int posACCION = drd.GetOrdinal("ACCION");
                            int posCONTROLLER = drd.GetOrdinal("CONTROLLER");

                            while (drd.Read())
                            {
                                paginaCLS = new PaginaCLS();
                                paginaCLS.iidpagina = drd.IsDBNull(posIIDPAGINA) ? 0 : drd.GetInt32(posIIDPAGINA);
                                paginaCLS.nm_pagina = drd.IsDBNull(posNM_PAG) ? "" : drd.GetString(posNM_PAG);
                                paginaCLS.actionP = drd.IsDBNull(posACCION) ? "" : drd.GetString(posACCION);
                                paginaCLS.controllerP = drd.IsDBNull(posCONTROLLER) ? "" : drd.GetString(posCONTROLLER);
                            }
                        }
                    }
                    // Cierro una vez de traer la data 
                    cn.Close();
                }
                catch (Exception ex)
                {
                    cn.Close();
                }
            }
            return paginaCLS;
        }

        /// <summary>
        /// Guardar 
        /// </summary>
        public int guardarPagina(PaginaCLS oPaginaCLS)
        {
            // 0 = error
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    // Abrir conexión 
                    cn.Open();

                    string comandoSQL = "";

                    if (oPaginaCLS.iidpagina == 0) // INSERT
                    {
                        comandoSQL = "INSERT admin_pagina(NM_PAG,ACCION,CONTROLLER,BHABILITADO) VALUES(@mensaje,@accion,@controller,1)";
                    }
                    else //// UPDATE
                    {
                        comandoSQL = "UPDATE admin_pagina SET NM_PAG=@mensaje,ACCION = @accion, CONTROLLER = @controller WHERE IIDPAGINA=@iidpagina";
                    }

                    //Llame al procedre
                    using (SqlCommand cmd = new SqlCommand(comandoSQL, cn))
                    {
                        //cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iidpagina", oPaginaCLS.iidpagina);
                        cmd.Parameters.AddWithValue("@mensaje", oPaginaCLS.nm_pagina);
                        cmd.Parameters.AddWithValue("@accion", oPaginaCLS.actionP);
                        cmd.Parameters.AddWithValue("@controller", oPaginaCLS.controllerP);
                        rpta = cmd.ExecuteNonQuery(); // Devuelve los registros afectados

                    }
                }
                catch (Exception ex)
                {
                    cn.Close();
                }
            }
            return rpta;
        }

        /// <summary>
        /// Lista Menu izquierdo
        /// </summary>
        /// 
        private SystemsEntities db = new SystemsEntities();
        public List<admin_menu> ListarMenuEF(int IDUsuario)
        {
            // List<MenuCLS> listaMenuEF = new List<MenuCLS>();

            //var listaMenuEF = (from menu in db.Menu
            //                   where menu.IIDMODULO == null
            //                   && menu.IIDROL == IDUsuario
            //                   && menu.BHABILITADO == 1
            //                   select new MenuCLS
            //                   {
            //                       iidmenu = menu.IIDMENU,
            //                       iidmoduloTM = menu.IIDMODULO,
            //                       iidrolMenu = menu.IIDROL,
            //                       order = menu.ORDEN,
            //                       nombreMenu = menu.MENSAJE,
            //                       controllerMenu = menu.CONTROLLER,
            //                       accionMenu = menu.ACCION,
            //                       Menu1 = (ICollection<MenuCLS>)menu.Menu1
            //                       //  rol = menu.
            //                       //    ds_modulo = menu.ds
            //                       // iidmoduloPTM =
            //                   }).ToList();

            var listaMenuEF = db.admin_menu.Where(x => x.IIDMODULO == null
           && x.IIDROL == IDUsuario
           && x.BHABILITADO == 1).ToList();

            return listaMenuEF;
        }

        /// <summary>
        /// Listar Menu al iniciar sesion
        /// </summary>
        /// <returns></returns>
        public List<PaginaCLS> listarMenu(int iidrol)
        {
            List<PaginaCLS> lista = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT P.IIDPAGINA,NM_PAG,ACCION,CONTROLLER FROM admin_pagina P INNER JOIN admin_Rol_Pagina RP ON P.IIDPAGINA = RP.IIDPAGINA  WHERE RP.BHABILITADO=1 AND RP.IIDROL = @iidtipousuario;", cn))
                    {
                       // cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iidtipousuario", iidrol);
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            lista = new List<PaginaCLS>();
                            PaginaCLS oPCLS;
                            int posIIDPAGINA = drd.GetOrdinal("IIDPAGINA");
                            int posNM_PAG = drd.GetOrdinal("NM_PAG");
                            int posACCION = drd.GetOrdinal("ACCION");
                            int posCONTROLLER = drd.GetOrdinal("CONTROLLER");

                            while (drd.Read())
                            {
                                oPCLS = new PaginaCLS();
                                oPCLS.iidpagina = drd.IsDBNull(posIIDPAGINA) ? 0 : drd.GetInt32(posIIDPAGINA);
                                oPCLS.nm_pagina = drd.IsDBNull(posNM_PAG) ? "" : drd.GetString(posNM_PAG);
                                oPCLS.actionP = drd.IsDBNull(posACCION) ? "" : drd.GetString(posACCION);
                                oPCLS.controllerP = drd.IsDBNull(posCONTROLLER) ? "" : drd.GetString(posCONTROLLER);

                                lista.Add(oPCLS);

                            }
                        }
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    cn.Close();
                }
            }
            return lista;
        }
    }
}
