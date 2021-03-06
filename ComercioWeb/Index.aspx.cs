﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using System.Security.Cryptography;

namespace ComercioWeb
{
    public partial class Index : System.Web.UI.Page
    {
        public Usuario Usuario { get; set; }
        public bool HayUsuarioActivo { get; set; }
        public Dominio.Carrito Carrito { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            HayUsuarioActivo = ExisteUsuario();
            VerificarLogout();
            CargarImagenes();
            VerificarCarrito();
        }
        public void CargarImagenes()
        {
            if(Session["ListarArticulosImagen"+Session.SessionID] == null)
            {
                NegocioDatos negocio = new NegocioDatos();
                Session["ListarArticulosImagen" + Session.SessionID] = negocio.ListarArticulos();
                rptListaImagenes.DataSource = (List<Articulo>)Session["ListarArticulosImagen" + Session.SessionID];
                rptListaImagenes.DataBind();
            }
            else
            {
                rptListaImagenes.DataSource = (List<Articulo>)Session["ListarArticulosImagen" + Session.SessionID];
                rptListaImagenes.DataBind();
            }
        }
        public void VerificarLogout()
        {
            string Logout = Request.QueryString["logout"];
            if (Logout != null)
            {
                if (Logout == "true")
                {
                    Session["Usuario" + Session.SessionID] = null;
                    Response.Redirect("IniciarSesion.aspx");
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
            {
                Carrito = (Dominio.Carrito)Session["Carrito" + Session.SessionID];
            }
            else
            {
                Carrito = new Dominio.Carrito();
            }
        }
    }
}