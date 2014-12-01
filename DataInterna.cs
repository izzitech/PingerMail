using System;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace PingerMail {
    // Link al código que use como explicación para hacer el XML de datos.
    // http://stackoverflow.com/questions/1212742/xml-serialize-generic-list-of-serializable-objects
    // Referencia desde el msdn.
    // http://msdn.microsoft.com/en-us/library/58a18dwa%28v=vs.110%29.aspx

    public enum estado { Normal, Ignorar };

    [Serializable]
    [XmlRoot("DataInterna")]
    [XmlInclude(typeof(Terminal))]
    [XmlInclude(typeof(Mail))]
    public class DataInterna {
        // El orden de los items es orientativo. Por más que los pongas como los pongas, el XML se arma como se le canta el "root" (orto).

        [XmlArray("Terminales")]
        [XmlArrayItem("Terminal")]
        public List<Terminal> Terminales = new List<Terminal>();

        [XmlArray("Destinatarios")]
        [XmlArrayItem("Destinatario")]
        public List<Mail> Mails = new List<Mail>();

        [XmlElement("Chequear_IPs_automaticamente")]
        public bool ChequeoAutomatico { get; set; }

        [XmlElement("Enviar_mails_automaticamente")]
        public bool EnviaMailAutomatico { get; set; }

        [XmlElement("Cerrar_automaticamente")]
        public bool CierreAutomatico { get; set; }

        [XmlElement("SMTP_Sender_Address")]
        public string senderSenderAddress { get; set; }

        [XmlElement("SMTP_Server_Address")]
        public string senderServerAddress { get; set; }

        [XmlElement("SMTP_Server_Port")]
        public ushort senderServerPort { get; set; }

        [XmlElement("SMTP_SSL_Enabled")]
        public bool senderSSLEnabled { get; set; }

        [XmlElement("SMPT_User")]
        public string senderUser { get; set; }

        [XmlElement("SMTP_Pass")]
        public string senderPass { get; set; }

        public DataInterna() { }

        public void AgegarTerminal(Terminal terminal) {
            Terminales.Add(terminal);
        }

        public void AgegarMail(Mail mail) {
            Mails.Add(mail);
        }
    }

    [XmlType("Terminal")]
    public class Terminal {
        [XmlAttribute("ID", DataType = "string")]
        public string id { get; set; }

        [XmlElement("Direccion_IP")]
        public string direccionIP { get; set; }

        [XmlElement("Nombre")]
        public string nombre { get; set; }

        [XmlElement("Ubicacion")]
        public string ubicacion { get; set; }

        [XmlElement("Descripcion")]
        public string descripcion { get; set; }

        [XmlElement("Estado")]
        public string estado { get; set; }

        [XmlIgnore]
        public bool online { get; set; }

        public Terminal() { }

        public Terminal(string direccionIP, string nombre, string ubicacion, string descripcion, string estado) {
            this.direccionIP = direccionIP;
            this.nombre = nombre;
            this.ubicacion = ubicacion;
            this.descripcion = descripcion;
            this.estado = estado;
        }
    }

    [XmlType("Mail")]
    public class Mail {
        [XmlAttribute("ID", DataType = "string")]
        public string id { get; set; }

        [XmlElement("Direccion")]
        public string direccionDeMail { get; set; }

        [XmlElement("Nombre")]
        public string nombre { get; set; }

        [XmlElement("Estado")]
        public string estado { get; set; }

        public Mail() { }

        public Mail(string direccionDeMail, string nombre, string estado) {
            this.direccionDeMail = direccionDeMail;
            this.nombre = nombre;
            this.estado = estado;
        }
    }
}
