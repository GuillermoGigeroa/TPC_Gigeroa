using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace ComercioWeb
{
    public partial class IniciarSesion : System.Web.UI.Page
    {
        public Encriptador Encriptador { get; set; }
        public List<Usuario> ListaUsuarios { get; set; }
        public string IngresoAprobado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Encriptador = new Encriptador();
            NegocioDatos negocio = new NegocioDatos();
            ListaUsuarios = negocio.ListarUsuarios();
            string login = Request.QueryString["login"];
            if (login != null)
            {
                if (login == "true")
                {
                    IngresoAprobado = "Si";
                }
                if (login == "false")
                {
                    IngresoAprobado = "No";
                }
            }
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            foreach(Usuario usuario in ListaUsuarios)
            {
                if (usuario.Email.Trim().ToLower() == txtEmail.Text.Trim().ToLower() && Encriptador.Desencriptar(usuario.Password) == txtPassword.Text)
                {
                    Response.Redirect("IniciarSesion.aspx?login=true");
                    break;
                }
            }
            Response.Redirect("IniciarSesion.aspx?login=false");
        }
    }
}