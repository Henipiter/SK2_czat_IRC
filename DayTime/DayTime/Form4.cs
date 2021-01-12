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
using System.Threading;

/*
 * Okienko obslugujace dodawanie/usuwanie/zmiane forum
 */

namespace DayTime
{
    public partial class Form4 : Form
    {
        private Form obj;
        private Socket fd;
        private IPEndPoint endPoint = null;
        private int function;
        public Form4(Socket socketFd,IPEndPoint end,int func)
        {
            InitializeComponent();
            this.obj = this;
            this.fd = socketFd;
            this.endPoint = end;
            this.function = func;
            if (this.function == 4) //aktualizacia okienka w zależności od wykonywanego polecenia
            {
                this.AddBox.Text ="Add forum";
                this.AddButton.Text = "Add";
            }
            else if(this.function == 5)
            {
                this.AddBox.Text = "Delete forum";
                this.AddButton.Text = "Delete";
            }
            else if (this.function == 1)
            {
                this.AddBox.Text = "Join forum";
                this.AddButton.Text = "Join";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxLogin_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddBox_Enter(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)////funcja obsługująca przycisk dodawania/usuwania/zmiany forum
        {
            SocketStateObject4 state = new SocketStateObject4();
            state.m_SocketFd = fd;
            state.msg = this.NameBox.Text.ToString();
            state.flag = this.function;//ustawienie flagi polecenia
            byte[] Buf;
            string mess;
            mess = state.flag.ToString() + "\n" + state.msg.Length + "\n" + state.msg;
            Buf = Encoding.ASCII.GetBytes(mess);

            // fd.BeginConnect(endPoint, new AsyncCallback(ConnectCallback2), state);
            fd.Send(Buf, Buf.Length, 0);//wysłanie polecenia do serwera
            
            this.Close();
        }
        public class SocketStateObject4
        {
            public const int BUF_SIZzE = 1024;
            public byte[] m_DataBuf = new byte[BUF_SIZzE];
            public StringBuilder m_StringBuilder = new StringBuilder();
            public Socket m_SocketFd = null;
            public string msg;
            public int flag;
        }
    }
}
