using System;
using System.Windows.Forms;

namespace PingerMail {
    public partial class Configuracion : Form {
        public Configuracion() {
            InitializeComponent();
            this.Icon = PingerMail.Properties.Resources.PingerMail;
        }

        private void button1_Click(object sender, EventArgs e) {
            DataInternaIO.dataInterna.senderSenderAddress = textBox1.Text;
            DataInternaIO.dataInterna.senderUser = textBox2.Text;
            DataInternaIO.dataInterna.senderPass = textBox3.Text;
            DataInternaIO.dataInterna.senderServerAddress = textBox4.Text;
            DataInternaIO.dataInterna.senderServerPort = (ushort)numericUpDown1.Value;
            DataInternaIO.dataInterna.senderSSLEnabled = trackBar1.Value == 1 ? true : false;
            DataInternaIO.write();
        }

        private void button2_Click(object sender, EventArgs e) {
            Varios.CrearDataInternaDePruebaEnMon();
            MostrarDatosEnUI();
        }

        private void button3_Click(object sender, EventArgs e) {
            DataInternaIO.read();
            MostrarDatosEnUI();
        }

        private void button4_Click(object sender, EventArgs e) {
            Application.Restart();
        }

        private void MostrarDatosEnUI() {
            textBox1.Text = DataInternaIO.dataInterna.senderSenderAddress;
            textBox2.Text = DataInternaIO.dataInterna.senderUser;
            textBox3.Text = DataInternaIO.dataInterna.senderPass;
            textBox4.Text = DataInternaIO.dataInterna.senderServerAddress;
            numericUpDown1.Value = DataInternaIO.dataInterna.senderServerPort;
            trackBar1.Value = DataInternaIO.dataInterna.senderSSLEnabled ? 1 : 0;
        }
    }
}
