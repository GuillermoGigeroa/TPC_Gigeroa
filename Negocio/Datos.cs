using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Negocio
{
    public class Datos
    {
        private readonly SqlConnection conexion;
        private readonly SqlCommand comando;
        private SqlDataReader lector;
        public Datos()
        {
            conexion = new SqlConnection();
            comando = new SqlCommand();
            comando.CommandType = System.Data.CommandType.Text;
        }
        public void ConfigurarConexion()
        {
            conexion.ConnectionString = "data source=GUILLEGIGEROA\\SQLEXPRESS; initial catalog=GIGEROA_DB; integrated security=sspi";
        }
        public void IngresarComando(string texto)
        {
            comando.Connection = conexion;
            comando.CommandText = texto;
        }
        public void ConectarDB()
        {
            conexion.Open();
        }
        public void Ejecutar()
        {
            comando.ExecuteNonQuery();
        }
        public void DesconectarDB()
        {
            ConfigurarConexion();
            conexion.Close();
        }
        public void PrepararLectura()
        {
            lector = comando.ExecuteReader();
        }
        public bool Leer()
        {
            return lector.Read();
        }
        public SqlDataReader Lectura()
        {
            return lector;
        }
        public void AgregarParametro(string nombre, object objeto)
        {
            comando.Parameters.AddWithValue(nombre, objeto);
        }
    }
}
