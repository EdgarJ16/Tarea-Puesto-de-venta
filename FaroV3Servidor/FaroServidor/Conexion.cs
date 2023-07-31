using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Faro.Datos;
using Faro.Entidades;
using Faro.Entidades.ProgramTools;
using FaroServidor.TCP;
using Newtonsoft.Json;
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
    public partial class Conexion : Form
    {
            
        TcpListener tcpListener;
        Thread subprocesoEscuchaClientes;

        ListaClientes toolClientes = new ListaClientes();
        DatosF accesoDatos;
        Facturero facturero;

        EscribirEnlabelDelegado modificarLabel;
        ModoficarListBoxDelegado modificarListBoxClientes;


        bool servidorIniciado = false;
        bool flag = true;

        public Conexion()
        {
            InitializeComponent();
            btnInicio.Enabled = flag;
            btnDetener.Enabled = !flag;

        


        }



        //Delegado, necesario para modificar controles de la interfaz gráfica desde un subproceso

        // modifican el lable que indica cuantos clientes se encuentran conectados 
        private delegate void EscribirEnlabelDelegado(string texto);
        private delegate void ModoficarListBoxDelegado(string texto, bool agregar);


//METODOS------------------------------------------------------------------------------------------------------------------------------------

        // METODO EN EL CUAL SE CREA EL SOCKET O CANAL POR EL CUAL SE COMUNICAN CLIENTE CON SERVIDOR 
        public void ComunicacionCliente(object cliente)
        {
            TcpClient tcCliente = (TcpClient)cliente;
            StreamReader reader = new StreamReader(tcCliente.GetStream());
            StreamWriter servidorStreamWriter = new StreamWriter(tcCliente.GetStream());//El StreamWriter debe ser único por subproceso y por cliente por eso se pasa por referencia

            while (servidorIniciado)
            {

                try
                {
                    /*Esta sección se bloquea hasta que el cliente envíe unmensaje*/
                    var mensaje = reader.ReadLine();
                    MensajeSocket<object> mensajeRecibido = JsonConvert.DeserializeObject<MensajeSocket<object>>(mensaje);//Se deserializa el objeto recibido mediante json
                    SeleccionarMetodo(mensajeRecibido.Metodo, mensaje, ref servidorStreamWriter);
                }
                catch (Exception e)
                {
                    //Ocurrió un error en el socket 
                    break;
                }
            }

        }




        // METODO INICIA LA ESCUCHA DE CLIENTES POR PARTE DEL SERVIDOR 
        //ENTRA EN CICLO ESPERANDO A QUE SE CONECTE UN CLIENTE PARA DAR INICIO AL SUBPROCESO QUE SE ENCARGA DE GESTIONAR 
        //LAS SOLICITUDES DEL CLIENTE 
        private void EscucharClientes()
        {
            int conectados = 0;
            tcpListener.Start();
            while (servidorIniciado)
            {
                //Se bloquea hasta que un cliente se haya conectado al servidor 
                TcpClient client = tcpListener.AcceptTcpClient();
                /*Se crea un nuevo hilo para manejar la comunicación con los clientes que se conectan al servidor*/
                Thread clientThread = new Thread(new ParameterizedThreadStart(ComunicacionCliente));
                clientThread.Start(client);

                toolClientes.Add(client);

                // se lleva un registro de cuantos clienteshay conectadoes }
              
                
                
                // el label gana +1

               // conectados++;
               // FrmServidor.newFrmServidor.clientes.Text = conectados.ToString();
            }
            




        }






        //METODO DETERMINA LA SOLICITUD DEL CLIENTE MEDIANTE UN SWITCH 
        //EN ESTE METODO SE DESEMPAQUETA LA INFORMACION SOLICITADA O CONSULTADA POR EL CLIENTE 
        public void SeleccionarMetodo(string pMetodo, string pMensaje, ref StreamWriter servidorStreamWriter)
        {
            switch (pMetodo)
            {


                //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                // SOBRE VENTA 

                //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                case "ObtenerArticulos":
                   
                    ObtenerArticulos(ref servidorStreamWriter);


                    break;



                //Asigna un Id a la OC y manda la OC con id asignado al cliente 
                case "asignarIdOC":
                    MensajeSocket<OrdenCompra> mensajeAsignarIdOrdenCompra = JsonConvert.DeserializeObject<MensajeSocket<OrdenCompra>>(pMensaje);// Se deserializa el objeto recibido mediante json
                    asignarIdOC(mensajeAsignarIdOrdenCompra.Entidad, ref servidorStreamWriter);


                    break;
                //actualiza cantidad en bd 
                case "ActualizarCantidad":

                    MensajeSocket<Articulo> mensajeActualizaCantidad = JsonConvert.DeserializeObject<MensajeSocket<Articulo>>(pMensaje);// Se deserializa el objeto recibido mediante json
                   
                    int cantidad = JsonConvert.DeserializeObject<int>(pMensaje);


                    ActualizaCantidad(mensajeActualizaCantidad.Entidad, cantidad);


                    break;


                // inlcuye los articulos al carrito 
                case "Incluir":
                    MensajeSocket<Articulo> mensajeIncluir = JsonConvert.DeserializeObject<MensajeSocket<Articulo>>(pMensaje);// Se deserializa el objeto recibido mediante json
                    Incluir(mensajeIncluir.Entidad);


                    break;


                //asigna ID a la OCD y devuelve al cliente el obj OCD con id asignado
                case "asignarIdOCD":
                    MensajeSocket<OrdenCompraDetalle> mensajeAsignarIdOrdenCompraDet = JsonConvert.DeserializeObject<MensajeSocket<OrdenCompraDetalle>>(pMensaje);// Se deserializa el objeto recibido mediante json
                    asignarIdOCD(mensajeAsignarIdOrdenCompraDet.Entidad, ref servidorStreamWriter);
                    break;




                //REALIZA LA COMPRA MEDIANTE LA CLASE FACTURERO
                case "GuardarFactura":
                    MensajeSocket<OrdenCompra> mensajeGuardarFactura = JsonConvert.DeserializeObject<MensajeSocket<OrdenCompra>>(pMensaje);// Se deserializa el objeto recibido mediante json
                    guardarFactura(mensajeGuardarFactura.Entidad);

                    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                    // SOBRE VENDEDORES y CONSULTAS

                    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                    break;
                case "ObtenerPlanilla":
                    // Se obtiene la lista por referencia y se manda al cliente 
                    ObtenerPlanilla(ref servidorStreamWriter);
                    break;

                case "ObtenerVentas":
                    // Se obtiene la lista por referencia y se manda al cliente 
                    ObtenerVentas(ref servidorStreamWriter);
                    break;


                    // SOBRE CONEXION 

                case "Desconectar":
                    MensajeSocket<string> mensajeDesconectar = JsonConvert.DeserializeObject<MensajeSocket<string>>(pMensaje);//Se deserializa el objeto recibido mediante json
                    Desconectar(mensajeDesconectar.Entidad);
                    break;

                case "Conectar":
                    MensajeSocket<string> mensajeConectar = JsonConvert.DeserializeObject<MensajeSocket<string>>(pMensaje);//Se deserializa el objeto recibido mediante json
                    Conectar(mensajeConectar.Entidad);
                    break;

                default:
                    break;


            }

        }


        //SEMAFOROS DE CONEXION 
        



        private void Desconectar(string pIdentificadorCliente)
        {
            toolClientes.pop();
            FrmServidor.newFrmServidor.clientes.Invoke(modificarLabel, new object[] {toolClientes.getLen()}    );


        }


        private void Conectar(string pIdentificadorCliente)
        {
            int temp = toolClientes.getLen();

            CheckForIllegalCrossThreadCalls = false;
            FrmServidor.newFrmServidor.clientes.Text = toolClientes.getLen().ToString();
            CheckForIllegalCrossThreadCalls = true; 




            FrmServidor.newFrmServidor.clientes.Invoke(modificarLabel, new object[] { toolClientes.getLen() });

        }



        //METODOS PARA LA VENTA <<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


        //METODO PARA ENVIAR LA LISTA DE ARTICULOS EN BODEGA
        private void ObtenerArticulos(ref StreamWriter servidorStreamWriter)
        {
            List<Articulo> articulos = new List<Articulo>();

            articulos = accesoDatos.ObtenerArticulos();


            //SE SERIALIZA EL OBJETO PARA MANDARLO AL CLIENTE 
            servidorStreamWriter.WriteLine(JsonConvert.SerializeObject(articulos));
            servidorStreamWriter.Flush();
        }

        // METODO QUE ASIGNA UN ID A LA ORDEN COMPRA Y LO ENVIA CON EL ID YA ASIGNADO (Facturero)
        private void asignarIdOC(OrdenCompra n, ref StreamWriter servidorStreamWriter)
        {


            facturero.asignarIdOC(n);


            //SE SERIALIZA EL OBJETO PARA MANDARLO AL CLIENTE 
            // SE ENVIA EL OBJETO CON EL ID ASIGNADO
            servidorStreamWriter.WriteLine(JsonConvert.SerializeObject(n));
            servidorStreamWriter.Flush();
        }

        //METODO ACTUALIZA CANTIDAD EN LA BASE DE DATOS
        private void ActualizaCantidad(Articulo art, int cantidad)
        {
            //RECIBE EL ARTIUCLO Y LA CANTIDAD A ACTUALIZAR Y MANDA LA INFO A LA BASE DE DATOS

            accesoDatos.ActualizaCantidad(art, cantidad);

        }

        //Metodo Incluye articulos seleccionados en el carrito
        private void Incluir(Articulo art)
        {
            //Incluye los archivos que se van a comprar al carrito de compra 

            facturero.incluir(art);

        }

        // METODO QUE ASIGNA UN ID A LA ORDEN COMPRA DETALLE Y LO ENVIA CON EL ID YA ASIGNADO (Facturero)
        private void asignarIdOCD(OrdenCompraDetalle n, ref StreamWriter servidorStreamWriter)
        {


            facturero.asignaIdOCD(n);


            //SE SERIALIZA EL OBJETO PARA MANDARLO AL CLIENTE 
            // SE ENVIA EL OBJETO CON EL ID ASIGNADO
            servidorStreamWriter.WriteLine(JsonConvert.SerializeObject(n));
            servidorStreamWriter.Flush();
        }

        //METODO AGREGA FACTURA 
        private void guardarFactura(OrdenCompra n)
        {

            //realiza la venta 
            facturero.guardarFactura(n);



        }




 //FIN METODOS PARA VENTA /METODOS PARA LA VENTA <<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>




//METODOS PARA VENDEDORES Y CONSULTAS<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>    
        
        
        //METODO PARA OBTENER PLANILLA
        private void ObtenerPlanilla(ref StreamWriter servidorStreamWriter)
        {
            List<Vendedor> planilla = new List<Vendedor>();

            planilla = accesoDatos.ObtenerPlanilla();


            //SE SERIALIZA EL OBJETO PARA MANDARLO AL CLIENTE 
            servidorStreamWriter.WriteLine(JsonConvert.SerializeObject(planilla));
            servidorStreamWriter.Flush();
        }

        //METODO PARA OBTENER VENTAS DE LA BD

        private void ObtenerVentas(ref StreamWriter servidorStreamWriter)
        {
            List<OrdenCompra> factura = new List<OrdenCompra>();

            factura = accesoDatos.ObtenerFacturasOC();


            //SE SERIALIZA EL OBJETO PARA MANDARLO AL CLIENTE 
            servidorStreamWriter.WriteLine(JsonConvert.SerializeObject(factura));
            servidorStreamWriter.Flush();
        }


        //FIN METODOS PARA VENTA /METODOS PARA LA VENTA <<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        //________________________________________________________________________________________________________________________________________________________________________


        //CONTROLES DEL FORM


        private void btnInicio_Click(object sender, EventArgs e)
        {
           
                
               
                    flag = false;





                    IPAddress local = IPAddress.Parse("127.0.0.1");
                    tcpListener = new TcpListener(local, 30000);
                    servidorIniciado = true;

                    subprocesoEscuchaClientes = new Thread(new ThreadStart(EscucharClientes));
                    subprocesoEscuchaClientes.Start();
                    subprocesoEscuchaClientes.IsBackground = true;
                   
                    btnDetener.Enabled = true;
                    btnInicio.Enabled = false;
                    lblEstado.ForeColor = Color.Green;
                    lblEstado2.Text = "ON";
            FrmServidor.newFrmServidor.stad.ForeColor = Color.Green;
            



        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (servidorIniciado)
            {
                flag = true;
                servidorIniciado = false;
                tcpListener.Stop();
                subprocesoEscuchaClientes.Abort();
                btnInicio.Enabled = true;
             
                btnDetener.Enabled = false;
                lblEstado.ForeColor = Color.Red;
                lblEstado2.Text = "OFF";
                FrmServidor.newFrmServidor.stad.ForeColor = Color.Red;

            }

        }

        private void lblEstado_Click(object sender, EventArgs e)
        {

        }

        private void lblEstado2_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }



    }
}
