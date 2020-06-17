using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class NegocioDatos
    {
        public List<Articulo> ListarArticulos()
        {
            List<Articulo> listado = ListarArticulosSinCategorias();
            //Se debe cargar la lista de categorías por cada artículo
            foreach (var articulo in listado)
            {
                articulo.Categorias = BuscarCategorias(articulo.ID_Articulo);
            }
            return listado;
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
                /*
                Trae los siguientes datos (sin datos de categoría):
                    > ID_Articulo
                    > Nombre
                    > ID_Marca
                    > Marca
                    > Descripción
                    > EsMateriaPrima
                    > URL_Imagen
                    > Estado
                    > Precio
                */
                SqlDataReader datosLeidos;
                Articulo articulo;
                while (datos.Leer())
                {
                    datosLeidos = datos.Lectura();
                    articulo = new Articulo();
                    //Se verifica si el articulo está activo o fue dado de baja para continuar
                    if(Convert.ToBoolean(datosLeidos["Estado"]))
                    {
                        articulo.ID_Articulo = -1;
                        if (!Convert.IsDBNull(datosLeidos["ID_Articulo"]))
                            articulo.ID_Articulo = Convert.ToInt32(datosLeidos["ID_Articulo"]);
                        articulo.Nombre = "NombreArticulo";
                        if (!Convert.IsDBNull(datosLeidos["Nombre"]))
                            articulo.Nombre = (string)datosLeidos["Nombre"];
                        //Acá pincha con un Null reference Exception
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
                        //Se agrega a la lista
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
                        //Se agrega a la lista
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
                    //Se agrega a la lista
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
                    //Se agrega a la lista
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
    }
}
