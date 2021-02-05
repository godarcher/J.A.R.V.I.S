using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication6
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public static string password = "011000010110111001101111011011100111100101101101011011110111010101110011";

        private void Form3_Load(object sender, EventArgs e)
        {
            SoundPlayer nc = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\lockdown.wav");
            nc.Play(); //lockdown initialized
        }

        private void button1_Click(object sender, EventArgs e) //unlock
        {
            if (textBox1.Text == password)
            {
                this.Close();
            }
        }
    }
}
