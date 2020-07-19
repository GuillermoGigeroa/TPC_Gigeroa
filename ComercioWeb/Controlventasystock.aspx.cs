﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace ComercioWeb
{
    public partial class Controlventasystock : System.Web.UI.Page
    {
        public Usuario Usuario { get; set; }
        public Dominio.Carrito Carrito { get; set; }
        public bool HayUsuarioActivo { get; set; }
        public List<Transaccion> ListaTransacciones { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            HayUsuarioActivo = ExisteUsuario();
            VerificarVendedor();
            VerificarCarrito();
            CargarVentas();
            CargarStock();
        }
        public void CargarVentas()
        {
            if(Session["Ventas"+Session.SessionID] == null)
            {
                NegocioDatos negocio = new NegocioDatos();
                List<Transaccion> lista = negocio.ListarVentas();
                Session["Ventas" + Session.SessionID] = lista;
                rptVentas.DataSource = (List<Transaccion>)Session["Ventas" + Session.SessionID];
                rptVentas.DataBind();
            }
            else
            {
                rptVentas.DataSource = (List<Transaccion>)Session["Ventas" + Session.SessionID];
                rptVentas.DataBind();
            }
        }
        public void CargarVentas(bool Forzar)
        {
            if (Forzar)
            {
                NegocioDatos negocio = new NegocioDatos();
                List<Transaccion> lista = negocio.ListarVentas();
                Session["Ventas" + Session.SessionID] = lista;
                rptVentas.DataSource = (List<Transaccion>)Session["Ventas" + Session.SessionID];
                rptVentas.DataBind();
            }
        }
        public void CargarStock()
        {
            if (Session["Stock" + Session.SessionID] == null)
            {
                NegocioDatos negocio = new NegocioDatos();
                Session["Stock" + Session.SessionID] = negocio.ListarArticulosAdmin();
                dgvStock.DataSource = (List<Articulo>)Session["Stock" + Session.SessionID];
                dgvStock.DataBind();
            }
            else
            {
                dgvStock.DataSource = (List<Articulo>)Session["Stock" + Session.SessionID];
                dgvStock.DataBind();
            }
        }
        public void CargarStock(bool Forzar)
        {
            if (Forzar)
            {
                NegocioDatos negocio = new NegocioDatos();
                Session["Stock" + Session.SessionID] = negocio.ListarArticulosAdmin();
                dgvStock.DataSource = (List<Articulo>)Session["Stock" + Session.SessionID];
                dgvStock.DataBind();
            }
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
    }
}