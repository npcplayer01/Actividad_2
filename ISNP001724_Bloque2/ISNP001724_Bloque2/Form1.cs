using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISNP001724_Bloque2
{
    public partial class Form1 : Form
    {
        db_conexion ObjConexion = new db_conexion();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        public int posicion = 0;
        string accion = "nuevo";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            obtenerDatos();
        }
        private void obtenerDatos()
        {
            ds = ObjConexion.obtenerDatos();
            dt = ds.Tables["peliculas"];
           dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };
            mostrarDatos();
        }
        private void mostrarDatos()
        {
            txtTitulo.Text = dt.Rows[posicion].ItemArray[1].ToString();
            txtAutor.Text = dt.Rows[posicion].ItemArray[2].ToString();
            txtSinopsis.Text = dt.Rows[posicion].ItemArray[3].ToString();
            txtDuracion.Text = dt.Rows[posicion].ItemArray[4].ToString();
            txtClasificacion.Text = dt.Rows[posicion].ItemArray[5].ToString();
            lblRegistro.Text = (posicion + 1) + " de " + dt.Rows.Count;
        }

        private void pbSiguiente_Click(object sender, EventArgs e)
        {
            if (posicion < dt.Rows.Count - 1)
            {
                posicion += 1;
                mostrarDatos();
                pbSiguiente.Enabled = true;
                pbFinal.Enabled = true;
            }
           else
           {
              pbSiguiente.Enabled = false;
                pbFinal.Enabled = false;
            }

        }

        private void pbFinal_Click(object sender, EventArgs e)
        {
            posicion = dt.Rows.Count - 1;
            mostrarDatos();
            pbAtras.Enabled = true;
            pbPrincipio.Enabled = true;
        }

        private void pbPrincipio_Click(object sender, EventArgs e)
        {
            posicion = 0;
            mostrarDatos();
            pbSiguiente.Enabled = true;
            pbFinal.Enabled = true;
        }

        private void pbAtras_Click(object sender, EventArgs e)
        {
            if (posicion > 0)
            {
                posicion -= 1;
                mostrarDatos();
                pbAtras.Enabled = true;
                pbPrincipio.Enabled = true;
            }
            else
            {
                pbAtras.Enabled = false;
                pbPrincipio.Enabled = false;
            }
        }
    }
}
