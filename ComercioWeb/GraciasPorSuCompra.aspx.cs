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
    public partial class GraciasPorSuCompra : System.Web.UI.Page
    {
        public Usuario Usuario { get; set; }
        public Dominio.Carrito Carrito { get; set; }
        public bool HayUsuarioActivo { get; set; }
        public int NumeroFactura { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            HayUsuarioActivo = ExisteUsuario();
            VerificarUsuario();
            VerificarCarrito();
            VerificarCompra();
            CargarArticulos();
        }
        private void CargarArticulos()
        {
            try
            {
                NegocioDatos Negocio = new NegocioDatos();
                Session["ListaArticulosCatalogo" + Session.SessionID] = Negocio.ListarArticulos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void VerificarCompra()
        {
            string compra = Request.QueryString["compra"];
            if (compra != null)
            {
                if (compra == "true")
                {
                    NumeroFactura = Convert.ToInt32(Session["NumeroFactura" + Session.SessionID]);
                    Session["Carrito" + Session.SessionID] = null;
                }
                else
                {
                    Response.Redirect("Carrito.aspx");
                }
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
    }
}