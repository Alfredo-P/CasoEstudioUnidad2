using MedidorModel.DAL;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CasoEstudioUnidad2.Comunicacion
{
    public class HebraServidor
    {
        private IMedidorDAL medidorDAL = MedidorDALArchivo.GetInstancia();


        public void Ejecutar()
        {

            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ServerSocket servidor = new ServerSocket(puerto);
            if (servidor.Iniciar()) 
            {
                while (true)
                {
                    Console.WriteLine("Esperando Cliente...");
                    Socket cliente = servidor.ObtenerCliente();
                    ClienteCom clienteCom = new ClienteCom(cliente);

                    HebraCliente clienteThread = new HebraCliente(clienteCom);
                    Thread t = new Thread(new ThreadStart(clienteThread.Ejecutar));
                    t.IsBackground = true;
                    t.Start();
                }
            }

            else
            {
                Console.WriteLine("No es posible conectarse con el servidor");
            }

        }
            
}
    }

