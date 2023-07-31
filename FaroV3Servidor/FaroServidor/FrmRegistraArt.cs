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
    public partial class FrmRegistraArt : Form
    {


        DatosF datos = new DatosF();
        GenerarID otpArt = new GenerarID();
        static int[] ids = new int[100];



        public FrmRegistraArt()
        {
            InitializeComponent();
            otpArt.generarID();
            ids = otpArt.getID();
        }

        private void FrmRegistraArt_Load(object sender, EventArgs e)
        {
            
        }

        private void agregarArt_Click(object sender, EventArgs e)
        {
            try
            {
                //Se validan cada entrada
                if (Validador.validarNum(txtPrecioVenta.Text, txtPrecioVenta) == true &&
                    Validador.validarNum(precioFinal.Text, precioFinal) == true &&
                    Validador.validarNum(cantidadArt.Text, cantidadArt) == true &&
                    Validador.validarChar(txtDescripcion.Text, txtDescripcion) == true
                    )
                {

                    // Aqui se le asignan los datos al objeto articulo para despues meterlo en el arreglo
                    // se genera un id 

                    int id;
                    int cantidad;
                    decimal precioV;
                    decimal precioF;
                    string descripcion;
                    bool estado;
                    string aux;
                    // se hace el casteo de datos validados 

                    cantidad = Convert.ToInt32(cantidadArt.Text);
                    aux = precioFinal.Text;
                    precioF = decimal.Parse(aux);
                    aux = txtPrecioVenta.Text;
                    precioV = decimal.Parse(aux);

                    descripcion = txtDescripcion.Text;
                    // se optiene estado
                    aux = estadoArt.SelectedItem.ToString();
                    if (aux == "Activo")
                    {
                        estado = true; //Indica que el estado es ACTIVO
                    }
                    else
                    {
                        estado = false;
                    }



                    // El id se asigna cuando ya se meta dentro del  arreglo

                    Articulo nuevoArticulo = new Articulo(0, cantidad, precioF, precioV, estado, descripcion);


                    //Asignacion de IDS



                    otpArt.asignaId(nuevoArticulo);













                    //LA CONEXION LO LLEVA AL CATCH DE UNA VEZ (>TODAVIA NO SE PORQUE PASA<)
                    datos.AgregarArticulo(nuevoArticulo);
                    MessageBox.Show("Datos correctamente ingresados");

                    // Se limpian los datos 

                    txtPrecioVenta.Text = "";
                    txtPrecioVenta.Text = "";
                    precioFinal.Text = "";
                    cantidadArt.Text = "";
                    txtDescripcion.Text = "";


                }
                else
                {
                    MessageBox.Show("Datos INCORRECTOS");
                }

            }
            catch (Exception err)
            {

                MessageBox.Show("Datos INCORRECTOS" + err.Message);

            }
        }
    }
}
