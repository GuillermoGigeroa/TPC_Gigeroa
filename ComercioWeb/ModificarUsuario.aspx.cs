using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace ComercioWeb
{
    public partial class ModificarUsuario : System.Web.UI.Page
    {
        public Usuario Usuario { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarUsuario();
            NegocioDatos negocio = new NegocioDatos();
            ListaProvincias.DataSource = negocio.ListarProvincias();
            ListaProvincias.DataBind();
            CargarDatos();
        }
        public void VerificarUsuario()
        {
            if (Session["Usuario" + Session.SessionID] != null)
                Usuario = (Usuario)Session["Usuario" + Session.SessionID];
            else
                Response.Redirect("IniciarSesion.aspx");
        }
        public void CargarDatos()
        {
            txtApellido.Text = Usuario.Apellidos;
            txtNombre.Text = Usuario.Nombres;
            txtDNI.Text = Convert.ToString(Usuario.DNI);
            txtTelefono.Text = Convert.ToString(Usuario.Telefono);
            NegocioABM negocio = new NegocioABM();
            ListaProvincias.SelectedIndex = negocio.IDProvincia(Usuario.Domicilio.Provincia)-1;
            txtCiudad.Text = Usuario.Domicilio.Ciudad;
            txtCodigoPostal.Text = Convert.ToString(Usuario.Domicilio.CodigoPostal);
            txtCalle.Text = Usuario.Domicilio.Calle;
            txtNumero.Text = Convert.ToString(Usuario.Domicilio.Numero);
            txtPiso.Text = Usuario.Domicilio.Piso;
            txtDepartamento.Text = Usuario.Domicilio.Departamento;
            txtReferencia.Text = Usuario.Domicilio.Referencia;
        }
        protected void chkEmail_CheckedChanged(object sender, EventArgs e)
        {
            Email.Visible = chkEmail.Checked;
            txtEmail.Visible = chkEmail.Checked;
            Email2.Visible = chkEmail.Checked;
            txtEmail2.Visible = chkEmail.Checked;
        }
        protected void chkPassword_CheckedChanged(object sender, EventArgs e)
        {
            Password.Visible = chkPassword.Checked;
            txtPasswordAnterior.Visible = chkPassword.Checked;
            NuevoPassword.Visible = chkPassword.Checked;
            txtPassword.Visible = chkPassword.Checked;
            NuevoPassword2.Visible = chkPassword.Checked;
            txtPassword2.Visible = chkPassword.Checked;
        }
    }
}