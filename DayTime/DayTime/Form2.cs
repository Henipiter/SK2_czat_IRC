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

namespace DayTime
{
    public partial class Form2 : Form
    {
        private Form obj;
        private Socket fd;
        private IPEndPoint endPoint = null;
        delegate void setThreadedtextBox3UsernameCallback(String text);
        delegate void setThreadedtextBox4ForumnameCallback(String text);
        delegate void setThreadedForumListBoxCallback(String text);
        delegate void setThreadedUserListBoxCallback(String text);
        delegate void setThreadedtextBox1ChatCallback(String text);
        delegate void setThreadedForumChangeButtonCallback(bool status);
        delegate void setThreadedForumAddButtonCallback(bool status);
        delegate void setThreadedForumDeleteButtonCallback(bool status);
        delegate void setThreadedUserWriteButtonCallback(bool status);
        public Form2(Socket socketFd,string username, IPEndPoint end)
        {
            InitializeComponent();
            this.obj = this;
            this.fd = socketFd;
            this.endPoint = end;
            this.setThreadedtextBox3Username(username);
            
            //receive.Hide();
            
        }
        

         string ReceiveMess(SocketStateObject state)
        {
            var buffer = new List<byte>();
            byte[] current;
            while (true)
            {
                current = new byte[1];
                int counter = this.fd.Receive(current, current.Length, SocketFlags.None);

                if (current[0] == 10)
                {
                    break;
                }

                buffer.Add(current[0]);

            }
            var w = buffer.ToArray();
            var ww = w.ToString();
            return Encoding.Default.GetString(buffer.ToArray());
        }
        public void StartReceiveMess()
        {
            SocketStateObject state = new SocketStateObject();
            state.m_SocketFd = fd;
            string mes;
            int i = 0;
            while (true)
            {
                i++;
                MessageBox.Show("enedeu");
                mes = ReceiveMess(state);
                MessageBox.Show("dwadwa " + mes);
                //fd.BeginReceive(state.m_DataBuf, 0, SocketStateObject3.BUF_SIZE, 0, new AsyncCallback(ReceiveCallback3), state);
                if (mes.Length >= 1)
                {
                    switch (mes[0])
                    {

                        case '1':
                            ///setThreadedChatbox1(state.m_StringBuilder.ToString());
                            break;
                        case '3':
                            //wyloguj
                            break;
                        case '7':
                            //setThreadedForumListBox(state.m_StringBuilder.ToString());
                            break;
                        case '8':
                            //this.setThreadedUserListBox(state.m_StringBuilder.ToString());
                            break;
                        default:
                            break;
                    }

                }
            }
        }
        







        private void setThreadedChangeForumButton(bool status)
        {
            if (this.ForumChangeButton.InvokeRequired)
            {
                setThreadedForumChangeButtonCallback buttonCallback = new setThreadedForumChangeButtonCallback(setThreadedChangeForumButton);
                this.obj.Invoke(buttonCallback, status);
            }
            else
            {
                this.ForumChangeButton.Enabled = status;
                
            }
        }
        private void setThreadedAddForumButton(bool status)
        {
            if (this.ForumAddButton.InvokeRequired)
            {
                setThreadedForumAddButtonCallback buttonCallback = new setThreadedForumAddButtonCallback(setThreadedAddForumButton);
                this.obj.Invoke(buttonCallback, status);
            }
            else
            {
                this.ForumAddButton.Enabled = status;
            }
        }
        private void setThreadedDeleteForumButton(bool status)
        {
            if (this.ForumDeleteButton.InvokeRequired)
            {
                setThreadedForumDeleteButtonCallback buttonCallback = new setThreadedForumDeleteButtonCallback(setThreadedDeleteForumButton);
                this.obj.Invoke(buttonCallback, status);
            }
            else
            {
                this.ForumDeleteButton.Enabled = status;
            }
        }
        private void setThreadedUserWriteButton(bool status)
        {
            if (this.UserWriteButton.InvokeRequired)
            {
                setThreadedUserWriteButtonCallback buttonCallback = new setThreadedUserWriteButtonCallback(setThreadedUserWriteButton);
                this.obj.Invoke(buttonCallback, status);
            }
            else
            {
                this.UserWriteButton.Enabled = status;
            }
        }

