using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ComercioWeb
{
    public partial class RegistrarUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NegocioDatos negocio = new NegocioDatos();
            ListaProvincias.DataSource = negocio.ListarProvincias();
            ListaProvincias.DataBind();
        }
    }
}