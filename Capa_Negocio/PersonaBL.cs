using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class PersonaBL
    {
        /// <summary>
        /// Listar Persona 
        /// </summary>
        /// <returns></returns>
        public List<PersonaCLS> listarPersona()
        {
            List<PersonaCLS> lista = new List<PersonaCLS>();
            PersonaDAL oPersonaDAL = new PersonaDAL();
            return oPersonaDAL.listarPersona();
        }

        /// <summary>
        /// Filtrar persona por rol
        /// </summary>
        /// <param name="iidrol"></param>
        /// <returns></returns>
        public List<PersonaCLS> FiltrarPersonaRol(int iidrol)
        {
            PersonaDAL oTmDAL = new PersonaDAL();
            return oTmDAL.FiltrarPersonaRol(iidrol);
        }

        /// <summary>
        /// Recuperar Persona
        /// </summary>
        /// <param name="iidtipousuario"></param>
        /// <returns></returns>
        public PersonaCLS recuperarPersona(int iidpersona)
        {
            PersonaDAL oTmDAL = new PersonaDAL();
            return oTmDAL.recuperarPersona(iidpersona);
        }

        /// <summary>
        /// Guardar Persona
        /// </summary>
        /// <param name="oPersona"></param>
        /// <returns></returns>
        public int GuardarPersona(PersonaCLS oPersona, UsuarioCLS oUsuarioCLS)
        {
            PersonaDAL oTmDAL = new PersonaDAL();
            return oTmDAL.GuardarPersona(oPersona, oUsuarioCLS);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="contra"></param>
        /// <returns></returns>
        public PersonaCLS uspLogin(string usuario, string contra)
        {
            PersonaDAL oTmDAL = new PersonaDAL();
            return oTmDAL.uspLogin(usuario, contra);
        }

    }
}
