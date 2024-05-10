using SocketCliente.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketCliente
{
    static class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            //string servidor = ConfigurationManager.AppSettings["servidor"];

            
            Console.WriteLine("Ingrese dirección IP del servidor:");
            string servidor = Console.ReadLine().Trim();
            /*
            Console.WriteLine("Ingrese puerto a conectarse:");
            puerto = Convert.ToInt32(Console.ReadLine().Trim());
            */
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Conectando a Servidor {0} en puerto {1}", servidor, puerto);
            ClienteSocket clienteSocket = new ClienteSocket(servidor, puerto);


            if (clienteSocket.Conectar())
            {
                Console.WriteLine("Conectado.");
                string recibido = "";
                string enviado = "";

                do
                {
                    enviado = Console.ReadLine().Trim();
                    clienteSocket.Escribir(enviado);

                    if (enviado.ToLower() == "chao")
                    {
                        break;
                    }

                    recibido = clienteSocket.Leer();
                    Console.WriteLine(recibido);
                } while (enviado.ToLower() != "chao" && recibido.ToLower() != "chao");
                clienteSocket.Desconectar();
                Console.WriteLine("Desconectado");
            }

            Console.ReadKey();
        }
    }
}
