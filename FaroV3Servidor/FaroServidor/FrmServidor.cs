using System;

using System.Drawing;

using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <TAREA 3 UNED PROGRAMACION AVANZADA>
/// NOMBRE :EDGAR ANTONIO JACOB MORENO 
/// CARNE/CEDULA : 0117660514
/// PUESTO DE VENTA FARO V.3 
/// 
/// 
/// 
/// 
/// 
/// EJEMPLOS / ALGOTITMOS TOMADOS DE LAS SIGUIENTES FUENTES 
/// 
/// YOUTUBE : 
/// Generalidades del uso de Forms y componentes GUI
/// https://www.youtube.com/watch?v=lY1Z0Hgo288&t=2195s
/// 
/// Uso del Data Grid View
/// https://www.youtube.com/watch?v=WVLL1xa6Ryo&t=270s
/// 
/// 
/// Comunicacion de datos entre forms
/// https://www.youtube.com/watch?v=t-4caAZKLJw
/// 
/// barra de progreso
/// https://www.youtube.com/watch?v=CKBZSzK2-TI
/// 
/// Manejo de datos desde c# y SQL Server 
/// https://www.youtube.com/watch?v=F8ou8NMpF0w
/// 
/// 
/// 
/// 
/// 
/// STACK OVER FLOW:
/// 
/// parseos
/// https://stackoverflow.com/questions/39876056/why-do-i-get-the-error-cant-convert-system-int32-to-system-string-while-i-am-n
/// 
/// barra de progreso
/// https://stackoverflow.com/questions/31381800/use-progress-bar-in-c-sharp
/// 
/// MICROSOFT DOCS
/// parseos 
/// https://docs.microsoft.com/en-us/dotnet/api/system.decimal.tostring?view=net-5.0
/// 
/// 
/// propiedades del combobox 
/// https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.combobox?view=windowsdesktop-5.0
/// 
/// 
/// uso de el tipo DATE TIME
/// https://docs.microsoft.com/en-us/dotnet/api/system.datetime.-ctor?view=net-5.0
/// 
/// TUTORIA 1 Impartida por mi tutor correspondiente 
/// Generalidades y componentes de gui 
/// generalidades y logica de funcionamiento de visual studio 
/// 
/// 
/// /// TUTORIA 2 Impartida por mi tutor correspondiente 
/// Generalidades del manejo de base de datos 
/// generalidades y logica de funcionamiento de la conxion con una base de datos en SQL Server con C#
/// generalidades del manejo de un proyecto en arquitectura por capas 
/// 
/// 
/// 
/// TUTORIA 3 GRabacion encontrada en el entorno virtual UNED 
/// Arquitectura cliente servidor 
/// comunicacion cliente servidor 
/// serializacion jason 
/// 
/// 
/// 
/// PROBLEMAS QUE PUEDE PRESENTAR LA APP PARA FUTURAS CORRECCIONES 
/// NO REALIZA VENTAS 
/// SI SE DESCONECTA NO DISMINUYE EL NUM DE CLIENTES EN SERVIDOR 
/// ADEMAS DE LA SOLICITUD DE CONEXION EL CLIENTE NO REALIZA EFECTIVAMENTE OTRA SOLICITUD 
/// 
/// 
/// </summary>


namespace FaroServidor
{
    public partial class FrmServidor : Form
    {


        public static FrmServidor newFrmServidor;
        Conexion ventanaServer = new Conexion();
        public Label stad;
        public Label clientes;
        bool flag = true;
 //Metodos 
//________________________________________________________________________________________________________________________________________________________________________



           
        // ABRIR FORMS
        public void abrirForm(object formH)
        {
            

            if (this.frmContenedorFrm.Controls.Count > 0)
                this.frmContenedorFrm.Controls.RemoveAt(0);
            Form fh = formH as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.frmContenedorFrm.Controls.Add(fh);
            this.frmContenedorFrm.Tag = fh;
            fh.Show();


        }
        public FrmServidor()
        {

           
            InitializeComponent();
            lblEstado.ForeColor = Color.Red;
            newFrmServidor = this;
            stad=this.lblEstado;
            clientes = this.lblClientes;
          
        }







//________________________________________________________________________________________________________________________________________________________________________


        //Botones que abren Forms

        private void btnRegistrarArt_Click(object sender, EventArgs e)
        {
            abrirForm(new FrmRegistraArt());
        }

        private void btnRegistrarEmp_Click(object sender, EventArgs e)
        {
            abrirForm(new FrmRegistraEmp());
        }

        private void btnActualizaInv_Click(object sender, EventArgs e)
        {
            abrirForm(new FrmActualizaArt());
        }

        private void btnConsultaVent_Click(object sender, EventArgs e)
        {
            abrirForm(new FrmConVentas());
        }

        private void btnConsultaInv_Click(object sender, EventArgs e)
        {
            abrirForm(new FrmConInv());
        }

        public void servidor(object v) {
            CheckForIllegalCrossThreadCalls = false;
            btnInicio.Enabled = true;

            if (flag)
            {

                flag = false;
                if (!ventanaServer.IsDisposed)
                {

                    btnInicio.Enabled = false;
                    ventanaServer.ShowDialog();

                }
                else {
                    Conexion ventanaServer = new Conexion();
                    ventanaServer.ShowDialog();


                }

            }
            else {
                ventanaServer.Dispose();
                




                flag = true;

            }
            btnInicio.Enabled = true;
            CheckForIllegalCrossThreadCalls = true;
            

        }

        //  CONROLES FORM PRINCIPAL
        private void btnInicio_Click(object sender, EventArgs e)
        {




     

                Thread server = new Thread(servidor);
                server.Start(ventanaServer);
                server.IsBackground = true;
                
        
          




        }




    }
}
