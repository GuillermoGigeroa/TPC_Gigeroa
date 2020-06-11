using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        public int ID_Articulo { get; set; }
        public string Nombre { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }
        public string Descripcion { get; set; }
        public bool EsMateriaPrima { get; set; }
        public string URL_Imagen { get; set; }
        public bool Estado { get; set; }
        public double Precio { get; set; }
    }
}
