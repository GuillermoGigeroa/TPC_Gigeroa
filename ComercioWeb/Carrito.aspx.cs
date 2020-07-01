using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace ComercioWeb
{
    public partial class Carrito : System.Web.UI.Page
    {
        public Usuario Usuario { get; set; }
        public bool HayUsuarioActivo { get; set; }
        public Dominio.Carrito MiCarrito { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            HayUsuarioActivo = ExisteUsuario();
            VerificarCarrito();
            VerificarEliminaciones();
            rptListaArticulosEnCarrito.DataSource = MiCarrito.ListaElementos;
            rptListaArticulosEnCarrito.DataBind();
        }
        public void VerificarCarrito()
        {
            if (Session["Carrito" + Session.SessionID] != null)
                MiCarrito = (Dominio.Carrito)Session["Carrito" + Session.SessionID];
            else
                MiCarrito = new Dominio.Carrito();
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
        public void VerificarEliminaciones()
        {
            string eliminar = Request.QueryString["eliminar"];
            if (eliminar != null)
            {
                List<ElementoCarrito> lista = new List<ElementoCarrito>();
                if(eliminar != "todos")
                {
                    if(EsNumero(eliminar))
                    {
                        foreach(ElementoCarrito elemento in MiCarrito.ListaElementos)
                        {
                            if(elemento.ID_Elemento != Convert.ToInt32(eliminar))
                                lista.Add(elemento);
                        }
                    }
                }
                MiCarrito.ListaElementos = lista;
            }
            Session["Carrito" + Session.SessionID] = MiCarrito;
        }
        public bool EsNumero(string esto)
        {
            foreach (char caracter in esto)
            {
                if (caracter < 48 || caracter > 57)
                {
                    return false;
                }
            }
            return true;
        }
    }
}