using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace ComercioWeb
{
    public partial class Factura : System.Web.UI.Page
    {
        public Usuario Usuario { get; set; }
        public bool HayUsuarioActivo { get; set; }
        public int NumeroFactura { get; set; }
        public string FechaTransaccion { get; set; }
        public double PrecioTotal { get; set; }
        public string NombreCliente { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            HayUsuarioActivo = ExisteUsuario();
            if (!HayUsuarioActivo)
                Response.Redirect("IniciarSesion.aspx");
            if (Request.QueryString["factura"] != null)
                CargarDatos(Request.QueryString["factura"]);
        }
        public void CargarDatos(string numeroFactura)
        {
            try
            {
                int numero = Convert.ToInt32(numeroFactura);
                Transaccion estaTransaccion = new Transaccion();
                List<Transaccion> lista = new List<Transaccion>();
                List<Transaccion> listaEncontrada = new List<Transaccion>();
                List<Articulo> listaArticulos = new List<Articulo>();
                NegocioDatos negocio = new NegocioDatos();
                listaArticulos = negocio.ListarArticulosAdmin();
                lista = negocio.ListarVentasAdmin();
                foreach (Transaccion transaccion in lista)
                {
                    if (transaccion.NumeroFactura == numero)
                    {
                        estaTransaccion = transaccion;
                        foreach(Articulo articulo in listaArticulos)
                        {
                            if(articulo.ID_Articulo == transaccion.Articulo.Articulo.ID_Articulo)
                            {
                                estaTransaccion.Articulo.Articulo.Precio = articulo.Precio;
                                break;
                            }
                        }
                        listaEncontrada.Add(estaTransaccion);
                    }
                }
                if(Usuario.TipoUsuario.ID_Tipo > 2)
                {
                    if(Usuario.ID_Usuario != listaEncontrada[0].ID_Usuario)
                    {
                        Response.Redirect("MiUsuario.aspx");
                    }
                }
                if(listaEncontrada.Count() > 0)
                {
                    NumeroFactura = numero;
                    FechaTransaccion = listaEncontrada[0].FechaAccion;
                    NombreCliente = listaEncontrada[0].Nombres + " " + listaEncontrada[0].Apellidos;
                    PrecioTotal = Total(listaEncontrada);
                    rptListaEncontrada.DataSource = listaEncontrada;
                    rptListaEncontrada.DataBind();
                }
                else
                {
                    Response.Redirect("MiUsuario.aspx");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public double Total(List<Transaccion> lista)
        {
            double resultado = 0;
            foreach(Transaccion transaccion in lista)
            {
                resultado += (transaccion.Articulo.Cantidad) * (transaccion.Articulo.Articulo.Precio);
            }
            return resultado;
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
    }
}