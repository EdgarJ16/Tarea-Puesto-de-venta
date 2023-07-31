using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Faro.Entidades;
using Faro.Entidades.ProgramTools;
using FaroServidor;

namespace FaroCliente
{
    public partial class FrmInicio : Form
    {
       
       
        public FrmInicio()
        {
            InitializeComponent();
        }


        //Delegados para cambiar el nombre

        EscribirEnlabelDelegado modificarLabel;

        private delegate void EscribirEnlabelDelegado(string texto);

        private void button1_Click(object sender, EventArgs e)
        {
            if (     Validador.validarNum(txtID.Text, txtID)   )
            {
                if (ConexionTCP.Conectar(txtID.Text))
                {
                    Vendedor emp = new Vendedor();



                    FrmCliente.frmCliente.nom.Invoke(modificarLabel, new object[] {
                    string.Format(" {0} {1} {2}", emp.nombre, emp.apellido1, emp.apellido2)});

                }
                else
                {
                    MessageBox.Show("Verifique que el servidor esté escuchando clientes...", "No es posible conectarse", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {                
                MessageBox.Show("Debe ingresar el identificador del cliente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }           
        
        }
    }
}