        private void setThreadedtextBox3Username(String text)
        {
            if (this.textBox3.InvokeRequired)
            {
                setThreadedtextBox3UsernameCallback textBoxCallback = new setThreadedtextBox3UsernameCallback(setThreadedtextBox3Username);
                this.obj.Invoke(textBoxCallback, text);
            }
            else
            {
                this.textBox3.Text = text;
            }
        }
        private void setThreadedtextBox4Forumname(String text)
        {
            if (this.textBox4.InvokeRequired)
            {
                setThreadedtextBox4ForumnameCallback textBoxCallback = new setThreadedtextBox4ForumnameCallback(setThreadedtextBox4Forumname);
                this.obj.Invoke(textBoxCallback, text);
            }
            else
            {
                //text = text.Substring(0, text.Length - 2);
                this.textBox4.Text = text;
            }
        }

        public void setThreadedForumListBox(String text)
        {
            if (this.ForumListBox.InvokeRequired)
            {
                setThreadedForumListBoxCallback textBoxCallback = new setThreadedForumListBoxCallback(setThreadedForumListBox);
                this.obj.Invoke(textBoxCallback, text);
            }
            else
            {
                //text = text.Substring(0, text.Length - 2);
                this.ForumListBox.Text = text;
            }
        }

        public void setThreadedUserListBox(String text)
        {
            if (this.UserListBox.InvokeRequired)
            {
                setThreadedUserListBoxCallback textBoxCallback = new setThreadedUserListBoxCallback(setThreadedUserListBox);
                this.obj.Invoke(textBoxCallback, text);
            }
            else
            {
                //text = text.Substring(0, text.Length - 2);
                this.UserListBox.Text = text;
            }
        }

        public void setThreadedChatbox1(String text)
        {
            if (this.textBox1.InvokeRequired)
            {
                setThreadedtextBox1ChatCallback textBoxCallback = new setThreadedtextBox1ChatCallback(setThreadedChatbox1);
                this.obj.Invoke(textBoxCallback, text);
            }
            else
            {
                //text = text.Substring(0, text.Length - 2);
                this.textBox1.Text = text;
            }
        }

