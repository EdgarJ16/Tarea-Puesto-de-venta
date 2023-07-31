using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faro.Entidades.ProgramTools
{
    public class Planilla
    {
        public static Vendedor empEsc = new Vendedor();
        public static List<string> registroCedulas = new List<string>();
        static int cantidad = 0;
        public void SeleccionarVendedor(Vendedor n)
        {
            if (n == null)
            {
                throw new Exception("ERROR");


            }
            else
            {

                empEsc = n;
            }

        }

        public bool Valida(string cedula)
        {
            foreach (String i in registroCedulas)
            {
                if (i == cedula)
                {
                    return false; // devuelve false si se encuentra una cedula en el registro 
                }


            }
            return true;


        }// fin valida

        // devuelve cantidad de objetos para controlar la psocion

        public int len()
        {

            return cantidad;
        }

        public Vendedor getEmpEsc()
        {
            return empEsc;
        }


        // Carga el registro de IDs
        public void setRegistro(List<Vendedor> planilla, string ID)
        {

            foreach (Vendedor i in planilla)
            {

                registroCedulas.Add(i.id);
                if (i.id == ID)
                {

                    empEsc = i;
                }


            }





        }
    }

}
