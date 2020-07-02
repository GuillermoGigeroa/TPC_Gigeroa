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
    public partial class Administrador : System.Web.UI.Page
    {
        public Usuario Usuario { get; set; }
        public Dominio.Carrito Carrito { get; set; }
        public bool HayUsuarioActivo { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            HayUsuarioActivo = ExisteUsuario();
            //VerificarAdmin();
            VerificarCarrito();
            if(!IsPostBack)
            {
                ListarUsuarios();
            }
        }
        public void ListarUsuarios()
        {
            NegocioDatos negocio = new NegocioDatos();
            ListaUsuarios.DataSource = negocio.ListarUsuarios();

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
        public void VerificarAdmin()
        {
            if (Session["Usuario" + Session.SessionID] != null)
            {
                Usuario = (Usuario)Session["Usuario" + Session.SessionID];
                if(Usuario.TipoUsuario.ID_Tipo != 1)
                {
                    Response.Redirect("Usuarios.aspx");
                }
            }
            else
                Response.Redirect("IniciarSesion.aspx");
        }
    }
}