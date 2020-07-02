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
        public Dominio.Carrito Carrito { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            Encriptador = new Encriptador();
            NegocioDatos negocio = new NegocioDatos();
            ListaUsuarios = negocio.ListarUsuarios();
            VerificarCompra();
            VerificarLogin();
            VerificarCarrito();
        }
        public void VerificarCompra()
        {
            string comprar = Request.QueryString["comprar"];
            if (comprar != null)
            {
                if (comprar == "false")
                    lblErrorCompra.Visible = true;
                else
                    lblErrorCompra.Visible = false;
            }
        }
        public void VerificarUsuario()
        {
            if (Session["Usuario" + Session.SessionID] != null)
            {
                Response.Redirect("Usuarios.aspx");
            }
        }
        public void VerificarLogin()
        {
            string login = Request.QueryString["login"];
            if (login != null)
            {
                if (login == "0")
                    lblLogin.Visible = true;
                else
                    lblLogin.Visible = false;
            }
        }
        public void VerificarCarrito()
        {
            if (Session["Carrito" + Session.SessionID] != null)
            {
                Carrito = (Dominio.Carrito)Session["Carrito" + Session.SessionID];
            }
            else
            {
                Carrito = new Dominio.Carrito();
            }
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            foreach(Usuario usuario in ListaUsuarios)
            {
                if (usuario.Email.Trim().ToLower() == txtEmail.Text.Trim().ToLower() && Encriptador.Desencriptar(usuario.Password) == txtPassword.Text)
                {
                    Session["Usuario" + Session.SessionID] = usuario;
                    Response.Redirect("Usuarios.aspx");
                    break;
                }
            }
            Response.Redirect("IniciarSesion.aspx?login=0");
        }
    }
}