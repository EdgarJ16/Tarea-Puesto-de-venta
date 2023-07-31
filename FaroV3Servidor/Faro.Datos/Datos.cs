using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
    public class DatosF
    {
        private string cadenaConexion;

        public DatosF()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["conexionFaro"].ConnectionString;

        }

        //Se agrega un articulo 
        ///<param name = "pArt"> Nuevo articulo a agregar </param>
        public void AgregarArticulo(Articulo pArt)
        {

            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            string query;

            conexion = new SqlConnection(cadenaConexion);
            query = " Insert	Into	Articulo (IdArticulo,	Descripcion,	Activo,		PrecioVendedor,	PrecioFinal, CantidadDisponible)" +
                     " Values (@IdArticulo,	@Descripcion,	@Activo,		@PrecioVendedor,	@PrecioFinal, @CantidadDisponible)";


            //se convierte a bit
            int estadoArt;
            if (pArt.activo == true)
            {
                estadoArt = 1;


            }
            else
            {
                estadoArt = 0;
            }

            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.Parameters.AddWithValue("@IdArticulo", pArt.id);
            comando.Parameters.AddWithValue("@Descripcion", pArt.descripcion);
            comando.Parameters.AddWithValue("@Activo", estadoArt);
            comando.Parameters.AddWithValue("@PrecioVendedor", pArt.precioVenta);
            comando.Parameters.AddWithValue("@PrecioFinal", pArt.precioFinal);
            comando.Parameters.AddWithValue("@CantidadDisponible", pArt.cantidad);
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

                conexion.Close();


            }
            catch (Exception e) {
                MessageBox.Show("Datos INCORRECTOS" + e.Message);

            }

        }

        /// <summary>
        /// Agrega un nuevo Vendedor en la base de datos
        /// </summary>
        /// <param name="pVendedor">Nuevo Vendedor a agregar</param>

        public void AgregarVendedor(Vendedor pVendedor)
        {
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            string query;

            conexion = new SqlConnection(cadenaConexion);
            query = " Insert	Into	Vendedor (Identificacion,	Nombre,	PrimerApellido,		SegundoApellido,	FechaNacimiento, Genero, FechaIngreso)" +
                        " Values (@Identificacion,	@Nombre,	@PrimerApellido,		@SegundoApellido,	@FechaNacimiento, @Genero, @FechaIngreso)";

            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.Parameters.AddWithValue("@Identificacion", pVendedor.id.ToString());
            comando.Parameters.AddWithValue("@Nombre", pVendedor.nombre.ToString());
            comando.Parameters.AddWithValue("@PrimerApellido", pVendedor.apellido1.ToString());
            comando.Parameters.AddWithValue("@SegundoApellido", pVendedor.apellido2.ToString());
            comando.Parameters.AddWithValue("@FechaNacimiento", pVendedor.nacimiento);
            comando.Parameters.AddWithValue("@Genero", (char)pVendedor.genero);
            comando.Parameters.AddWithValue("@FechaIngreso", pVendedor.fechaIngreso);

            conexion.Open();
            comando.ExecuteNonQuery();

            conexion.Close();

        }

        /// <summary>
        /// Retorna una colección genérica de autores de la base de datos
        /// </summary>
        /// <param name="p_IdArt"></param>
        /// <returns></returns>
        public List<Articulo> ObtenerArticulos()
        {
            List<Articulo> listaArt = new List<Articulo>();

            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            string query;
            SqlDataReader reader;

            conexion = new SqlConnection(cadenaConexion);

            query = " Select	" +
                "IdArticulo,	Descripcion,	Activo,		PrecioVendedor,	PrecioFinal, CantidadDisponible" +
                    " From	    Articulo";

            comando.CommandText = query;
            comando.Connection = conexion;
            try {

                conexion.Open();

                reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listaArt.Add(new Articulo
                        {
                            id = reader.GetInt32(0),
                            descripcion = reader.GetString(1),
                            activo = reader.GetBoolean(2),
                            precioVenta = reader.GetDecimal(3),
                            precioFinal = reader.GetDecimal(4),
                            cantidad = reader.GetInt32(5)

                        });
                    }
                }

                conexion.Close();

            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            
            
            }
       
            return listaArt;
        }



        /// <summary>
        /// Retorna una colección genérica de autores de la base de datos
        /// </summary>
        /// <param name="p_IdArt"></param>
        /// <returns></returns>

        public List<Vendedor> ObtenerPlanilla()
        {
            List<Vendedor> listaEmp = new List<Vendedor>();

            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            string query;
            SqlDataReader reader;

            conexion = new SqlConnection(cadenaConexion);

            query = " Select	" +
                "Identificacion,	Nombre,	PrimerApellido,		SegundoApellido,	FechaNacimiento, Genero, FechaIngreso" +
                    " From	    Vendedor";

            comando.CommandText = query;
            comando.Connection = conexion;
            try {

                conexion.Open();

                reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {


                        listaEmp.Add(new Vendedor
                        {
                            id = reader.GetString(0),
                            nombre = reader.GetString(1),
                            apellido1 = reader.GetString(2),
                            apellido2 = reader.GetString(3),
                            nacimiento = reader.GetDateTime(4),
                            genero = 'S',
                            fechaIngreso = reader.GetDateTime(6)

                        });

                    }




                }// fin if



                conexion.Close();

            }
            catch (Exception e) 
            {
                MessageBox.Show("ERROR "+e);
            
            }
            

            return listaEmp;
        }//FIn obtiener planilla 



        /// <summary>
        /// Retorna una colección genérica de autores de la base de datos
        /// </summary>
        /// <param name="p_ArtCompra"></param>
        /// <returns></returns>
        public bool ActualizaCantidad(Articulo pArt, int cantidad) {
            pArt.cantidad = cantidad;
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            conexion = new SqlConnection(cadenaConexion);
            string query = "update Articulo set CantidadDisponible ="+pArt.cantidad.ToString()+ "where IdArticulo ="+pArt.id.ToString()+";";
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = query;
            comando.Connection = conexion;

            
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

                conexion.Close();

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("OCURRIO UN ERROR :" + e.Message);
                return false;
            }

        }









        /// <summary>
        /// Retorna una colección genérica de autores de la base de datos
        /// </summary>
        /// <param name="p_ArtCompra"></param>
        /// <returns></returns>
        public bool ActualizaArticulo(Articulo pArt)
        {
            int bit;


            if (pArt.activo)
            {
                bit = 1;
            }
            else {
                bit = 0;


            }

            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            conexion = new SqlConnection(cadenaConexion);
            string query = "UPDATE Articulo SET CantidadDisponible =" + pArt.cantidad.ToString() + ",PrecioFinal =" + pArt.precioFinal.ToString() + ", PrecioVendedor =" + pArt.precioVenta +",Activo ="+ bit.ToString()  + " where IdArticulo =" + pArt.id.ToString()+";";
            
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = query;
            comando.Connection = conexion;


            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

                conexion.Close();

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("OCURRIO UN ERROR :" + e.Message);
                return false;
            }

        }







        /// <summary>
        /// Agrega un nuevo Vendedor en la base de datos
        /// </summary>
        /// <param name="pVendedor">Nuevo Vendedor a agregar</param>

        public void AgregarFactura(OrdenCompra pFactura)
        {
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            string query;

            conexion = new SqlConnection(cadenaConexion);
            query = " Insert	Into	OrdenCompra (IdOrden,	Identificacion,	Fecha)" +
                        " Values (@IdOrden,	@Identificacion		,	@Fecha)";

            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = query;
            comando.Connection = conexion;


            comando.Parameters.AddWithValue("@IdOrden", pFactura.IdOrden);
            comando.Parameters.AddWithValue("@Identificacion", pFactura.Identificacion);
            comando.Parameters.AddWithValue("@Fecha", pFactura.Fecha);
            try
            {

                conexion.Open();
                comando.ExecuteNonQuery();

                conexion.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }




        /// <summary>
        /// Agrega un nuevo Vendedor en la base de datos
        /// </summary>
        /// <param name="pVendedor">Nuevo Vendedor a agregar</param>

        public void AgregarFacturaDetalle(OrdenCompraDetalle pFacturaD)
        {
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            string query;

            conexion = new SqlConnection(cadenaConexion);
            query = " Insert	Into	OrdenCompraDetalle (IdDetalle,	IdOrden,	IdArticulo,		CantidadArticulo, PrecioVendedor, PrecioFinal)" +
                        " Values (@IdDetalle,	@IdOrden,	@IdArticulo		,	@CantidadArticulo, @PrecioVendedor, @PrecioFinal)";

            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = query;
            comando.Connection = conexion;

            comando.Parameters.AddWithValue("@IdDetalle", pFacturaD.idDetalle);
            comando.Parameters.AddWithValue("@IdOrden", pFacturaD.idOrden);
            comando.Parameters.AddWithValue("@IdArticulo", pFacturaD.idArticulo);
            comando.Parameters.AddWithValue("@CantidadArticulo", pFacturaD.cantidadArt);
            comando.Parameters.AddWithValue("@PrecioVendedor",pFacturaD.precioVTotal);
            comando.Parameters.AddWithValue("@PrecioFinal",pFacturaD.precioFTotal );
            try
            {

                conexion.Open();
                comando.ExecuteNonQuery();

                conexion.Close();

            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        
        }

        /// <summary>
        /// Retorna una colección genérica de autores de la base de datos
        /// </summary>
        /// <param name="p_IdArt"></param>
        /// <returns></returns>
        public List<OrdenCompraDetalle> ObtenerFacturasDetalles()
        {
            List<OrdenCompraDetalle> listaOCDetalle = new List<OrdenCompraDetalle>();

            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            string query;
            SqlDataReader reader;

            conexion = new SqlConnection(cadenaConexion);

            query = " Select	" +
                "IdDetalle,	IdOrden,	IdArticulo,		CantidadArticulo, PrecioVendedor, PrecioFinal" +
                    " From	   OrdenCompraDetalle";

            comando.CommandText = query;
            comando.Connection = conexion;
            try
            {

                conexion.Open();

                reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listaOCDetalle.Add(new OrdenCompraDetalle
                        {
                            idDetalle = reader.GetInt32(0),
                            idOrden = reader.GetInt32(1),
                            idArticulo = reader.GetInt32(2),
                            cantidadArt = reader.GetInt32(3),
                            precioFTotal = reader.GetDecimal(4),
                            precioVTotal = reader.GetDecimal(5)

                        });
                    }
                }

                conexion.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);


            }

            return listaOCDetalle;
        }





        /// <summary>
        /// Retorna una colección genérica de autores de la base de datos
        /// </summary>
        /// <param name="p_IdArt"></param>
        /// <returns></returns>
        public List<OrdenCompra> ObtenerFacturasOC()
        {
            List<OrdenCompra> listaOC = new List<OrdenCompra>();

            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            string query;
            SqlDataReader reader;

            conexion = new SqlConnection(cadenaConexion);

            query = " Select	" +
                "IdOrden,	Identificacion,		Fecha" +
                    " From	   OrdenCompra";

            comando.CommandText = query;
            comando.Connection = conexion;
            try
            {

                conexion.Open();

                reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listaOC.Add(new OrdenCompra
                        {
                           
                            IdOrden = reader.GetInt32(0),
                            Identificacion = reader.GetString(1),
                            Fecha = reader.GetDateTime(2)
                            
                        });
                    }
                }

                conexion.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);


            }

            return listaOC;
        }


    }//Fin clase datos


}//fin name space

