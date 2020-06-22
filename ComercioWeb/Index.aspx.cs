﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace ComercioWeb
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NegocioDatos negocio = new NegocioDatos();
            rptListaImagenes.DataSource = negocio.ListarArticulos();
            rptListaImagenes.DataBind();
        }
    }
}