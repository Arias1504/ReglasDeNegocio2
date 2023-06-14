using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using libOpeMiCiudad;

namespace appPractica2
{
    public partial class frmMiCiudad : Form
    {
        public frmMiCiudad()
        {
            InitializeComponent();
        }

        #region "Metodos propios"
        private void Mensaje( string texto )
        {
            MessageBox.Show( texto, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        private void btnTerminar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            int intEst;
            float fltvD, fltkKw, fltkM3, fltkMin;
            try
            {
                //Captura de info
                intEst = this.cmbEstrato.SelectedIndex + 1;
                fltvD = Convert.ToSingle(this.txtVrDolar.Text);
                fltkKw = Convert.ToSingle(this.txtKw.Text);
                fltkM3 = Convert.ToSingle(this.txtM3.Text);
                fltkMin = Convert.ToSingle(this.txtMinut.Text);

                //Crear el objeto 
                clsOpeMiCiudad Op = new clsOpeMiCiudad();

                //Enviar info 
                Op.vrDolar = fltvD;
                Op.Estrato = intEst;
                Op.cantAgua = fltkM3;
                Op.cantEnergia = fltkKw;
                Op.cantTelefono = fltkMin;

                if(!Op.Facturar())
                {
                    Mensaje(Op.Error);
                    Op = null;
                    return;
                }

                //Recuperar y mostrar info
                this.lblEnergia.Text = Op.vrEnergia.ToString();
                this.lblAgua.Text = Op.vrAgua.ToString();
                this.lblTel.Text = Op.vrTelefono.ToString();
                this.lblTotalAPag.Text = Op.totalAPagar.ToString();
                this.gpbAPagar.Visible = true;
                Op = null;
            }
            catch (Exception ex)
            {

                Mensaje (ex.Message);
            }
        }

        private void frmMiCiudad_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 6; i++)
                this.cmbEstrato.Items.Add(i);
        }
    }
}
