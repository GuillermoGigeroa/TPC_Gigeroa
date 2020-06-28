using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class NegocioABM
    {
        public void UsuarioAlta(Usuario usuario)
        {
            try
            {
                Encriptador encriptador = new Encriptador();
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_AltaUsuario");
                datos.AgregarParametro("@Email",usuario.Email);
                datos.AgregarParametro("@Password",encriptador.Encriptar(usuario.Password));
                datos.AgregarParametro("@Nombres",usuario.Nombres);
                datos.AgregarParametro("@Apellidos",usuario.Apellidos);
                datos.AgregarParametro("@DNI",usuario.DNI);
                datos.AgregarParametro("@IDProvincia", IDProvincia(usuario.Domicilio.Provincia));
                datos.AgregarParametro("@Ciudad",usuario.Domicilio.Ciudad);
                datos.AgregarParametro("@Calle",usuario.Domicilio.Calle);
                datos.AgregarParametro("@Numero",usuario.Domicilio.Numero);
                datos.AgregarParametro("@Piso",usuario.Domicilio.Piso);
                datos.AgregarParametro("@CP",usuario.Domicilio.CodigoPostal);
                datos.AgregarParametro("@Departamento",usuario.Domicilio.Departamento);
                datos.AgregarParametro("@Referencia",usuario.Domicilio.Referencia);
                datos.AgregarParametro("@IDTipo",usuario.TipoUsuario.ID_Tipo);
                datos.AgregarParametro("@Telefono",usuario.Telefono);
                datos.AgregarParametro("@Activo", 1);
                datos.ConectarDB();
                datos.Ejecutar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.DesconectarDB();
            }
        }
        public int IDProvincia(string provinciaRecibida)
        {
            try
            {
                NegocioDatos negocio = new NegocioDatos();
                List<string> Provincias = negocio.ListarProvincias();
                int x = 1;
                foreach (string provincia in Provincias)
                {
                    if (provinciaRecibida.Trim().ToLower() == provincia.Trim().ToLower())
                        return x;
                    x += 1;
                }
                return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UsuarioBaja(int ID_Usuario)
        {
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_BajaUsuario");
                //crear store procedure en BBDD
                datos.AgregarParametro("@IDUsuario", ID_Usuario);
                datos.ConectarDB();
                datos.Ejecutar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.DesconectarDB();
            }
        }
        public void UsuarioModificacion(Usuario usuario)
        {
            try
            {
                Encriptador encriptador = new Encriptador();
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_ModificacionUsuario");
                //crear store procedure en BBDD
                datos.AgregarParametro("@IDUsuario", usuario.ID_Usuario);
                datos.AgregarParametro("@Email", usuario.Email);
                datos.AgregarParametro("@Password", encriptador.Encriptar(usuario.Password));
                datos.AgregarParametro("@Nombres", usuario.Nombres);
                datos.AgregarParametro("@Apellidos", usuario.Apellidos);
                datos.AgregarParametro("@DNI", usuario.DNI);
                datos.AgregarParametro("@IDProvincia", IDProvincia(usuario.Domicilio.Provincia));
                datos.AgregarParametro("@Ciudad", usuario.Domicilio.Ciudad);
                datos.AgregarParametro("@Calle", usuario.Domicilio.Calle);
                datos.AgregarParametro("@Numero", usuario.Domicilio.Numero);
                datos.AgregarParametro("@Piso", usuario.Domicilio.Piso);
                datos.AgregarParametro("@CP", usuario.Domicilio.CodigoPostal);
                datos.AgregarParametro("@Departamento", usuario.Domicilio.Departamento);
                datos.AgregarParametro("@Referencia", usuario.Domicilio.Referencia);
                datos.AgregarParametro("@IDTipo", usuario.TipoUsuario.ID_Tipo);
                datos.AgregarParametro("@Telefono", usuario.Telefono);
                datos.AgregarParametro("@Activo", 1);
                datos.ConectarDB();
                datos.Ejecutar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.DesconectarDB();
            }
        }
        public void ArticuloAlta(Articulo articulo)
        {
            try
            {
                Encriptador encriptador = new Encriptador();
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_AltaArticulo");
                //crear store procedure en BBDD
                datos.AgregarParametro("@Nombre", articulo.Nombre);
                datos.AgregarParametro("@IDMarca", articulo.MarcaArticulo.ID_Marca);
                datos.AgregarParametro("@Descripcion", articulo.Descripcion);
                datos.AgregarParametro("@EsMateriaPrima", articulo.EsMateriaPrima);
                datos.AgregarParametro("@URL_Imagen", articulo.URL_Imagen);
                datos.AgregarParametro("@Estado", 1);
                datos.AgregarParametro("@Precio", articulo.Precio);
                //hacer un proceso en el cual agregue las categorias
                datos.ConectarDB();
                datos.Ejecutar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.DesconectarDB();
            }
        }
    }
}
