using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Faro.Datos;
using Faro.Entidades;
using Faro.Presentacion;
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
    public partial class FrmRegistraEmp : Form
    {

        DatosF datos = new DatosF();



        public FrmRegistraEmp()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Se validan los datos 
            try
            {
                // se valida cada entrada 

                // se realizan los respectivos parseos 
                // una vez los datos son validadoss se guardan en la lista
                //(DateTime n, DateTime I, Char g, string ape1, string ape2, string nom, string ced)
                if (Validador.validarChar(txtNombre.Text, txtNombre) &&
             Validador.validarChar(txtApe1.Text, txtApe1) &&
             Validador.validarChar(txtApe2.Text, txtApe2) &&
             Validador.validarNum(txtID.Text, txtID)
              )
                {
                    if (txtID.Text.Trim().Length != 9)
                    {
                        throw new Exception("Las identificaciones deben contener nueve numberos");
                    }
                    else
                    {
                        // se realizan los respectivos parseos 
                        // una vez los datos son validadoss se guardan en la lista
                        //(DateTime n, DateTime I, Char g, string ape1, string ape2, string nom, string ced)
                        Vendedor nuevoRegistro = new Vendedor(dateNacimiento.Value, dateIngreso.Value, 'p', txtApe1.Text, txtApe2.Text, txtNombre.Text, txtID.Text);



                        datos.AgregarVendedor(nuevoRegistro);

                        MessageBox.Show("Datos correctamente ingresados");

                        txtNombre.Text = "";
                        txtApe1.Text = "";
                        txtApe2.Text = "";
                        txtID.Text = "";

                    }

                }//FIN IF 


            }
            catch (Exception error)
            {
                MessageBox.Show("Error :" + error);

            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
