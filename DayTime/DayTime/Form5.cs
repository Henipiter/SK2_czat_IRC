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
    public partial class Form5 : Form
    {
        private Form obj;
        private Socket fd;
        private IPEndPoint endPoint = null;
        public Form5()
        {
            InitializeComponent();
        }
        private Form2 mainForm = null;
        public Form5(Form callingForm, Socket socketFd, IPEndPoint end)
        {
            InitializeComponent();
            this.obj = this;
            MessageBox.Show("Creating...");
            mainForm = callingForm as Form2;
            this.fd = socketFd;
            this.endPoint = end;
            SocketStateObject3 state = new SocketStateObject3();
            state.m_SocketFd = fd;
            string mes;
            int i = 0;
            while (true)
            {
                i++;

                MessageBox.Show("enedeu");
                mes = ReceiveMess(state);
                MessageBox.Show("dwadwa " +mes);

                //fd.BeginReceive(state.m_DataBuf, 0, SocketStateObject3.BUF_SIZE, 0, new AsyncCallback(ReceiveCallback3), state);
                if (mes.Length >= 1)
                {
                    switch (mes[0])
                    {

                        case '1':
                            this.mainForm.setThreadedChatbox1(state.m_StringBuilder.ToString());
                            break;
                        case '3':
                            //wyloguj
                            break;
                        case '7':
                            this.mainForm.setThreadedForumListBox(state.m_StringBuilder.ToString());
                            break;
                        case '8':
                            this.mainForm.setThreadedUserListBox(state.m_StringBuilder.ToString());
                            break;
                        default:
                            break;
                    }

                }
            }
        }


        public static string ReceiveMess(SocketStateObject3 state)
        {
            var buffer = new List<byte>();
            byte[] current;
            while (true)
            {
                current = new byte[128];
                int counter = state.m_SocketFd.Receive(current, current.Length, SocketFlags.None);

                if (current[ counter-1  ] == 10)
                {
                    break;
                }
                for(int i = 0; i < counter; i++)
                {
                    buffer.Add(current[i]);
                }
            }
            var w = buffer;
            var ww = w.ToString();
            return Encoding.Default.GetString(buffer.ToArray());
        }
        private void ReceiveCallback3(IAsyncResult ar)
        {
            try
            {
                /* retrieve the SocketStateObject */
                SocketStateObject3 state = (SocketStateObject3)ar.AsyncState;
                Socket socketFd = state.m_SocketFd;
                
                /* read data */
                int size = socketFd.EndReceive(ar);
                string a;
                char b = '1';

                while(b!= '\n')
                {
                    state.m_StringBuilder.Append(Encoding.ASCII.GetString(state.m_DataBuf, 0, size));
                    MessageBox.Show(state.m_StringBuilder.ToString() + size.ToString());

                    a = state.m_StringBuilder.ToString();
                    b = a[a.Length - 1];
                }

                /*
                if (b != '\n')
                {
                    state.m_StringBuilder.Append(Encoding.ASCII.GetString(state.m_DataBuf, 0, size));
                    MessageBox.Show(state.m_StringBuilder.ToString() + size.ToString());
                   
                    a = state.m_StringBuilder.ToString();
                    b = a[a.Length - 1];
                    MessageBox.Show("kupa" + a );
                    socketFd.BeginReceive(state.m_DataBuf, 0, SocketStateObject3.BUF_SIZE, 0, new AsyncCallback(ReceiveCallback3), state);
                }*/
                
                    string msgg = state.m_StringBuilder.ToString();
                    MessageBox.Show("aaa");
                    /* all the data has arrived */
                    if (state.m_StringBuilder.Length >= 1)
                    {
                        switch (msgg[0])
                        {

                            case '1':
                                this.mainForm.setThreadedChatbox1(state.m_StringBuilder.ToString());
                                break;
                            case '3':
                                //wyloguj
                                break;
                            case '7':
                                this.mainForm.setThreadedForumListBox(state.m_StringBuilder.ToString());
                                break;
                            case '8':
                                this.mainForm.setThreadedUserListBox(state.m_StringBuilder.ToString());
                                break;
                            default:
                                break;
                        }

                    }
                state = null;
                socketFd = null;
                
            }
            catch (Exception exc)
            {

                MessageBox.Show("wyjatek");
            }
        }
        



       

        
        
    }
    public class SocketStateObject3
    {
        public const int BUF_SIZE = 1024;
        public byte[] m_DataBuf = new byte[BUF_SIZE];
        public StringBuilder m_StringBuilder = new StringBuilder();
        public Socket m_SocketFd = null;
        public string msg;
        public int flag;
    }
}

