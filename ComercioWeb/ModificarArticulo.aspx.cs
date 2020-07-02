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
    public partial class ModificarArticulo : System.Web.UI.Page
    {
        public Usuario Usuario { get; set; }
        public Dominio.Carrito Carrito { get; set; }
        public bool HayUsuarioActivo { get; set; }
        public int Contador { get; set; }
        public Articulo Articulo { get; set; }
        public string URL { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            HayUsuarioActivo = ExisteUsuario();
            VerificarVendedor();
            VerificarCarrito();
            CargarArticulo();
            if (!IsPostBack)
            {
                ListarArticulos();
                ListarMarcas();
                ListarCategorias();
                CargarArticuloSeleccionado(ListaArticulos);
            }
        }
        public void ListarArticulos()
        {
            if (Session["ListaArticulos" + Session.SessionID] != null)
            {
                ListaArticulos.DataSource = (List<Articulo>)Session["ListaArticulos" + Session.SessionID];
                ListaArticulos.DataTextField = "Nombre";
                ListaArticulos.DataValueField = "ID_Articulo";
                ListaArticulos.DataBind();
            }
            else
            {
                NegocioDatos negocio = new NegocioDatos();
                Session["ListaArticulos" + Session.SessionID] = negocio.ListarArticulos();
                ListaArticulos.DataSource = (List<Articulo>)Session["ListaArticulos" + Session.SessionID];
                ListaArticulos.DataTextField = "Nombre";
                ListaArticulos.DataValueField = "ID_Articulo";
                ListaArticulos.DataBind();
            }
        }
        public void CargarArticulo()
        {
            if (Session["Articulo" + Session.SessionID] != null)
            {
                Articulo = (Articulo)Session["Articulo" + Session.SessionID];
            }
            else
            {
                Articulo = new Articulo();
                Articulo.Estado = true;
                Articulo.EsMateriaPrima = false;
            }
        }
        public void ListarMarcas()
        {
            try
            { //Hacete el traspaso a Session, porque saturas las conexiones a BBDD
                NegocioDatos negocio = new NegocioDatos();
                ListaMarcas.DataSource = negocio.ListarMarcas();
                ListaMarcas.DataTextField = "Nombre";
                ListaMarcas.DataValueField = "ID_Marca";
                ListaMarcas.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ListarCategorias()
        {
            try
            { //Hacete el traspaso a Session, porque saturas las conexiones a BBDD
                NegocioDatos negocio = new NegocioDatos();
                ListaCategorias.DataSource = negocio.ListarCategorias();
                ListaCategorias.DataTextField = "Nombre";
                ListaCategorias.DataValueField = "ID_Categoria";
                ListaCategorias.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
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
        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            Categoria categoria = new Categoria();
            categoria.ID_Categoria = Convert.ToInt32(ListaCategorias.SelectedItem.Value);
            categoria.Nombre = ListaCategorias.SelectedItem.Text;
            categoria.Activo = true;
            if (SePuedeAgregar(categoria))
            {
                lblAgregar.Visible = true;
                lblAgregarError.Visible = false;
                Articulo.Categorias.Add(categoria);
                Session["Articulo" + Session.SessionID] = Articulo;
                ListaAgregados.DataSource = Articulo.Categorias;
                ListaAgregados.DataTextField = "Nombre";
                ListaAgregados.DataValueField = "ID_Categoria";
                ListaAgregados.DataBind();
                lblErrorCategoria.Visible = false;
            }
            else
            {
                lblAgregar.Visible = false;
                lblAgregarError.Visible = true;
            }
        }
        public bool SePuedeAgregar(Categoria categoriaParaAnalizar)
        {
            try
            {
                foreach (Categoria categoria in Articulo.Categorias)
                {
                    if (categoria.ID_Categoria == categoriaParaAnalizar.ID_Categoria)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnEliminarCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                if (Articulo.Categorias.Count() > 1)
                {
                    List<Categoria> listaCategorias = new List<Categoria>();
                    foreach (Categoria categoria in Articulo.Categorias)
                    {
                        if (Convert.ToInt32(ListaAgregados.SelectedItem.Value) != categoria.ID_Categoria)
                            listaCategorias.Add(categoria);
                    }
                    Articulo.Categorias = listaCategorias;
                    lblEliminar.Visible = true;
                }
                else
                {
                    Articulo.Categorias = new List<Categoria>();
                    lblEliminar.Visible = true;
                }
                Session["Articulo" + Session.SessionID] = Articulo;
                ListaAgregados.DataSource = Articulo.Categorias;
                ListaAgregados.DataTextField = "Nombre";
                ListaAgregados.DataValueField = "ID_Categoria";
                ListaAgregados.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == "")
            {
                lblNombreError.Visible = true;
                return;
            }
            else
                lblNombreError.Visible = false;
            Articulo.Nombre = AcomodarTexto(txtNombre.Text.Trim());
            Session["Articulo" + Session.SessionID] = Articulo;
        }
        protected void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            if (txtDescripcion.Text.Trim() == "")
            {
                lblDescripcion.Visible = true;
                return;
            }
            else
                lblDescripcion.Visible = false;
            Articulo.Descripcion = AcomodarTexto(txtDescripcion.Text.Trim());
            Session["Articulo" + Session.SessionID] = Articulo;
        }
        public string AcomodarTexto(string texto)
        {
            string nuevoTexto = "";
            int contador = 0;
            foreach (char caracter in texto)
            {
                if (contador < 150)
                    nuevoTexto += Convert.ToChar(texto[contador]);
                else
                    break;
                contador++;
            }
            return nuevoTexto;
        }
        protected void ListaMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Articulo.MarcaArticulo.Activo = true;
                Articulo.MarcaArticulo.ID_Marca = Convert.ToInt32(ListaMarcas.SelectedItem.Value);
                Articulo.MarcaArticulo.Nombre = ListaMarcas.SelectedItem.Text;
                Session["Articulo" + Session.SessionID] = Articulo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ListaCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblAgregar.Visible = false;
            lblAgregarError.Visible = false;
            lblEliminar.Visible = false;
        }
        protected void ListaAgregados_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblAgregar.Visible = false;
            lblAgregarError.Visible = false;
            lblEliminar.Visible = false;
        }
        protected void txtURL_TextChanged(object sender, EventArgs e)
        {
            if (txtURL.Text.Trim() == "")
            {
                lblURL.Visible = true;
            }
            else
            {
                lblURL.Visible = false;
            }
        }
        protected void btnVistaPrevia_Click(object sender, EventArgs e)
        {
            CargarImagen();
        }
        public void CargarImagen()
        {
            if (txtURL.Text.Trim() == "")
            {
                lblURL.Visible = true;
            }
            else
            {
                lblURL.Visible = false;
                URL = txtURL.Text;
            }
        }
        protected void txtPrecioEntero_TextChanged(object sender, EventArgs e)
        {
            if (ContieneSoloNumeros(txtPrecioEntero) && ContieneSoloNumeros(txtPrecioDecimales))
                lblPrecio.Visible = false;
            else
                lblPrecio.Visible = true;
        }
        protected void txtPrecioDecimales_TextChanged(object sender, EventArgs e)
        {
            if (ContieneSoloNumeros(txtPrecioEntero) && ContieneSoloNumeros(txtPrecioDecimales))
            {
                lblPrecio.Visible = false;
                int decimales = Convert.ToInt32(txtPrecioDecimales.Text);
                if (decimales < 1000)
                {
                    if (decimales < 100)
                    {
                        if (decimales < 10)
                        {
                            decimales *= 100;
                        }
                        else
                        {
                            decimales *= 10;
                        }
                    }
                }
                txtPrecioDecimales.Text = Convert.ToString(decimales);
            }
            else
                lblPrecio.Visible = true;
        }
        public bool ContieneSoloNumeros(TextBox txtCaja)
        {
            foreach (char caracter in txtCaja.Text)
            {
                if (caracter < 48 || caracter > 57)
                {
                    return false;
                }
            }
            return true;
        }
        protected void ListaArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarArticuloSeleccionado(ListaArticulos);
        }
        public void CargarArticuloSeleccionado(DropDownList dropDown)
        {
            List<Articulo> lista = new List<Articulo>();
            Articulo articuloEncontrado = new Articulo();
            if (Session["ListaArticulos" + Session.SessionID] != null)
                lista = (List<Articulo>)Session["ListaArticulos" + Session.SessionID];
            else
            {
                NegocioDatos negocio = new NegocioDatos();
                Session["ListaArticulos" + Session.SessionID] = negocio.ListarArticulos();
                lista = (List<Articulo>)Session["ListaArticulos" + Session.SessionID];
            }
            bool SeEncontro = false;
            foreach(Articulo articulo in lista)
            {
                if(articulo.ID_Articulo == Convert.ToInt32(dropDown.SelectedItem.Value))
                {
                    articuloEncontrado = articulo;
                    SeEncontro = true;
                    break;
                }
            }
            if(SeEncontro)
            {
                Session["Articulo" + Session.SessionID] = articuloEncontrado;
                txtNombre.Text = articuloEncontrado.Nombre;
                txtDescripcion.Text = articuloEncontrado.Descripcion;
                txtPrecioEntero.Text = ObtenerNumeroEntero(articuloEncontrado.Precio);
                txtPrecioDecimales.Text = ObtenerNumeroDecimal(articuloEncontrado.Precio);
                ListaMarcas.SelectedIndex = articuloEncontrado.MarcaArticulo.ID_Marca - 1;
                ListaAgregados.DataSource = articuloEncontrado.Categorias;
                ListaAgregados.DataTextField = "Nombre";
                ListaAgregados.DataValueField = "ID_Categoria";
                ListaAgregados.DataBind();
                txtURL.Text = articuloEncontrado.URL_Imagen;
                CargarImagen();
            }
        }
        public string ObtenerNumeroEntero(double numero)
        {
            string numeroTexto = Convert.ToString(numero);
            string numeroFinal = "";
            for(int i = 0; i < numeroTexto.Length; i++)
            {
                if(numeroTexto[i] != ',')
                    numeroFinal += numeroTexto[i];
                else
                    break;
            }
            return numeroFinal;
        }
        public string ObtenerNumeroDecimal(double numero)
        {
            string numeroTexto = Convert.ToString(numero);
            string numeroFinal = "";
            bool SeEncuentraComa = false;
            for (int i = 0; i < numeroTexto.Length; i++)
            {
                if(SeEncuentraComa)
                {
                    numeroFinal += numeroTexto[i];
                }
                if (numeroTexto[i] == ',')
                    SeEncuentraComa = true;
            }
            if (numeroFinal == "")
                return "00";
            return numeroFinal;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo = (Articulo)Session["Articulo" + Session.SessionID];
            if (txtNombre.Text == "")
            {
                lblNombreError.Visible = true;
                return;
            }
            if (txtDescripcion.Text == "")
            {
                lblDescripcion.Visible = true;
                return;
            }
            if (!(ContieneSoloNumeros(txtPrecioEntero) && ContieneSoloNumeros(txtPrecioDecimales)))
            {
                lblPrecio.Visible = true;
                return;
            }
            if (txtURL.Text == "")
            {
                lblURL.Visible = true;
                return;
            }
            if (Articulo.Categorias.Count < 1)
            {
                lblErrorCategoria.Visible = true;
                lblAgregarError.Visible = false;
                lblEliminar.Visible = false;
                return;
            }
            else
            {
                lblErrorCategoria.Visible = false;
            }
            Articulo.Precio = Convert.ToDouble(txtPrecioEntero.Text) + ConvertirDecimales(txtPrecioDecimales.Text);
            Articulo.Nombre = txtNombre.Text;
            Articulo.Descripcion = txtDescripcion.Text;
            Articulo.URL_Imagen = txtURL.Text;
            Articulo.MarcaArticulo.Activo = true;
            Articulo.MarcaArticulo.ID_Marca = Convert.ToInt32(ListaMarcas.SelectedItem.Value);
            Articulo.MarcaArticulo.Nombre = ListaMarcas.SelectedItem.Text;
            NegocioABM negocio = new NegocioABM();
            negocio.ArticuloModificacion(Articulo);
            Articulo = new Articulo();
            Session["Articulo" + Session.SessionID] = Articulo;
            lblModificadoCorrectamente.Visible = true;
            ListarArticulos();
        }
        public double ConvertirDecimales(string numero)
        {
            if(numero.Length == 1)
            {
                return (Convert.ToDouble(numero) / 10);
            }
            if (numero.Length == 2)
            {
                return (Convert.ToDouble(numero) / 100);
            }
            if (numero.Length == 3)
            {
                return (Convert.ToDouble(numero) / 1000);
            }
            return -99999;
        }
    }
}