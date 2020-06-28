using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace ComercioWeb
{
    public partial class Usuarios : System.Web.UI.Page
    {
        public Usuario Usuario { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarUsuario();
        }
        public void VerificarUsuario()
        {
            if (Session["Usuario" + Session.SessionID] != null)
                Usuario = (Usuario)Session["Usuario" + Session.SessionID];
            else
                Response.Redirect("IniciarSesion.aspx");
        }
    }
}