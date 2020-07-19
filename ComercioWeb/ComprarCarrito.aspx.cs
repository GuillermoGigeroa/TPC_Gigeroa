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
    public partial class ComprarCarrito : System.Web.UI.Page
    {
        public Usuario Usuario { get; set; }
        public bool HayUsuarioActivo { get; set; }
        public Dominio.Carrito MiCarrito { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            HayUsuarioActivo = ExisteUsuario();
            if (!HayUsuarioActivo)
                Response.Redirect("IniciarSesion.aspx?login=carrito");
            VerificarCarrito();
        }
        public void VerificarCarrito()
        {
            if (Session["Carrito" + Session.SessionID] != null)
                MiCarrito = (Dominio.Carrito)Session["Carrito" + Session.SessionID];
            else
                MiCarrito = new Dominio.Carrito();
            if (MiCarrito.ListaElementos.Count == 0)
                Response.Redirect("Carrito.aspx");
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
        private void ConfirmarCompra()
        {
            if (MiCarrito.ListaElementos.Count != 0)
            {
                NegocioDatos negocio = new NegocioDatos();
                Session["NumeroFactura" + Session.SessionID] = negocio.CrearFactura();
                negocio.AgregarVenta(Convert.ToInt32(Session["NumeroFactura" + Session.SessionID]), Convert.ToInt32(((Usuario)Session["Usuario" + Session.SessionID]).ID_Usuario));
                foreach (ElementoCarrito elemento in MiCarrito.ListaElementos)
                {
                    negocio.AgregarVentaAXV(elemento);
                    negocio.ComprarArticulo(elemento);
                }
                Response.Redirect("GraciasPorSuCompra.aspx?compra=true");
            }
        }
        protected void btnComprar_Click(object sender, EventArgs e)
        {
            ConfirmarCompra();
        }
    }
}