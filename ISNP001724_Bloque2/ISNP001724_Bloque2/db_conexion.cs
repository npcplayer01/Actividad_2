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
            miComando.CommandText = "SELECT * FROM Peliculas";
            miAdaptador.SelectCommand = miComando;
            miAdaptador.Fill(ds, "Peliculas");

            return ds; }
        public string administrarPeliculas(String[] Peliculas) { 
     String sql = "";
            if (Peliculas[0] == "Nuevo")
            {
                sql = "INSERT INTO Peliculas(titulo, Autor, Sinopsis, Duración, Clasificación) VALUES(" +
                    "'" + Peliculas[2] + "'," +
                    "'" + Peliculas[3] + "'," +
                    "'" + Peliculas[4] + "'," +
                    "'" + Peliculas[5] + "'," +
                    "'" + Peliculas[6] + "')";
            }
            else if (Peliculas[0] == "Modificar")
            {
                sql = "UPDATE Peliculas SET Titulo='" + Peliculas[2] + "', Autor='" + Peliculas[3] + "', " +
                    "Sinopsis='" + Peliculas[4] + "', Duración='" + Peliculas[5] + "', Clasificación='" + Peliculas[6] + "' WHERE id =" + Peliculas[1];

            }
            else if (Peliculas[0] == "Eliminar")
{
    sql = "DELETE FROM Peliculas WHERE id ='" + Peliculas[1] + "'";
}
return ejecutarSQL(sql);
        }
                private string ejecutarSQL(string sql)
        {
            try
            {
                miComando.Connection = miConexion;
                miComando.CommandText = sql;
                return miComando.ExecuteNonQuery().ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }

}
