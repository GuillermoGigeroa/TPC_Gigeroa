﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace ComercioWeb
{
    public partial class Administrador : System.Web.UI.Page
    {
        public Usuario Usuario { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarAdmin();
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