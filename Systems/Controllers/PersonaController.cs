using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oesia_Arequipa.Controllers
{
    public class PersonaController : Controller
    {
        // GET: Persona
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Listar Pesona
        /// </summary>
        /// <returns></returns>
        public JsonResult listarPersona()
        {
            PersonaBL oPersonaBL = new PersonaBL();
            //ruta
            //string rutaAbsolutaNoFoto = Server.MapPath("~/Image/nofoto.png");
            //Como leo sus bytes
            //byte[] buferNoFoto = io.File.ReadAllBytes(rutaAbsolutaNoFoto);
            //string baseNoFoto = Convert.ToBase64String(buferNoFoto);
            //string mime = "data:image/png;base64,";
            //string fotoFinal = mime + baseNoFoto;
            var json = Json(oPersonaBL.listarPersona(), JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 999999999;
            return json;
        }

        /// <summary>
        ///  Filtrar persona por rol
        /// </summary>
        /// <param name="iidrol"></param>
        /// <returns></returns>
        public JsonResult FiltrarPersonaRol(int iidrol)
        {
            PersonaBL oPersonaBL = new PersonaBL();

            return Json(oPersonaBL.FiltrarPersonaRol(iidrol), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Recuperar persona
        /// </summary>
        /// <param name="iidpersona"></param>
        /// <returns></returns>
        public JsonResult recuperarPersona(int iidpersona)
        {
            PersonaBL oPersonaBL = new PersonaBL();
            return Json(oPersonaBL.recuperarPersona(iidpersona), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Guardar persona
        /// </summary>
        /// <param name="oPersona"></param>
        /// <returns></returns>
        public int Guardar(PersonaCLS oPersona, UsuarioCLS oUsuarioCLS)
        {          
            //LLamamos a la capa de negocio
            PersonaBL obj = new PersonaBL();
            return obj.GuardarPersona(oPersona, oUsuarioCLS);
        }
    }
}