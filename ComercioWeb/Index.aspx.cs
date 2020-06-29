using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace ComercioWeb
{
    public partial class Index : System.Web.UI.Page
    {
        public Usuario Usuario { get; set; }
        public bool HayUsuarioActivo { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            HayUsuarioActivo = false;
            Usuario = new Usuario();
            NegocioDatos negocio = new NegocioDatos();
            rptListaImagenes.DataSource = negocio.ListarArticulos();
            rptListaImagenes.DataBind();
            string Logout = Request.QueryString["logout"];
            if (Logout != null)
            {
                if(Logout == "true")
                    Session["Usuario" + Session.SessionID] = null;
            }
            if (Session["Usuario"+Session.SessionID] != null)
            {
                HayUsuarioActivo = true;
                Usuario = (Usuario)Session["Usuario" + Session.SessionID];
            }
        }
    }
}