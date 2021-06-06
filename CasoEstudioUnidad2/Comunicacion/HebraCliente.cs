using MedidorModel.DAL;
using MedidorModel.DTO;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasoEstudioUnidad2.Comunicacion
{
    class HebraCliente
    {
        private ClienteCom clienteCom;
        private IMedidorDAL medidorDAL = MedidorDALArchivo.GetInstancia();

        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }

        public void Ejecutar()
        {
            clienteCom.Escribir("Ingrese Numero de medidor");
            string numero = clienteCom.Leer();
            clienteCom.Escribir("Ingrese fecha con formato yyyy/Mm/Dd HH:mm:ss");
            string fecha = clienteCom.Leer();
            clienteCom.Escribir("Ingrese valor de consumo");
            string valor = clienteCom.Leer();


            Medidor medidor = new Medidor()
            {
                NroMedidor = Convert.ToInt32(numero),
                Fecha = DateTime.Parse(fecha), 
                ValorConsumo = Convert.ToDecimal(valor)
            };

            lock (medidorDAL) 
            {
                medidorDAL.AgregarMedidor(medidor);
            }
            clienteCom.Escribir("Datos Ingresados correctamente.");
            clienteCom.Desconectar();
            
        }

    }
}
