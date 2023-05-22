using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class RolCLS
    {
        public int iidrol { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }

        // Recuperar las paginas de cada tipo de usuario
        public List<int> idpaginas { get; set; }

        // Recuperar los menus de cada tipo de usuario
        public List<int> idpmenus { get; set; }

        public List<string> nameP { get; set; }

        public List<PaginaCLS> listaPaginas { get; set; }
    }
}
