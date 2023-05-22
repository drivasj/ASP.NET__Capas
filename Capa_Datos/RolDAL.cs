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
    public class RolDAL:CadenaDAL
    {
        /// <summary>
        /// Listar Rol
        /// </summary>
        /// <returns></returns>
        public List<RolCLS> listarRol()
        {
            List<RolCLS> lista = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT IIDROL, NM_ROL, DESCRIPCION FROM admin_rol WHERE BHABILITADO = 1", cn))
                    {
                      //  cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            lista = new List<RolCLS>();
                            RolCLS oMCLS;
                            int posIIDROL = drd.GetOrdinal("IIDROL");
                            int posNM_ROL = drd.GetOrdinal("NM_ROL");
                            int posDESCRIPCION = drd.GetOrdinal("DESCRIPCION");

                            while (drd.Read())
                            {
                                oMCLS = new RolCLS();
                                oMCLS.iidrol = drd.IsDBNull(posIIDROL) ? 0 : drd.GetInt32(posIIDROL);
                                oMCLS.nombre = drd.IsDBNull(posNM_ROL) ? "" : drd.GetString(posNM_ROL);
                                oMCLS.descripcion = drd.IsDBNull(posDESCRIPCION) ? "" : drd.GetString(posDESCRIPCION);

                                lista.Add(oMCLS);
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
        /// Recuperar Rol
        /// </summary>
        /// <returns></returns>
        /// 
        public RolCLS RecuperarRol(int iidrolR)
        {
            RolCLS oTipoUCLS = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();

                    string ComandoSQL = "SELECT  IIDROL, NM_ROL, DESCRIPCION FROM admin_rol WHERE IIDROL =@idtipousuario; SELECT IIDPAGINA FROM admin_rol_pagina WHERE IIDROL =@idtipousuario and BHABILITADO = 1"; 
                                        

                    using (SqlCommand cmd = new SqlCommand(ComandoSQL, cn))
                    {
                       // cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idtipousuario", iidrolR);
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            int posIIDROL = drd.GetOrdinal("IIDROL");
                            int posNM_ROL = drd.GetOrdinal("NM_ROL");
                            int posDESCRIPCION = drd.GetOrdinal("DESCRIPCION");

                            while (drd.Read())
                            {
                                oTipoUCLS = new RolCLS();
                                oTipoUCLS.iidrol = drd.IsDBNull(posIIDROL) ? 0 : drd.GetInt32(posIIDROL);
                                oTipoUCLS.nombre = drd.IsDBNull(posNM_ROL) ? "" : drd.GetString(posNM_ROL);
                                oTipoUCLS.descripcion = drd.IsDBNull(posDESCRIPCION) ? "" : drd.GetString(posDESCRIPCION);
                            }
                        }

                        // Viene el detalle para ver si hay otro select abajo
                        if (drd.NextResult())
                        {
                            oTipoUCLS.idpaginas = new List<int>();
                            while (drd.Read())
                            {
                                oTipoUCLS.idpaginas.Add(drd.GetInt32(0));
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
            return oTipoUCLS;
        }

        /// <summary>
        /// Guardar Rol 
        /// </summary>
        public int GuardarRol(RolCLS oRol)
        {
            // 0 = error
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    // Abrir conexión 
                    cn.Open();
                    using (SqlTransaction transaccion = cn.BeginTransaction())
                    {
                        //Llame al procedre
                        using (SqlCommand cmd = new SqlCommand("[SPO_GR]", cn, transaccion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@idtipousuario", oRol.iidrol);
                            cmd.Parameters.AddWithValue("@nombre", oRol.nombre);
                            cmd.Parameters.AddWithValue("@descripcion", oRol.descripcion);

                            //Si el id es 0 es un nuevo registro
                            SqlParameter parametro = null;
                            if (oRol.iidrol == 0)
                            {
                                parametro = cmd.Parameters.Add("@@identity", SqlDbType.Int);
                                parametro.Direction = ParameterDirection.ReturnValue;
                            }
                            rpta = cmd.ExecuteNonQuery(); // Devuelve los registros afectados
                                                          // Solo en agregar
                            if (oRol.iidrol == 0)
                            {
                                //Tenemos el id
                                oRol.iidrol = (int)parametro.Value;
                            }

                        }

                        //Deshabilitar paginas
                        using (SqlCommand cmd = new SqlCommand("UPDATE admin_rol_pagina SET BHABILITADO=0 WHERE IIDROL= @idtipousuario", cn, transaccion))
                        {
                        //    cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@idtipousuario", oRol.iidrol);

                            rpta = cmd.ExecuteNonQuery(); // Devuelve los registros afectados                      
                        }

                        // Agregar paginas al usuario
                        for (int i = 0; i < oRol.idpaginas.Count; i++)
                        {
                            using (SqlCommand cmd = new SqlCommand("[SPO_GPR]", cn, transaccion))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@idtipousuario", oRol.iidrol);
                                cmd.Parameters.AddWithValue("@idpagina", oRol.idpaginas[i]);

                                rpta = cmd.ExecuteNonQuery(); // Devuelve los registros afectados                      
                            }
                        }
                        transaccion.Commit();
                    } //Fin  SqlTransaction


                    //Cierro una vez de traer la data
                    cn.Close();
                }
                catch (Exception ex)
                {
                    cn.Close();
                }
            }

            return rpta;
        }

        /////--------------------Rol Pagina Menu---------------------///////

        /// <summary>
        ///Recuperar Rol Menu
        /// </summary>
        /// <param name="iidrolMenu"></param>
        /// <returns></returns>
        public RolCLS RecuperarRolMenu(int iidrolMenu)
        {
            RolCLS oTipoUCLS = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();

                    string ComandoSQL = "SELECT IIDROL, NM_ROL FROM admin_rol WHERE IIDROL =  @idtipousuario; SELECT IIDPAGINA FROM admin_menu WHERE IIDROL = @idtipousuario AND BHABILITADO =1 AND IIDMODULO IS NOT NULL;";
                   
                    using (SqlCommand cmd = new SqlCommand(ComandoSQL, cn))
                    {
                       // cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idtipousuario", iidrolMenu);
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            int posIIDROL = drd.GetOrdinal("IIDROL");
                            int posNM_ROL = drd.GetOrdinal("NM_ROL");

                            while (drd.Read())
                            {
                                oTipoUCLS = new RolCLS();
                                oTipoUCLS.iidrol = drd.IsDBNull(posIIDROL) ? 0 : drd.GetInt32(posIIDROL);
                                oTipoUCLS.nombre = drd.IsDBNull(posNM_ROL) ? "" : drd.GetString(posNM_ROL);
                            }
                        }

                        // Viene el detalle para ver si hay otro select abajo
                        if (drd.NextResult())
                        {
                            oTipoUCLS.idpaginas = new List<int>();
                            //     oTipoUCLS.nameP = new List<string>();
                            while (drd.Read())
                            {
                                oTipoUCLS.idpaginas.Add(drd.GetInt32(0));
                                //   oTipoUCLS.nameP.Add(drd.GetString(1));
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
            return oTipoUCLS;
        }

        /// <summary>
        /// Guardar Rol Menu
        /// </summary>
        /// <param name="oRolCLS"></param>
        /// <returns></returns>
        public int GuardarRolMenu(RolCLS oRolCLS)
        {          
            // 0 = error
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    // Abrir conexión 
                    cn.Open();
                    using (SqlTransaction transaccion = cn.BeginTransaction())
                    {
                        //Llame al procedre
                        using (SqlCommand cmd = new SqlCommand("[SPO_GR]", cn, transaccion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@idtipousuario", oRolCLS.iidrol);
                            cmd.Parameters.AddWithValue("@nombre", oRolCLS.nombre);
                            cmd.Parameters.AddWithValue("@descripcion", oRolCLS.descripcion);

                            //Si el id es 0 es un nuevo registro
                            //SqlParameter parametro = null;
                            //if (oTipoUsuario.iidtipousuario == 0)
                            //{
                            //    parametro = cmd.Parameters.Add("@@identity", SqlDbType.Int);
                            //    parametro.Direction = ParameterDirection.ReturnValue;
                            //}
                            //rpta = cmd.ExecuteNonQuery(); // Devuelve los registros afectados
                            //                              // Solo en agregar
                            //if (oTipoUsuario.iidtipousuario == 0)
                            //{
                            //    //Tenemos el id
                            //    oTipoUsuario.iidtipousuario = (int)parametro.Value;
                            //}

                        }

                        //Deshabilitar paginas Menu
                        using (SqlCommand cmd = new SqlCommand("UPDATE admin_menu SET BHABILITADO=0 WHERE IIDROL= @idtipousuario AND NOT Modulop =1", cn, transaccion))
                        {
                           // cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@idtipousuario", oRolCLS.iidrol);


                            cmd.ExecuteNonQuery(); // Devuelve los registros afectados                      
                        }


                        //int CantidadName = oTipoUsuario.nameP.Count;
                        //int rp2 = CantidadName;
                        //int CantidadPaginas = oTipoUsuario.idpaginas.Count;
                        //int rp = CantidadPaginas;

                        // Agregar paginas al usuario
                        for (int i = 0; i < oRolCLS.idpaginas.Count; i++)
                        {
                            using (SqlCommand cmd = new SqlCommand("[SPO_GPRM]", cn, transaccion))
                            {

                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@idtipousuario", oRolCLS.iidrol);
                                cmd.Parameters.AddWithValue("@idpagina", oRolCLS.idpaginas[i]);
                                //    cmd.Parameters.AddWithValue("@mensaje", oTipoUsuario.nameP[i]);
                                //   cmd.Parameters.AddWithValue("@mensaje", lista.mensaje);

                                rpta = cmd.ExecuteNonQuery(); // Devuelve los registros afectados                      
                            }
                        }
                        transaccion.Commit();
                    } //Fin  SqlTransaction


                    //Cierro una vez de traer la data
                    cn.Close();
                }
                catch (Exception ex)
                {
                    cn.Close();
                }
            }

            return rpta;
        }


        /////--------------------Asignar Menu---------------------///////

        /// <summary>
        /// Listar Pagina
        /// </summary>
        /// <returns></returns>
        public List<MenuCLS> listarTMenu()
        {
            List<MenuCLS> lista = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();

                    string ComandoSQL = "SELECT IIDMENU,T.NM_ROL [ROL], MENSAJE [PAGINA],CASE WHEN (SELECT MENSAJE FROM admin_menu M2 WHERE M2.IIDMENU = M.IIDMODULO  ) IS NULL THEN 'Sin asignar' ELSE (SELECT MENSAJE FROM admin_menu M2 WHERE M2.IIDMENU = M.IIDMODULO  )END AS [MODULO],CASE WHEN IIDMODULO IS NULL THEN 0 ELSE IIDMODULO END AS IIDMODULO FROM admin_menu M INNER JOIN admin_rol T ON T.IIDROL=M.IIDROL  WHERE M.BHABILITADO =1 AND Modulop IS NULL";

                    using (SqlCommand cmd = new SqlCommand(ComandoSQL, cn))
                    {
                       // cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            lista = new List<MenuCLS>();
                            MenuCLS oMenuCLS;
                            int posIIDMENU = drd.GetOrdinal("IIDMENU");
                            int posROL = drd.GetOrdinal("ROL");
                            int posPAGINA = drd.GetOrdinal("PAGINA");
                            int posMODULO = drd.GetOrdinal("MODULO");
                            int posIIDMODULO = drd.GetOrdinal("IIDMODULO");
                            //  int posIIDMODULO = drd.GetOrdinal("IIDMODULO");

                            while (drd.Read())
                            {
                                oMenuCLS = new MenuCLS();
                                oMenuCLS.iidmenu = drd.IsDBNull(posIIDMENU) ? 0 : drd.GetInt32(posIIDMENU);
                                oMenuCLS.rol = drd.IsDBNull(posROL) ? "" : drd.GetString(posROL);
                                oMenuCLS.nombreMenu = drd.IsDBNull(posPAGINA) ? "" : drd.GetString(posPAGINA);
                                oMenuCLS.ds_modulo = drd.IsDBNull(posMODULO) ? "" : drd.GetString(posMODULO);
                                oMenuCLS.iidmoduloTM = drd.IsDBNull(posIIDMODULO) ? 0 : drd.GetInt32(posIIDMODULO);
                                lista.Add(oMenuCLS);
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
        /// Recuperar TMenu
        /// </summary>
        /// <returns></returns>
        public MenuCLS recuperarTMenu(int iidmenuR)
        {
            MenuCLS oMenuCLS = null;
            // Cadena de conexión, antes agregar la referencia -> ensamblado -> System.Configuration 
            // string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    // Abrir conexión 
                    cn.Open();

                    string ComandoSQL = "SELECT IIDMENU,T.NM_ROL [ROL], MENSAJE [PAGINA], CASE WHEN (SELECT MENSAJE FROM admin_menu M2 WHERE M2.IIDMENU = M.IIDMODULO  ) IS NULL THEN 'Sin asignar' ELSE (SELECT MENSAJE FROM admin_menu M2 WHERE M2.IIDMENU = M.IIDMODULO  )END AS [MODULO],CASE WHEN IIDMODULO IS NULL THEN 0 ELSE IIDMODULO END AS IIDMODULO FROM admin_menu M INNER JOIN admin_rol T ON T.IIDROL=M.IIDROL WHERE IIDMENU = @iidmenu";

                    //Llame al procedre
                    using (SqlCommand cmd = new SqlCommand(ComandoSQL, cn))
                    {
                        // Buena practica (Opcional)-> Indicamos que es un procedure
                      //  cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iidmenu", iidmenuR);
                        SqlDataReader drd = cmd.ExecuteReader();

                        // Para probar
                        if (drd != null)
                        {

                            int posIIDMENU = drd.GetOrdinal("IIDMENU");
                            int posROL = drd.GetOrdinal("ROL");
                            int posPAGINA = drd.GetOrdinal("PAGINA");
                            int posMODULO = drd.GetOrdinal("MODULO");
                            int posIIDMODULO = drd.GetOrdinal("IIDMODULO");

                            while (drd.Read())
                            {
                                oMenuCLS = new MenuCLS();
                                oMenuCLS.iidmenu = drd.IsDBNull(posIIDMENU) ? 0 : drd.GetInt32(posIIDMENU);
                                oMenuCLS.rol = drd.IsDBNull(posROL) ? "" : drd.GetString(posROL);
                                oMenuCLS.nombreMenu = drd.IsDBNull(posPAGINA) ? "" : drd.GetString(posPAGINA);
                                oMenuCLS.ds_modulo = drd.IsDBNull(posMODULO) ? "" : drd.GetString(posMODULO);
                                oMenuCLS.iidmoduloTM = drd.IsDBNull(posIIDMODULO) ? 0 : drd.GetInt32(posIIDMODULO);

                            }
                        }
                    }



                    //Cierro una vez de traer la data 
                    cn.Close();

                }
                catch (Exception ex)
                {
                    cn.Close();
                }
            }

            return oMenuCLS;

        }

        /// <summary>
        /// Asignar Módulos a las páginas del Menu
        /// </summary>
        public int GuardarTmenu(MenuCLS oMenuCLS)
        {
            // 0 = error
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    // Abrir conexión 
                    cn.Open();

                    //string comandoSQL = "";

                    //if (oMenuCLS.iidmenu == 0) // INSERT
                    //{
                    //    comandoSQL = "";
                    //}
                    //else //// UPDATE
                    //{
                    //    comandoSQL = "";
                    //}

                    //Llame al procedre
                    using (SqlCommand cmd = new SqlCommand("IF @iidmenu >0 UPDATE admin_menu SET IIDMODULO = @iidmodulo WHERE IIDMENU=@iidmenu", cn))
                    {
                       // cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iidmenu", oMenuCLS.iidmenu);
                        cmd.Parameters.AddWithValue("@iidmodulo", oMenuCLS.iidmoduloTM);

                        rpta = cmd.ExecuteNonQuery(); // Devuelve los registros afectados
                        cn.Close();
                    }
                }
                catch (Exception ex)
                {
                    rpta = 0;
                }
            }

            return rpta;
        }
    }
}
