using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace DayTime
{
    public partial class Form1 : Form
    {
        private Form obj; 
        private Form2 frm; 
        private Socket fd; 
        private IPEndPoint endPoint = null;
        delegate void setThreadedTextBoxCallback(String text);
        delegate void setThreadedStatusLabelCallback(String text);
        delegate void setThreadedButtonCallback(bool status);

        public Form1()
        {
            InitializeComponent();
            this.obj = this;
            
        }

        private void setThreadedTextBox(String text)
        {
            if (this.textBoxDate.InvokeRequired)
            {
                setThreadedTextBoxCallback textBoxCallback = new setThreadedTextBoxCallback(setThreadedTextBox);
                this.obj.Invoke(textBoxCallback, text);
            }
            else
            {
                //text = text.Substring(0, text.Length - 2);
                this.textBoxDate.Text = text;
            }
        }

        private void setThreadedStatusLabel(String text)
        {
            if (this.statusStrip.InvokeRequired)
            {
                setThreadedStatusLabelCallback statusLabelCallback = new setThreadedStatusLabelCallback(setThreadedStatusLabel);
                this.obj.Invoke(statusLabelCallback, text);
            }
            else
            {
                this.toolStripStatusLabel1.Text = text;
            }
        }

        private void setThreadedButton(bool status)
        {
            if (this.loginButton.InvokeRequired)
            {
                setThreadedButtonCallback buttonCallback = new setThreadedButtonCallback(setThreadedButton);
                this.obj.Invoke(buttonCallback, status);
            }
            else
            {
                this.loginButton.Enabled = status;
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                /* retrieve the SocketStateObject */
                SocketStateObject2 state = (SocketStateObject2)ar.AsyncState;
                Socket socketFd = fd;

                /* read data */
                int size = socketFd.EndReceive(ar);
                string a;
                char b;
                state.m_StringBuilder.Append(Encoding.ASCII.GetString(state.m_DataBuf, 0, size));
                /* get the rest of the data */
                a = state.m_StringBuilder.ToString();
                b = a[a.Length - 1];
                if (b == '\n')
                {
                    /* all the data has arrived */

                    MessageBox.Show("jjj" + a + "j");
                    switch (a[0])
                    {
                        case 'Y':
                            Form2 frm = new Form2(socketFd, this.textBoxLogin.Text.ToString(), endPoint, this);
                            frm.Show();
                            this.Hide();
                            socketFd.EndReceive(ar);
                            break;
                        case 'N':
                            MessageBox.Show("Blad logowania");
                            socketFd.EndReceive(ar);
                            break;
                       
                        default:
                            break;
                    }
                    state = null;
                    state = new SocketStateObject2();

                }
                socketFd.BeginReceive(state.m_DataBuf, 0, 1, 0, new AsyncCallback(ReceiveCallback), state);


            }
            catch (Exception exc)
            {
                MessageBox.Show("Exception:\t\n" + exc.Message.ToString());
            }
        }


    
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                /* retrieve the socket from the state object */
                Socket socketFd = (Socket) ar.AsyncState;
                byte[] Buf;
                string Login, Password,mess;
                /* complete the connection */
                socketFd.EndConnect(ar);

                /* create the SocketStateObject */
                SocketStateObject state = new SocketStateObject();
                state.m_SocketFd = socketFd;
                this.fd=socketFd;
                //state.m_StringBuilder = new StringBuilder( this.textBoxAddr.Text.ToString(), 8 );
                setThreadedStatusLabel("Wait! Reading...");
                //socketFd.Send(dataBuf, dataBuf.Length, 0);
                Login = this.textBoxLogin.Text.ToString();
                Password = this.textBoxPassword.Text.ToString();
                mess =Login+"\n"+Password+"\n";
                Buf = Encoding.ASCII.GetBytes(mess);

                socketFd.Send(Buf, Buf.Length, 0);
                setThreadedButton(true);
                //state.m_SocketFd.BeginReceive(state.m_DataBuf, 0, SocketStateObject.BUF_SIZE, 0, new AsyncCallback(ReceiveCallback), state);


            }
            catch (Exception exc)
            {
                MessageBox.Show("Exception:\t\n" + exc.Message.ToString());
                setThreadedStatusLabel("Check \"Server Info\" and try again!");
                setThreadedButton(true);
            }
        }

        private void GetHostEntryCallback(IAsyncResult ar)
        {
            try
            {
                IPHostEntry hostEntry = null;
                IPAddress[] addresses = null;
                Socket socketFd = null;
                IPEndPoint endPoint = null;

                /* complete the DNS query */
                hostEntry = Dns.EndGetHostEntry(ar);
                addresses = hostEntry.AddressList;

                /* create a socket */
                socketFd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                
                /* remote endpoint for the socket */
                endPoint = new IPEndPoint(addresses[0], 1234);
                
                frm = new Form2(socketFd, this.textBoxLogin.Text.ToString(), endPoint, this);
                //frm.Show();
                setThreadedStatusLabel("Wait! Connecting...");

                /* connect to the server */
                socketFd.BeginConnect(endPoint, new AsyncCallback(ConnectCallback), socketFd);

                /* get DNS host information */
            }
            catch (Exception exc)
            {
                MessageBox.Show("Exception:\t\n" + exc.Message.ToString());
                setThreadedStatusLabel("Check \"Server Info\" and try again!");
                setThreadedButton(true);
            }
        }

        private void buttonGetDate_Click(object sender, EventArgs e)
        {
            try
            {
                setThreadedButton(false);
                setThreadedTextBox("");
                setThreadedStatusLabel("Wait! DNS query...");
                /* get DNS host information */
                Dns.BeginGetHostByName("192.168.1.14", new AsyncCallback(GetHostEntryCallback), null);
                //Form2 frm = new Form2(this.fd, this.textBoxLogin.Text.ToString(), endPoint, this);
                frm.Show();
                frm.startReceive();

                //this.Hide();
                // Thread t = new Thread(StartReceiveMess);
                //t.Start();



            }
            catch (Exception exc)
            {
                MessageBox.Show("Exception:\t\n" + exc.Message.ToString());
                setThreadedStatusLabel("Check \"Server Info\" and try again!");
                setThreadedButton(true);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPort_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPort_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public class SocketStateObject
    {
        public const int BUF_SIZE = 1024;
        public byte[] m_DataBuf = new byte[BUF_SIZE];
        public StringBuilder m_StringBuilder = new StringBuilder();
        public Socket m_SocketFd = null;
  
    }


   
}