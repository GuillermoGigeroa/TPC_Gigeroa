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
            List<Articulo> listado = new List<Articulo>();
            try
            {
                Datos datos = new Datos();
                datos.ConfigurarConexion();
                datos.IngresarComando("INGRESE COMANDO PARA TRAER INFORMACION");
                datos.ConectarDB();
                datos.PrepararLectura();
                SqlDataReader datosLeidos;
                Articulo articulo;
                while (datos.Leer())
                {
                    datosLeidos = datos.Lectura();
                    articulo = new Articulo();
                    if (!Convert.IsDBNull(datosLeidos["ID"]))
                    {
                        articulo.ID_Articulo = (int)datosLeidos["Id"];
                    }
                    else
                    {
                        articulo.ID_Articulo = -1;
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
    }
}
