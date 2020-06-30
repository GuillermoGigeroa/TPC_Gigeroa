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
            HayUsuarioActivo = false;
            Usuario = new Usuario();
            if (Session["Usuario" + Session.SessionID] != null)
            {
                HayUsuarioActivo = true;
                Usuario = (Usuario)Session["Usuario" + Session.SessionID];
            }
            if (Session["Carrito" + Session.SessionID] != null)
                MiCarrito = (Dominio.Carrito)Session["Carrito" + Session.SessionID];
            else
                MiCarrito = new Dominio.Carrito();
            VerificarEliminaciones();
            rptListaArticulosEnCarrito.DataSource = MiCarrito.ListaElementos;
            rptListaArticulosEnCarrito.DataBind();
        }
        public void VerificarEliminaciones()
        {
            string eliminar = Request.QueryString["eliminar"];
            if (eliminar != null)
            {
                List<ElementoCarrito> lista = new List<ElementoCarrito>();
                if(eliminar != "todos")
                {
                    foreach(ElementoCarrito elemento in MiCarrito.ListaElementos)
                    {
                        if(elemento.ID_Elemento != Convert.ToInt32(eliminar))
                            lista.Add(elemento);
                    }
                }
                MiCarrito.ListaElementos = lista;
            }
            Session["Carrito" + Session.SessionID] = MiCarrito;
        }
    }
}