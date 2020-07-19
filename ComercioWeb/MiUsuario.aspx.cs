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
    public partial class MiUsuario : System.Web.UI.Page
    {
        public Usuario Usuario { get; set; }
        public Dominio.Carrito Carrito { get; set; }
        public bool HayUsuarioActivo { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            HayUsuarioActivo = ExisteUsuario();
            VerificarUsuario();
            VerificarCarrito();
            Compras();
        }
        public bool ExisteUsuario()
        {
            Usuario = new Usuario();
            if (Session["Usuario" + Session.SessionID] != null)
            {
                Usuario = (Usuario)Session["Usuario" + Session.SessionID];
                return true;
            }
            return false;
        }
        public void VerificarCarrito()
        {
            if (Session["Carrito" + Session.SessionID] != null)
                Carrito = (Dominio.Carrito)Session["Carrito" + Session.SessionID];
            else
                Carrito = new Dominio.Carrito();
        }
        public void VerificarUsuario()
        {
            if (Session["Usuario" + Session.SessionID] != null)
                Usuario = (Usuario)Session["Usuario" + Session.SessionID];
            else
                Response.Redirect("IniciarSesion.aspx");
        }
        public void Compras()
        {
            if (Session["Compras" + Session.SessionID] == null)
            {
                NegocioDatos negocio = new NegocioDatos();
                List<Transaccion> lista = negocio.ListarCompras((Usuario)Session["Usuario" + Session.SessionID]);
                Session["Compras" + Session.SessionID] = lista;
                rptCompras.DataSource = (List<Transaccion>)Session["Compras" + Session.SessionID];
                rptCompras.DataBind();
            }
            else
            {
                rptCompras.DataSource = (List<Transaccion>)Session["Compras" + Session.SessionID];
                rptCompras.DataBind();
            }
        }
        public void Compras(bool Forzar)
        {
            if (Forzar)
            {
                NegocioDatos negocio = new NegocioDatos();
                List<Transaccion> lista = negocio.ListarCompras((Usuario)Session["Usuario" + Session.SessionID]);
                Session["Compras" + Session.SessionID] = lista;
                rptCompras.DataSource = (List<Transaccion>)Session["Compras" + Session.SessionID];
                rptCompras.DataBind();
            }
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            Compras(true);
            btnActualizar.Visible = false;
        }
    }
}