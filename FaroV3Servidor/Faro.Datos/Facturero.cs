using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faro.Entidades;
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


namespace Faro.Datos
{

    public class Facturero
    {
        public static List<OrdenCompraDetalle> listaFacturas = new List<OrdenCompraDetalle>();
        public static List<OrdenCompra> ordenCompras = new List<OrdenCompra>();
        public static List<OrdenCompraDetalle> carrito = new List<OrdenCompraDetalle>();
        public static List<Articulo> carritoArticulos = new List<Articulo>();
    
        public decimal totalFinalV;
        public decimal totalFinalF;
        public static int idFinal;
        public static int idFinalDetalle;
        public int cantidad = 0;

        DatosF datos = new DatosF();

        public Facturero() {
            try {
        
                ordenCompras = datos.ObtenerFacturasOC();
                listaFacturas = datos.ObtenerFacturasDetalles();
            }
            catch (Exception e) {

            }


        }

        public void incluir(Articulo n) {

            carritoArticulos.Add(n);
        }


        public void asignaIdOCD(OrdenCompraDetalle n) {

            // Se asigna un id 
            int id = generaIdOCD(n);

            // se almacena 

            n.idDetalle = id;
            idFinalDetalle = id;
            carrito.Add(n);
 



        }

        public void asignarIdOC(OrdenCompra n) {
            int id = generaIdOC(n);
            idFinal = id;
            n.IdOrden = id;

        }



        public void guardarFactura(OrdenCompra n)
        {
            totalFinalV = totalPrecioVenta(carritoArticulos);

            totalFinalF = totalPrecioFinal(carritoArticulos);

            datos.AgregarFactura(n);
            foreach (OrdenCompraDetalle i in carrito) {

                if (!validaOCD(i)) {

                    i.precioFTotal = getPF();


                    i.precioVTotal = getPV();

                    i.cantidadArt = carritoArticulos.Count;
                    datos.AgregarFacturaDetalle(i);


                }



            }
            cantidad = carrito.Count;
            totalFinalF = 0;
            totalFinalV = 0;

            carritoArticulos.Clear();


        }

        public decimal getPV() {
           
            decimal aux = totalFinalV;
     
            return aux;
        }

        public decimal getPF() {
            decimal aux = totalFinalF;

            return totalFinalF;
        }


        public int getIDD()
        {
            return idFinalDetalle;


        }
        public int getID() {
            return idFinal;


        }

        public int getCantidad() {

            int aux = cantidad;
            cantidad = 0;
            return aux;
            
        } 

     
        public decimal totalPrecioVenta(List<Articulo> n) {

            decimal total=0;
            foreach (Articulo i in n) {

                total += i.precioVenta;
            
            }
            totalFinalV = total;
            return total;
        
        }
        public decimal totalPrecioFinal(List<Articulo> n)
        {

            decimal total = 0;
            foreach (Articulo i in n)
            {

                total += i.precioFinal;

            }
            totalFinalF = total;
            return total;

        }
        public int generaIdOCD(OrdenCompraDetalle n) {
            Random randint = new Random();
            int id;
            do
            {
              
                id = randint.Next(1000);
                n.idDetalle = id;
            } while (validaOCD(n));

            return id;

        }

        public int generaIdOC(OrdenCompra n)
        {
            Random randint = new Random();
            int id;
            do
            {

                id = randint.Next(1000);
                n.IdOrden = id;
            } while (validaOC(n));
    
            return id;

        }



        public bool validaOC(OrdenCompra n)
        {
            // se actualizan los datos 
            ordenCompras = datos.ObtenerFacturasOC();
            //

            foreach (OrdenCompra i in ordenCompras)
            {
                if (i.IdOrden == n.IdOrden)
                {
                    return true;

                }

            }
            return false;

        }

        public bool validaOCD(OrdenCompraDetalle n)
        {
            // se actualizan los datos 
            listaFacturas = datos.ObtenerFacturasDetalles();



            foreach (OrdenCompraDetalle i in listaFacturas)
            {
                if (i.idDetalle == n.idDetalle)
                {
                    return true;

                }

            }
            return false;

        }


    }
}
