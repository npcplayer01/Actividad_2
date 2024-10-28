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
        string accion = "Nuevo";

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
            dt = ds.Tables["Peliculas"];
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

        private void habDeshBotones()
        {
            if (posicion == 0)
            {
                pbPrincipio.Enabled = false;
                pbAtras.Enabled = false;
                pbSiguiente.Enabled = true;
                pbFinal.Enabled = true;
            }
            else if (posicion >= dt.Rows.Count - 1)
            {
                pbSiguiente.Enabled = false;
                pbFinal.Enabled = false;
                pbPrincipio.Enabled = true;
                pbAtras.Enabled = true;
            }
            else
            {
                pbPrincipio.Enabled = true;
                pbAtras.Enabled = true;
                pbSiguiente.Enabled = true;
                pbFinal.Enabled = true;
            }

        }
        private void pbSiguiente_Click(object sender, EventArgs e)
        {
            if (posicion < dt.Rows.Count - 1)
            {
                posicion += 1;
                mostrarDatos();

                pbAtras.Enabled = true;
                pbPrincipio.Enabled = true;
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

        private void limpiarCajas()
        {
            txtTitulo.Text = "";
            txtAutor.Text = "";
            txtSinopsis.Text = "";
            txtDuracion.Text = "";
            txtClasificacion.Text = "";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (btnNuevo.Text == "Nuevo")
            {
                btnNuevo.Text = "Guardar";
                btnModificar.Text = "Cancelar";
                limpiarCajas();
                accion = "Nuevo";
            }
            else
            {
                String[] datos = {
                    accion,
                    dt.Rows[posicion].ItemArray[1].ToString(),
                    txtTitulo.Text,
                    txtAutor.Text,
                    txtSinopsis.Text,
                    txtDuracion.Text,
                    txtClasificacion.Text
                };

                string respuesta = ObjConexion.administrarPeliculas(datos);
                if (respuesta != "1")
                {
                    MessageBox.Show("Error: " + respuesta, "Eliminando datos de pelicula", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    obtenerDatos();

                }



                btnNuevo.Text = "Nuevo";
                btnModificar.Text = "Modificar";
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (btnModificar.Text == "Modificar")
            {
                btnModificar.Text = "Cancelar";
                btnNuevo.Text = "Guardar";
                accion = "Modificar";
            }
            else
            {
                mostrarDatos();
                btnModificar.Text = "Modificar";
                btnNuevo.Text = "Nuevo";
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de eliminar a: " + txtTitulo.Text, "Eliminando peliculas",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                String[] datos = {
                    "Eliminar", dt.Rows[posicion].ItemArray[0].ToString(),
                };
                String response = ObjConexion.administrarPeliculas(datos);
                if (response != "1")
                {
                    MessageBox.Show("Error: " + response, "Eliminando datos de pelicula", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    obtenerDatos();
                }
            }
        }
    }
}