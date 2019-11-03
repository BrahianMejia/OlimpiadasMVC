using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controlador.Paises;

namespace MVC_Vista_
{
    public partial class Form1 : Form
    {

        PaisesDTO paisesDTO = null;
        PaisesDAO paisesDAO = null;
        DataTable Dtt = null;

        public Form1()
        {
            InitializeComponent();
            ListarPaises();
            btneliminar.Enabled = false;
            btnguardarcambiospais.Enabled = false;
            btncancelar.Enabled = false;
        }

        public void ListarPaises()
        {

            paisesDTO = new PaisesDTO();

            paisesDAO = new PaisesDAO(paisesDTO);

            Dtt = new DataTable();
            Dtt = paisesDAO.ListarPaises();

            if (Dtt.Rows.Count > 0)
            {
                dtpaises.DataSource = Dtt;
            }
            else
            {
                MessageBox.Show("No hay registros de Paises");
            }
        }

        public void Guardar()
        {
            paisesDTO = new PaisesDTO();
            paisesDTO.setNombrepais(txtnombrepais.Text);
            paisesDAO = new PaisesDAO(paisesDTO);

            paisesDAO.GuardarPais();
            MessageBox.Show("Registro guardado", "Mensaje");
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            if (txtnombrepais.Text.Trim() == "")
            {
                MessageBox.Show("Intenta un dato válido.", "Mensaje");
                txtnombrepais.Focus();
            }
            else
            {
                Guardar();
                ListarPaises();
                txtnombrepais.Clear();
                txtnombrepais.Focus();
            }
        }

        private void dtpaises_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label4.Visible = true;
            txtcodigo.Visible = true;

            txtcodigo.Text = dtpaises.Rows[dtpaises.CurrentRow.Index].Cells[0].Value.ToString();
            txtnombrepais.Text = dtpaises.Rows[dtpaises.CurrentRow.Index].Cells[1].Value.ToString();

            btnguardar.Enabled = false;
            txtcodigo.Enabled = false;
            btnguardarcambiospais.Enabled = true;
            btncancelar.Enabled = true;
            btneliminar.Enabled = true;
        }

        public void GuardarCambios()
        {
            paisesDTO = new PaisesDTO();
            paisesDTO.setIdpais(Convert.ToInt16(txtcodigo.Text));
            paisesDTO.setNombrepais(txtnombrepais.Text);
            paisesDAO = new PaisesDAO(paisesDTO);

            paisesDAO.GuardarCambiosPais();
            MessageBox.Show("Registro actualizado", "Mensaje");
        }

        private void btnguardarcambiospais_Click(object sender, EventArgs e)
        {

            if (txtnombrepais.Text.Trim() == "")
            {
                MessageBox.Show("Intenta un dato válido", "Mensaje");
                txtnombrepais.Focus();
            }
            else
            {
                GuardarCambios();
                ListarPaises();

                label4.Visible = false;
                txtcodigo.Visible = false;
                btnguardar.Enabled = true;
                btnguardarcambiospais.Enabled = false;
                btneliminar.Enabled = false;
                btncancelar.Enabled = false;
                txtnombrepais.Clear();
                txtnombrepais.Focus();
            }
            
        }

        public void EliminarRegistroPais()
        {
            paisesDTO = new PaisesDTO();
            paisesDTO.setIdpais(Convert.ToInt16(txtcodigo.Text));
            paisesDAO = new PaisesDAO(paisesDTO);

            paisesDAO.EliminarPais();
            MessageBox.Show("Registro eliminado", "Mensaje");
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistroPais();
            ListarPaises();
            btnguardar.Enabled = true;
            btnguardarcambiospais.Enabled = true;
            btneliminar.Enabled = true;
            btncancelar.Enabled = true;

        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            ListarPaises();

            label4.Visible = false;
            txtcodigo.Visible = false;
            btnguardar.Enabled = true;
            btnguardarcambiospais.Enabled = false;
            btneliminar.Enabled = false;
            btncancelar.Enabled = false;
            txtnombrepais.Clear();
            txtnombrepais.Focus();
        }
    }
}
