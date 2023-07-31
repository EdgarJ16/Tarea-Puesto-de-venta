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
    public partial class FrmConVentas : Form
    {

        public int pos;
        public int tempo;
        public string iDVendedor;

        DatosF datos = new DatosF();
        List<Vendedor> planilla = new List<Vendedor>();
        List<OrdenCompraDetalle> detalle = new List<OrdenCompraDetalle>();
        List<OrdenCompraDetalle> comprado = new List<OrdenCompraDetalle>();
        public FrmConVentas()
        {
            InitializeComponent();
            dgvVentas.DataSource = datos.ObtenerFacturasOC();
            planilla = datos.ObtenerPlanilla();
            detalle = datos.ObtenerFacturasDetalles();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int n = e.RowIndex;

            pos = n;
            try
            {
                if (!(n == -1))
                {// se selecciono un dato
                    lblIDSelecto.Text = dgvVentas.Rows[n].Cells[0].Value.ToString();
                    tempo = int.Parse(lblIDSelecto.Text);
                    btnConfirmar.Enabled = true;
                    iDVendedor = dgvVentas.Rows[n].Cells[1].Value.ToString();
                    // Se cargan los datos }


                }
                else
                {
                    btnConfirmar.Enabled = false;

                }


            }
            catch (Exception e4)
            {

                MessageBox.Show("DEBE SELECCIONAR UN DATO!!!!");

            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {


                // se obtiene la factura detalle 

                OrdenCompraDetalle Articulos = new OrdenCompraDetalle();
                foreach (OrdenCompraDetalle i in detalle)
                {
                    if (i.idOrden == tempo)
                    {
                        comprado.Add(i);
                        Articulos = i;

                    }


                }

                //Se obtiene la orden escogidsa





                Vendedor empleadx = new Vendedor();
                // se obtiene el vendedor 
                foreach (Vendedor i in planilla)
                {
                    if (i.id == iDVendedor)
                    {
                        empleadx = i;

                    }


                }





                // se despliegan los datos 

                lblFecha.Text = dgvVentas.Rows[pos].Cells[2].Value.ToString();
                lblCantidadArt.Text = Articulos.cantidadArt.ToString();
                lblIDVendedor.Text = empleadx.id;
                lblPrecioF.Text = Articulos.precioFTotal.ToString();

                lblPrecioV.Text = Articulos.precioVTotal.ToString();


            }
            catch (Exception err)
            {

                //lbl.Text = dgvStock.Rows[n].Cells[2].Value.ToString();
                //txtPV.Text = dgvStock.Rows[n].Cells[3].Value.ToString();
                //txtCantidad.Text = dgvStock.Rows[n].Cells[1].Value.ToString();
                //descripcion = dgvStock.Rows[n].Cells[5].Value.ToString();
                //txtCantidad = 


            }
            // se asigna la info de la factura correspondiente a las etiquetas 
        }
    }
}
