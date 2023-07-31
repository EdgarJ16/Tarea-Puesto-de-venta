using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FaroCliente;

namespace FaroServidor
{
    public partial class FrmCliente : Form
    {

        //Control de hilos

        //se crea un hilo que orquesta la conexion con el servidor en back ground 
        Thread orquestadorConexion;


        public static FrmCliente frmCliente;

        public Label nom;
        
        public FrmCliente()
        {
            InitializeComponent();
            frmCliente = this;
            nom = this.lblNombre;
        
        }
        // ABRIR FORMS
        private void abrirForm(object formH) {
            if (this.frmContenedorFrm.Controls.Count > 0)
                this.frmContenedorFrm.Controls.RemoveAt(0);
            Form fh = formH as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.frmContenedorFrm.Controls.Add(fh);
            this.frmContenedorFrm.Tag = fh;
            fh.Show();
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            // SE REALIZA LA LOGICA PARA QUE EL BOTON SOLO PUEDA SER 
            orquestadorConexion = new Thread(abreform) ;
           

         

                orquestadorConexion.Start();
                orquestadorConexion.IsBackground = true;
         
            
    

           
        }

        private void abreform()
        {

            //se resetea la cuenta porque se asume que ya se ario el frm conexion
            CheckForIllegalCrossThreadCalls = false;
            
            btnInicio.Enabled = false;
            CheckForIllegalCrossThreadCalls = true;

            Form fh = new FrmInicio();

            fh.ShowDialog();



            if (!fh.IsDisposed)
            {


                CheckForIllegalCrossThreadCalls = false;
                btnConsulta.Enabled = false;
                btnVenta.Enabled = false;
                btnInicio.Enabled = true;
                CheckForIllegalCrossThreadCalls = true;



            }
        }


            private void button2_Click(object sender, EventArgs e)
        {
            abrirForm(new FrmVenta());
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            abrirForm(new FrmConsultaVentas());
        }
    }
}
