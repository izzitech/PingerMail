// *********************************************************************
//  PingerMail
//
//  Autor: Iván E. Sierra
//  Fecha: Miercoles 28 de Agosto de 2013
//  Ultima modificación: Miercoles 1 de Diciembre de 2014
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PingerMail {
    public partial class Form1 : Form {
        XDocument xml = new XDocument();

        //DataTable dtDGVhosts;
        //DataView dvDGVhosts;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            //// SIN USO
            // dtDGVhosts = CrearTablaDeHosts();
            // dtDGVhosts = LlenarTablaDeHosts(dtDGVhosts, DataInternaIO.dataInterna.Terminales);
            // dvDGVhosts = new DataView(dtDGVhosts);

            DataInternaIO.read();
            dataGridView1.DataSource = DataInternaIO.dataInterna.Terminales;

            checkBox1.Checked = DataInternaIO.dataInterna.ChequeoAutomatico;
            checkBox3.Checked = DataInternaIO.dataInterna.EnviaMailAutomatico;
            checkBox2.Checked = DataInternaIO.dataInterna.CierreAutomatico;
        }

        #region Funciones

        //// SIN USO
        //private DataTable CrearTablaDeHosts() {
        //    DataTable hosts = new DataTable("Tabla de hosts");
        //    DataColumn direccionIP = new DataColumn("Direccion IP");
        //    DataColumn nombre = new DataColumn("Nombre");
        //    DataColumn ubicacion = new DataColumn("Ubicacion");
        //    DataColumn descripcion = new DataColumn("Descripcion");
        //    DataColumn estado = new DataColumn("Estado");
        //    DataColumn online = new DataColumn("Online");
        //    DataColumn id = new DataColumn("ID");

        //    direccionIP.DataType = System.Type.GetType("System.String");
        //    nombre.DataType = System.Type.GetType("System.String");
        //    ubicacion.DataType = System.Type.GetType("System.String");
        //    descripcion.DataType = System.Type.GetType("System.String");
        //    estado.DataType = System.Type.GetType("System.String");
        //    online.DataType = System.Type.GetType("System.Boolean");
        //    id.DataType = System.Type.GetType("System.Int64");

        //    hosts.Columns.Add(direccionIP);
        //    hosts.Columns.Add(nombre);
        //    hosts.Columns.Add(ubicacion);
        //    hosts.Columns.Add(descripcion);
        //    hosts.Columns.Add(estado);
        //    hosts.Columns.Add(online);
        //    hosts.Columns.Add(id);

        //    return hosts;
        //}

        //// SIN USO
        //private DataTable LlenarTablaDeHosts(DataTable hosts, List<Terminal> terminales) {
        //    for (int i = 0; i < terminales.Count; i++) {
        //        DataRow row;
        //        row = hosts.NewRow();
        //        row["Direccion IP"] = terminales[i].direccionIP;
        //        row["Nombre"] = terminales[i].nombre;
        //        row["Ubicacion"] = terminales[i].ubicacion;
        //        row["Descripcion"] = terminales[i].descripcion;
        //        row["Estado"] = terminales[i].estado.ToString();
        //        row["ID"] = i;

        //        hosts.Rows.Add(row);
        //    }

        //    return hosts;
        //}

        //// SIN USO
        //private List<Terminal> PingearListaDeHostsEnDataGridView() {
        //    Ping ping = new Ping();
        //    List<Terminal> terminalesConFalla = new List<Terminal>();

        //    for (int i = 0; i < dataGridView1.Rows.Count; i++) {
        //        PingReply pingReply = ping.Send(dataGridView1.Rows[i].Cells["Direccion IP"].Value.ToString());
        //        if (pingReply.Status == IPStatus.Success) {
        //            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
        //        } else {
        //            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;

        //            Terminal terminalConFalla = new Terminal();
        //            terminalConFalla.direccionIP = dataGridView1.Rows[i].Cells["Direccion IP"].Value.ToString();
        //            terminalConFalla.nombre = dataGridView1.Rows[i].Cells["Nombre"].Value.ToString();
        //            terminalConFalla.ubicacion = dataGridView1.Rows[i].Cells["Ubicacion"].Value.ToString();
        //            terminalConFalla.descripcion = dataGridView1.Rows[i].Cells["Descripcion"].Value.ToString();
        //            terminalesConFalla.Add(terminalConFalla);
        //        }
        //        dataGridView1.Refresh();
        //    }
        //    return terminalesConFalla;
        //}

        private List<Terminal> PingearYDevolverTerminalesConFalla(ref List<Terminal> terminales) {
            Ping ping = new Ping();
            List<Terminal> terminalesConFalla = new List<Terminal>();

            for (int i = 0; i < terminales.Count; i++) {
                PingReply pingReply = ping.Send(terminales[i].direccionIP);
                if (pingReply.Status == IPStatus.Success) {
                    terminales[i].online = true;
                } else {
                    terminales[i].online = false;

                    Terminal terminalConFalla = new Terminal();
                    terminalConFalla.direccionIP = terminales[i].direccionIP;
                    terminalConFalla.nombre = terminales[i].nombre;
                    terminalConFalla.ubicacion = terminales[i].ubicacion;
                    terminalConFalla.descripcion = terminales[i].descripcion;
                    terminalesConFalla.Add(terminalConFalla);
                }
            }
            return terminalesConFalla;
        }

        private void ColorearTerminalesEnDGV() {
            for (int i = 0; i < dataGridView1.Rows.Count; i++) {
                if ((bool)dataGridView1.Rows[i].Cells["Online"].Value) {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                } else {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void EnviarInformePorMail(List<Terminal> terminalesConFalla) {
            string cadenaTerminalesConFalla;
            string mailSubject = "Resultado de Pings: ";
            int cantidadTotalDeHosts = DataInternaIO.dataInterna.Terminales.Count;
            int cantidadDeHostsOffline = terminalesConFalla.Count;

            if (cantidadDeHostsOffline > 0) {
                if (terminalesConFalla.Count == 1) {
                    cadenaTerminalesConFalla = string.Format("se registraron {0} hosts.\nNo respondio el siguiente host: \n\n", cantidadTotalDeHosts);
                    cadenaTerminalesConFalla += terminalesConFalla[0].nombre + ": " + terminalesConFalla[0].descripcion + "\n";
                    mailSubject += "un host inactivo";
                } else {
                    cadenaTerminalesConFalla = string.Format("se registraron {0} hosts.\nNo respondieron los siguientes {1} hosts: \n\n", cantidadTotalDeHosts, cantidadDeHostsOffline);
                    foreach (Terminal terminal in terminalesConFalla) {
                        cadenaTerminalesConFalla += (terminal.nombre + ": " + terminal.descripcion + "\n");
                    }
                    mailSubject += string.Format("ALERTA {0} hosts inactivos", cantidadDeHostsOffline);
                }
            } else {
                cadenaTerminalesConFalla = string.Format("se registraron {0} hosts.\nTodos los hosts se encuentran activos.", cantidadTotalDeHosts);
                mailSubject += "Correcto";
            }


            estado estado;
            try {
                foreach (Mail mail in DataInternaIO.dataInterna.Mails) {
                    if (Enum.TryParse(mail.estado, out estado)) {
                        if (estado == estado.Normal) {
                            izzimail.Mail.Send( DataInternaIO.dataInterna.senderSenderAddress,
                                                mail.direccionDeMail,
                                                mail.nombre,
                                                mailSubject,
                                                mail.nombre + ", " + cadenaTerminalesConFalla,
                                                "Pinger",
                                                DataInternaIO.dataInterna.senderServerAddress,
                                                DataInternaIO.dataInterna.senderServerPort,
                                                DataInternaIO.dataInterna.senderSSLEnabled,
                                                DataInternaIO.dataInterna.senderUser,
                                                DataInternaIO.dataInterna.senderPass
                                               );
                        }
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show("You're in trouble... " + ex.Message);
            }

        }

        #endregion

        private void button3_Click(object sender, EventArgs e) {
            PingearYDevolverTerminalesConFalla(ref DataInternaIO.dataInterna.Terminales);
            ColorearTerminalesEnDGV();
        }

        private void button1_Click(object sender, EventArgs e) {
            Configuracion configuracionForm = new Configuracion();
            configuracionForm.ShowDialog();
        }

        private void Form1_Shown(object sender, EventArgs e) {
            List<Terminal> terminalesConFalla;
            if (DataInternaIO.dataInterna.ChequeoAutomatico) {
                terminalesConFalla = PingearYDevolverTerminalesConFalla(ref DataInternaIO.dataInterna.Terminales);

                if (DataInternaIO.dataInterna.EnviaMailAutomatico) {
                    EnviarInformePorMail(terminalesConFalla);
                }

                if (DataInternaIO.dataInterna.CierreAutomatico) {
                    Application.Exit();
                } else {
                    ColorearTerminalesEnDGV();
                }
            }
        }
    }
}