        private void ReceiveCallback2(IAsyncResult ar)
        {
            try
            {
                /* retrieve the SocketStateObject */
                SocketStateObject2 state = (SocketStateObject2)ar.AsyncState;
                Socket socketFd = state.m_SocketFd;
                
                /* read data */
                int size = socketFd.EndReceive(ar);
                string a;
                char b = '1';
                string c;
                
                if (b!='\n')
                {
                    state.m_StringBuilder.Append(Encoding.ASCII.GetString(state.m_DataBuf, 0, size));
                    MessageBox.Show(state.m_StringBuilder.ToString()+ size.ToString());
                    /* get the rest of the data */
                    a = state.m_StringBuilder.ToString();
                    b = a[a.Length - 1]; 
                    MessageBox.Show("jjj" + a + " d " +Char.ToString(b)+ "ss");
                    if (b == '\n')
                    {
                        MessageBox.Show("aaa");
                        /* all the data has arrived */
                        
                            switch (a[0])
                            {
                                case '2':
                                    setThreadedChatbox1(a.Substring(1));
                                    break;
                                case '3':
                                    //wyloguj
                                    break;
                                case '7':
                                    setThreadedForumListBox(a.Substring(1));
                                    break;
                                case '8':
                                    setThreadedUserListBox(state.m_StringBuilder.ToString());
                                    break;
                                default:
                                    break;
                            }
                        state.m_StringBuilder = null;
                        
                    }
                    socketFd.BeginReceive(state.m_DataBuf, 0, SocketStateObject.BUF_SIZE, 0, new AsyncCallback(ReceiveCallback2), state);
                }
                else
                {
                    MessageBox.Show("aaa");
                    /* all the data has arrived */
                    if (state.m_StringBuilder.Length > 1)
                    {
                        switch (state.flag)
                        {
                            case 2:
                                setThreadedChatbox1(state.m_StringBuilder.ToString());
                                break;
                            case 3:
                                //wyloguj
                                break;
                            case 7:
                                setThreadedForumListBox(state.m_StringBuilder.ToString());
                                break;
                            case 8:
                                setThreadedUserListBox(state.m_StringBuilder.ToString());
                                break;
                            default:
                                break;
                        }

                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Exception:\t\n" + exc.Message.ToString());
            }
        }
       
        private void ConnectCallback2(IAsyncResult ar)
        {
            try
            {
                /* retrieve the socket from the state object */
                SocketStateObject2 state = (SocketStateObject2)ar.AsyncState;
                Socket socketFd = state.m_SocketFd;
               
                /* complete the connection */
                socketFd.EndConnect(ar);

                /* create the SocketStateObject */
                
                //state.m_StringBuilder = new StringBuilder( this.textBoxAddr.Text.ToString(), 8 );
                //socketFd.Send(dataBuf, dataBuf.Length, 0);
                byte[] Buf;
                string mess = state.msg;
                //mess = state.flag.ToString() + "\n" + state.msg.Length + "\n" + state.msg;
                Buf = Encoding.ASCII.GetBytes(mess);
                socketFd.Send(Buf, Buf.Length, 0);
                //socketFd.BeginReceive(state.m_DataBuf, 0, SocketStateObject.BUF_SIZE, 0, new AsyncCallback(ReceiveCallback2), state);
                
                /* begin receiving the data */


            }
            catch (Exception exc)
            {
                MessageBox.Show("Exception:\t\n" + exc.Message.ToString());
            }
        }

        private void ForumRefreshButton_Click(object sender, EventArgs e)
        {
            SocketStateObject2 state = new SocketStateObject2();
            state.m_SocketFd = fd;
            state.msg = "";
            state.flag = 7;
            byte[] Buf;
            string mess;
            mess = state.flag.ToString() + "\n" + state.msg.Length + "\n" + state.msg;
            Buf = Encoding.ASCII.GetBytes(mess);
            
           // fd.BeginConnect(endPoint, new AsyncCallback(ConnectCallback2), state);
            fd.Send(Buf, Buf.Length, 0);

           // StartReceiveMess();
            //MessageBox.Show(":::"+state.m_StringBuilder.ToString());
        }
        private void ForumAddButton_Click(object sender, EventArgs e)
        {
            SocketStateObject2 state = new SocketStateObject2();
            state.m_SocketFd = fd;
            state.m_SocketFd.BeginReceive(state.m_DataBuf, 0, SocketStateObject.BUF_SIZE, 0, new AsyncCallback(ReceiveCallback2), state);
        }
        private void SendButton_Click(object sender, EventArgs e)
        {
            SocketStateObject2 state = new SocketStateObject2();
            state.m_SocketFd = fd;
            state.msg = this.textBox2.Text.ToString();
            state.flag = 2;
            fd.BeginConnect(endPoint, new AsyncCallback(ConnectCallback2), state);
        }

        

        private void ForumDeleteButton_Click(object sender, EventArgs e)
        {

        }

        

        public void ForumListBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserWriteButton_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3(fd, endPoint);
            frm.Show();
        }

        private void UserRefreshButton_Click(object sender, EventArgs e)
        {
            SocketStateObject2 state = new SocketStateObject2();
            state.m_SocketFd = fd;
            state.msg = "";
            state.flag = 8;
            fd.BeginConnect(endPoint, new AsyncCallback(ConnectCallback2), state);
            fd.BeginReceive(state.m_DataBuf, 0, SocketStateObject.BUF_SIZE, 0, new AsyncCallback(ReceiveCallback2), state);
        }

        private void UserListBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ForumChangeButton_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    public class SocketStateObject2
    {
        public const int BUF_SIZzE = 1024;
        public byte[] m_DataBuf = new byte[BUF_SIZzE];
        public StringBuilder m_StringBuilder = new StringBuilder();
        public Socket m_SocketFd = null;
        public string msg;
        public int flag;
    }
}
