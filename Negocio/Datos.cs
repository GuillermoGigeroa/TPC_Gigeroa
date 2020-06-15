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
        public SqlDataReader lector;
        public SqlConnection conexion;
        public SqlCommand comando;
        public Datos()
        {
            conexion = new SqlConnection();
            comando = new SqlCommand();
            comando.Connection = conexion;
        }
        public void ConfigurarConexion()
        {
            conexion.ConnectionString = "data source=GUILLEGIGEROA\\SQLEXPRESS; initial catalog=GIGEROA_DB; integrated security=sspi";
        }
        public void Query(string query)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = query;
        }
        public void StoreProcedure(string sp)
        {
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = sp;
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
            conexion.Close();
        }
        public void PrepararLector()
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
