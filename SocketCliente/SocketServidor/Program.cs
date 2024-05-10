using SocketServidor.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace SocketServidor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Iniciando servidor en el puerto {0}", puerto);
            ServerSocket servidorSocket = new ServerSocket(puerto);
            if (servidorSocket.Iniciar())
            {
                while(true)
                {
                    Console.WriteLine("Servidor iniciado.");
                    Console.WriteLine("Esperando cliente...");
                    Socket socketCliente = servidorSocket.ObtenerCliente();
                    ClienteCom clienteCom = new ClienteCom(socketCliente);
                    string recibido = "";
                    string enviado = "";
                    do
                    {
                        recibido = clienteCom.Leer();
                        Console.WriteLine(recibido);
                        if (recibido.ToLower() == "chao")
                        {
                            break;
                        }
                        enviado = Console.ReadLine().Trim();
                        clienteCom.Escribir(enviado);
                    } while (enviado.ToLower() != "chao" && recibido.ToLower() != "chao");
                    clienteCom.Desconectar();
                    Console.WriteLine("Desconectado");
                }
            }
            else
            {
                Console.WriteLine("Error, el puerto {0} está en uso.", puerto);
            }

            Console.ReadKey();
        }
    }
}
