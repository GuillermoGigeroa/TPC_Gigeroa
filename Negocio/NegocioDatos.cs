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
        private Datos datos;
        public NegocioDatos()
        {
            datos = new Datos();
            datos.ConfigurarConexion();
        }
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
        public List<Articulo> ListarArticulosSinCategorias()
        {
            List<Articulo> listado = new List<Articulo>();
            try
            {
                datos.StoreProcedure("SP_ListarArticulosSinCategoria");
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
                datos.ConectarDB();
                datos.PrepararLector();
                SqlDataReader datosLeidos;
                Articulo articulo;
                while (datos.Leer())
                {
                    datosLeidos = datos.Lectura();
                    //Se verifica si el articulo está activo o fue dado de baja para continuar
                    if(Convert.ToBoolean(datosLeidos["Estado"]))
                    {
                        articulo = new Articulo();

                        articulo.ID_Articulo = -1;
                        if (!Convert.IsDBNull(datosLeidos["ID_Articulo"]))
                            articulo.ID_Articulo = (int)datosLeidos["ID_Articulo"];
                        articulo.Nombre = "Error";
                        if (!Convert.IsDBNull(datosLeidos["Nombre"]))
                            articulo.Nombre = (string)datosLeidos["Nombre"];
                        articulo.Marca.ID_Marca = -1;
                        if (!Convert.IsDBNull(datosLeidos["ID_Marca"]))
                            articulo.Marca.ID_Marca = (int)datosLeidos["ID_Marca"];
                        articulo.Marca.Nombre = "Error";
                        if (!Convert.IsDBNull(datosLeidos["Marca"]))
                            articulo.Marca.Nombre = (string)datosLeidos["Marca"];
                        articulo.Descripcion = "Error";
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
                datos.DesconectarDB();
            }
        }
        public List<Categoria> BuscarCategorias(int identificador)
        {
            List<Categoria> categorias = new List<Categoria>();
            try
            {
                datos.StoreProcedure("SP_BuscarCategoriasDelArticulo " + identificador);
                /*
                Trae los siguientes datos (sin datos de categoría):
                    > ID_Categoría
                    > Nombre
                    > Activo
                */
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
                            categoria.ID_Categoria = (int)datosLeidos["ID_Categoria"];
                        categoria.Nombre = "Error";
                        if (!Convert.IsDBNull(datosLeidos["Categoria"]))
                            categoria.Nombre = (string)datosLeidos["Categoria"];
                        categoria.Activo = true;
                        if (!Convert.IsDBNull(datosLeidos["Activo"]))
                            categoria.Activo = (bool)datosLeidos["Activo"];
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
                datos.DesconectarDB();
            }
        }
    }
}
