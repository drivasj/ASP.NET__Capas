using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class PersonaCLS
    {
        public int iidpersona { get; set; }
        public int iidp { get; set; }
        public int iidrol { get; set; }
        public string nombreCompleto { get; set; }
        public string nombreSexo { get; set; }
        public string nombreRol { get; set; }
        //Prpiedades adicionales
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string telefono { get; set; }
        public int iidsexo { get; set; }

        // Propiedades foto
        public string nombrefoto { get; set; }
        public byte[] foto { get; set; }
        public string fotobase64 { get; set; }
        public List<int> valor { get; set; } // CHECKBOX

        //Usuario login
        public int iidususario { get; set; }
        public string nombreusuario { get; set; }

        public int bhabilitado { get; set; }
    }
}
