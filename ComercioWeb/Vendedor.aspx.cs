using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace ComercioWeb
{
    public partial class Vendedor : System.Web.UI.Page
    {
        public Usuario Usuario { get; set; }
        public Dominio.Carrito Carrito { get; set; }
        public bool HayUsuarioActivo { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            HayUsuarioActivo = ExisteUsuario();
            VerificarVendedor();
            VerificarCarrito();
            if(!IsPostBack)
            {//Asi ver si funciona a la perfeccion
                ListarCategorias();
                ListarMarcas();
            }
        }
        public void ListarMarcas()
        {
            try
            {
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
            {
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
            try
            {
                AjustarCaracteres(txtNuevaCategoria);
                if(txtNuevaCategoria.Text.Trim() != "")
                {
                    NegocioABM negocio = new NegocioABM();
                    negocio.AgregarCategoria(txtNuevaCategoria.Text);
                    txtNuevaCategoria.Text = "";
                    ListarCategorias();
                    lblCategoriaAgregada.Visible = true;
                }
                else
                {
                    lblCategoria.Visible = true;
                    lblCategoriaAgregada.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            try
            {
                AjustarCaracteres(txtNuevaMarca);
                if(txtNuevaMarca.Text.Trim() != "")
                {
                    NegocioABM negocio = new NegocioABM();
                    negocio.AgregarMarca(txtNuevaMarca.Text);
                    txtNuevaMarca.Text = "";
                    ListarMarcas();
                    lblMarcaAgregada.Visible = true;
                }
                else
                {
                    lblMarca.Visible = true;
                    lblMarcaAgregada.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void txtNuevaCategoria_TextChanged(object sender, EventArgs e)
        {
            AjustarCaracteres(txtNuevaCategoria);
            if (txtNuevaCategoria.Text.Trim() != "")
            {
                lblCategoria.Visible = false;
            }
            else
            {
                lblCategoria.Visible = true;
            }
            lblCategoriaAgregada.Visible = false;
        }
        protected void txtNuevaMarca_TextChanged(object sender, EventArgs e)
        {
            AjustarCaracteres(txtNuevaMarca);
            if (txtNuevaMarca.Text.Trim() != "")
            {
                lblMarca.Visible = false;
            }
            else
            {
                lblMarca.Visible = true;
            }
            lblMarcaAgregada.Visible = false;
        }
        public void AjustarCaracteres(TextBox txtCaja)
        {
            if (txtCaja.Text.Length > 50)
            {
                string nuevoTexto = "";
                for (int i = 0; i < 50; i++)
                {
                    nuevoTexto += Convert.ToChar(txtCaja.Text[i]);
                }
                txtNuevaCategoria.Text = nuevoTexto;
            }
        }
        protected void btnEliminarMarca_Click(object sender, EventArgs e)
        {
            try
            {
                NegocioABM negocio = new NegocioABM();
                negocio.BajaMarca(Convert.ToInt32(ListaMarcas.SelectedItem.Value));
                ListarMarcas();
                lblMarcaEliminada.Visible = true;
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
                NegocioABM negocio = new NegocioABM();
                negocio.BajaCategoria(Convert.ToInt32(ListaCategorias.SelectedItem.Value));
                ListarCategorias();
                lblCategoriaEliminada.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ListaCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCategoriaEliminada.Visible = false;
        }
        protected void ListaMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMarcaEliminada.Visible = false;
        }
    }
}