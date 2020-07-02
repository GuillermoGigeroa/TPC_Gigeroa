﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace ComercioWeb
{
    public partial class Catalogo : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulos { get; set; }
        public List<Marca> ListaMarcas { get; set; }
        public List<Categoria> ListaCategorias { get; set; }
        public NegocioDatos Negocio { get; set; }
        public Usuario Usuario { get; set; }
        public bool HayUsuarioActivo { get; set; }
        public Dominio.Carrito Carrito { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            Carrito = new Dominio.Carrito();
            Negocio = new NegocioDatos();
            HayUsuarioActivo = ExisteUsuario();
            CargarArticulos(Negocio);
            CargarMarcas(Negocio);
            CargarCategorias(Negocio);
            AnalizarFiltros();
            CargarAlCarrito();
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
        public void AnalizarFiltros()
        {
            string filtroCategoria = Request.QueryString["fcat"];
            if (filtroCategoria != null)
                FiltrarPorCategoria(filtroCategoria);
            string filtroMarca = Request.QueryString["fmar"];
            if (filtroMarca != null)
                FiltrarPorMarca(filtroMarca);
        }
        public void CargarAlCarrito()
        {
            try
            {
                if (Session["Carrito" + Session.SessionID] != null)
                {
                    Carrito = (Dominio.Carrito)Session["Carrito" + Session.SessionID];
                }
                string ID_Articulo = Request.QueryString["idArt"];
                string Cantidad = Request.QueryString["cant"];
                if (ID_Articulo != null)
                {
                    foreach (Articulo articulo in ListaArticulos)
                    {
                        if (articulo.ID_Articulo == Convert.ToInt32(ID_Articulo))
                        {
                            if (Cantidad != null)
                                Carrito.AgregarArticulo(articulo, Convert.ToInt32(Cantidad));
                            else
                                Carrito.AgregarArticulo(articulo, 1);
                            break;
                        }
                    }
                }
                Session["Carrito" + Session.SessionID] = Carrito;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarArticulos(NegocioDatos Negocio)
        {
            try
            { //Hacete el traspaso a Session, porque saturas las conexiones a BBDD
                ListaArticulos = Negocio.ListarArticulos();
                rptListaArticulos.DataSource = ListaArticulos;
                rptListaArticulos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarMarcas(NegocioDatos Negocio)
        {
            try
            { //Hacete el traspaso a Session, porque saturas las conexiones a BBDD
                ListaMarcas = Negocio.ListarMarcas();
                rptListaMarcas.DataSource = ListaMarcas;
                rptListaMarcas.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarCategorias(NegocioDatos Negocio)
        {
            try
            { //Hacete el traspaso a Session, porque saturas las conexiones a BBDD
                ListaCategorias = Negocio.ListarCategorias();
                rptListaCategorias.DataSource = ListaCategorias;
                rptListaCategorias.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void FiltrarPorCategoria(string idCategoria)
        {
            try
            {
                List<Articulo> ListaFiltrada = new List<Articulo>();
                foreach (Articulo Articulo in ListaArticulos)
                {
                    foreach (Categoria Categoria in Articulo.Categorias)
                    {
                        if (Categoria.ID_Categoria == Convert.ToInt32(idCategoria))
                        {
                            ListaFiltrada.Add(Articulo);
                        }
                    }
                }
                rptListaArticulos.DataSource = ListaFiltrada;
                rptListaArticulos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void FiltrarPorMarca(string idMarca)
        {
            try
            {
                List<Articulo> ListaFiltrada = new List<Articulo>();
                ListaFiltrada = ListaArticulos.FindAll(Articulo => Articulo.MarcaArticulo.ID_Marca == Convert.ToInt32(idMarca));
                rptListaArticulos.DataSource = ListaFiltrada;
                rptListaArticulos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<Articulo> listaFiltrada;
                if (txtBuscar.Text.Trim() == "")
                    listaFiltrada = ListaArticulos;
                else
                    listaFiltrada = ListaArticulos.FindAll(articulo => articulo.Nombre.ToLower().Contains(txtBuscar.Text.Trim().ToLower()));
                rptListaArticulos.DataSource = listaFiltrada;
                rptListaArticulos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}