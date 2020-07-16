using System;
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
            HayUsuarioActivo = ExisteUsuario();
            Negocio = new NegocioDatos();
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
                            {
                                if(ConteoTotal(articulo,Carrito))
                                {
                                    Carrito.AgregarArticulo(articulo, Convert.ToInt32(Cantidad));
                                    lblError.Visible = false;
                                }
                                else
                                {
                                    lblError.Visible = true;
                                }
                            }
                            else
                            {
                                if (ConteoTotal(articulo, Carrito))
                                {
                                    Carrito.AgregarArticulo(articulo, 1);
                                    lblError.Visible = false;
                                }
                                else
                                {
                                    lblError.Visible = true;
                                }
                            }
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
        public bool ConteoTotal(Articulo articulo, Dominio.Carrito MiCarrito)
        {
            int conteoStock = articulo.Stock;
            foreach(ElementoCarrito elemento in MiCarrito.ListaElementos)
            {
                if(elemento.Articulo.ID_Articulo == articulo.ID_Articulo)
                {
                    conteoStock -= elemento.Cantidad;
                }
            }
            if(conteoStock <= 0)
            {
                return false;
            }
            return true;
        }
        private void CargarArticulos(NegocioDatos Negocio)
        {
            try
            {
                if(Session["ListaArticulosCatalogo" + Session.SessionID] == null)
                {
                    ListaArticulos = Negocio.ListarArticulos();
                    Session["ListaArticulosCatalogo" + Session.SessionID] = ListaArticulos;
                    rptListaArticulos.DataSource = ListaArticulos;
                    rptListaArticulos.DataBind();
                }
                else
                {
                    ListaArticulos = (List<Articulo>)Session["ListaArticulosCatalogo" + Session.SessionID];
                    rptListaArticulos.DataSource = ListaArticulos;
                    rptListaArticulos.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarArticulos(NegocioDatos Negocio, bool Forzar)
        {
            try
            {
                if (Forzar)
                {
                    ListaArticulos = Negocio.ListarArticulos();
                    Session["ListaArticulosCatalogo" + Session.SessionID] = ListaArticulos;
                    rptListaArticulos.DataSource = ListaArticulos;
                    rptListaArticulos.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarMarcas(NegocioDatos Negocio)
        {
            try
            {
                if (Session["ListaMarcasCatalogo" + Session.SessionID] == null)
                {
                    ListaMarcas = Negocio.ListarMarcas();
                    Session["ListaMarcasCatalogo" + Session.SessionID] = ListaMarcas;
                    rptListaMarcas.DataSource = ListaMarcas;
                    rptListaMarcas.DataBind();
                }
                else
                {
                    ListaMarcas = (List<Marca>)Session["ListaMarcasCatalogo" + Session.SessionID];
                    rptListaMarcas.DataSource = ListaMarcas;
                    rptListaMarcas.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarMarcas(NegocioDatos Negocio, bool Forzar)
        {
            try
            {
                if (Forzar)
                {
                    ListaMarcas = Negocio.ListarMarcas();
                    Session["ListaMarcasCatalogo" + Session.SessionID] = ListaMarcas;
                    rptListaMarcas.DataSource = ListaArticulos;
                    rptListaMarcas.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarCategorias(NegocioDatos Negocio)
        {
            try
            {
                if (Session["ListaCategoriasCatalogo" + Session.SessionID] == null)
                {
                    ListaCategorias = Negocio.ListarCategorias();
                    Session["ListaCategoriasCatalogo" + Session.SessionID] = ListaCategorias;
                    rptListaCategorias.DataSource = ListaCategorias;
                    rptListaCategorias.DataBind();
                }
                else
                {
                    ListaCategorias = (List<Categoria>)Session["ListaCategoriasCatalogo" + Session.SessionID];
                    rptListaCategorias.DataSource = ListaCategorias;
                    rptListaCategorias.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarCategorias(NegocioDatos Negocio, bool Forzar)
        {
            try
            {
                if (Forzar)
                {
                    ListaCategorias = Negocio.ListarCategorias();
                    Session["ListaCategoriasCatalogo" + Session.SessionID] = ListaCategorias;
                    rptListaCategorias.DataSource = ListaCategorias;
                    rptListaCategorias.DataBind();
                }
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