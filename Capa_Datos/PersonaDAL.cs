using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Usuario;

namespace Capa_Datos
{
    public class PersonaDAL : CadenaDAL
    {
        /// <summary>
        /// Listar Persona
        /// </summary>
        /// <returns></returns>
        public List<PersonaCLS> listarPersona()
        {
            List<PersonaCLS> lista = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand
                        (" SELECT P.IIDPERSONA,  P.NOMBRE + ' ' + P.APPATERNO + ' ' + P.APMATERNO AS NOMBRECOMPLETO, S.NOMBRE AS NOMBRESEXO, T.NM_ROL AS NOMBREROL, P.BHABILITADO FROM admin_persona P INNER JOIN admin_sexo S ON P.IIDSEXO = S.IIDSEXO INNER JOIN admin_rol T ON P.IIDROL = T.IIDROL WHERE P.BHABILITADO = 1"
                        , cn))
                    {
                        //  cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            lista = new List<PersonaCLS>();
                            PersonaCLS oPersonaCLS;
                            int posIIDPERSONA = drd.GetOrdinal("IIDPERSONA");
                            int posNOMBRECOMPLETO = drd.GetOrdinal("NOMBRECOMPLETO");
                            int posNOMBRESEXO = drd.GetOrdinal("NOMBRESEXO");
                            int posNOMBREROL = drd.GetOrdinal("NOMBREROL");
                            int posBHABILITADO = drd.GetOrdinal("BHABILITADO");
                            //  int posFOTO = drd.GetOrdinal("foto");
                            //  int posNOMBREFOTO = drd.GetOrdinal("nombrefoto");

                            while (drd.Read())
                            {
                                oPersonaCLS = new PersonaCLS();
                                oPersonaCLS.iidpersona = drd.IsDBNull(posIIDPERSONA) ? 0 : drd.GetInt32(posIIDPERSONA);
                                oPersonaCLS.nombreCompleto = drd.IsDBNull(posNOMBRECOMPLETO) ? "" : drd.GetString(posNOMBRECOMPLETO);
                                oPersonaCLS.nombreSexo = drd.IsDBNull(posNOMBRESEXO) ? "" : drd.GetString(posNOMBRESEXO);
                                oPersonaCLS.nombreRol = drd.IsDBNull(posNOMBREROL) ? "" : drd.GetString(posNOMBREROL);
                                oPersonaCLS.bhabilitado = drd.IsDBNull(posBHABILITADO) ? 0 : drd.GetInt32(posBHABILITADO);


                                //oPersonaCLS.nombrefoto = drd.IsDBNull(posNOMBREFOTO) ? "" : drd.GetString(posNOMBREFOTO);

                                //if (!drd.IsDBNull(posFOTO))
                                //{
                                //    string nomfoto = oPersonaCLS.nombrefoto;
                                //    // .jpg .png
                                //    string extension = Path.GetExtension(nomfoto);
                                //    string nombresinextension = extension.Substring(1);
                                //    byte[] fotobyte = (byte[])drd.GetValue(posFOTO);
                                //    //nimee data:image/formato;base64,
                                //    //data:image/jpg;base64,
                                //    //data:image/png;base64,
                                //    //data:image/jpge;base64,

                                //    string mime = "data:image/" + nombresinextension + ";base64,";
                                //    string fotobase = Convert.ToBase64String(fotobyte);
                                //    oPersonaCLS.fotobase64 = mime + fotobase;


                                //    oPersonaCLS.foto = (byte[])drd.GetValue(posFOTO);
                                //}
                                //else
                                //{
                                //    oPersonaCLS.fotobase64 = fotofinal;
                                //}

                                lista.Add(oPersonaCLS);
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
        /// Filtrar Persona por Rol
        /// </summary>
        /// <returns></returns>
        /// 
        public List<PersonaCLS> FiltrarPersonaRol(int iidrol)
        {
            List<PersonaCLS> lista = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("if @iidrol ='' SELECT P.IIDPERSONA,P.NOMBRE+ ' '+P.APPATERNO+ ' '+P.APMATERNO as NOMBRECOMPLETO , S.NOMBRE AS NOMBRESEXO,T.NM_ROL AS NOMBRETIPOUSUARIO FROM admin_persona P INNER JOIN admin_sexo S ON P.IIDSEXO=S.IIDSEXO INNER JOIN admin_rol T ON P.IIDROL=T.IIDROL ELSE SELECT P.IIDPERSONA,P.NOMBRE+ ' '+P.APPATERNO+ ' '+P.APMATERNO as NOMBRECOMPLETO ,S.NOMBRE AS NOMBRESEXO , T.NM_ROL AS NOMBRETIPOUSUARIO FROM admin_persona P INNER JOIN admin_sexo S ON P.IIDSEXO=S.IIDSEXO INNER JOIN admin_rol T ON P.IIDROL=T.IIDROL WHERE P.BHABILITADO=1 and p.IIDROL=@iidrol", cn))
                    {
                        // cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iidrol", iidrol);
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            lista = new List<PersonaCLS>();
                            PersonaCLS oMCLS;
                            int posIIDPERSONA = drd.GetOrdinal("IIDPERSONA");
                            int posNOMBRECOMPLETO = drd.GetOrdinal("NOMBRECOMPLETO");
                            int posNOMBRESEXO = drd.GetOrdinal("NOMBRESEXO");
                            int posNOMBRETIPOUSUARIO = drd.GetOrdinal("NOMBRETIPOUSUARIO");


                            while (drd.Read())
                            {
                                oMCLS = new PersonaCLS();
                                oMCLS.iidpersona = drd.IsDBNull(posIIDPERSONA) ? 0 : drd.GetInt32(posIIDPERSONA);
                                oMCLS.nombreCompleto = drd.IsDBNull(posNOMBRECOMPLETO) ? "" : drd.GetString(posNOMBRECOMPLETO);
                                oMCLS.nombreSexo = drd.IsDBNull(posNOMBRESEXO) ? "" : drd.GetString(posNOMBRESEXO);
                                oMCLS.nombreRol = drd.IsDBNull(posNOMBRETIPOUSUARIO) ? "" : drd.GetString(posNOMBRETIPOUSUARIO);

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
        /// Recuperar Persona
        /// </summary>
        /// <returns></returns>
        /// 
        public PersonaCLS recuperarPersona(int iidpersona)
        {
            PersonaCLS oPersonaCLS = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT p.IIDPERSONA,NOMBRE,APPATERNO,APMATERNO,TELEFONO,IIDSEXO,IIDROL,ISNULL( u.IIDUSUARIO,0)AS IIDUSUARIO ,ISNULL(u.NOMBREUSUARIO,'') AS NOMBREUSUARIO FROM admin_persona p LEFT JOIN admin_usuario u ON U.IIDPERSONA =P.IIDPERSONA WHERE p.IIDPERSONA=@idpersona", cn))
                    {
                       // cmd.CommandType = CommandType.StoredProcedure; // QUITAR
                        cmd.Parameters.AddWithValue("@idpersona", iidpersona);
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            int posIIDPERSONA = drd.GetOrdinal("IIDPERSONA");
                            int posNOMBRE = drd.GetOrdinal("NOMBRE");
                            int posAPPATERNO = drd.GetOrdinal("APPATERNO");
                            int posAPMATERNO = drd.GetOrdinal("APMATERNO");
                            int posTELEFONOFIJO = drd.GetOrdinal("TELEFONO");
                            int posIIDSEXO = drd.GetOrdinal("IIDSEXO");
                            int posIIDTIPOUSUARIO = drd.GetOrdinal("IIDROL");
                           // int posFOTO = drd.GetOrdinal("foto");
                           // int posNOMBREFOTO = drd.GetOrdinal("nombrefoto");
                            int posIIDUSUARIO = drd.GetOrdinal("IIDUSUARIO");
                            int posNOMBREUSUARIO = drd.GetOrdinal("NOMBREUSUARIO");

                            while (drd.Read())
                            {
                                oPersonaCLS = new PersonaCLS();
                                oPersonaCLS.iidpersona = drd.IsDBNull(posIIDPERSONA) ? 0 : drd.GetInt32(posIIDPERSONA);
                                oPersonaCLS.nombre = drd.IsDBNull(posNOMBRE) ? "" : drd.GetString(posNOMBRE);
                                oPersonaCLS.apellidoPaterno = drd.IsDBNull(posAPPATERNO) ? "" : drd.GetString(posAPPATERNO).Trim();
                                oPersonaCLS.apellidoMaterno = drd.IsDBNull(posAPMATERNO) ? "" : drd.GetString(posAPMATERNO).Trim();
                                oPersonaCLS.telefono = drd.IsDBNull(posTELEFONOFIJO) ? "" : drd.GetString(posTELEFONOFIJO).Trim();
                                oPersonaCLS.iidsexo = drd.IsDBNull(posIIDSEXO) ? 0 : drd.GetInt32(posIIDSEXO);
                                oPersonaCLS.iidrol = drd.IsDBNull(posIIDTIPOUSUARIO) ? 0 : drd.GetInt32(posIIDTIPOUSUARIO);
                                //  oPersonaCLS.nombrefoto = drd.IsDBNull(posNOMBREFOTO) ? "" : drd.GetString(posNOMBREFOTO);
                                //para el login
                                oPersonaCLS.iidususario = drd.IsDBNull(posIIDUSUARIO) ? 0 : drd.GetInt32(posIIDUSUARIO);
                                oPersonaCLS.nombreusuario = drd.IsDBNull(posNOMBREUSUARIO) ? "" : drd.GetString(posNOMBREUSUARIO);                         
                            }           
                        }

                    } // Cierro una vez de traer la data 
                    cn.Close();
                }
                catch (Exception ex)
                {
                    cn.Close();
                }
            }
            return oPersonaCLS;
        }

        /// <summary>
        /// Guardar Persona
        /// </summary>
        public int GuardarPersona(PersonaCLS oPersona, UsuarioCLS oUsuarioCLS)
        {
            // 0 = error
            int rpta = 0;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    // Abrir conexión 
                    cn.Open();
                    //Transacción: Afecta a mas de una tabla 
                    using (SqlTransaction transaccion = cn.BeginTransaction())
                    {
                        //Guardar Persona
                        using (SqlCommand cmd = new SqlCommand("SPO_GP", cn, transaccion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@iddpersona", oPersona.iidpersona);
                            cmd.Parameters.AddWithValue("@NOMBRE", oPersona.nombre);
                            cmd.Parameters.AddWithValue("@APPATERNO", oPersona.apellidoPaterno);
                            cmd.Parameters.AddWithValue("@APMATERNO", oPersona.apellidoMaterno);
                            cmd.Parameters.AddWithValue("@TELEFONO", oPersona.telefono);
                            cmd.Parameters.AddWithValue("@IIDSEXO", oPersona.iidsexo);
                            cmd.Parameters.AddWithValue("@IIDTIPOUSUARIO", oPersona.iidrol);
                                        
                         // Return el iidpersona
                          SqlParameter parametro = null;
                          if (oPersona.iidpersona == 0)
                          {
                              parametro = cmd.Parameters.Add("@@identity", SqlDbType.Int);
                              parametro.Direction = ParameterDirection.ReturnValue;                        
                          }
                          cmd.ExecuteNonQuery(); // Devuelve los registros afectados

                          // iidpersona = al valor retornado
                          if (oPersona.iidpersona == 0)
                          {
                              oPersona.iidpersona = (int)parametro.Value;
                          }
                        }

                        //Guardar Usuario
                        if (oPersona.iidususario == 0) // INSERT
                        {
                            using (SqlCommand cmd = new SqlCommand("INSERT admin_usuario(NOMBREUSUARIO,CONTRA,IIDPERSONA,BHABILITADO) VALUES(@nombreusuario,@contra,@iidpersona,1);", cn, transaccion))
                            {
                                //  cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@iidusuario", oPersona.iidususario);
                                cmd.Parameters.AddWithValue("@nombreusuario", oUsuarioCLS.nombreusuario);
                                cmd.Parameters.AddWithValue("@contra", Generic.cifrarCadena(oUsuarioCLS.contra));
                                cmd.Parameters.AddWithValue("@iidpersona", oPersona.iidpersona);
                                rpta = cmd.ExecuteNonQuery(); // Devuelve los registros afectados
                            }
                        }
                        else //// UPDATE
                        {
                            using (SqlCommand cmd = new SqlCommand("UPDATE admin_usuario SET NOMBREUSUARIO=@nombreusuario WHERE IIDUSUARIO=@iidusuario", cn, transaccion))
                            {
                               // cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@iidusuario", oPersona.iidususario);
                                cmd.Parameters.AddWithValue("@nombreusuario", oUsuarioCLS.nombreusuario);
                                cmd.Parameters.AddWithValue("@iidpersona", oPersona.iidpersona);
                                rpta = cmd.ExecuteNonQuery(); // Devuelve los registros afectados
                            }
                        }
                            transaccion.Commit();
                        //  transaccion.Rollback();
                    }
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
        /// Login
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="contra"></param>
        /// <returns></returns>
        public PersonaCLS uspLogin(string usuario, string contra)
        {
            PersonaCLS oPersonaCLS = null;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    string contracifrado = Generic.cifrarCadena(contra);
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT u.IIDUSUARIO, p.nombre+' '+p.appaterno+' '+p.APMATERNO as NOMBRECOMPLETO, p.IIDROL FROM  admin_usuario u INNER JOIN admin_persona p ON u.iidpersona=p.iidpersona WHERE u.nombreusuario=@nombreusuario AND u.contra=@contra AND p.BHABILITADO=1;", cn))
                    {
                        //Buena practica (Opcional)->Indicamos que es un procedure
                      //  cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombreusuario", usuario);
                        cmd.Parameters.AddWithValue("@contra", contracifrado);
                        SqlDataReader drd = cmd.ExecuteReader(); // como es un select lo que devuelve

                        if (drd != null)
                        {
                            // instanciamos el objeto PersonaCLS
                            oPersonaCLS = new PersonaCLS();
                            //Leer datos
                            while (drd.Read())
                            {
                                oPersonaCLS.iidususario = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oPersonaCLS.nombreCompleto = drd.IsDBNull(1) ? "" : drd.GetString(1);
                                oPersonaCLS.iidrol = drd.IsDBNull(2) ? 0 : drd.GetInt32(2);              
                            }
                        }
                    }

                    //Cierro una vez de traer la data
                    //  cn.Close();
                }
                catch (Exception ex)
                {
                    oPersonaCLS = null;
                }
            }
            return oPersonaCLS;
        }
    }
}