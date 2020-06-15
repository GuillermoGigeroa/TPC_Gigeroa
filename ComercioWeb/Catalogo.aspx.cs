using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace ComercioWeb
{
    public partial class Catalogo : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulos;
        protected void Page_Load(object sender, EventArgs e)
        {
            NegocioDatos Negocio = new NegocioDatos();
            ListaArticulos = Negocio.ListarArticulos();
            repetidor.DataSource = ListaArticulos;
            repetidor.DataBind();
        }
    }
}