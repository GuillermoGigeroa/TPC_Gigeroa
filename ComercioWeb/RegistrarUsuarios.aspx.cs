using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ComercioWeb
{
    public partial class RegistrarUsuarios : System.Web.UI.Page
    {
        public Dominio.Carrito Carrito { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            NegocioDatos negocio = new NegocioDatos();
            ListaProvincias.DataSource = negocio.ListarProvincias();
            ListaProvincias.DataBind();
            string tipoUsuario = Request.QueryString["tipo"];
            if (tipoUsuario != null)
            {
                if (tipoUsuario == "admin")
                    Session["TipoUsuario" + Session.SessionID] = 1;
                if (tipoUsuario == "1")
                    Session["TipoUsuario" + Session.SessionID] = 1;
                if (tipoUsuario == "seller")
                    Session["TipoUsuario" + Session.SessionID] = 2;
                if (tipoUsuario == "2")
                    Session["TipoUsuario" + Session.SessionID] = 2;
                if (tipoUsuario == "user")
                    Session["TipoUsuario" + Session.SessionID] = 3;
                if (tipoUsuario == "3")
                    Session["TipoUsuario" + Session.SessionID] = 3;
            }
            if (Session["Carrito" + Session.SessionID] != null)
            {
                Carrito = (Dominio.Carrito)Session["Carrito" + Session.SessionID];
            }
            else
            {
                Carrito = new Dominio.Carrito();
            }
        }
        public bool VerificarMail()
        {
            if (txtEmail.Text.Trim() == txtEmail2.Text.Trim())
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
            if (txtPassword.Text.Trim() == txtPassword2.Text.Trim() && txtPassword.Text.Trim() != "")
            {
                txtPassword.BackColor = Color.White;
                txtPassword2.BackColor = Color.White;
                return true;
            }
            else
            {
                txtPassword.BackColor = Color.HotPink;
                txtPassword2.BackColor = Color.HotPink;
                return false;
            }
        }
        public bool VerificarCampo(TextBox txtCaja)
        {
            if(txtCaja.Text.Trim() != "")
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
        protected void txtEmail2_TextChanged(object sender, EventArgs e)
        {
            VerificarMail();
        }
        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            if (!VerificarMail())
                return;
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
            if (!VerificarPassword())
            {
                lblPassword.Visible = true;
                return;
            }
            else
            {
                lblPassword.Visible = false;
            }
            if (!VerificarCheck())
                return;
            NegocioABM negocio = new NegocioABM();
            Usuario usuario = UsuarioCargado();
            negocio.UsuarioAlta(usuario);
            Encriptador encriptador = new Encriptador();
            usuario.Password = encriptador.Encriptar(usuario.Password);
            Session["Usuario" + Session.SessionID] = usuario;
            Response.Redirect("Usuarios.aspx?newuser=1");
        }
        public bool VerificarCheck()
        {
            if (chkTerminosCondiciones.Checked)
            {
                chkTerminosCondiciones.BackColor = Color.White;
                return true;
            }
            else
            {
                chkTerminosCondiciones.BackColor = Color.HotPink;
                return false;
            }
        }
        public Usuario UsuarioCargado()
        {
            Usuario usuario = new Usuario();
            usuario.Email = txtEmail.Text;
            usuario.Password = txtPassword.Text;
            usuario.Nombres = txtNombre.Text;
            usuario.Apellidos = txtApellido.Text;
            usuario.DNI = Convert.ToInt32(txtDNI.Text);
            usuario.Domicilio.Provincia = ListaProvincias.SelectedValue;
            if (Session["TipoUsuario" + Session.SessionID] != null)
                usuario.TipoUsuario = BuscarTipo(Convert.ToInt32(Session["TipoUsuario" + Session.SessionID]));
            else
                usuario.TipoUsuario = BuscarTipo(3);//Tipo Cliente
            usuario.Telefono = Convert.ToInt32(txtTelefono.Text);
            usuario.Activo = true;
            usuario.Domicilio.Ciudad = txtCiudad.Text;
            usuario.Domicilio.Calle = txtCalle.Text;
            usuario.Domicilio.Numero = Convert.ToInt32(txtNumero.Text);
            usuario.Domicilio.Piso = txtPiso.Text;
            usuario.Domicilio.CodigoPostal = Convert.ToInt32(txtCodigoPostal.Text);
            usuario.Domicilio.Departamento = txtDepartamento.Text;
            usuario.Domicilio.Referencia = txtReferencia.Text;
            return usuario;
        }
        public TipoUsuario BuscarTipo(int ID_Tipo)
        {
            NegocioDatos negocio = new NegocioDatos();
            List<TipoUsuario> listaTipos = negocio.ListarTipos();
            foreach(TipoUsuario tipo in listaTipos)
            {
                if(tipo.ID_Tipo == ID_Tipo)
                {
                    return tipo;
                }
            }
            TipoUsuario tipoDesconocido = new TipoUsuario();
            tipoDesconocido.ID_Tipo = 3;
            tipoDesconocido.Nombre = "Cliente";
            return tipoDesconocido;
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
        protected void txtDNI_TextChanged(object sender, EventArgs e)
        {
            string texto = txtDNI.Text;
            foreach(char caracter in txtDNI.Text)
            {
                if((caracter < 48 || caracter > 57) && caracter != 46)
                {
                    txtDNI.BackColor = Color.HotPink;
                    lblDNI.Visible = true;
                    return;
                }
            }
            txtDNI.Text = "";
            for(int i = 0; i < texto.Length; i++)
            {
                if(texto[i] != '.')
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
        protected void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            if(ContieneSoloNumeros(txtTelefono))
                lblTelefono.Visible = false;
            else
                lblTelefono.Visible = true;
        }
        protected void txtCodigoPostal_TextChanged(object sender, EventArgs e)
        {
            if (ContieneSoloNumeros(txtCodigoPostal))
                lblCodigoPostal.Visible = false;
            else
                lblCodigoPostal.Visible = true;
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