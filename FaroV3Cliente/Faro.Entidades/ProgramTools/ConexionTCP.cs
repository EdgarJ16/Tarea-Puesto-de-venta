using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Faro.Entidades.ProgramTools
{

    public class ConexionTCP
    {
        // SE ENCARGA DE CONTROLAR LAS SOLICITUDES AL SERVIDOR

        //El IPEndpoint contiene informacion del puerto local o remoto para hacer posible la informacion 
        //documentacion en : https://docs.microsoft.com/en-us/dotnet/api/system.net.ipendpoint?view=net-6.0
        private static IPAddress ipServidor;
        private static TcpClient cliente;
        private static IPEndPoint serverEndPoint;
        private static StreamWriter clienteStreamWriter;
        private static StreamReader clienteStreamReader;
        public static Planilla planilla;

        //METODOS PARA LA CONEXION 

        //MEtodo se encarga de estalecer la conexion con el servidor 
        //Obtenido de ejemplo tutoria 3 UNED
        public static bool Conectar(string pIdentificadorCliente)
        {
            try
            {

                
                // UNA VEZ EFECTUADA LA CONEXION SE VALIDA INMEDIATAMENTE SI EL VENDEDOR ESTA EN EL REGISTRO
                //SI NO ESTA ESTA SE DESCONECTA :
                ipServidor = IPAddress.Parse("127.0.0.1");
                cliente = new TcpClient();
                serverEndPoint = new IPEndPoint(ipServidor, 30000);
                cliente.Connect(serverEndPoint);
               
                clienteStreamWriter = new StreamWriter(cliente.GetStream());
                MensajeSocket<string> mensajeConectar = new MensajeSocket<string> { Metodo = "Conectar", Entidad = pIdentificadorCliente };
                clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeConectar));
                clienteStreamWriter.Flush();
                // UNA VEZ CONECTADO SE PROCEDE A CARGAR LA PLANILLA PARA EVALUAR LA EXISTENCIA DEL O LA DEPNDIENTE ENJ LA BD 



                // UNA VEZ CARGADO EL REGISTRO SE PROCEDE A EVALUAR SI ESTA REGISTRADO
                //  if (!planilla.Valida(pIdentificadorCliente))// Si lo encontró 
                //{
                //}
                //else
                // {

                //throw new Exception("ERROR");


                //}


            }
            catch (Exception n )
            {

                return false;
            }

            return true;
        }

        //Metodo que desconecta al cliente del servidor 
        public static void Desconectar(string pIdentificadorCliente)
        {
            //Se cierra la conexión del cliente
            cliente.Close();
        }



        //METODOS PARA LA VENTA 

        //Metodo obtiene los Articulos en bodega para que puedan ser vendidos 
        public static List<Articulo> ObtenerArticulos()
        {
            List<Articulo> listaArticulos = new List<Articulo>();

            try
            {
                //Se envia el mensaje  a traves del socket el cual solicita al servidor los articulos en bodega

                MensajeSocket<string> mensajeObtenerArticulos = new MensajeSocket<string> { Metodo = "ObtenerArticulos" };


                clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeObtenerArticulos));
                clienteStreamWriter.Flush();

                var mensaje = clienteStreamReader.ReadLine();


                listaArticulos = JsonConvert.DeserializeObject<List<Articulo>>(mensaje);

                // Una vez el servidor interpreta el mensaje devuelve la lista de articulos solicitada por el clien
                return listaArticulos;
            }
            catch (Exception)
            {

                throw;
            }
        }


        // Metodo por el cual se envia la orden compra con id generico y el servidor se encarga de devolver el objeto
        //Con un id generado por el servidor
        public static OrdenCompra AsignarIdOC(OrdenCompra n)
        {


            try
            {
                //Se envia el ojeto al que se le va asignar un id 
                MensajeSocket<OrdenCompra> mensajeAsignarId = new MensajeSocket<OrdenCompra> { Metodo = "asignarIdOC", Entidad = n };


                clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeAsignarId));
                clienteStreamWriter.Flush();

                var mensaje = clienteStreamReader.ReadLine();


                n = JsonConvert.DeserializeObject<OrdenCompra>(mensaje);

                // Una vez el servidor interpreta el mensaje devuelve la Orden compra con un Id ya asignado
                return n;
            }
            catch (Exception)
            {

                throw;
            }
        }


        //Metodo Hace la solicitud al servidor para actualizar la cantidad en la base de datos 
        public static void ActualizarCantidad(Articulo n, int cantidad)
        {


            try
            {
                //Se envia el mensaje  a traves del socket el cual solicita al servidor los articulos en bodega

                MensajeSocket<Articulo> mensajeActualizarCantidad = new MensajeSocket<Articulo> { Metodo = "ActualizarCantidad", Entidad = n };

                clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeActualizarCantidad));



                MensajeSocket<int> Cantidad = new MensajeSocket<int> { Metodo = "ActualizarCantidad", Entidad = cantidad };
                clienteStreamWriter.Flush();






            }
            catch (Exception)
            {

                throw;
            }
        }


        //MEtodo manda una lista de articulos seleccionados al servidor para que los incluya en el carrito
        public static void Incluir(Articulo artseleccionado)
        {


            try
            {
                //Se envia el mensaje  a traves del socket el cual solicita al servidor los articulos en bodega

                MensajeSocket<Articulo> mensajeIncluir = new MensajeSocket<Articulo> { Metodo = "Incluir", Entidad = artseleccionado };

                clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeIncluir));


                clienteStreamWriter.Flush();



                var mensaje = clienteStreamReader.ReadLine();


            }
            catch (Exception)
            {

                throw;
            }
        }


        //Metodo envia al servidor un objeto orden compra detalle con un id generico y lo devuele con un id definido por el servidor 
        public static OrdenCompraDetalle AsignaIDOCD(OrdenCompraDetalle n)
        {


            try
            {
                //Se envia el mensaje  a traves del socket el cual solicita al servidor los articulos en bodega

                MensajeSocket<OrdenCompraDetalle> mensajeAsignaID = new MensajeSocket<OrdenCompraDetalle> { Metodo = "asignarIdOCD", Entidad = n };

                clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeAsignaID));


                clienteStreamWriter.Flush();



                var mensaje = clienteStreamReader.ReadLine();
                //EL LECTOR LEE EL MENSAJE DEL SERVIDOR QUE ES EL OBJETO CON EL ID ASIGNADO

                n = JsonConvert.DeserializeObject<OrdenCompraDetalle>(mensaje);
                return n;
            }
            catch (Exception)
            {

                throw;
            }
        }


        //metodo que envia la factura de la compra realizada al servidor para que el servidor pueda guardar la venta en la BD 
        public static void GuardarFactura(OrdenCompra n)
        {


            try
            {
                //Se envia el ojeto al que se le va asignar un id 
                MensajeSocket<OrdenCompra> mensajeGuardarFactura = new MensajeSocket<OrdenCompra> { Metodo = "GuardarFactura", Entidad = n };


                clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeGuardarFactura));
                clienteStreamWriter.Flush();


                // Una vez el servidor interpreta el mensaje devuelve la Orden compra con un Id ya asignado

            }
            catch (Exception)
            {

                throw;
            }
        }





        //METODOS SOBRE VENDEDORES Y CONSULTAS

        //Metodo obtiene PLanilla enviada por el servidor
        public static List<Vendedor> ObtenerVendedores()
        {
            List<Vendedor> listaVendedores = new List<Vendedor>();

            try
            {
                //Se envia el mensaje  a traves del socket el cual solicita al servidor los articulos en bodega
              
                MensajeSocket<string> mensajeObtenerVendedor = new MensajeSocket<string> { Metodo = "ObtenerPlanilla" };

                clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeObtenerVendedor));
                clienteStreamWriter.Flush();

                var mensaje = clienteStreamReader.ReadLine();


                listaVendedores = JsonConvert.DeserializeObject<List<Vendedor>>(mensaje);

                // Una vez el servidor interpreta el mensaje devuelve la lista de articulos solicitada por el clien
                return listaVendedores;
            }
            catch (Exception e)
            {
               
                System.Windows.Forms.MessageBox.Show("Error :"+ e.Message);
                return listaVendedores;
            }

        }


        //Metodo Obtene Ventas enviadas por el servidor 
        public static List<Vendedor> ObtenerVentas()
        {
            List<Vendedor> ventas = new List<Vendedor>();

            try
            {
                //Se envia el mensaje  a traves del socket el cual solicita al servidor los articulos en bodega

                MensajeSocket<string> mensajeObtenerVentas = new MensajeSocket<string> { Metodo = "ObtenerVentas" };


                clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeObtenerVentas));
                clienteStreamWriter.Flush();

                var mensaje = clienteStreamReader.ReadLine();


                ventas = JsonConvert.DeserializeObject<List<Vendedor>>(mensaje);

                // Una vez el servidor interpreta el mensaje devuelve la lista de articulos solicitada por el clien
                return ventas;
            }
            catch (Exception)
            {

                throw;
            }

        }





    }
}
