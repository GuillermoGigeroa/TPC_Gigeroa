using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            Negocio = new NegocioDatos();
            CargarArticulos(Negocio);
            CargarMarcas(Negocio);
            CargarCategorias(Negocio);
            string filtroCategoria = Request.QueryString["fcat"];
            if (filtroCategoria != null)
                FiltrarPorCategoria(filtroCategoria);
            string filtroMarca = Request.QueryString["fmar"];
            if (filtroMarca != null)
                FiltrarPorMarca(filtroMarca);
        }
        private void CargarArticulos(NegocioDatos Negocio)
        {
            ListaArticulos = Negocio.ListarArticulos();
            rptListaArticulos.DataSource = ListaArticulos;
            rptListaArticulos.DataBind();
        }
        private void CargarMarcas(NegocioDatos Negocio)
        {
            ListaMarcas = Negocio.ListarMarcas();
            rptListaMarcas.DataSource = ListaMarcas;
            rptListaMarcas.DataBind();
        }
        private void CargarCategorias(NegocioDatos Negocio)
        {
            ListaCategorias = Negocio.ListarCategorias();
            rptListaCategorias.DataSource = ListaCategorias;
            rptListaCategorias.DataBind();
        }
        protected void FiltrarPorCategoria(string idCategoria)
        {
            try
            {
                List<Articulo> ListaFiltrada = new List<Articulo>();
                foreach (Articulo Articulo in ListaArticulos)
                {
                    foreach(Categoria Categoria in Articulo.Categorias)
                    {
                        if(Categoria.ID_Categoria == Convert.ToInt32(idCategoria))
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