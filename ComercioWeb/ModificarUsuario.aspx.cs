using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using System.Drawing;
using System.Text;
using System.Runtime.Hosting;

namespace ComercioWeb
{
    public partial class ModificarUsuario : System.Web.UI.Page
    {
        public Usuario Usuario { get; set; }
        public Dominio.Carrito Carrito { get; set; }
        public bool HayUsuarioActivo { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            HayUsuarioActivo = ExisteUsuario();
            VerificarUsuario();
            CargarInformacion();
            VerificarChanged();
            VerificarCarrito();
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
        public void CargarInformacion()
        {
            if (!IsPostBack)
            {
                NegocioDatos negocio = new NegocioDatos();
                ListaProvincias.DataSource = negocio.ListarProvincias();
                ListaProvincias.DataBind();
                CargarDatos();
            }
        }
        public void VerificarChanged()
        {
            string Actualizado = Request.QueryString["changed"];
            if (Actualizado != null)
            {
                lblActualizado.Visible = true;
            }
            VerificarCarrito();
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
        public void VerificarUsuario()
        {
            if (Session["Usuario" + Session.SessionID] != null)
                Usuario = (Usuario)Session["Usuario" + Session.SessionID];
            else
                Response.Redirect("IniciarSesion.aspx");
        }
        public bool VerificarCampo(TextBox txtCaja)
        {
            if (txtCaja.Text.Trim() != "")
            {
                txtCaja.BackColor = Color.White;
                return true;
            }
            else
            {
                txtCaja.BackColor = Color.HotPink;
                return false;
            }
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
            if(!chkEmail.Checked)
                lblEmail.Visible = false;
        }
        protected void chkPassword_CheckedChanged(object sender, EventArgs e)
        {
            Password.Visible = chkPassword.Checked;
            txtPasswordAnterior.Visible = chkPassword.Checked;
            NuevoPassword.Visible = chkPassword.Checked;
            txtPassword.Visible = chkPassword.Checked;
            NuevoPassword2.Visible = chkPassword.Checked;
            txtPassword2.Visible = chkPassword.Checked;
            if (!chkPassword.Checked)
                lblPassword.Visible = false;
        }
        public bool VerificarMail()
        {
            if (txtEmail.Text.Trim() == txtEmail2.Text.Trim() && txtEmail.Text.Trim() != "")
            {
                txtEmail.BackColor = Color.White;
                txtEmail2.BackColor = Color.White;
                lblEmail.Visible = false;
                return true;
            }
            else
            {
                txtEmail.BackColor = Color.HotPink;
                txtEmail2.BackColor = Color.HotPink;
                lblEmail.Visible = true;
                return false;
            }
        }
        public bool VerificarPassword()
        {
            Encriptador encriptador = new Encriptador();
            if (txtPasswordAnterior.Text == encriptador.Desencriptar(Usuario.Password))
            {
                txtPasswordAnterior.BackColor = Color.White;
                lblPassword.Text = "Los campos de contraseña no son iguales.";
                if (txtPassword.Text.Trim() == txtPassword2.Text.Trim() && txtPassword.Text.Trim() != "")
                {
                    txtPassword.BackColor = Color.White;
                    txtPassword2.BackColor = Color.White;
                    lblPassword.Visible = false;
                    return true;
                }
                else
                {
                    txtPassword.BackColor = Color.HotPink;
                    txtPassword2.BackColor = Color.HotPink;
                    lblPassword.Visible = true;
                    return false;
                }
            }
            else
            {
                txtPasswordAnterior.BackColor = Color.HotPink;
                lblPassword.Text = "La contraseña antigua no es correcta.";
                lblPassword.Visible = true;
                return false;
            }
        }
        protected void txtEmail2_TextChanged(object sender, EventArgs e)
        {
            VerificarMail();
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            chkPassword.Checked = Password.Visible;
            chkEmail.Checked = Email.Visible;
            if (chkEmail.Checked)
            {
                if (!VerificarMail())
                    return;
            }
            if (!VerificarCampo(txtApellido))
                return;
            if (!VerificarCampo(txtNombre))
                return;
            if (!VerificarCampo(txtDNI))
                return;
            if (!VerificarCampo(txtTelefono))
                return;
            if (!VerificarCampo(txtDNI))
                return;
            if (!VerificarCampo(txtCiudad))
                return;
            if (!VerificarCampo(txtCodigoPostal))
                return;
            if (!VerificarCampo(txtCalle))
                return;
            if (!VerificarCampo(txtNumero))
                return;
            if(chkPassword.Checked)
            {
                if (!VerificarPassword())
                    return;
            }
            ActualizarUsuario();
            Response.Redirect("ModificarUsuario.aspx?changed=true");
        }
        public void ActualizarUsuario()
        {
            Usuario Actualizacion = new Usuario();
            Encriptador encriptador = new Encriptador();
            if(txtEmail.Text.Trim() != "")
                Actualizacion.Email = txtEmail.Text;
            else
                Actualizacion.Email = Usuario.Email;
            if(txtPassword.Text.Trim() != "")
                Actualizacion.Password = txtPassword.Text;
            else
                Actualizacion.Password = encriptador.Desencriptar(Usuario.Password);
            Actualizacion.ID_Usuario = Usuario.ID_Usuario;
            Actualizacion.Nombres = txtNombre.Text;
            Actualizacion.Apellidos = txtApellido.Text;
            Actualizacion.DNI = Convert.ToInt32(txtDNI.Text);
            Actualizacion.Domicilio.Provincia = ListaProvincias.SelectedValue;
            Actualizacion.Domicilio.Ciudad = txtCiudad.Text;
            Actualizacion.Domicilio.Calle = txtCalle.Text;
            Actualizacion.Domicilio.Numero = Convert.ToInt32(txtNumero.Text);
            Actualizacion.Domicilio.Piso = txtPiso.Text;
            Actualizacion.Domicilio.CodigoPostal = Convert.ToInt32(txtCodigoPostal.Text);
            Actualizacion.Domicilio.Departamento = txtDepartamento.Text;
            Actualizacion.Domicilio.Referencia = txtReferencia.Text;
            Actualizacion.TipoUsuario = Usuario.TipoUsuario;
            Actualizacion.Telefono = Convert.ToInt32(txtTelefono.Text);
            Actualizacion.Activo = Usuario.Activo;
            Actualizacion.IDListaFavoritos = Usuario.IDListaFavoritos;
            Actualizacion.ListaFavoritos = Usuario.ListaFavoritos;
            NegocioABM negocioABM = new NegocioABM();
            negocioABM.UsuarioModificacion(Actualizacion);
            Actualizacion.Password = encriptador.Encriptar(Actualizacion.Password);
            Session["Usuario" + Session.SessionID] = Actualizacion;
        }
        protected void txtDNI_TextChanged(object sender, EventArgs e)
        {
            string texto = txtDNI.Text;
            foreach (char caracter in txtDNI.Text)
            {
                if ((caracter < 48 || caracter > 57) && caracter != 46)
                {
                    txtDNI.BackColor = Color.HotPink;
                    lblDNI.Visible = true;
                    return;
                }
            }
            txtDNI.Text = "";
            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i] != '.')
                    txtDNI.Text += Convert.ToChar(texto[i]);
            }
            if (txtDNI.Text.Length >= 5 && txtDNI.Text.Length <= 8)
            {
                txtDNI.BackColor = Color.White;
                lblDNI.Visible = false;
            }
            else
            {
                txtDNI.BackColor = Color.HotPink;
                lblDNI.Visible = true;
            }
        }
        public bool ContieneSoloNumeros(TextBox txtCaja)
        {
            foreach (char caracter in txtCaja.Text)
            {
                if (caracter < 48 || caracter > 57)
                {
                    txtCaja.BackColor = Color.HotPink;
                    return false;
                }
            }
            txtCaja.BackColor = Color.White;
            return true;
        }
        protected void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            if (ContieneSoloNumeros(txtTelefono))
                lblTelefono.Visible = false;
            else
                lblTelefono.Visible = true;
        }
        protected void txtCodigoPostal_TextChanged(object sender, EventArgs e)
        {
            foreach (char caracter in txtCodigoPostal.Text)
            {
                if (caracter < 48 || caracter > 57)
                {
                    txtCodigoPostal.BackColor = Color.HotPink;
                    return;
                }
            }
            txtCodigoPostal.BackColor = Color.White;
        }
        protected void txtNumero_TextChanged(object sender, EventArgs e)
        {
            if (ContieneSoloNumeros(txtNumero))
                lblNumero.Visible = false;
            else
                lblNumero.Visible = true;
        }
    }
}