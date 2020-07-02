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
        public void UsuarioModificacion(Usuario usuario)
        {
            try
            {
                Encriptador encriptador = new Encriptador();
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_ActualizarUsuario");
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
                datos.AgregarParametro("@Activo", usuario.Activo);
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
        //Chusmear este que falta implementar con admin
        public void UsuarioBaja(int ID_Usuario, bool Activo)
        {
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_BajaUsuario");
                //crear store procedure en BBDD
                datos.AgregarParametro("@IDUsuario", ID_Usuario);
                datos.AgregarParametro("@Activo", Activo);
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
        public void UsuarioBaja(int ID_Usuario)
        {
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_BajaUsuario");
                //crear store procedure en BBDD
                datos.AgregarParametro("@IDUsuario", ID_Usuario);
                datos.AgregarParametro("@Activo", 0);
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
                datos.StoreProcedure("SP_AgregarArticulo");
                datos.AgregarParametro("@IDMarca", articulo.MarcaArticulo.ID_Marca);
                datos.AgregarParametro("@IDCategoria", articulo.Categorias[0].ID_Categoria);
                datos.AgregarParametro("@Nombre", articulo.Nombre);
                datos.AgregarParametro("@Descripcion", articulo.Descripcion);
                datos.AgregarParametro("@EsMateriaPrima", articulo.EsMateriaPrima);
                datos.AgregarParametro("@ImagenURL", articulo.URL_Imagen);
                datos.AgregarParametro("@Precio", articulo.Precio);
                datos.ConectarDB();
                datos.Ejecutar();
                datos.DesconectarDB();
                bool primero = true;
                foreach(Categoria categoria in articulo.Categorias)
                {
                    if(!primero)
                    {
                        datos = new Datos();
                        datos.ConfigurarConexion();
                        datos.StoreProcedure("SP_AgregarCategoriaAlUltimoArticulo");
                        datos.AgregarParametro("@IDCategoria", categoria.ID_Categoria);
                        datos.ConectarDB();
                        datos.Ejecutar();
                    }
                    primero = false;
                }
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
        public void AgregarCategoria(string Nombre)
        {
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_AgregarCategoria");
                datos.AgregarParametro("@Nombre", Nombre);
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
        public void AgregarMarca(string Nombre)
        {
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_AgregarMarca");
                datos.AgregarParametro("@Nombre", Nombre);
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
        public void BajaCategoria(int IDCategoria, bool Activo)
        {
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_BajaCategoria");
                datos.AgregarParametro("@IDCategoria", IDCategoria);
                datos.AgregarParametro("@Activo", Activo);
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
        public void BajaCategoria(int IDCategoria)
        {
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_BajaCategoria");
                datos.AgregarParametro("@IDCategoria", IDCategoria);
                datos.AgregarParametro("@Activo", 0);
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
        public void BajaMarca(int IDMarca, bool Activo)
        {
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_BajaMarca");
                datos.AgregarParametro("@IDMarca", IDMarca);
                datos.AgregarParametro("@Activo", Activo);
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
        public void BajaMarca(int IDMarca)
        {
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_BajaMarca");
                datos.AgregarParametro("@IDMarca", IDMarca);
                datos.AgregarParametro("@Activo", 0);
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
