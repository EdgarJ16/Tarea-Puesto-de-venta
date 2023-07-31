using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Faro.Entidades;
/// <TAREA 2 UNED PROGRAMACION AVANZADA>
/// NOMBRE :EDGAR ANTONIO JACOB MORENO 
/// CARNE/CEDULA : 0117660514
/// PUESTO DE VENTA FARO V.2 
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
/// 
/// 
/// PROBLEMAS QUE PUEDE PRESENTAR LA APP PARA FUTURAS CORRECCIONES 

/// 
/// </summary>


namespace Faro.Presentacion
{
    public class GenerarID
    {
        // Se genera arreglo de 20 pos con ids diferentes 
        //instancias 
        int[] listaId = new int[100];
        static int cantidadArt = 0;
        static int cantidadFact = 0;

        // Se llena un vector de 20 pos con ids aleatoreos
        public void generarID()
        {
            for (int i = 0; i < 100; i++)
            {
                Random randint = new Random();
                int id = randint.Next(1000);
                if (valida(listaId, id))
                {
                    listaId[i] = id;


                }
                else
                {
                    while (valida(listaId, id) == false)
                    {
                        id = randint.Next(1000);

                    }
                    listaId[i] = id;

                }
            }
        }

        public int[] getID()
        {

            return listaId;

        }
        // valida que no hayan ids repetidos 
        public bool valida(int[] lista, int eval)
        {

            for (int i = 0; i < 100; i++)
            {
                if (lista[i] == eval)
                { // si encuentra un id repetido regresa un false 
                    return false;
                }


            }
            return true;// id no esta registrado

        }

        public void asignaId(Articulo n) {

            n.id = listaId[cantidadArt];
            cantidadArt++;
        }
        public void asignaId(OrdenCompraDetalle n) { 
        n.idDetalle= listaId[cantidadFact];
            cantidadFact++;
        }
    }
}
