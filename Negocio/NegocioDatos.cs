﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;
using System.Runtime.Remoting.Messaging;
using System.ComponentModel;

namespace Negocio
{
    public class NegocioDatos
    {
        public List<Articulo> ListarArticulos()
        {
            try
            {
                List<Articulo> listado = ListarArticulosSinCategorias();
                foreach (Articulo articulo in listado)
                    articulo.Categorias = BuscarCategorias(articulo.ID_Articulo);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Articulo> ListarArticulosAdmin()
        {
            try
            {
                List<Articulo> listado = ListarArticulosSinCategoriasAdmin();
                foreach (Articulo articulo in listado)
                    articulo.Categorias = BuscarCategorias(articulo.ID_Articulo);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<Articulo> ListarArticulosSinCategorias()
        {
            List<Articulo> listado = new List<Articulo>();
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_ListarArticulosSinCategoria");
                datos.ConectarDB();
                datos.PrepararLector();
                SqlDataReader datosLeidos;
                Articulo articulo;
                while (datos.Leer())
                {
                    datosLeidos = datos.Lectura();
                    articulo = new Articulo();
                    if (Convert.ToBoolean(datosLeidos["Estado"]) && Convert.ToInt32(datosLeidos["Stock"]) > 0)
                    {
                        articulo.ID_Articulo = -1;
                        if (!Convert.IsDBNull(datosLeidos["ID_Articulo"]))
                            articulo.ID_Articulo = Convert.ToInt32(datosLeidos["ID_Articulo"]);
                        articulo.Nombre = "NombreArticulo";
                        if (!Convert.IsDBNull(datosLeidos["Nombre"]))
                            articulo.Nombre = (string)datosLeidos["Nombre"];
                        articulo.MarcaArticulo.ID_Marca = -1;
                        if (!Convert.IsDBNull(datosLeidos["ID_Marca"]))
                            articulo.MarcaArticulo.ID_Marca = Convert.ToInt32(datosLeidos["ID_Marca"]);
                        articulo.MarcaArticulo.Nombre = "NombreMarca";
                        if (!Convert.IsDBNull(datosLeidos["Marca"]))
                            articulo.MarcaArticulo.Nombre = (string)datosLeidos["Marca"];
                        articulo.Descripcion = "DescripcionArticulo";
                        if (!Convert.IsDBNull(datosLeidos["Descripcion"]))
                            articulo.Descripcion = (string)datosLeidos["Descripcion"];
                        articulo.EsMateriaPrima = false;
                        if (!Convert.IsDBNull(datosLeidos["EsMateriaPrima"]))
                            articulo.EsMateriaPrima = (bool)datosLeidos["EsMateriaPrima"];
                        articulo.URL_Imagen = "https://www.publicdomainpictures.net/pictures/280000/nahled/not-found-image-15383864787lu.jpg";
                        if (!Convert.IsDBNull(datosLeidos["URL_Imagen"]))
                            articulo.URL_Imagen = (string)datosLeidos["URL_Imagen"];
                        articulo.Estado = true;
                        if (!Convert.IsDBNull(datosLeidos["Estado"]))
                            articulo.Estado = (bool)datosLeidos["Estado"];
                        articulo.Precio = -1;
                        if (!Convert.IsDBNull(datosLeidos["Precio"]))
                            articulo.Precio = Convert.ToDouble(datosLeidos["Precio"]);
                        articulo.Stock = -1;
                        if (!Convert.IsDBNull(datosLeidos["Stock"]))
                            articulo.Stock = Convert.ToInt32(datosLeidos["Stock"]);
                        listado.Add(articulo);
                    }
                }
                return listado;
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
        private List<Categoria> BuscarCategorias(int identificador)
        {
            List<Categoria> categorias = new List<Categoria>();
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_BuscarCategoriasDelArticulo");
                datos.AgregarParametro("@Identificador", identificador);
                datos.ConectarDB();
                datos.PrepararLector();
                SqlDataReader datosLeidos;
                Categoria categoria;
                while (datos.Leer())
                {
                    datosLeidos = datos.Lectura();
                    //Se verifica si el articulo está activo o fue dado de baja para continuar
                    if (Convert.ToBoolean(datosLeidos["Activo"]))
                    {
                        categoria = new Categoria();

                        categoria.ID_Categoria = -1;
                        if (!Convert.IsDBNull(datosLeidos["ID_Categoria"]))
                            categoria.ID_Categoria = Convert.ToInt32(datosLeidos["ID_Categoria"]);
                        categoria.Nombre = "NombreCategoria";
                        if (!Convert.IsDBNull(datosLeidos["Categoria"]))
                            categoria.Nombre = (string)datosLeidos["Categoria"];
                        categoria.Activo = true;
                        categorias.Add(categoria);
                    }
                }
                return categorias;
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
        public List<Marca> ListarMarcas()
        {
            List<Marca> marcas = new List<Marca>();
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.Query("Select * from VW_Marcas");
                datos.ConectarDB();
                datos.PrepararLector();
                SqlDataReader datosLeidos;
                Marca marca;
                while (datos.Leer())
                {
                    marca = new Marca();
                    datosLeidos = datos.Lectura();
                    marca.ID_Marca = -1;
                    if (!Convert.IsDBNull(datosLeidos["ID_Marca"]))
                        marca.ID_Marca = Convert.ToInt32(datosLeidos["ID_Marca"]);
                    marca.Nombre = "NombreMarca";
                    if (!Convert.IsDBNull(datosLeidos["Marca"]))
                        marca.Nombre = (string)datosLeidos["Marca"];
                    marca.Activo = true;
                    marcas.Add(marca);
                }
                return marcas;
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
        public List<Categoria> ListarCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.Query("Select * from VW_Categorias");
                datos.ConectarDB();
                datos.PrepararLector();
                SqlDataReader datosLeidos;
                Categoria categoria;
                while (datos.Leer())
                {
                    categoria = new Categoria();
                    datosLeidos = datos.Lectura();
                    categoria.ID_Categoria = -1;
                    if (!Convert.IsDBNull(datosLeidos["ID_Categorias"]))
                        categoria.ID_Categoria = Convert.ToInt32(datosLeidos["ID_Categorias"]);
                    categoria.Nombre = "NombreCategoria";
                    if (!Convert.IsDBNull(datosLeidos["Categoria"]))
                        categoria.Nombre = (string)datosLeidos["Categoria"];
                    categoria.Activo = true;
                    categorias.Add(categoria);
                }
                return categorias;
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
        public List<string> ListarProvincias()
        {
            List<string> lista = new List<string>();
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.Query("Select * from VW_Provincias");
                datos.ConectarDB();
                datos.PrepararLector();
                SqlDataReader datosLeidos;
                string Provincia;
                while (datos.Leer())
                {
                    datosLeidos = datos.Lectura();
                    Provincia = "Otro";
                    if (!Convert.IsDBNull(datosLeidos["Provincia"]))
                        Provincia = Convert.ToString(datosLeidos["Provincia"]);
                    lista.Add(Provincia);
                }
                return lista;
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
        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                Encriptador encriptador = new Encriptador();
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.Query("Select * from VW_UsuariosCompletos");
                datos.ConectarDB();
                datos.PrepararLector();
                SqlDataReader datosLeidos;
                Usuario usuario;
                while (datos.Leer())
                {
                    datosLeidos = datos.Lectura();
                    usuario = new Usuario();
                    if (Convert.ToBoolean(datosLeidos["Activo"]))
                    {
                        usuario.ID_Usuario = -1;
                        if (!Convert.IsDBNull(datosLeidos["IDUsuario"]))
                            usuario.ID_Usuario = Convert.ToInt32(datosLeidos["IDUsuario"]);
                        usuario.Email = "nocargado@noemail.com";
                        if (!Convert.IsDBNull(datosLeidos["Email"]))
                            usuario.Email = Convert.ToString(datosLeidos["Email"]);
                        usuario.Password = "nocargado_nocargado";
                        if (!Convert.IsDBNull(datosLeidos["Contra"]))
                            usuario.Password = Convert.ToString(datosLeidos["Contra"]);
                        usuario.Nombres = "Nombres";
                        if (!Convert.IsDBNull(datosLeidos["Nombres"]))
                            usuario.Nombres = Convert.ToString(datosLeidos["Nombres"]);
                        usuario.Apellidos = "Apellidos";
                        if (!Convert.IsDBNull(datosLeidos["Apellidos"]))
                            usuario.Apellidos = Convert.ToString(datosLeidos["Apellidos"]);
                        usuario.DNI = 0;
                        if (!Convert.IsDBNull(datosLeidos["DNI"]))
                            usuario.DNI = Convert.ToInt32(datosLeidos["DNI"]);
                        usuario.Telefono = 0;
                        if (!Convert.IsDBNull(datosLeidos["Telefono"]))
                            usuario.Telefono = Convert.ToInt32(datosLeidos["Telefono"]);
                        usuario.TipoUsuario.ID_Tipo = -1;
                        if (!Convert.IsDBNull(datosLeidos["ID_Tipo"]))
                            usuario.TipoUsuario.ID_Tipo = Convert.ToInt32(datosLeidos["ID_Tipo"]);
                        usuario.TipoUsuario.Nombre = "NombreTipo";
                        if (!Convert.IsDBNull(datosLeidos["Tipo"]))
                            usuario.TipoUsuario.Nombre = Convert.ToString(datosLeidos["Tipo"]);
                        usuario.Domicilio.Provincia = "Provincia";
                        if (!Convert.IsDBNull(datosLeidos["Provincia"]))
                            usuario.Domicilio.Provincia = Convert.ToString(datosLeidos["Provincia"]);
                        usuario.Domicilio.Ciudad = "Ciudad";
                        if (!Convert.IsDBNull(datosLeidos["Ciudad"]))
                            usuario.Domicilio.Ciudad = Convert.ToString(datosLeidos["Ciudad"]);
                        usuario.Domicilio.Calle = "Calle";
                        if (!Convert.IsDBNull(datosLeidos["Calle"]))
                            usuario.Domicilio.Calle = Convert.ToString(datosLeidos["Calle"]);
                        usuario.Domicilio.Numero = -1;
                        if (!Convert.IsDBNull(datosLeidos["Numero"]))
                            usuario.Domicilio.Numero = Convert.ToInt32(datosLeidos["Numero"]);
                        usuario.Domicilio.Piso = "Piso";
                        if (!Convert.IsDBNull(datosLeidos["Piso"]))
                            usuario.Domicilio.Piso = Convert.ToString(datosLeidos["Piso"]);
                        usuario.Domicilio.Departamento = "Departamento";
                        if (!Convert.IsDBNull(datosLeidos["Depto"]))
                            usuario.Domicilio.Departamento = Convert.ToString(datosLeidos["Depto"]);
                        usuario.Domicilio.CodigoPostal = -1;
                        if (!Convert.IsDBNull(datosLeidos["CP"]))
                            usuario.Domicilio.CodigoPostal = Convert.ToInt32(datosLeidos["CP"]);
                        usuario.Domicilio.Referencia = "Referencia";
                        if (!Convert.IsDBNull(datosLeidos["Referencia"]))
                            usuario.Domicilio.Referencia = Convert.ToString(datosLeidos["Referencia"]);
                        usuario.Activo = true;
                        lista.Add(usuario);
                    }
                }
                return lista;
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
        public List<TipoUsuario> ListarTipos()
        {
            List<TipoUsuario> lista = new List<TipoUsuario>();
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.Query("Select * from VW_Tipos");
                datos.ConectarDB();
                datos.PrepararLector();
                SqlDataReader datosLeidos;
                TipoUsuario tipo;
                while (datos.Leer())
                {
                    tipo = new TipoUsuario();
                    datosLeidos = datos.Lectura();
                    if (!Convert.IsDBNull(datosLeidos["ID_Tipo"]))
                        tipo.ID_Tipo = Convert.ToInt32(datosLeidos["ID_Tipo"]);
                    if (!Convert.IsDBNull(datosLeidos["Nombre"]))
                        tipo.Nombre = Convert.ToString(datosLeidos["Nombre"]);
                    lista.Add(tipo);
                }
                return lista;
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
        public List<Usuario> ListarUsuariosAdmin()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                Encriptador encriptador = new Encriptador();
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.Query("Select * from VW_UsuariosCompletosAdmin");
                datos.ConectarDB();
                datos.PrepararLector();
                SqlDataReader datosLeidos;
                Usuario usuario;
                while (datos.Leer())
                {
                    datosLeidos = datos.Lectura();
                    usuario = new Usuario();

                    usuario.Activo = true;
                    if (!Convert.IsDBNull(datosLeidos["Activo"]))
                        usuario.Activo = Convert.ToBoolean(datosLeidos["Activo"]);
                    usuario.ID_Usuario = -1;
                    if (!Convert.IsDBNull(datosLeidos["IDUsuario"]))
                        usuario.ID_Usuario = Convert.ToInt32(datosLeidos["IDUsuario"]);
                    usuario.Email = "nocargado@noemail.com";
                    if (!Convert.IsDBNull(datosLeidos["Email"]))
                        usuario.Email = Convert.ToString(datosLeidos["Email"]);
                    usuario.Password = "nocargado_nocargado";
                    if (!Convert.IsDBNull(datosLeidos["Contra"]))
                        usuario.Password = Convert.ToString(datosLeidos["Contra"]);
                    usuario.Nombres = "Nombres";
                    if (!Convert.IsDBNull(datosLeidos["Nombres"]))
                        usuario.Nombres = Convert.ToString(datosLeidos["Nombres"]);
                    usuario.Apellidos = "Apellidos";
                    if (!Convert.IsDBNull(datosLeidos["Apellidos"]))
                        usuario.Apellidos = Convert.ToString(datosLeidos["Apellidos"]);
                    usuario.DNI = 0;
                    if (!Convert.IsDBNull(datosLeidos["DNI"]))
                        usuario.DNI = Convert.ToInt32(datosLeidos["DNI"]);
                    usuario.Telefono = 0;
                    if (!Convert.IsDBNull(datosLeidos["Telefono"]))
                        usuario.Telefono = Convert.ToInt32(datosLeidos["Telefono"]);
                    usuario.TipoUsuario.ID_Tipo = -1;
                    if (!Convert.IsDBNull(datosLeidos["ID_Tipo"]))
                        usuario.TipoUsuario.ID_Tipo = Convert.ToInt32(datosLeidos["ID_Tipo"]);
                    usuario.TipoUsuario.Nombre = "NombreTipo";
                    if (!Convert.IsDBNull(datosLeidos["Tipo"]))
                        usuario.TipoUsuario.Nombre = Convert.ToString(datosLeidos["Tipo"]);
                    usuario.Domicilio.Provincia = "Provincia";
                    if (!Convert.IsDBNull(datosLeidos["Provincia"]))
                        usuario.Domicilio.Provincia = Convert.ToString(datosLeidos["Provincia"]);
                    usuario.Domicilio.Ciudad = "Ciudad";
                    if (!Convert.IsDBNull(datosLeidos["Ciudad"]))
                        usuario.Domicilio.Ciudad = Convert.ToString(datosLeidos["Ciudad"]);
                    usuario.Domicilio.Calle = "Calle";
                    if (!Convert.IsDBNull(datosLeidos["Calle"]))
                        usuario.Domicilio.Calle = Convert.ToString(datosLeidos["Calle"]);
                    usuario.Domicilio.Numero = -1;
                    if (!Convert.IsDBNull(datosLeidos["Numero"]))
                        usuario.Domicilio.Numero = Convert.ToInt32(datosLeidos["Numero"]);
                    usuario.Domicilio.Piso = "Piso";
                    if (!Convert.IsDBNull(datosLeidos["Piso"]))
                        usuario.Domicilio.Piso = Convert.ToString(datosLeidos["Piso"]);
                    usuario.Domicilio.Departamento = "Departamento";
                    if (!Convert.IsDBNull(datosLeidos["Depto"]))
                        usuario.Domicilio.Departamento = Convert.ToString(datosLeidos["Depto"]);
                    usuario.Domicilio.CodigoPostal = -1;
                    if (!Convert.IsDBNull(datosLeidos["CP"]))
                        usuario.Domicilio.CodigoPostal = Convert.ToInt32(datosLeidos["CP"]);
                    usuario.Domicilio.Referencia = "Referencia";
                    if (!Convert.IsDBNull(datosLeidos["Referencia"]))
                        usuario.Domicilio.Referencia = Convert.ToString(datosLeidos["Referencia"]);
                    lista.Add(usuario);
                }
                return lista;
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
        private List<Articulo> ListarArticulosSinCategoriasAdmin()
        {
            List<Articulo> listado = new List<Articulo>();
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_ListarArticulosSinCategoria");
                datos.ConectarDB();
                datos.PrepararLector();
                SqlDataReader datosLeidos;
                Articulo articulo;
                while (datos.Leer())
                {
                    datosLeidos = datos.Lectura();
                    articulo = new Articulo();

                    articulo.Estado = true;
                    if (!Convert.IsDBNull(datosLeidos["Estado"]))
                        articulo.Estado = Convert.ToBoolean(datosLeidos["Estado"]);
                    articulo.ID_Articulo = -1;
                    if (!Convert.IsDBNull(datosLeidos["ID_Articulo"]))
                        articulo.ID_Articulo = Convert.ToInt32(datosLeidos["ID_Articulo"]);
                    articulo.Nombre = "NombreArticulo";
                    if (!Convert.IsDBNull(datosLeidos["Nombre"]))
                        articulo.Nombre = (string)datosLeidos["Nombre"];
                    articulo.MarcaArticulo.ID_Marca = -1;
                    if (!Convert.IsDBNull(datosLeidos["ID_Marca"]))
                        articulo.MarcaArticulo.ID_Marca = Convert.ToInt32(datosLeidos["ID_Marca"]);
                    articulo.MarcaArticulo.Nombre = "NombreMarca";
                    if (!Convert.IsDBNull(datosLeidos["Marca"]))
                        articulo.MarcaArticulo.Nombre = (string)datosLeidos["Marca"];
                    articulo.Descripcion = "DescripcionArticulo";
                    if (!Convert.IsDBNull(datosLeidos["Descripcion"]))
                        articulo.Descripcion = (string)datosLeidos["Descripcion"];
                    articulo.EsMateriaPrima = false;
                    if (!Convert.IsDBNull(datosLeidos["EsMateriaPrima"]))
                        articulo.EsMateriaPrima = (bool)datosLeidos["EsMateriaPrima"];
                    articulo.URL_Imagen = "https://www.publicdomainpictures.net/pictures/280000/nahled/not-found-image-15383864787lu.jpg";
                    if (!Convert.IsDBNull(datosLeidos["URL_Imagen"]))
                        articulo.URL_Imagen = (string)datosLeidos["URL_Imagen"];
                    articulo.Estado = true;
                    if (!Convert.IsDBNull(datosLeidos["Estado"]))
                        articulo.Estado = (bool)datosLeidos["Estado"];
                    articulo.Precio = -1;
                    if (!Convert.IsDBNull(datosLeidos["Precio"]))
                        articulo.Precio = Convert.ToDouble(datosLeidos["Precio"]);
                    articulo.Stock = -1;
                    if (!Convert.IsDBNull(datosLeidos["Stock"]))
                        articulo.Stock = Convert.ToInt32(datosLeidos["Stock"]);
                    listado.Add(articulo);
                }
                return listado;
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
        public int CrearFactura()
        {
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_CrearFactura");
                datos.ConectarDB();
                datos.Ejecutar();
                datos.DesconectarDB();
                datos = new Datos();
                datos.ConfigurarConexion();
                datos.Query("Select * from VW_UltimaFactura");
                datos.ConectarDB();
                datos.PrepararLector();
                datos.Leer();
                SqlDataReader datosLeidos = datos.Lectura();
                int NumeroFactura = 0;
                if (!Convert.IsDBNull(datosLeidos["Numero"]))
                    NumeroFactura = Convert.ToInt32(datosLeidos["Numero"]);
                return NumeroFactura;
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
        public void AgregarVenta(int NumeroFactura, int IDUsuario)
        {
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_AgregarVenta");
                datos.AgregarParametro("@NumeroFactura", NumeroFactura);
                datos.AgregarParametro("@IDUsuario", IDUsuario);
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
        public void AgregarVentaAXV(ElementoCarrito elemento)
        {
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_AgregarVentaAXV");
                datos.AgregarParametro("@IDArticulo", elemento.Articulo.ID_Articulo);
                datos.AgregarParametro("@Cantidad", elemento.Cantidad);
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
        public void ComprarArticulo(ElementoCarrito elemento)
        {
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_ComprarArticulo");
                datos.AgregarParametro("@IDArticulo", elemento.Articulo.ID_Articulo);
                datos.AgregarParametro("@Stock", elemento.Cantidad);
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
        public List<Transaccion> ListarVentas()
        {
            List<Transaccion> listaVentas = new List<Transaccion>();
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_VerTransacciones");
                datos.ConectarDB();
                datos.PrepararLector();
                SqlDataReader datosLeidos;
                Transaccion venta;
                while (datos.Leer())
                {
                    venta = new Transaccion();
                    datosLeidos = datos.Lectura();
                    venta.NumeroFactura = -1;
                    if (!Convert.IsDBNull(datosLeidos["NumeroFactura"]))
                        venta.NumeroFactura = Convert.ToInt32(datosLeidos["NumeroFactura"]);
                    venta.Articulo.Articulo.ID_Articulo = -1;
                    if (!Convert.IsDBNull(datosLeidos["IDArticulo"]))
                        venta.Articulo.Articulo.ID_Articulo = Convert.ToInt32(datosLeidos["IDArticulo"]);
                    venta.Articulo.Articulo.Nombre = "Nombre";
                    if (!Convert.IsDBNull(datosLeidos["Nombre"]))
                        venta.Articulo.Articulo.Nombre = Convert.ToString(datosLeidos["Nombre"]);
                    venta.Articulo.Cantidad = -1;
                    if (!Convert.IsDBNull(datosLeidos["Cantidad"]))
                        venta.Articulo.Cantidad = Convert.ToInt32(datosLeidos["Cantidad"]);
                    venta.FechaAccion = "Nunca";
                    if (!Convert.IsDBNull(datosLeidos["Fecha"]) && !Convert.IsDBNull(datosLeidos["Hora"]))
                        venta.FechaAccion = Convert.ToString(datosLeidos["Fecha"])+", "+ Convert.ToString(datosLeidos["Hora"]);
                    venta.Email = "Email";
                    if (!Convert.IsDBNull(datosLeidos["Email"]))
                        venta.Email = Convert.ToString(datosLeidos["Email"]);
                    venta.Telefono = -1;
                    if (!Convert.IsDBNull(datosLeidos["Telefono"]))
                        venta.Telefono = Convert.ToInt32(datosLeidos["Telefono"]);
                    venta.Nombres = "Nombres";
                    if (!Convert.IsDBNull(datosLeidos["Nombres"]))
                        venta.Nombres = Convert.ToString(datosLeidos["Nombres"]);
                    venta.Apellidos = "Apellidos";
                    if (!Convert.IsDBNull(datosLeidos["Apellidos"]))
                        venta.Apellidos = Convert.ToString(datosLeidos["Apellidos"]);
                    venta.DNI = -1;
                    if (!Convert.IsDBNull(datosLeidos["DNI"]))
                        venta.DNI = Convert.ToInt32(datosLeidos["DNI"]);
                    venta.Estado.Codigo = -1;
                    if (!Convert.IsDBNull(datosLeidos["IDEstado"]))
                        venta.Estado.Codigo = Convert.ToInt32(datosLeidos["IDEstado"]);
                    venta.Estado.Descripcion = "Estado";
                    if (!Convert.IsDBNull(datosLeidos["Estado"]))
                        venta.Estado.Descripcion = Convert.ToString(datosLeidos["Estado"]);
                    venta.Domicilio.Provincia = "Provincia";
                    if (!Convert.IsDBNull(datosLeidos["Provincia"]))
                        venta.Domicilio.Provincia = Convert.ToString(datosLeidos["Provincia"]);
                    venta.Domicilio.Ciudad = "Ciudad";
                    if (!Convert.IsDBNull(datosLeidos["Ciudad"]))
                        venta.Domicilio.Ciudad = Convert.ToString(datosLeidos["Ciudad"]);
                    venta.Domicilio.Calle = "Calle";
                    if (!Convert.IsDBNull(datosLeidos["Calle"]))
                        venta.Domicilio.Calle = Convert.ToString(datosLeidos["Calle"]);
                    venta.Domicilio.Numero = -1;
                    if (!Convert.IsDBNull(datosLeidos["Numero"]))
                        venta.Domicilio.Numero = Convert.ToInt32(datosLeidos["Numero"]);
                    venta.Domicilio.Piso = "Piso";
                    if (!Convert.IsDBNull(datosLeidos["Piso"]))
                        venta.Domicilio.Piso = Convert.ToString(datosLeidos["Piso"]);
                    venta.Domicilio.CodigoPostal = -1;
                    if (!Convert.IsDBNull(datosLeidos["CP"]))
                        venta.Domicilio.CodigoPostal = Convert.ToInt32(datosLeidos["CP"]);
                    venta.Domicilio.Departamento = "Depto";
                    if (!Convert.IsDBNull(datosLeidos["Depto"]))
                        venta.Domicilio.Departamento = Convert.ToString(datosLeidos["Depto"]);
                    venta.Domicilio.Referencia = "Referencia";
                    if (!Convert.IsDBNull(datosLeidos["Referencia"]))
                        venta.Domicilio.Referencia = Convert.ToString(datosLeidos["Referencia"]);
                    listaVentas.Add(venta);
                }
                return listaVentas;
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
        public List<Transaccion> ListarCompras(Usuario usuario)
        {
            List<Transaccion> listaCompras = new List<Transaccion>();
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_VerTransaccionesDe");
                datos.AgregarParametro("@IDUsuario", usuario.ID_Usuario);
                datos.ConectarDB();
                datos.PrepararLector();
                SqlDataReader datosLeidos;
                Transaccion venta;
                while (datos.Leer())
                {
                    venta = new Transaccion();
                    datosLeidos = datos.Lectura();
                    venta.ID_Usuario = -1;
                    if (!Convert.IsDBNull(datosLeidos["IDUsuario"]))
                        venta.ID_Usuario = Convert.ToInt32(datosLeidos["IDUsuario"]);
                    venta.NumeroFactura = -1;
                    if (!Convert.IsDBNull(datosLeidos["NumeroFactura"]))
                        venta.NumeroFactura = Convert.ToInt32(datosLeidos["NumeroFactura"]);
                    venta.Articulo.Articulo.ID_Articulo = -1;
                    if (!Convert.IsDBNull(datosLeidos["IDArticulo"]))
                        venta.Articulo.Articulo.ID_Articulo = Convert.ToInt32(datosLeidos["IDArticulo"]);
                    venta.Articulo.Articulo.Nombre = "Nombre";
                    if (!Convert.IsDBNull(datosLeidos["Nombre"]))
                        venta.Articulo.Articulo.Nombre = Convert.ToString(datosLeidos["Nombre"]);
                    venta.Articulo.Cantidad = -1;
                    if (!Convert.IsDBNull(datosLeidos["Cantidad"]))
                        venta.Articulo.Cantidad = Convert.ToInt32(datosLeidos["Cantidad"]);
                    venta.FechaAccion = "Nunca";
                    if (!Convert.IsDBNull(datosLeidos["Fecha"]) && !Convert.IsDBNull(datosLeidos["Hora"]))
                        venta.FechaAccion = Convert.ToString(datosLeidos["Fecha"]) + ", " + Convert.ToString(datosLeidos["Hora"]);
                    venta.Email = "Email";
                    if (!Convert.IsDBNull(datosLeidos["Email"]))
                        venta.Email = Convert.ToString(datosLeidos["Email"]);
                    venta.Telefono = -1;
                    if (!Convert.IsDBNull(datosLeidos["Telefono"]))
                        venta.Telefono = Convert.ToInt32(datosLeidos["Telefono"]);
                    venta.Nombres = "Nombres";
                    if (!Convert.IsDBNull(datosLeidos["Nombres"]))
                        venta.Nombres = Convert.ToString(datosLeidos["Nombres"]);
                    venta.Apellidos = "Apellidos";
                    if (!Convert.IsDBNull(datosLeidos["Apellidos"]))
                        venta.Apellidos = Convert.ToString(datosLeidos["Apellidos"]);
                    venta.DNI = -1;
                    if (!Convert.IsDBNull(datosLeidos["DNI"]))
                        venta.DNI = Convert.ToInt32(datosLeidos["DNI"]);
                    venta.Estado.Codigo = -1;
                    if (!Convert.IsDBNull(datosLeidos["IDEstado"]))
                        venta.Estado.Codigo = Convert.ToInt32(datosLeidos["IDEstado"]);
                    venta.Estado.Descripcion = "Estado";
                    if (!Convert.IsDBNull(datosLeidos["Estado"]))
                        venta.Estado.Descripcion = Convert.ToString(datosLeidos["Estado"]);
                    venta.Domicilio.Provincia = "Provincia";
                    if (!Convert.IsDBNull(datosLeidos["Provincia"]))
                        venta.Domicilio.Provincia = Convert.ToString(datosLeidos["Provincia"]);
                    venta.Domicilio.Ciudad = "Ciudad";
                    if (!Convert.IsDBNull(datosLeidos["Ciudad"]))
                        venta.Domicilio.Ciudad = Convert.ToString(datosLeidos["Ciudad"]);
                    venta.Domicilio.Calle = "Calle";
                    if (!Convert.IsDBNull(datosLeidos["Calle"]))
                        venta.Domicilio.Calle = Convert.ToString(datosLeidos["Calle"]);
                    venta.Domicilio.Numero = -1;
                    if (!Convert.IsDBNull(datosLeidos["Numero"]))
                        venta.Domicilio.Numero = Convert.ToInt32(datosLeidos["Numero"]);
                    venta.Domicilio.Piso = "Piso";
                    if (!Convert.IsDBNull(datosLeidos["Piso"]))
                        venta.Domicilio.Piso = Convert.ToString(datosLeidos["Piso"]);
                    venta.Domicilio.CodigoPostal = -1;
                    if (!Convert.IsDBNull(datosLeidos["CP"]))
                        venta.Domicilio.CodigoPostal = Convert.ToInt32(datosLeidos["CP"]);
                    venta.Domicilio.Departamento = "Depto";
                    if (!Convert.IsDBNull(datosLeidos["Depto"]))
                        venta.Domicilio.Departamento = Convert.ToString(datosLeidos["Depto"]);
                    venta.Domicilio.Referencia = "Referencia";
                    if (!Convert.IsDBNull(datosLeidos["Referencia"]))
                        venta.Domicilio.Referencia = Convert.ToString(datosLeidos["Referencia"]);
                    listaCompras.Add(venta);
                }
                return listaCompras;
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
        public List<Transaccion> ListarVentasAdmin()
        {
            List<Transaccion> listaVentas = new List<Transaccion>();
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.StoreProcedure("SP_VerTransaccionesAdmin");
                datos.ConectarDB();
                datos.PrepararLector();
                SqlDataReader datosLeidos;
                Transaccion venta;
                while (datos.Leer())
                {
                    venta = new Transaccion();
                    datosLeidos = datos.Lectura();
                    venta.NumeroFactura = -1;
                    if (!Convert.IsDBNull(datosLeidos["NumeroFactura"]))
                        venta.NumeroFactura = Convert.ToInt32(datosLeidos["NumeroFactura"]);
                    venta.Articulo.Articulo.ID_Articulo = -1;
                    if (!Convert.IsDBNull(datosLeidos["IDArticulo"]))
                        venta.Articulo.Articulo.ID_Articulo = Convert.ToInt32(datosLeidos["IDArticulo"]);
                    venta.Articulo.Articulo.Nombre = "Nombre";
                    if (!Convert.IsDBNull(datosLeidos["Nombre"]))
                        venta.Articulo.Articulo.Nombre = Convert.ToString(datosLeidos["Nombre"]);
                    venta.Articulo.Cantidad = -1;
                    if (!Convert.IsDBNull(datosLeidos["Cantidad"]))
                        venta.Articulo.Cantidad = Convert.ToInt32(datosLeidos["Cantidad"]);
                    venta.FechaAccion = "Nunca";
                    if (!Convert.IsDBNull(datosLeidos["Fecha"]) && !Convert.IsDBNull(datosLeidos["Hora"]))
                        venta.FechaAccion = Convert.ToString(datosLeidos["Fecha"]) + ", " + Convert.ToString(datosLeidos["Hora"]);
                    venta.Email = "Email";
                    if (!Convert.IsDBNull(datosLeidos["Email"]))
                        venta.Email = Convert.ToString(datosLeidos["Email"]);
                    venta.Telefono = -1;
                    if (!Convert.IsDBNull(datosLeidos["Telefono"]))
                        venta.Telefono = Convert.ToInt32(datosLeidos["Telefono"]);
                    venta.Nombres = "Nombres";
                    if (!Convert.IsDBNull(datosLeidos["Nombres"]))
                        venta.Nombres = Convert.ToString(datosLeidos["Nombres"]);
                    venta.Apellidos = "Apellidos";
                    if (!Convert.IsDBNull(datosLeidos["Apellidos"]))
                        venta.Apellidos = Convert.ToString(datosLeidos["Apellidos"]);
                    venta.DNI = -1;
                    if (!Convert.IsDBNull(datosLeidos["DNI"]))
                        venta.DNI = Convert.ToInt32(datosLeidos["DNI"]);
                    venta.Estado.Codigo = -1;
                    if (!Convert.IsDBNull(datosLeidos["IDEstado"]))
                        venta.Estado.Codigo = Convert.ToInt32(datosLeidos["IDEstado"]);
                    venta.Estado.Descripcion = "Estado";
                    if (!Convert.IsDBNull(datosLeidos["Estado"]))
                        venta.Estado.Descripcion = Convert.ToString(datosLeidos["Estado"]);
                    venta.Domicilio.Provincia = "Provincia";
                    if (!Convert.IsDBNull(datosLeidos["Provincia"]))
                        venta.Domicilio.Provincia = Convert.ToString(datosLeidos["Provincia"]);
                    venta.Domicilio.Ciudad = "Ciudad";
                    if (!Convert.IsDBNull(datosLeidos["Ciudad"]))
                        venta.Domicilio.Ciudad = Convert.ToString(datosLeidos["Ciudad"]);
                    venta.Domicilio.Calle = "Calle";
                    if (!Convert.IsDBNull(datosLeidos["Calle"]))
                        venta.Domicilio.Calle = Convert.ToString(datosLeidos["Calle"]);
                    venta.Domicilio.Numero = -1;
                    if (!Convert.IsDBNull(datosLeidos["Numero"]))
                        venta.Domicilio.Numero = Convert.ToInt32(datosLeidos["Numero"]);
                    venta.Domicilio.Piso = "Piso";
                    if (!Convert.IsDBNull(datosLeidos["Piso"]))
                        venta.Domicilio.Piso = Convert.ToString(datosLeidos["Piso"]);
                    venta.Domicilio.CodigoPostal = -1;
                    if (!Convert.IsDBNull(datosLeidos["CP"]))
                        venta.Domicilio.CodigoPostal = Convert.ToInt32(datosLeidos["CP"]);
                    venta.Domicilio.Departamento = "Depto";
                    if (!Convert.IsDBNull(datosLeidos["Depto"]))
                        venta.Domicilio.Departamento = Convert.ToString(datosLeidos["Depto"]);
                    venta.Domicilio.Referencia = "Referencia";
                    if (!Convert.IsDBNull(datosLeidos["Referencia"]))
                        venta.Domicilio.Referencia = Convert.ToString(datosLeidos["Referencia"]);
                    listaVentas.Add(venta);
                }
                return listaVentas;
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
