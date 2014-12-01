using System.Collections.Generic;

namespace PingerMail {
    public static class Varios {
        public static void CrearDataInternaDePruebaEnMon() {
            DataInternaIO.dataInterna.ChequeoAutomatico = false;
            DataInternaIO.dataInterna.EnviaMailAutomatico = true;
            DataInternaIO.dataInterna.CierreAutomatico = false;

            DataInternaIO.dataInterna.Mails = new List<Mail>();
            DataInternaIO.dataInterna.Terminales = new List<Terminal>();
            CrearTerminalesDePrueba();
            CrearListaDeMails();
            CrearSender();
            DataInternaIO.write();
            DataInternaIO.read();
        }

        private static void CrearListaDeMails() {
            Mail mail = new Mail();
            mail.id = "0";
            mail.direccionDeMail = "test1@mail.com";
            mail.nombre = "Chuck";
            mail.estado = "Normal";
            DataInternaIO.dataInterna.AgegarMail(mail);

            mail = new Mail();
            mail.id = "1";
            mail.direccionDeMail = "test2@mail.com";
            mail.nombre = "Bob";
            mail.estado = "Normal";
            DataInternaIO.dataInterna.AgegarMail(mail);

            mail = new Mail();
            mail.id = "2";
            mail.direccionDeMail = "test3@mail.com";
            mail.nombre = "Steve";
            mail.estado = "Ignorar";
            DataInternaIO.dataInterna.AgegarMail(mail);
        }

        private static void CrearTerminalesDePrueba() {
            Terminal terminal = new Terminal();
            terminal.id = "0";
            terminal.direccionIP = "192.168.1.1";
            terminal.nombre = "Gateway";
            terminal.ubicacion = "Datacenter";
            terminal.descripcion = "Cisco Gateway";
            terminal.estado = "Normal";
            DataInternaIO.dataInterna.AgegarTerminal(terminal);

            terminal = new Terminal();
            terminal.id = "1";
            terminal.direccionIP = "192.168.1.2";
            terminal.nombre = "Main server";
            terminal.ubicacion = "Datacenter";
            terminal.descripcion = "Main server with virtual machines";
            terminal.estado = "Normal";
            DataInternaIO.dataInterna.AgegarTerminal(terminal);

            terminal = new Terminal();
            terminal.id = "2";
            terminal.direccionIP = "192.168.1.3";
            terminal.nombre = "DMZ";
            terminal.ubicacion = "Datacenter";
            terminal.descripcion = "Webserver";
            terminal.estado = "Normal";
            DataInternaIO.dataInterna.AgegarTerminal(terminal);

            terminal = new Terminal();
            terminal.id = "3";
            terminal.direccionIP = "192.168.1.4";
            terminal.nombre = "NAS";
            terminal.ubicacion = "Datacenter";
            terminal.descripcion = "Network Attached Storage";
            terminal.estado = "Normal";
            DataInternaIO.dataInterna.AgegarTerminal(terminal);
        }

        private static void CrearSender() {
            DataInternaIO.dataInterna.senderUser = "sender@testserver.com";
            DataInternaIO.dataInterna.senderPass = "password-without-encryption";
        }
    }
}