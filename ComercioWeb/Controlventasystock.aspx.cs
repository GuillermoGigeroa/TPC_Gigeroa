using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Microsoft.Win32;
using Negocio;

namespace ComercioWeb
{
    public partial class Controlventasystock : System.Web.UI.Page
    {
        public Usuario Usuario { get; set; }
        public Dominio.Carrito Carrito { get; set; }
        public bool HayUsuarioActivo { get; set; }
        public List<Transaccion> ListaTransacciones { get; set; }
        public bool ModoAdmin { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            HayUsuarioActivo = ExisteUsuario();
            CheckAdmin();
            VerificarVendedor();
            VerificarCarrito();
            CargarVentas();
            CargarStock();
            DetectarCambioEstado();
            DetectarCambioStock();
        }
        public void CheckAdmin()
        {
            string admin = Request.QueryString["admin"];
            if (admin != null)
                ModoAdmin = true;
        }
        public void DetectarCambioEstado()
        {
            try
            {
                string factura = Request.QueryString["fact"];
                string estado = Request.QueryString["estado"];
                if (factura != null)
                {

                    List<Transaccion> lista = (List<Transaccion>)Session["Ventas" + Session.SessionID];
                    bool Encontrado;
                    int Codigo;
                    if (estado != null)
                    {
                        Encontrado = true;
                        Codigo = Convert.ToInt32(estado);
                    }
                    else
                    {
                        Encontrado = false;
                        Codigo = 1;
                    }
                    foreach (Transaccion t in lista)
                    {
                        if (t.NumeroFactura == Convert.ToInt32(factura))
                        {
                            if (!Encontrado)
                            {
                                if (t.Estado.Codigo < 4)
                                    t.Estado.Codigo++;
                                else
                                    t.Estado.Codigo = 1;
                                Encontrado = true;
                                Codigo = t.Estado.Codigo;
                            }
                            else
                            {
                                t.Estado.Codigo = Codigo;
                            }
                            switch (t.Estado.Codigo)
                            {
                                case 1:
                                    {
                                        t.Estado.Descripcion = "Pendiente";
                                    }
                                    break;
                                case 2:
                                    {
                                        t.Estado.Descripcion = "En camino";
                                    }
                                    break;
                                case 3:
                                    {
                                        t.Estado.Descripcion = "Entregado";
                                    }
                                    break;
                                case 4:
                                    {
                                        t.Estado.Descripcion = "Error";
                                    }
                                    break;
                                default:
                                    {
                                        t.Estado.Descripcion = "DescripcionEstado";
                                    }
                                    break;
                            }
                        }
                    }
                    Session["Ventas" + Session.SessionID] = lista;
                    CargarVentasVirtual();
                    Response.Redirect("Controlventasystock.aspx");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DetectarCambioStock()
        {
            try
            {
                string cantidad = Request.QueryString["cant"];
                string IDArticulo = Request.QueryString["idart"];
                if (cantidad != null && IDArticulo != null)
                {
                    List<Articulo> listaArticulos = (List<Articulo>)Session["Stock" + Session.SessionID];
                    foreach (Articulo a in listaArticulos)
                    {
                        if (a.ID_Articulo == Convert.ToInt32(IDArticulo))
                        {
                            if (a.Stock + Convert.ToInt32(cantidad) >= 0)
                                a.Stock += Convert.ToInt32(cantidad);
                            else
                                Response.Redirect("Controlventasystock.aspx?error=stockCantidad");
                            break;
                        }
                    }
                    Session["Stock" + Session.SessionID] = listaArticulos;
                    CargarStockVirtual();
                    Response.Redirect("Controlventasystock.aspx");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargarVentas()
        {
            if (Session["Ventas" + Session.SessionID] == null)
            {
                NegocioDatos negocio = new NegocioDatos();
                List<Transaccion> lista;
                if (ModoAdmin)
                    lista = negocio.ListarVentasAdmin();
                else
                    lista = negocio.ListarVentas();
                Session["Ventas" + Session.SessionID] = lista;
                rptVentas.DataSource = (List<Transaccion>)Session["Ventas" + Session.SessionID];
                rptVentas.DataBind();
                CargarListaFacturas();
            }
            else
            {
                rptVentas.DataSource = (List<Transaccion>)Session["Ventas" + Session.SessionID];
                rptVentas.DataBind();
                CargarListaFacturas();
            }
        }
        public void CargarVentas(bool Forzar)
        {
            if (Forzar)
            {
                NegocioDatos negocio = new NegocioDatos();
                List<Transaccion> lista;
                if (ModoAdmin)
                    lista = negocio.ListarVentasAdmin();
                else
                    lista = negocio.ListarVentas();
                Session["Ventas" + Session.SessionID] = lista;
                rptVentas.DataSource = (List<Transaccion>)Session["Ventas" + Session.SessionID];
                rptVentas.DataBind();
                CargarListaFacturas(Forzar);
            }
        }
        public void CargarVentasVirtual()
        {
            rptVentas.DataSource = (List<Transaccion>)Session["Ventas" + Session.SessionID];
            rptVentas.DataBind();
            CargarListaFacturas();
        }
        public void CargarStock()
        {
            if (Session["Stock" + Session.SessionID] == null)
            {
                NegocioDatos negocio = new NegocioDatos();
                Session["Stock" + Session.SessionID] = negocio.ListarArticulosAdmin();
                rptStock.DataSource = (List<Articulo>)Session["Stock" + Session.SessionID];
                rptStock.DataBind();
                CargarListaArticulos();
            }
            else
            {
                rptStock.DataSource = (List<Articulo>)Session["Stock" + Session.SessionID];
                rptStock.DataBind();
                CargarListaArticulos();
            }
        }
        public void CargarStock(bool Forzar)
        {
            if (Forzar)
            {
                NegocioDatos negocio = new NegocioDatos();
                Session["Stock" + Session.SessionID] = negocio.ListarArticulosAdmin();
                rptStock.DataSource = (List<Articulo>)Session["Stock" + Session.SessionID];
                rptStock.DataBind();
                CargarListaArticulos(Forzar);
            }
        }
        public void CargarStockVirtual()
        {
            rptStock.DataSource = (List<Articulo>)Session["Stock" + Session.SessionID];
            rptStock.DataBind();
            CargarListaArticulos();
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
            {
                Carrito = (Dominio.Carrito)Session["Carrito" + Session.SessionID];
            }
            else
            {
                Carrito = new Dominio.Carrito();
            }
        }
        public void VerificarVendedor()
        {
            if (Session["Usuario" + Session.SessionID] != null)
            {
                Usuario = (Usuario)Session["Usuario" + Session.SessionID];
                if (Usuario.TipoUsuario.ID_Tipo > 2)
                {
                    Response.Redirect("Usuarios.aspx");
                }
            }
            else
                Response.Redirect("IniciarSesion.aspx");
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarVentas(true);
            CargarStock(true);
            btnActualizar.Visible = false;
        }
        public string Estado(bool estado)
        {
            if (estado)
                return "Activo";
            return "Inactivo";
        }
        protected void btnGuardarVentas_Click(object sender, EventArgs e)
        {
            NegocioABM negocio = new NegocioABM();
            negocio.ActualizarEstadosVentas((List<Transaccion>)Session["Ventas" + Session.SessionID]);
            CargarVentas(true);
        }
        protected void btnGuardarStock_Click(object sender, EventArgs e)
        {
            NegocioABM negocio = new NegocioABM();
            negocio.ActualizarStockArticulo((List<Articulo>)Session["Stock" + Session.SessionID]);
            CargarStock(true);
        }
        protected void btnModificarFactura_Click(object sender, EventArgs e)
        {
            string cadena = "Controlventasystock.aspx?";
            cadena += "fact=" + drpFacturas.SelectedItem.Value;
            cadena += "&estado=" + drpEstados.SelectedItem.Value;
            Response.Redirect(cadena);
        }
        public void CargarListaFacturas()
        {
            if (!IsPostBack)
            {
                int ultimo = 0;
                List<Transaccion> facturas = new List<Transaccion>();
                foreach (Transaccion t in (List<Transaccion>)Session["Ventas" + Session.SessionID])
                {
                    if (ultimo != t.NumeroFactura)
                    {
                        ultimo = t.NumeroFactura;
                        facturas.Add(t);
                    }
                }
                drpFacturas.DataSource = facturas;
                drpFacturas.DataTextField = "NumeroFactura";
                drpFacturas.DataValueField = "NumeroFactura";
                drpFacturas.SelectedIndex = 0;
                drpFacturas.DataBind();
                ActualizarEstado();
            }
        }
        public void CargarListaFacturas(bool Forzar)
        {
            if (Forzar)
            {
                int ultimo = 0;
                List<Transaccion> facturas = new List<Transaccion>();
                foreach (Transaccion t in (List<Transaccion>)Session["Ventas" + Session.SessionID])
                {
                    if (ultimo != t.NumeroFactura)
                    {
                        ultimo = t.NumeroFactura;
                        facturas.Add(t);
                    }
                }
                drpFacturas.DataSource = facturas;
                drpFacturas.DataTextField = "NumeroFactura";
                drpFacturas.DataValueField = "NumeroFactura";
                drpFacturas.SelectedIndex = 0;
                drpFacturas.DataBind();
                ActualizarEstado();
            }
        }
        public void CargarListaArticulos()
        {
            if (!IsPostBack)
            {
                drpArticulos.DataSource = (List<Articulo>)Session["Stock" + Session.SessionID];
                drpArticulos.DataTextField = "ID_Articulo";
                drpArticulos.DataValueField = "ID_Articulo";
                drpArticulos.SelectedIndex = 0;
                drpArticulos.DataBind();
            }
        }
        public void CargarListaArticulos(bool Forzar)
        {
            if (Forzar)
            {
                drpArticulos.DataSource = (List<Articulo>)Session["Stock" + Session.SessionID];
                drpArticulos.DataTextField = "ID_Articulo";
                drpArticulos.DataValueField = "ID_Articulo";
                drpArticulos.SelectedIndex = 0;
                drpArticulos.DataBind();
            }
        }
        protected void btnModificarStock_Click(object sender, EventArgs e)
        {
            int numero = Convert.ToInt32(txtCantidad.Text);
            string cadena = "Controlventasystock.aspx?";
            if (numero >= 1 && numero <= 100)
            {
                cadena += "idart=" + drpArticulos.SelectedItem.Value;
                cadena += "&cant=" + numero;
                Response.Redirect(cadena);
            }
        }
        protected void drpFacturas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarEstado();
        }
        public void ActualizarEstado()
        {
            int factura = Convert.ToInt32(drpFacturas.SelectedItem.Value);
            foreach (Transaccion t in (List<Transaccion>)Session["Ventas" + Session.SessionID])
            {
                if (factura == t.NumeroFactura)
                {
                    drpEstados.SelectedIndex = t.Estado.Codigo - 1;
                }
            }
        }
    }
}