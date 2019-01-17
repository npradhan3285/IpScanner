using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpScanner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            foreach (System.Net.NetworkInformation.NetworkInterface net in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                if (net.OperationalStatus ==System.Net.NetworkInformation.OperationalStatus.Up)
                {
                    IPInterfaceProperties prop = net.GetIPProperties();
                    if (!string.IsNullOrWhiteSpace(logRichTextBox.Text))
                    {

                        logRichTextBox.AppendText("\r\n" + net.Name + " : sN / w connected");
                        foreach (UnicastIPAddressInformation ip in net.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                logRichTextBox.AppendText("\r\n" + "IP :" + ip.Address.ToString());
                                logRichTextBox.AppendText("\r\n" + "Speed :" + net.Speed);
                                //Console.WriteLine(ip.Address.ToString());
                            }
                        }

                    }
                    else
                    {
                        logRichTextBox.AppendText(net.Name + " : N / w connected");
                        foreach (UnicastIPAddressInformation ip in net.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                logRichTextBox.AppendText("\r\n" + "IP :" + ip.Address.ToString());
                                logRichTextBox.AppendText("\r\n" + "Speed :" + net.Speed);
                                //Console.WriteLine(ip.Address.ToString());
                            }
                        }
                    }
                    logRichTextBox.ScrollToCaret();
                    //Console.WriteLine("N/w connected");
                }
                else
                    Console.WriteLine("N/w not connected");
            }
            IPAddress[] IPS = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in IPS)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    logRichTextBox.AppendText("\r\n" + "IP address: " + ip);
                    //Console.WriteLine("IP address: " + ip);
                }
            }
            logRichTextBox.AppendText("\r\n" + "---------------------------------------------------------------------");
            IPGlobalProperties network = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] connections = network.GetActiveTcpConnections();
            foreach (TcpConnectionInformation tci in connections)
            {
                logRichTextBox.AppendText("\r\n" + "Local Address: " + tci.LocalEndPoint.Address.ToString() + "Local Port: " + tci.LocalEndPoint.Port.ToString());
                logRichTextBox.AppendText("\r\n" + "Remote Address: " + tci.RemoteEndPoint.Address.ToString() + "Remote Port: " + tci.RemoteEndPoint.Port.ToString());
            }
        }
    }
}
