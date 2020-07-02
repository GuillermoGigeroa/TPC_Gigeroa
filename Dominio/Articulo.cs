using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
    public class Articulo
    {
        public Articulo()
        {
            MarcaArticulo = new Marca();
            Categorias = new List<Categoria>();
        }
        public int ID_Articulo { get; set; }
        public string Nombre { get; set; }
        public Marca MarcaArticulo { get; set; }
        public List<Categoria> Categorias { get; set; }
        public string Descripcion { get; set; }
        public bool EsMateriaPrima { get; set; }
        public string URL_Imagen { get; set; }
        public bool Estado { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
    }
}