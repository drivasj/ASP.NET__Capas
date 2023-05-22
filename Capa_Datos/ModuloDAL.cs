using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Datos
{
    public class ModuloDAL:CadenaDAL
    {
       

        /// <summary>
        /// Listar Modulo
        /// </summary>
        /// <returns></returns>
        public List<ModuloCLS> listarModulo()
        {
            List<ModuloCLS> lista = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT IIDMODULO,NOMBRE,CONTROLLER,ACCION,IIDMODULOMENU FROM admin_modulo WHERE BHABILITADO =1", cn))
                    {
                       // cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            lista = new List<ModuloCLS>();
                            ModuloCLS oModuloCLS;
                            int posIIDMODULO = drd.GetOrdinal("IIDMODULO");
                            int posNOMBRE = drd.GetOrdinal("NOMBRE");
                            int posCONTROLLER = drd.GetOrdinal("CONTROLLER");
                            int posACCION = drd.GetOrdinal("ACCION");
                            int posIIDMODULOMENU = drd.GetOrdinal("IIDMODULOMENU");

                            while (drd.Read())
                            {
                                oModuloCLS = new ModuloCLS();
                                oModuloCLS.iidmodulo = drd.IsDBNull(posIIDMODULO) ? 0 : drd.GetInt32(posIIDMODULO);
                                oModuloCLS.nombreModulo = drd.IsDBNull(posNOMBRE) ? "" : drd.GetString(posNOMBRE);
                                oModuloCLS.controllerModulo = drd.IsDBNull(posCONTROLLER) ? "" : drd.GetString(posCONTROLLER);
                                oModuloCLS.accionModulo = drd.IsDBNull(posACCION) ? "" : drd.GetString(posACCION);
                                oModuloCLS.iidmodulomenu = drd.IsDBNull(posIIDMODULOMENU) ? 0 : drd.GetInt32(posIIDMODULOMENU);

                                lista.Add(oModuloCLS);
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
        /// Recuperar Modulo
        /// </summary>
        /// <returns></returns>
        public ModuloCLS recuperarModulo(int iidmoduloR)
        {
            ModuloCLS oModuloCLS = null;
            // Cadena de conexión, antes agregar la referencia -> ensamblado -> System.Configuration 
            // string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    // Abrir conexión 
                    cn.Open();

                    //Llame al procedre
                    using (SqlCommand cmd = new SqlCommand("SELECT IIDMODULO,NOMBRE,CONTROLLER,ACCION,IIDMODULOMENU FROM admin_modulo WHERE BHABILITADO =1 AND IIDMODULO = @idmodulo", cn))
                    {
                        // Buena practica (Opcional)-> Indicamos que es un procedure
                       // cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idmodulo", iidmoduloR);
                        SqlDataReader drd = cmd.ExecuteReader();

                        // Para probar
                        if (drd != null)
                        {
                            int posIIDMODULO = drd.GetOrdinal("IIDMODULO");
                            int posNOMBRE = drd.GetOrdinal("NOMBRE");
                            int posCONTROLLER = drd.GetOrdinal("CONTROLLER");
                            int posACCION = drd.GetOrdinal("ACCION");
                            int posIIDMODULOMENU = drd.GetOrdinal("IIDMODULOMENU");

                            while (drd.Read())
                            {
                                oModuloCLS = new ModuloCLS();
                                oModuloCLS.iidmodulo = drd.IsDBNull(posIIDMODULO) ? 0 : drd.GetInt32(posIIDMODULO);
                                oModuloCLS.nombreModulo = drd.IsDBNull(posNOMBRE) ? "" : drd.GetString(posNOMBRE);
                                oModuloCLS.controllerModulo = drd.IsDBNull(posCONTROLLER) ? "" : drd.GetString(posCONTROLLER);
                                oModuloCLS.accionModulo = drd.IsDBNull(posACCION) ? "" : drd.GetString(posACCION);
                                oModuloCLS.iidmodulomenu = drd.IsDBNull(posIIDMODULOMENU) ? 0 : drd.GetInt32(posIIDMODULOMENU);
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
            return oModuloCLS;
        }

        /// <summary>
        /// Guardar Modulo
        /// </summary>
        public int GuardarModulo(ModuloCLS oModuloCLS)
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

                    if (oModuloCLS.iidmodulo == 0)
                    {
                         comandoSQL = "INSERT  admin_modulo(NOMBRE,CONTROLLER,ACCION,BHABILITADO) values(@nombre,@controller,@accion,1)";
                    }
                    else
                    {
                         comandoSQL = "UPDATE admin_modulo SET NOMBRE=@nombre, CONTROLLER = @controller,  ACCION= @accion WHERE IIDMODULO=@modulo";
                    }
             
                    //Llame al procedre
                    using (SqlCommand cmd = new SqlCommand(comandoSQL, cn))
                    {
                     

                        // cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@modulo", oModuloCLS.iidmodulo);
                        cmd.Parameters.AddWithValue("@nombre", oModuloCLS.nombreModulo);
                        cmd.Parameters.AddWithValue("@controller", oModuloCLS.controllerModulo);
                        cmd.Parameters.AddWithValue("@accion", oModuloCLS.accionModulo);

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

        //------------------Modulo Menu---------------------------////

        /// <summary>
        /// DesHabilitar Modulo Menu
        /// </summary>
        /// <param name="iidmenuD"></param>
        /// <returns></returns>
        public int DesHabilitarModuloMenu(int iidmenuD)
        {
            // 0 = error
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    // Abrir conexión 
                    cn.Open();

                    //Llame al procedre
                    using (SqlCommand cmd = new SqlCommand("UPDATE admin_menu SET BHABILITADO=0 WHERE IIDMENU=@id", cn))
                    {
                    //    cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", iidmenuD);

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

        /// <summary>
        /// Guardar Modulo Menu 
        /// </summary>
        public int GuardarModuloMenu(ModuloMenuCLS oModuloCLS)
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
                        using (SqlCommand cmd = new SqlCommand("[SPO_GMM]", cn, transaccion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@iidmuenu", oModuloCLS.iidmenuMM);
                            cmd.Parameters.AddWithValue("@iidtipousuario", oModuloCLS.iidrol);
                            cmd.Parameters.AddWithValue("@mensaje", oModuloCLS.mensajeMM);
                            cmd.Parameters.AddWithValue("@accion", oModuloCLS.accionMM);
                            cmd.Parameters.AddWithValue("@controller", oModuloCLS.controladorMM);

                            //Si el id es 0 es un nuevo registro
                            SqlParameter parametro = null;
                            if (oModuloCLS.iidmenuMM == 0)
                            {
                                parametro = cmd.Parameters.Add("@@identity", SqlDbType.Int);
                                parametro.Direction = ParameterDirection.ReturnValue;
                            }
                            rpta = cmd.ExecuteNonQuery(); // Devuelve los registros afectados
                                                          // Solo en agregar
                            if (oModuloCLS.iidmenuMM == 0)
                            {
                                //Tenemos el id
                                oModuloCLS.iidmenuMM = (int)parametro.Value;
                            }

                        }

                        //Actualizar el campo IIDMODULOMENU de la tabla modulo con el IIDMENU
                        using (SqlCommand cmd = new SqlCommand("UPDATE admin_modulo SET IIDMODULOMENU = @iidmodulomenu WHERE IIDMODULO = (SELECT MAX(IIDMODULO) FROM admin_modulo)", cn, transaccion))
                        {
                           // cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@iidmodulomenu", oModuloCLS.iidmenuMM);

                            rpta = cmd.ExecuteNonQuery(); // Devuelve los registros afectados                      
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

        /// <summary>
        /// Listar Modulo Menu
        /// </summary>
        /// <returns></returns>
        public List<ModuloMenuCLS> ListarModuloMenu()
        {
            List<ModuloMenuCLS> lista = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT M.IIDMENU,M.IIDROL,TP.NM_ROL AS [NOMBRE_ROL],M.MENSAJE,M.CONTROLLER,M.ACCION,M.BHABILITADO FROM admin_menu M INNER JOIN admin_rol TP ON M.IIDROL = TP.IIDROL WHERE IIDMODULO IS NULL AND M.BHABILITADO =1 AND ModuloP = 1", cn))
                    {
                        //cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            lista = new List<ModuloMenuCLS>();
                            ModuloMenuCLS oModuloCLS;
                            int posIIDMENU = drd.GetOrdinal("IIDMENU");
                            int posIIDROL = drd.GetOrdinal("IIDROL");
                            int posNOMBRE_ROL = drd.GetOrdinal("NOMBRE_ROL");
                            int posMENSAJE = drd.GetOrdinal("MENSAJE");
                            int posCONTROLLER = drd.GetOrdinal("CONTROLLER");
                            int posACCION = drd.GetOrdinal("ACCION");

                            while (drd.Read())
                            {
                                oModuloCLS = new ModuloMenuCLS();
                                oModuloCLS.iidmenuMM = drd.IsDBNull(posIIDMENU) ? 0 : drd.GetInt32(posIIDMENU);
                                oModuloCLS.iidrol = drd.IsDBNull(posIIDROL) ? 0 : drd.GetInt32(posIIDROL);
                                oModuloCLS.nombrerol = drd.IsDBNull(posNOMBRE_ROL) ? "" : drd.GetString(posNOMBRE_ROL);
                                oModuloCLS.mensajeMM = drd.IsDBNull(posMENSAJE) ? "" : drd.GetString(posMENSAJE);
                                oModuloCLS.controladorMM = drd.IsDBNull(posCONTROLLER) ? "" : drd.GetString(posCONTROLLER);
                                oModuloCLS.accionMM = drd.IsDBNull(posACCION) ? "" : drd.GetString(posACCION);
                                lista.Add(oModuloCLS);
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
        /// Recuperar Modulo Menu
        /// </summary>
        /// <returns></returns>
        /// 
        public ModuloMenuCLS RecuperarModuloMenu(int iidmenuR)
        {
            ModuloMenuCLS oModuloCLS = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT IIDMENU,IIDROL,MENSAJE,CONTROLLER,ACCION,BHABILITADO FROM admin_menu WHERE IIDMODULO IS NULL AND BHABILITADO =1 and IIDMENU = @idmenu ", cn))
                    {
                       // cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idmenu", iidmenuR);
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            int posIIDMENU = drd.GetOrdinal("IIDMENU");
                            int posIIDROL = drd.GetOrdinal("IIDROL");
                            int posMENSAJE = drd.GetOrdinal("MENSAJE");
                            int posCONTROLLER = drd.GetOrdinal("CONTROLLER");
                            int posACCION = drd.GetOrdinal("ACCION");

                            while (drd.Read())
                            {
                                oModuloCLS = new ModuloMenuCLS();
                                oModuloCLS.iidmenuMM = drd.IsDBNull(posIIDMENU) ? 0 : drd.GetInt32(posIIDMENU);
                                oModuloCLS.iidrol = drd.IsDBNull(posIIDROL) ? 0 : drd.GetInt32(posIIDROL);
                                oModuloCLS.mensajeMM = drd.IsDBNull(posMENSAJE) ? "" : drd.GetString(posMENSAJE);
                                oModuloCLS.controladorMM = drd.IsDBNull(posCONTROLLER) ? "" : drd.GetString(posCONTROLLER);
                                oModuloCLS.accionMM = drd.IsDBNull(posACCION) ? "" : drd.GetString(posACCION);

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
            return oModuloCLS;
        }

    }
}
