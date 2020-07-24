using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
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
            VerificarAdmin();
            VerificarCarrito();
            if (!IsPostBack)
            {
                ListarUsuariosAdmin();
                CargarDatosInicialUsuario();
                ListarArticulosAdmin();
                CargarDatosArticulosInicial();
            }
        }
        public void ListarArticulosAdmin()
        {
            if (Session["ListaArticulosAdmin" + Session.SessionID] == null)
            {
                NegocioDatos negocio = new NegocioDatos();
                Session["ListaArticulosAdmin" + Session.SessionID] = negocio.ListarArticulosAdmin();
                ListaArticulos.DataSource = (List<Articulo>)Session["ListaArticulosAdmin" + Session.SessionID];
                ListaArticulos.DataTextField = "Nombre";
                ListaArticulos.DataValueField = "ID_Articulo";
                ListaArticulos.SelectedIndex = 0;
                ListaArticulos.DataBind();
            }
            else
            {
                ListaArticulos.DataSource = (List<Articulo>)Session["ListaArticulosAdmin" + Session.SessionID];
                ListaArticulos.DataTextField = "Nombre";
                ListaArticulos.DataValueField = "ID_Articulo";
                ListaArticulos.SelectedIndex = 0;
                ListaArticulos.DataBind();
            }
        }
        public void ListarUsuariosAdmin()
        {
            if (Session["ListaUsuarios" + Session.SessionID] == null)
            {
                NegocioDatos negocio = new NegocioDatos();
                Session["ListaUsuarios" + Session.SessionID] = negocio.ListarUsuariosAdmin();
                ListaUsuarios.DataSource = (List<Usuario>)Session["ListaUsuarios" + Session.SessionID];
                ListaUsuarios.DataTextField = "Email";
                ListaUsuarios.DataValueField = "ID_Usuario";
                ListaUsuarios.SelectedIndex = 0;
                ListaUsuarios.DataBind();
            }
            else
            {
                ListaUsuarios.DataSource = (List<Usuario>)Session["ListaUsuarios" + Session.SessionID];
                ListaUsuarios.DataTextField = "Email";
                ListaUsuarios.DataValueField = "ID_Usuario";
                ListaUsuarios.SelectedIndex = 0;
                ListaUsuarios.DataBind();
            }
        }
        public void ActualizarUsuariosAdmin()
        {
            NegocioDatos negocio = new NegocioDatos();
            Session["ListaUsuarios" + Session.SessionID] = negocio.ListarUsuariosAdmin();
            ListaUsuarios.DataSource = (List<Usuario>)Session["ListaUsuarios" + Session.SessionID];
            ListaUsuarios.DataTextField = "Email";
            ListaUsuarios.DataValueField = "ID_Usuario";
            ListaUsuarios.SelectedIndex = 0;
            ListaUsuarios.DataBind();
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
                if (Usuario.TipoUsuario.ID_Tipo != 1)
                {
                    Response.Redirect("Usuarios.aspx");
                }
            }
            else
                Response.Redirect("IniciarSesion.aspx");
        }
        protected void ListaUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDatosUsuario();
            lblBlanqueo.Visible = false;
        }
        public void CargarDatosInicialUsuario()
        {
            try
            {
                List<Usuario> listaUsuarios = (List<Usuario>)Session["ListaUsuarios" + Session.SessionID];
                foreach (Usuario usuario in listaUsuarios)
                {
                    lblNombre.Text = "Nombres: " + usuario.Nombres;
                    lblApellido.Text = "Apellidos: " + usuario.Apellidos;
                    lblDNI.Text = "DNI: " + usuario.DNI;
                    lblTipo.Text = "Tipo: " + usuario.TipoUsuario.Nombre;
                    if (usuario.Activo)
                        lblEstado.Text = "Estado: Activo";
                    else
                        lblEstado.Text = "Estado: Inactivo";
                    break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargarDatosUsuario()
        {
            try
            {
                List<Usuario> listaUsuarios = (List<Usuario>)Session["ListaUsuarios" + Session.SessionID];
                foreach (Usuario usuario in listaUsuarios)
                {
                    if (Convert.ToInt32(ListaUsuarios.SelectedItem.Value) == usuario.ID_Usuario)
                    {
                        lblNombre.Text = "Nombres: " + usuario.Nombres;
                        lblApellido.Text = "Apellidos: " + usuario.Apellidos;
                        lblDNI.Text = "DNI: " + usuario.DNI;
                        lblTipo.Text = "Tipo: " + usuario.TipoUsuario.Nombre;
                        if (usuario.Activo)
                            lblEstado.Text = "Estado: Activo";
                        else
                            lblEstado.Text = "Estado: Inactivo";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnBlanquearPassword_Click(object sender, EventArgs e)
        {
            try
            {
                List<Usuario> listaUsuarios = (List<Usuario>)Session["ListaUsuarios" + Session.SessionID];
                Usuario usuarioModificado = new Usuario();
                int SelectedIndex = ListaUsuarios.SelectedIndex;
                foreach (Usuario usuario in listaUsuarios)
                {
                    if (Convert.ToInt32(ListaUsuarios.SelectedItem.Value) == usuario.ID_Usuario)
                    {
                        usuarioModificado = usuario;
                        usuarioModificado.Password = "123";
                        NegocioABM negocio = new NegocioABM();
                        negocio.UsuarioModificacion(usuarioModificado);
                        ActualizarUsuariosAdmin();
                        ListaUsuarios.SelectedIndex = SelectedIndex;
                        CargarDatosUsuario();
                        lblBlanqueo.Visible = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnActivar_Click(object sender, EventArgs e)
        {
            List<Usuario> listaUsuarios = (List<Usuario>)Session["ListaUsuarios" + Session.SessionID];
            int SelectedIndex = ListaUsuarios.SelectedIndex;
            foreach (Usuario usuario in listaUsuarios)
            {
                if (Convert.ToInt32(ListaUsuarios.SelectedItem.Value) == usuario.ID_Usuario)
                {
                    NegocioABM negocio = new NegocioABM();
                    negocio.UsuarioBaja(usuario.ID_Usuario, !usuario.Activo);
                    ActualizarUsuariosAdmin();
                    ListaUsuarios.SelectedIndex = SelectedIndex;
                    CargarDatosUsuario();
                    break;
                }
            }
        }
        protected void btnActivarArticulo_Click(object sender, EventArgs e)
        {
            List<Articulo> listaArticulos = (List<Articulo>)Session["ListaArticulosAdmin" + Session.SessionID];
            int SelectedIndex = ListaArticulos.SelectedIndex;
            foreach (Articulo articulo in listaArticulos)
            {
                if (Convert.ToInt32(ListaArticulos.SelectedItem.Value) == articulo.ID_Articulo)
                {
                    NegocioABM negocio = new NegocioABM();
                    negocio.ArticuloBaja(articulo.ID_Articulo, !articulo.Estado);
                    ActualizarArticulosAdmin();
                    ListaArticulos.SelectedIndex = SelectedIndex;
                    CargarDatosArticulos();
                    break;
                }
            }
        }
        public void ActualizarArticulosAdmin()
        {
            NegocioDatos negocio = new NegocioDatos();
            Session["ListaArticulosAdmin" + Session.SessionID] = negocio.ListarArticulosAdmin();
            ListaArticulos.DataSource = (List<Articulo>)Session["ListaArticulosAdmin" + Session.SessionID];
            ListaArticulos.DataTextField = "Nombre";
            ListaArticulos.DataValueField = "ID_Articulo";
            ListaArticulos.SelectedIndex = 0;
            ListaArticulos.DataBind();
        }
        public void CargarDatosArticulos()
        {
            try
            {
                List<Articulo> listaArticulos = (List<Articulo>)Session["ListaArticulosAdmin" + Session.SessionID];
                foreach (Articulo articulo in listaArticulos)
                {
                    if (Convert.ToInt32(ListaArticulos.SelectedItem.Value) == articulo.ID_Articulo)
                    {
                        lblID_Articulo.Text = "ID del artículo: " + Convert.ToString(articulo.ID_Articulo);
                        lblNombreArticulo.Text = "Nombre: " + articulo.Nombre;
                        lblMarca.Text = "Marca: " + articulo.MarcaArticulo.Nombre;
                        lblDescripcion.Text = "Descripción: " + articulo.Descripcion;
                        lblPrecio.Text = "Precio: " + Convert.ToString(articulo.Precio);
                        lblStock.Text = "Stock: " + Convert.ToString(articulo.Stock);
                        if (articulo.Estado)
                            lblEstadoArticulo.Text = "Estado: Activo";
                        else
                            lblEstadoArticulo.Text = "Estado: Inactivo";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargarDatosArticulosInicial()
        {
            try
            {
                List<Articulo> listaArticulos = (List<Articulo>)Session["ListaArticulosAdmin" + Session.SessionID];
                foreach (Articulo articulo in listaArticulos)
                {
                    lblID_Articulo.Text = "ID del artículo: " + Convert.ToString(articulo.ID_Articulo);
                    lblNombreArticulo.Text = "Nombre: " + articulo.Nombre;
                    lblMarca.Text = "Marca: " + articulo.MarcaArticulo.Nombre;
                    lblDescripcion.Text = "Descripción: " + articulo.Descripcion;
                    lblPrecio.Text = "Precio: " + Convert.ToString(articulo.Precio);
                    lblStock.Text = "Stock: " + Convert.ToString(articulo.Stock);
                    if (articulo.Estado)
                        lblEstadoArticulo.Text = "Estado: Activo";
                    else
                        lblEstadoArticulo.Text = "Estado: Inactivo";
                    break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ListaArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDatosArticulos();
        }
    }
}