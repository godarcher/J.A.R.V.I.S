using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication6
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public static int number = 0; //how many numbers good
        public static int wronganswers = 0; //how many times wrong answered

        public static string usbinfo = ""; //info of plugged in usb devices
        public static string body = ""; //content of your mail

        //ip addres
        public static IPAddress GetIPAddress(string hostName)
        {
            Ping ping = new Ping();
            var replay = ping.Send(hostName);

            if (replay.Status == IPStatus.Success)
            {
                return replay.Address;
            }
            return null;
        }

        private void FormMain_KeyPress(object sender, KeyPressEventArgs e)
        {
         

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            SoundPlayer nc = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\ghostprotection.wav");
            nc.Play(); //notepad closed
            //pictureBox1.Image = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\gifbackground.gif");
        }


        private void timer1_Tick(object sender, EventArgs e) //two seconds
        {
            //pictureBox1.Image = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\impossible2.png");
            timer1.Enabled = false;
        }

        private void timer2_Tick(object sender, EventArgs e) //five seconds
        {

        }

        private void timer3_Tick(object sender, EventArgs e) //eight seconds
        {
            //pictureBox3.Image = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\pointless.png");
            timer3.Enabled = false;
        }

        private void timer4_Tick(object sender, EventArgs e) //ten seconds
        {
            //pictureBox4.Image = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\do.png");
            timer4.Enabled = false;
        }

        void window_MouseLeftButtonDown_1(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control || Control.ModifierKeys == Keys.Alt)
            {
                this.Close();
                Environment.Exit(1);
            }
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt)
            {
                this.Close();
                Environment.Exit(1);
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            if (number == 0) { number = 1; } //1 
            else if (number == 3) { number = 4; } //4
            else if (number == 8) { number = 9; } //9 
            else if (number == 12) { number = 13; } //13
            else
            {
                wronganswers = wronganswers + 1;
                if (wronganswers < 3) //you have three tries
                {
                    number = 0; //reset possible good parts
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                }
                else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
                {
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                               //detecting  user

                    //list of usb devices
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        if (drive.DriveType == DriveType.Removable)
                        {
                            usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                            Console.WriteLine(usbinfo);
                        }
                    }

                    //how much he knows
                    int partgood = number;
                    number = 0; //after copying data we reset our data.

                    //get computer location (in form of ip)
                    string location = GetIPAddress("google.com").ToString();

                    //get the time
                    string time = DateTime.Now.ToString("h:mm:ss tt");


                    //send a mail
                    string smtpAddress = "smtp.gmail.com";
                    int portNumber = 587;
                    bool enableSSL = true;

                    string emailFrom = "joostgrunwald2001@gmail.com";
                    string password = "Happy2001";
                    string emailTo = "joostgrunwald2001@gmail.com";
                    string subject = "Security breached";
                    if (usbinfo != "") //if usb's are plugged in
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + "The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " Info of the usb drives at the time: " + usbinfo
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    else if (usbinfo == "")
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + " The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " There were no usb devices plugged in at the time this happened."
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom);
                        mail.To.Add(emailTo);
                        mail.Subject = subject;
                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        // Can set to false, if you are sending pure text.

                        //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                        //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.Credentials = new NetworkCredential(emailFrom, password);
                            smtp.EnableSsl = enableSSL;
                            smtp.Send(mail);
                        }
                    }

                }
                else if (wronganswers == 4) //lockdown!
                {
                    using (Form3 f3 = new Form3())
                    {
                        wronganswers = 0; //reset (lockdown)
                        f3.ShowDialog(this);
                    }
                }
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (number == 2) { number = 3; } //3
            else
            {
                wronganswers = wronganswers + 1;
                if (wronganswers < 3) //you have three tries
                {
                    number = 0; //reset possible good parts
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                }
                else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
                {
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                               //detecting  user

                    //list of usb devices
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        if (drive.DriveType == DriveType.Removable)
                        {
                            usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                            Console.WriteLine(usbinfo);
                        }
                    }

                    //how much he knows
                    int partgood = number;
                    number = 0; //after copying data we reset our data.

                    //get computer location (in form of ip)
                    string location = GetIPAddress("google.com").ToString();

                    //get the time
                    string time = DateTime.Now.ToString("h:mm:ss tt");


                    //send a mail
                    string smtpAddress = "smtp.gmail.com";
                    int portNumber = 587;
                    bool enableSSL = true;

                    string emailFrom = "joostgrunwald2001@gmail.com";
                    string password = "Happy2001";
                    string emailTo = "joostgrunwald2001@gmail.com";
                    string subject = "Security breached";
                    if (usbinfo != "") //if usb's are plugged in
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + "The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " Info of the usb drives at the time: " + usbinfo
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    else if (usbinfo == "")
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + " The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " There were no usb devices plugged in at the time this happened."
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom);
                        mail.To.Add(emailTo);
                        mail.Subject = subject;
                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        // Can set to false, if you are sending pure text.

                        //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                        //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.Credentials = new NetworkCredential(emailFrom, password);
                            smtp.EnableSsl = enableSSL;
                            smtp.Send(mail);
                        }
                    }

                }
                else if (wronganswers == 4) //lockdown!
                {
                    using (Form3 f3 = new Form3())
                    {
                        wronganswers = 0; //reset (lockdown)
                        f3.ShowDialog(this);
                    }
                }
            }
        }

        private void button49_Click(object sender, EventArgs e)
        {
            if (number == 4) { number = 5; } //5
            else if (number == 5) { number = 6; } //6
            else
            {
                wronganswers = wronganswers + 1;
                if (wronganswers < 3) //you have three tries
                {
                    number = 0; //reset possible good parts
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                }
                else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
                {
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                               //detecting  user

                    //list of usb devices
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        if (drive.DriveType == DriveType.Removable)
                        {
                            usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                            Console.WriteLine(usbinfo);
                        }
                    }

                    //how much he knows
                    int partgood = number;
                    number = 0; //after copying data we reset our data.

                    //get computer location (in form of ip)
                    string location = GetIPAddress("google.com").ToString();

                    //get the time
                    string time = DateTime.Now.ToString("h:mm:ss tt");


                    //send a mail
                    string smtpAddress = "smtp.gmail.com";
                    int portNumber = 587;
                    bool enableSSL = true;

                    string emailFrom = "joostgrunwald2001@gmail.com";
                    string password = "Happy2001";
                    string emailTo = "joostgrunwald2001@gmail.com";
                    string subject = "Security breached";
                    if (usbinfo != "") //if usb's are plugged in
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + "The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " Info of the usb drives at the time: " + usbinfo
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    else if (usbinfo == "")
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + " The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " There were no usb devices plugged in at the time this happened."
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom);
                        mail.To.Add(emailTo);
                        mail.Subject = subject;
                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        // Can set to false, if you are sending pure text.

                        //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                        //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.Credentials = new NetworkCredential(emailFrom, password);
                            smtp.EnableSsl = enableSSL;
                            smtp.Send(mail);
                        }
                    }

                }
                else if (wronganswers == 4) //lockdown!
                {
                    using (Form3 f3 = new Form3())
                    {
                        wronganswers = 0; //reset (lockdown)
                        f3.ShowDialog(this);
                    }
                }
            }
        }

        private void button70_Click(object sender, EventArgs e)
        {
            if (number == 1) { number = 2; } //2
            else if (number == 6) { number = 7; } //7
            else if (number == 11) { number = 12; } //12
            else if (number == 14) { number = 15; } //15
            else if (number == 16) { number = 17; } //17
            else
            {
                wronganswers = wronganswers + 1;
                if (wronganswers < 3) //you have three tries
                {
                    number = 0; //reset possible good parts
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                }
                else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
                {
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                               //detecting  user

                    //list of usb devices
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        if (drive.DriveType == DriveType.Removable)
                        {
                            usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                            Console.WriteLine(usbinfo);
                        }
                    }

                    //how much he knows
                    int partgood = number;
                    number = 0; //after copying data we reset our data.

                    //get computer location (in form of ip)
                    string location = GetIPAddress("google.com").ToString();

                    //get the time
                    string time = DateTime.Now.ToString("h:mm:ss tt");


                    //send a mail
                    string smtpAddress = "smtp.gmail.com";
                    int portNumber = 587;
                    bool enableSSL = true;

                    string emailFrom = "joostgrunwald2001@gmail.com";
                    string password = "Happy2001";
                    string emailTo = "joostgrunwald2001@gmail.com";
                    string subject = "Security breached";
                    if (usbinfo != "") //if usb's are plugged in
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + "The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " Info of the usb drives at the time: " + usbinfo
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    else if (usbinfo == "")
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + " The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " There were no usb devices plugged in at the time this happened."
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom);
                        mail.To.Add(emailTo);
                        mail.Subject = subject;
                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        // Can set to false, if you are sending pure text.

                        //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                        //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.Credentials = new NetworkCredential(emailFrom, password);
                            smtp.EnableSsl = enableSSL;
                            smtp.Send(mail);
                        }
                    }

                }
                else if (wronganswers == 4) //lockdown!
                {
                    using (Form3 f3 = new Form3())
                    {
                        wronganswers = 0; //reset (lockdown)
                        f3.ShowDialog(this);
                    }
                }
            }
        }

        private void button48_Click(object sender, EventArgs e)
        {
            if (number == 7) { number = 8; } //8
            else
            {
                wronganswers = wronganswers + 1;
                if (wronganswers < 3) //you have three tries
                {
                    number = 0; //reset possible good parts
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                }
                else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
                {
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                               //detecting  user

                    //list of usb devices
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        if (drive.DriveType == DriveType.Removable)
                        {
                            usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                            Console.WriteLine(usbinfo);
                        }
                    }

                    //how much he knows
                    int partgood = number;
                    number = 0; //after copying data we reset our data.

                    //get computer location (in form of ip)
                    string location = GetIPAddress("google.com").ToString();

                    //get the time
                    string time = DateTime.Now.ToString("h:mm:ss tt");


                    //send a mail
                    string smtpAddress = "smtp.gmail.com";
                    int portNumber = 587;
                    bool enableSSL = true;

                    string emailFrom = "joostgrunwald2001@gmail.com";
                    string password = "Happy2001";
                    string emailTo = "joostgrunwald2001@gmail.com";
                    string subject = "Security breached";
                    if (usbinfo != "") //if usb's are plugged in
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + "The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " Info of the usb drives at the time: " + usbinfo
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    else if (usbinfo == "")
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + " The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " There were no usb devices plugged in at the time this happened."
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom);
                        mail.To.Add(emailTo);
                        mail.Subject = subject;
                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        // Can set to false, if you are sending pure text.

                        //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                        //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.Credentials = new NetworkCredential(emailFrom, password);
                            smtp.EnableSsl = enableSSL;
                            smtp.Send(mail);
                        }
                    }

                }
                else if (wronganswers == 4) //lockdown!
                {
                    using (Form3 f3 = new Form3())
                    {
                        wronganswers = 0; //reset (lockdown)
                        f3.ShowDialog(this);
                    }
                }
            }
        }

        private void button59_Click(object sender, EventArgs e)
        {
            if (number == 9) { number = 10; } //10
            else
            {
                wronganswers = wronganswers + 1;
                if (wronganswers < 3) //you have three tries
                {
                    number = 0; //reset possible good parts
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                }
                else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
                {
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                               //detecting  user

                    //list of usb devices
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        if (drive.DriveType == DriveType.Removable)
                        {
                            usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                            Console.WriteLine(usbinfo);
                        }
                    }

                    //how much he knows
                    int partgood = number;
                    number = 0; //after copying data we reset our data.

                    //get computer location (in form of ip)
                    string location = GetIPAddress("google.com").ToString();

                    //get the time
                    string time = DateTime.Now.ToString("h:mm:ss tt");


                    //send a mail
                    string smtpAddress = "smtp.gmail.com";
                    int portNumber = 587;
                    bool enableSSL = true;

                    string emailFrom = "joostgrunwald2001@gmail.com";
                    string password = "Happy2001";
                    string emailTo = "joostgrunwald2001@gmail.com";
                    string subject = "Security breached";
                    if (usbinfo != "") //if usb's are plugged in
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + "The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " Info of the usb drives at the time: " + usbinfo
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    else if (usbinfo == "")
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + " The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " There were no usb devices plugged in at the time this happened."
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom);
                        mail.To.Add(emailTo);
                        mail.Subject = subject;
                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        // Can set to false, if you are sending pure text.

                        //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                        //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.Credentials = new NetworkCredential(emailFrom, password);
                            smtp.EnableSsl = enableSSL;
                            smtp.Send(mail);
                        }
                    }

                }
                else if (wronganswers == 4) //lockdown!
                {
                    using (Form3 f3 = new Form3())
                    {
                        wronganswers = 0; //reset (lockdown)
                        f3.ShowDialog(this);
                    }
                }
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (number == 10) { number = 11; } //11
            else
            {
                wronganswers = wronganswers + 1;
                if (wronganswers < 3) //you have three tries
                {
                    number = 0; //reset possible good parts
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                }
                else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
                {
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                               //detecting  user

                    //list of usb devices
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        if (drive.DriveType == DriveType.Removable)
                        {
                            usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                            Console.WriteLine(usbinfo);
                        }
                    }

                    //how much he knows
                    int partgood = number;
                    number = 0; //after copying data we reset our data.

                    //get computer location (in form of ip)
                    string location = GetIPAddress("google.com").ToString();

                    //get the time
                    string time = DateTime.Now.ToString("h:mm:ss tt");


                    //send a mail
                    string smtpAddress = "smtp.gmail.com";
                    int portNumber = 587;
                    bool enableSSL = true;

                    string emailFrom = "joostgrunwald2001@gmail.com";
                    string password = "Happy2001";
                    string emailTo = "joostgrunwald2001@gmail.com";
                    string subject = "Security breached";
                    if (usbinfo != "") //if usb's are plugged in
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + "The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " Info of the usb drives at the time: " + usbinfo
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    else if (usbinfo == "")
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + " The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " There were no usb devices plugged in at the time this happened."
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom);
                        mail.To.Add(emailTo);
                        mail.Subject = subject;
                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        // Can set to false, if you are sending pure text.

                        //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                        //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.Credentials = new NetworkCredential(emailFrom, password);
                            smtp.EnableSsl = enableSSL;
                            smtp.Send(mail);
                        }
                    }

                }
                else if (wronganswers == 4) //lockdown!
                {
                    using (Form3 f3 = new Form3())
                    {
                        wronganswers = 0; //reset (lockdown)
                        f3.ShowDialog(this);
                    }
                }
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (number == 13) { number = 14; } //14
            else if (number == 17) { number = 18; } //18
            else
            {
                wronganswers = wronganswers + 1;
                if (wronganswers < 3) //you have three tries
                {
                    number = 0; //reset possible good parts
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                }
                else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
                {
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                               //detecting  user

                    //list of usb devices
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        if (drive.DriveType == DriveType.Removable)
                        {
                            usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                            Console.WriteLine(usbinfo);
                        }
                    }

                    //how much he knows
                    int partgood = number;
                    number = 0; //after copying data we reset our data.

                    //get computer location (in form of ip)
                    string location = GetIPAddress("google.com").ToString();

                    //get the time
                    string time = DateTime.Now.ToString("h:mm:ss tt");


                    //send a mail
                    string smtpAddress = "smtp.gmail.com";
                    int portNumber = 587;
                    bool enableSSL = true;

                    string emailFrom = "joostgrunwald2001@gmail.com";
                    string password = "Happy2001";
                    string emailTo = "joostgrunwald2001@gmail.com";
                    string subject = "Security breached";
                    if (usbinfo != "") //if usb's are plugged in
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + "The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " Info of the usb drives at the time: " + usbinfo
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    else if (usbinfo == "")
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + " The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " There were no usb devices plugged in at the time this happened."
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom);
                        mail.To.Add(emailTo);
                        mail.Subject = subject;
                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        // Can set to false, if you are sending pure text.

                        //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                        //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.Credentials = new NetworkCredential(emailFrom, password);
                            smtp.EnableSsl = enableSSL;
                            smtp.Send(mail);
                        }
                    }

                }
                else if (wronganswers == 4) //lockdown!
                {
                    using (Form3 f3 = new Form3())
                    {
                        wronganswers = 0; //reset (lockdown)
                        f3.ShowDialog(this);
                    }
                }
            }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            if (number == 15) { number = 16; } //16
            else
            {
                wronganswers = wronganswers + 1;
                if (wronganswers < 3) //you have three tries
                {
                    number = 0; //reset possible good parts
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                }
                else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
                {
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                               //detecting  user

                    //list of usb devices
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        if (drive.DriveType == DriveType.Removable)
                        {
                            usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                            Console.WriteLine(usbinfo);
                        }
                    }

                    //how much he knows
                    int partgood = number;
                    number = 0; //after copying data we reset our data.

                    //get computer location (in form of ip)
                    string location = GetIPAddress("google.com").ToString();

                    //get the time
                    string time = DateTime.Now.ToString("h:mm:ss tt");


                    //send a mail
                    string smtpAddress = "smtp.gmail.com";
                    int portNumber = 587;
                    bool enableSSL = true;

                    string emailFrom = "joostgrunwald2001@gmail.com";
                    string password = "Happy2001";
                    string emailTo = "joostgrunwald2001@gmail.com";
                    string subject = "Security breached";
                    if (usbinfo != "") //if usb's are plugged in
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + "The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " Info of the usb drives at the time: " + usbinfo
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    else if (usbinfo == "")
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + " The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " There were no usb devices plugged in at the time this happened."
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom);
                        mail.To.Add(emailTo);
                        mail.Subject = subject;
                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        // Can set to false, if you are sending pure text.

                        //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                        //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.Credentials = new NetworkCredential(emailFrom, password);
                            smtp.EnableSsl = enableSSL;
                            smtp.Send(mail);
                        }
                    }

                }
                else if (wronganswers == 4) //lockdown!
                {
                    using (Form3 f3 = new Form3())
                    {
                        wronganswers = 0; //reset (lockdown)
                        f3.ShowDialog(this);
                    }
                }
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (number == 18) { number = 19; } //18
            else
            {
                wronganswers = wronganswers + 1;
                if (wronganswers < 3) //you have three tries
                {
                    number = 0; //reset possible good parts
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                }
                else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
                {
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                               //detecting  user

                    //list of usb devices
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        if (drive.DriveType == DriveType.Removable)
                        {
                            usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                            Console.WriteLine(usbinfo);
                        }
                    }

                    //how much he knows
                    int partgood = number;
                    number = 0; //after copying data we reset our data.

                    //get computer location (in form of ip)
                    string location = GetIPAddress("google.com").ToString();

                    //get the time
                    string time = DateTime.Now.ToString("h:mm:ss tt");


                    //send a mail
                    string smtpAddress = "smtp.gmail.com";
                    int portNumber = 587;
                    bool enableSSL = true;

                    string emailFrom = "joostgrunwald2001@gmail.com";
                    string password = "Happy2001";
                    string emailTo = "joostgrunwald2001@gmail.com";
                    string subject = "Security breached";
                    if (usbinfo != "") //if usb's are plugged in
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + "The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " Info of the usb drives at the time: " + usbinfo
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    else if (usbinfo == "")
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + " The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " There were no usb devices plugged in at the time this happened."
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom);
                        mail.To.Add(emailTo);
                        mail.Subject = subject;
                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        // Can set to false, if you are sending pure text.

                        //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                        //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.Credentials = new NetworkCredential(emailFrom, password);
                            smtp.EnableSsl = enableSSL;
                            smtp.Send(mail);
                        }
                    }

                }
                else if (wronganswers == 4) //lockdown!
                {
                    using (Form3 f3 = new Form3())
                    {
                        wronganswers = 0; //reset (lockdown)
                        f3.ShowDialog(this);
                    }
                }
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            if (number == 19) //code = right
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\systemunlocked.wav");
                no.Play(); //system unlocked
                this.Close();
            }
            else
            {
                wronganswers = wronganswers + 1;
                if (wronganswers < 3) //you have three tries
                {
                    number = 0; //reset possible good parts
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                }
                else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
                {
                    SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                    no.Play(); //wrong answer
                               //detecting  user

                    //list of usb devices
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        if (drive.DriveType == DriveType.Removable)
                        {
                            usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                            Console.WriteLine(usbinfo);
                        }
                    }

                    //how much he knows
                    int partgood = number;
                    number = 0; //after copying data we reset our data.

                    //get computer location (in form of ip)
                    string location = GetIPAddress("google.com").ToString();

                    //get the time
                    string time = DateTime.Now.ToString("h:mm:ss tt");


                    //send a mail
                    string smtpAddress = "smtp.gmail.com";
                    int portNumber = 587;
                    bool enableSSL = true;

                    string emailFrom = "joostgrunwald2001@gmail.com";
                    string password = "Happy2001";
                    string emailTo = "joostgrunwald2001@gmail.com";
                    string subject = "Security breached";
                    if (usbinfo != "") //if usb's are plugged in
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + "The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " Info of the usb drives at the time: " + usbinfo
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    else if (usbinfo == "")
                    {
                        body = "The breacher knew the first " + number.ToString() + " letters of your password"
                        + " The ip location of the pc that can be used for localizing is: " + location
                        + " The moment when somebody tried to login is " + time
                        + " There were no usb devices plugged in at the time this happened."
                        + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                    }

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom);
                        mail.To.Add(emailTo);
                        mail.Subject = subject;
                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        // Can set to false, if you are sending pure text.

                        //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                        //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.Credentials = new NetworkCredential(emailFrom, password);
                            smtp.EnableSsl = enableSSL;
                            smtp.Send(mail);
                        }
                    }

                }
                else if (wronganswers == 4) //lockdown!
                {
                    using (Form3 f3 = new Form3())
                    {
                        wronganswers = 0; //reset (lockdown)
                        f3.ShowDialog(this);
                    }
                }
            }


        }

        private void button1_Click(object sender, EventArgs e) //dont use
        {

        }

        private void button15_Click(object sender, EventArgs e) //0-10 begin
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button24_Click(object sender, EventArgs e) //0-10 end
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button40_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button45_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button46_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button47_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button50_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button51_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button52_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button53_Click(object sender, EventArgs e) //dont use
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button56_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button57_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button58_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button60_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button61_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button62_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }

        private void button55_Click(object sender, EventArgs e)
        {
            wronganswers = wronganswers + 1;
            if (wronganswers < 3) //you have three tries
            {
                number = 0; //reset possible good parts
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
            }
            else if (wronganswers == 3) //you can't anser correct anymore but you are recorded and watched while doing this, its a fake to get as much information about the breacher as possible.
            {
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wronganswer.wav");
                no.Play(); //wrong answer
                //detecting  user

                //list of usb devices
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        usbinfo = string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel);
                        Console.WriteLine(usbinfo);
                    }
                }

                //how much he knows
                int partgood = number;
                number = 0; //after copying data we reset our data.

                //get computer location (in form of ip)
                string location = GetIPAddress("google.com").ToString();

                //get the time
                string time = DateTime.Now.ToString("h:mm:ss tt");


                //send a mail
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "joostgrunwald2001@gmail.com";
                string password = "Happy2001";
                string emailTo = "joostgrunwald2001@gmail.com";
                string subject = "Security breached";
                if (usbinfo != "") //if usb's are plugged in
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + "The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " Info of the usb drives at the time: " + usbinfo
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                else if (usbinfo == "")
                {
                    body = "The breacher knew the first " + number.ToString() + " letters of your password"
                    + " The ip location of the pc that can be used for localizing is: " + location
                    + " The moment when somebody tried to login is " + time
                    + " There were no usb devices plugged in at the time this happened."
                    + " This rapport has been made by J.A.R.V.I.S your personal assistent, you can leave further defence up to him";
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            else if (wronganswers == 4) //lockdown!
            {
                using (Form3 f3 = new Form3())
                {
                    wronganswers = 0; //reset (lockdown)
                    f3.ShowDialog(this);
                }
            }
        }
    }
}
