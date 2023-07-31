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

namespace FaroCliente
{
    public partial class FrmVenta : Form
    {

   


        public DateTime fecha = DateTime.Now;
        public int tempo;
        public int pos;
        public int cantidadArt = 0;
        public static int aux;
        List<Articulo> bodega = new List<Articulo>();
        List<Articulo> carrotArt = new List<Articulo>();
        public static string carrito;

        public FrmVenta()
        {


            InitializeComponent();


            try
            {

                //Solicitud cliente servidor para traer articulos de la bd ///////////////////////////////////////////////////////////////////////////////////
                bodega = ConexionTCP.ObtenerArticulos();
                dgvCatalog.DataSource = bodega;

            }
            catch (Exception er3)
            {
                MessageBox.Show("ERROR  " + er3.StackTrace);
            }
        }// fin de frm contstruuctor

        private void dgvCatalog_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            int n = e.RowIndex;

            pos = n;
            try
            {
                if (!(n == -1))
                {// se selecciono un dato
                    txtIdArtSelec.Text = dgvCatalog.Rows[n].Cells[0].Value.ToString();
                    tempo = int.Parse(txtIdArtSelec.Text);
                    btnAgregar.Enabled = true;

                }
                else
                {
                    btnAgregar.Enabled = false;

                }


            }
            catch (Exception e4)
            {

                MessageBox.Show("DEBE SELECCIONAR UN DATO!!!!");

            }


        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // se actualizan los datos en el data grid view 
            try
            {
                aux = (int)dgvCatalog.Rows[pos].Cells[1].Value;
                if (aux <= 0)
                {
                    aux = 0;
                    throw new Exception("Articulo agotado");

                }
                else
                {


                    // se obtiene el dato del registro 




                    // se actualiza la cantidad disponible en bodega
                    aux--;

                    dgvCatalog.Rows[pos].Cells[1].Value = aux;

                    // se actualiza en la base de datos 

                    foreach (Articulo i in bodega)
                    {
                        if (tempo == i.id)
                        {


                            carrotArt.Add(i);


                            carrito += "-" + i.descripcion + "\n";

                        }

                    }





                    // Se suma unop al contador del carrito 

                    cantidadArt++;

                    // Se llena el data grid viwe del carrito con los objetos a llevar

                    try
                    {







                    }
                    catch (Exception er3)
                    {

                    }



                    btnConfirmar.Enabled = true;

                    // se obtienen los datos del articulo




                    // public OrdenCompraDetalle(int ids, DateTime f, Vendedor v, Articulo a, int cant)
                    {

                        // se agregan los objetois articulos al vector carrito 



                    }
                }
            }
            catch (Exception e4)
            {

                MessageBox.Show("error", e4.Message);
            }
            //Se actualizan los respectivos labels 
            // SE HACE UN CALCULO DEL PF Y P V

            lblCantidadArt.Text = cantidadArt.ToString();


        }//FIn agregar

        private void btnFinalizar_Click(object sender, EventArgs e)
        {


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            // realiza la logica back end para almacenar los datos en un vector de 15 celdas para asignarlo al  vector de clase

            try
            {

                //fecha = DateTime.Now.ToString("h:mm:ss tt");
                // Se instancia un objeto 

                // se obtienen los datos del Vendedor/a form anterior 

                // se almancena los datos en un vector 


                // se instancia un objeto detallede compra

                //se agrega la compra a la lista de ventas realizadas 


                // se actualiza base de datos 


                OrdenCompra oCompra = new OrdenCompra();

                // se asigna el id del vendedor a la factura generada 
                //oCompra.Identificacion = escogidx.getEmpEsc().id;



                // Se hace una solicitud Cliente Servidor para Asignar un ID al objeto orden Compra /////////////////////////////////////////////////////////////
              




                oCompra.Fecha = DateTime.Now;


                foreach (Articulo i in bodega)
                {

                    foreach (Articulo comprado in carrotArt)
                    {
                        if (i.id == comprado.id)
                        {


                            OrdenCompraDetalle nuevaCompra = new OrdenCompraDetalle();



                            /// SE HACE LA SOLICITUD AL SERVIDOR PARA ACTUALIZAR LA CANTIDAD EN LA BD /////////////////////////////////////////////////////////////////
                        
                            // SE HACE UNA SOLICITUD AL SERVIDOR PARA OBTENER  LA LISTA DE ARTICULOS DE LA BD ////////////////////////////////////////////////////////////
                         
                            dgvCatalog.DataSource = bodega;



                            // se llena la factura
                            nuevaCompra.cantidadArt = 0;
                            nuevaCompra.idArticulo = comprado.id;
                            nuevaCompra.precioFTotal = 0;
                            nuevaCompra.precioVTotal = 0;
                            nuevaCompra.idOrden = oCompra.IdOrden;


                            // UNA VEZ REALIZADO LA ORDEN COMPRA DETALLE 
                            // SE VINCULA CON ORDEN COMPRA MEDIANTE EL ID ORDEN 

                            // SE HACE LA SOLICITUD AL SERVIDOR PARA INCLUIR EL ARTICULO AL CARRITO/////////////////////////////////////////////////////////////////////////////////
                           


                            // SE HACE LA SOLICITUD AL SERVIDOR PARA ASIGNAR ID A LA NUEVA COMPRA ///////////////////////////////////////////////////////////////////////////////////
                            // SE ENVIA EL OBJETO Y SE REGRESA CON EL ID ASIGNADO/////////////////////////////////////////////////////////////////////////////////////////////////
                            




                        }

                    }


                }
                // UNA VEZ FINALIZADA LA VINCULACION SE ALMACENA LA ORDEN COMPRA EN LA BASE DE DATOS

                // SE ENVIA LA SOLICITUD AL SERVIDOR PARA INLCUIR LA COMPRA A LA BD /////////////////////////////////////////////////////////////////////////////////////////////////////
              




            }
            catch (Exception ell)
            {



            }


            // Se almacena la factura en el facturero
            //facturas.guardarFactura();

            this.Close();
        }

        private void lstview_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }





}

