using CasoEstudioUnidad2.Comunicacion;
using MedidorModel.DAL;
using MedidorModel.DTO;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CasoEstudioUnidad2
{
    class Program
    {
        private static IMedidorDAL medidorDAL = MedidorDALArchivo.GetInstancia();
        static void Main(string[] args)
        {
            HebraServidor hebra = new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.IsBackground = true;
            t.Start();
            while (Menu()) ;
        }
        private static bool Menu()
        {
            bool continuar = true;
            Console.WriteLine("Bienvenido");
            Console.WriteLine("1. Ingresar Lectura");
            Console.WriteLine("2. Obtener Lecturas");
            Console.WriteLine("ok. Salir");

            switch (Console.ReadLine().Trim())
            {
                case "1":
                    IngresarLectura();
                    break;
                case "2":
                    ObtenerLecturas();
                    break;
                case "ok":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Elija bien su opcion");
                    break;
            }
            return continuar;

        }

        private static void IngresarLectura()
        {
            try 
            {
                Console.WriteLine("Ingresar  NumeroDelMedidor|Fecha|ValorConsumo");
                string ingreso_datos = Console.ReadLine().Trim();
                string[] datos = ingreso_datos.Split('|', '|', '|');

                int num = Convert.ToInt32(datos[0]);
                string fecha = Convert.ToString(datos[1]);
                decimal valor = Convert.ToDecimal(datos[2]);

                Medidor medidor = new Medidor()
                {
                    NroMedidor = num,
                    Fecha = DateTime.Parse(fecha),
                    ValorConsumo = valor
                };
                lock (medidorDAL)
                {
                    medidorDAL.AgregarMedidor(medidor);
                }
            } 
            catch (Exception e)
            {
                Console.WriteLine("Datos mal ingresados");

            }
            


        }

        private static void ObtenerLecturas()
        {
            List<Medidor> medidores = null;
            lock (medidorDAL)
            {
                medidores = medidorDAL.ObtenerMedidor();
            }
            foreach (Medidor m in medidores) 
            {
                Console.WriteLine(m);
            } 
        }

     
    }
}

