using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace DayTime
{
    public partial class Form3 : Form
    {
        private Form obj;
        private Socket fd;
        private IPEndPoint endPoint = null;
        public Form3(Socket socketFd,IPEndPoint end)
        {
            InitializeComponent();
            this.obj = this;
            this.fd = socketFd;
            this.endPoint = end;

        }

        

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                /* retrieve the socket from the state object */
                SocketStateObject2 state = (SocketStateObject2)ar.AsyncState;
                Socket socketFd = state.m_SocketFd;
                byte[] Buf;
                string mess;
                /* complete the connection */
                socketFd.EndConnect(ar);

                /* create the SocketStateObject */
                ;
                //state.m_StringBuilder = new StringBuilder( this.textBoxAddr.Text.ToString(), 8 );
                //socketFd.Send(dataBuf, dataBuf.Length, 0);

                mess = state.flag.ToString() + "\n" + state.msg.Length + "\n" + state.msg;
                Buf = Encoding.ASCII.GetBytes(mess);

                socketFd.Send(Buf, Buf.Length, 0);
                /* begin receiving the data */


            }
            catch (Exception exc)
            {
                MessageBox.Show("Exception:\t\n" + exc.Message.ToString());
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UsernameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PrivateMessageBox_Enter(object sender, EventArgs e)
        {

        }

        private void MessageLabel_Click(object sender, EventArgs e)
        {

        }

        private void MessageBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            SocketStateObject2 state = new SocketStateObject2();
            state.m_SocketFd = fd;
            string uname= this.UsernameBox.Text.ToString();
            string pmsg= this.MessageBox1.Text.ToString();
            state.msg = uname + "/t" + pmsg;
            state.flag = 6;
            string mess;
            byte[] Buf;
            //fd.BeginConnect(endPoint, new AsyncCallback(ConnectCallback),state);
            mess = state.flag.ToString() + "\n" + state.msg.Length + "\n" + state.msg;
            Buf = Encoding.ASCII.GetBytes(mess);

            fd.Send(Buf, Buf.Length, 0);
            this.Close();
        }
    }
}
