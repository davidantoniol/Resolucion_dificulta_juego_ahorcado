using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio_explicacion_masktestbox_y_rege
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public bool validar_gmail(String email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                //Normalizar el dominio
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examina la parte del dominio del correo electrónico y la normaliza.
                string DomainMapper(Match match)
                {
                    // Utilice la clase IdnMapping para convertir nombres de dominio Unicode.
                    var idn = new IdnMapping();

                    // Extraiga y procese el nombre de dominio(arroja ArgumentException en inválido)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

    private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Resultado: " + maskedTextBox1.Text;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String email = textBox1.Text;
            bool comprobar_correo = false;
            comprobar_correo = validar_gmail(email);

            if(comprobar_correo == true)
            {
                label4.Text = "El formato del correo es correcto.";
            }
            else
            {
                label4.Text = "El formato del correo no es correcto.";
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
