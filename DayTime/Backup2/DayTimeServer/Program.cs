using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Globalization;

/*
 * $Id: Program.cs,v 1.1 2006/10/24 19:32:59 mkalewski Exp $
 */

namespace DayTimeServer
{
    class Program
    {
        private const int SERVER_PORT = 1234;
        private const int QUEUE_SIZE = 5;
        private static ManualResetEvent m_AcceptDone = new ManualResetEvent(false);

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                /* retrieve the socket from the ar object */
                Socket socketFd = (Socket)ar.AsyncState;

                /* end pending asynchronous send */
                int bytesSent = socketFd.EndSend(ar);

                Console.WriteLine("\t\tSent {0} bytes to the client\n\tEND connection", bytesSent);

                /* shutdown and close socket */
                socketFd.Shutdown(SocketShutdown.Both);
                socketFd.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message.ToString());
            }

        }
        
        private static void SendDate(Socket socketFd)
        {
            try
            {
                /* get system date and time */
                Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL");
                DateTime dateTime = DateTime.Now;

                byte[] dataBuf = Encoding.ASCII.GetBytes(dateTime.ToString());

                /* begin sending the date */
                socketFd.BeginSend(dataBuf, 0, dataBuf.Length, 0, new AsyncCallback(SendCallback), socketFd);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message.ToString());
            }
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                /* get the socket that handles the client request */
                Socket socketFd = (Socket) ar.AsyncState;
                Socket socketNew = socketFd.EndAccept(ar);

                /* the main socket is now free */
                m_AcceptDone.Set();

                Console.WriteLine("\tNEW connection");

                SendDate(socketNew);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message.ToString());
            }
        }

        private static void RunServer()
        {
            Socket socketFd = null;
            IPEndPoint endPoint = null;

            try
            {
                /* create a socket */
                socketFd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                /* local endpoint for the socket */
                endPoint = new IPEndPoint(IPAddress.Any, SERVER_PORT);

                /* bind the socket to the local endpoint */
                socketFd.Bind(endPoint);

                /* specify queue size */
                socketFd.Listen(QUEUE_SIZE);

                while (true)
                {
                    m_AcceptDone.Reset();

                    Console.WriteLine("Waiting for a connection...");

                    /* block for connection request */
                    socketFd.BeginAccept(new AsyncCallback(AcceptCallback), socketFd);

                    m_AcceptDone.WaitOne();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message.ToString());
            }
            finally
            {
                /* shutdown and close socket */
                socketFd.Shutdown(SocketShutdown.Both);
                socketFd.Close();
            }
        }

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("START");
                RunServer();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message.ToString());
            }

        }
    }
}
