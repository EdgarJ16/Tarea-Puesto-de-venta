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
    public partial class FrmActualizaArt : Form
    {

        //instancias de clase
        DatosF datos = new DatosF();
        public int pos, tempo;
        public bool estado;
        public string descripcion;

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            //  SE VALIDAN LOS DATOS

            try
            {
                //Se validan cada entrada
                if (Validador.validarNum(txtCantidad.Text, txtCantidad) == true &&
                    Validador.validarNum(txtPF.Text, txtPF) == true &&
                    Validador.validarNum(txtPV.Text, txtPV) == true)
                {

                    // Aqui se le asignan los datos al objeto articulo para despues meterlo en el arreglo

                    // Una vez seleccionado el dato se determina el id del articulo a modificar para enviar al otro form
                    Articulo articuloActualizado = new Articulo();

                    articuloActualizado.id = tempo;
                    articuloActualizado.cantidad = int.Parse(txtCantidad.Text);
                    articuloActualizado.precioFinal = decimal.Parse(txtPF.Text);
                    articuloActualizado.precioVenta = decimal.Parse(txtPV.Text);










                    if (cbestado.SelectedItem == "Activo")
                    {

                        estado = true;

                    }
                    else
                    {

                        estado = false;
                    }


                    articuloActualizado.activo = estado;
                    articuloActualizado.descripcion = descripcion;






                    try
                    {
                        //LA CONEXION LO LLEVA AL CATCH DE UNA VEZ (>TODAVIA NO SE PORQUE PASA<)
                        datos.ActualizaArticulo(articuloActualizado);

                        // Se actualiza la tabla 
                        dgvStock.DataSource = datos.ObtenerArticulos();


                        MessageBox.Show("Datos correctamente ingresados");

                        // Se limpian los datos 
                        lblID.Text = "-";
                        txtCantidad.Text = "";
                        txtPF.Text = "";
                        txtPV.Text = "";
                        btnActualizar.Enabled = false;


                    }
                    catch (Exception ex)
                    {



                    }



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

        private void dgvStock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int n = e.RowIndex;

            pos = n;
            try
            {
                if (!(n == -1))
                {// se selecciono un dato
                    lblID.Text = dgvStock.Rows[n].Cells[0].Value.ToString();
                    tempo = int.Parse(lblID.Text);
                    btnActualizar.Enabled = true;

                    // Se cargan los datos }
                    estado = (bool)dgvStock.Rows[n].Cells[4].Value;

                    if (estado)
                    {

                        cbestado.SelectedItem = "Activo";

                    }
                    else
                    {
                        cbestado.SelectedItem = "Inactivo";

                    }

                    txtPF.Text = dgvStock.Rows[n].Cells[2].Value.ToString();
                    txtPV.Text = dgvStock.Rows[n].Cells[3].Value.ToString();
                    txtCantidad.Text = dgvStock.Rows[n].Cells[1].Value.ToString();
                    descripcion = dgvStock.Rows[n].Cells[5].Value.ToString();
                    //txtCantidad = 

                }
                else
                {
                    btnActualizar.Enabled = false;

                }


            }
            catch (Exception e4)
            {

                MessageBox.Show("DEBE SELECCIONAR UN DATO!!!!");

            }

        }

        public FrmActualizaArt()
        {
            InitializeComponent();
            dgvStock.DataSource = datos.ObtenerArticulos();
        }




    }
}
