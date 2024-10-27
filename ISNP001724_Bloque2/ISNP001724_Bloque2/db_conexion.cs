using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ISNP001724_Bloque2
{
    internal class db_conexion
    {
        SqlConnection miConexion = new SqlConnection();
        SqlCommand miComando = new SqlCommand();
        SqlDataAdapter miAdaptador = new SqlDataAdapter();
        DataSet ds = new DataSet();

        public db_conexion()
        {
            miConexion.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\peliculas.mdf;Integrated Security=True";
            miConexion.Open();
        }
        public DataSet obtenerDatos() {
            ds.Clear();
            miComando.Connection = miConexion;
            miComando.CommandText = "SELECT * FROM peliculas";
            miAdaptador.SelectCommand = miComando;
            miAdaptador.Fill(ds, "peliculas");

            return ds; }
    }

    
}
