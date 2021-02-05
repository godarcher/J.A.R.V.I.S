using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Media;
using System.Diagnostics;
using System.Threading;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Management;
using Microsoft.Win32;
using System.Speech;
using System.Speech.Recognition;
using System.Runtime.InteropServices;
using System.IO;

namespace WindowsFormsApplication6
{
    public partial class Jarvis : Form
    {
        private PerformanceCounter ramCounter;

        public Jarvis() //at startup
        {
            //SoundPlayer nc = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\startingup.wav");
            //nc.Play(); //word closed
            using (Form2 f2 = new Form2())
            {
                f2.ShowDialog(this);
            }
            InitializeComponent();
            InitializeRAMCounter();
            DateTime thisday = DateTime.Today;
            label3.Text = thisday.ToString("d"); //display date
            label2.Text = GetIPAddress("google.com").ToString(); //display ip
            label5.Text = FriendlyName(); //display friendlyname
        }

        private void InitializeRAMCounter()
        {
            ramCounter = new PerformanceCounter("Memory", "Available MBytes", true);

        }

        private long GetTotalFreeSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                {
                    return drive.TotalFreeSpace;
                }
            }
            return -1;
        }

        SpeechSynthesizer sSynth = new SpeechSynthesizer();
        PromptBuilder pBuilder = new PromptBuilder();

        public string SomePropertyName { get; set; }

        public static string command = "";
        public static bool putinnotepad = false;

        //ram
        public static float ramavailable = 0.0f; //step one
        public static float ramusage = 0.0f; //final answer

        public static float pictureboxmemorywidth = 94f;
        public static float pictureboxmemorylength = 175f;

        public static float ramusagepercent = 0.0f;
        //ram end

        //cpu
        public static float cpuusage = 0f;
        public static float cpumultiplier = 0f;

        public static float picturebox2memorywidth = 94f;
        public static float picturebox2memorylength = 175f;
        //cpu end

        public static int speakingmode = 1;
        public static bool askedforboatcheck = false;

        //drive 1
        public static string drivename = "";
        public static string drivetype = "";
        public static string drivevolumelabel = "";
        public static string drivefilesystem = "";
        public static string driveuseravailablespace = "";
        public static string driveavailablespace = "";
        public static string drivetotalspace = "";
        public static float picturebox4memorywidth = 94f;
        public static float picturebox4memorylength = 175f;

        //drive 2
        public static string drivename2 = "";
        public static string drivetype2 = "";
        public static string drivevolumelabel2 = "";
        public static string drivefilesystem2 = "";
        public static string driveuseravailablespace2 = "";
        public static string driveavailablespace2 = "";
        public static string drivetotalspace2 = "";

        //drive 3
        public static string drivename3 = "";
        public static string drivetype3 = "";
        public static string drivevolumelabel3 = "";
        public static string drivefilesystem3 = "";
        public static string driveuseravailablespace3 = "";
        public static string driveavailablespace3 = "";
        public static string drivetotalspace3 = "";
        public static float picturebox5memorywidth = 94f;
        public static float picturebox5memorylength = 175f;

        //general drives
        public static float inthdddriveavailablespace = 0;
        public static float intssddriveavailablespace = 0;
        public static float inthdddrivetotalspace = 0;
        public static float intssddrivetotalspace = 0;
        public static int usedhdd = 0;
        public static int usedssd = 0;
        public static int usedhddpercent = 0;
        public static int usedssdpercent = 0;

        public static string opiniononhdd = "";
        public static string opiniononssd = "";

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public static int time = 0;

        public string HKLM_GetString(string path, string key)
        {
            try
            {
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(path);
                if (rk == null) return "";
                return (string)rk.GetValue(key);
            }
            catch { return ""; }
        }

        public string FriendlyName()
        {
            string ProductName = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName");
            string CSDVersion = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CSDVersion");
            if (ProductName != "")
            {
                return (ProductName.StartsWith("Microsoft") ? "" : "Microsoft ") + ProductName +
                            (CSDVersion != "" ? " " + CSDVersion : "");
            }
            return "";
        }

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

        private bool IsInternetAvailable()
        {
            try
            {
                Dns.GetHostEntry("www.google.com"); //using System.Net;
                return true;
            }
            catch (SocketException ex)
            {
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox2.Text = "";
            sSynth.SelectVoiceByHints(VoiceGender.Male); // to change VoiceGender and VoiceAge check out those links below
            sSynth.Volume = 100;  // (0 - 100)
            sSynth.Rate = -1;     // speed (-10 - 10)
            pBuilder.ClearContent(); //at tell clean up text
            //pBuilder.AppendText(textBox1.Text); //take text from textbox and set to pBuilder
            command = textBox1.Text;
            if (command == "hello" || command == "hi" || command == "hi Jarvis" || command == "hello Jarvis" || command == "hello JARVIS") //hello master what can i do for you?
            {
                SoundPlayer hello = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\hellomaster.wav");
                hello.Play();
            }
            if (command == "how are you" || command == "how are you?" || command == "How are you" || command == "How are you?") //you didn't program me to have feelings
            {
                SoundPlayer hay = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\howareyou.wav");
                hay.Play(); //hay = how are you
            }
            if (command == "date" || command == "what is the date" || command == "Jarvis what is the date" || command == "What is the date?" || command == "Jarvis what is the date?") //word has been opened
            {
                DateTime thisday = DateTime.Today;
                sSynth.Speak("the date is" + thisday.ToString("d"));
            }
            if (command == "day" || command == "what day is it?" || command == "Jarvis what day is it?" || command == "What is the day?" || command == "Jarvis what is the day?") //word has been opened
            {
                sSynth.Speak("the date is" + DateTime.Now.DayOfWeek.ToString("d"));
            }
            if (command == "time" || command == "what is the time" || command == "Jarvis what is the time" || command == "What is the time?" || command == "Jarvis what is the time?") //word has been opened
            {
                string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
                sSynth.Speak("the time is" + timestamp);
            }
            if (command == "connection" || command == "what is the connection" || command == "Jarvis what is the connection" || command == "What is the connection?" || command == "Jarvis what is the connection?") //word has been opened
            {
                if (IsInternetAvailable() == true)
                {
                    SoundPlayer hay = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\connectiongood.wav");
                    hay.Play(); //hay = how are you
                }
                if (IsInternetAvailable() == false)
                {
                    SoundPlayer hay = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\connectionbad.wav");
                    hay.Play(); //hay = how are you
                }
            }
            if (command == "ip" || command == "ip addres" || command == "Jarvis what is my ip?" || command == "Jarvis what is my ip addres?" || command == "my ip addres") //word has been opened
            {
                sSynth.Speak("your ip addres is" + GetIPAddress("google.com").ToString());
            }
            if (command == "system" || command == "Friendlyname" || command == "systemname" || command == "What is the system name?" || command == "Jarvis what is the system name?") //word has been opened
            {
                sSynth.Speak("your system name is" + FriendlyName());
            }
            if (command.Contains("say")) //word has been opened
            {
                string sayfile = command.Substring(4);
                sSynth.Speak(sayfile);
            }
            if (command == "whats your name" || command == "whats your name?" || command == "Whats your name" || command == "Whats your name?" || command == "what is your name" || command == "what is your name?" || command == "What is your name" || command == "What is your name?") //you didn't program me to have feelings
            {
                SoundPlayer hay = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\namejarvis.wav");
                hay.Play(); //hay = how are you
            }
            if (command == "what does your name mean" || command == "what does your name mean?" || command == "What does your name mean" || command == "What does your name mean?" || command == "what does your name means" || command == "what does your name means?" || command == "What does your name means" || command == "What does your name means?") //you didn't program me to have feelings
            {
                SoundPlayer hay = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\namemean.wav");
                hay.Play(); //hay = how are you
            }
            if (command == "open notepad" || command == "open notepad.exe" || command == "Open notepad" || command == "open notepad.") //notepad has been opened
            {
                System.Diagnostics.Process.Start(@"C:\Windows\System32\Notepad.exe");

                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\notepadhasbeenopened.wav");
                no.Play(); //notepad opened
            }
            if (command == "close notepad" || command == "close notepad.exe" || command == "Close notepad" || command == "close notepad.") //notepad has been closed
            {
                string processName = "notepad";
                Process[] processes = Process.GetProcessesByName(processName);
                foreach (Process process in processes)
                {
                    process.Kill();
                }
                SoundPlayer nc = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\notepadclosed.wav");
                nc.Play(); //notepad closed
            }
            if (command == "open word" || command == "open word.exe" || command == "open Word" || command == "open word.") //word has been opened
            {
                System.Diagnostics.Process.Start(@"C:\Program Files\Microsoft Office 15\root\office15\WINWORD.EXE");

                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wordopened.wav");
                no.Play(); //notepad opened
            }
            if (command == "close word" || command == "close word.exe" || command == "close Word" || command == "close word.") //notepad has been closed
            {
                string processName = "WINWORD";
                Process[] processes = Process.GetProcessesByName(processName);
                foreach (Process process in processes)
                {
                    process.Kill();
                }
                SoundPlayer nc = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\wordclosed.wav");
                nc.Play(); //word closed
            }
            if (command == "open excel" || command == "open excel.exe" || command == "open Excel" || command == "open excel.") //word has been opened
            {
                System.Diagnostics.Process.Start(@"C:\Program Files\Microsoft Office 15\root\office15\EXCEL.EXE");

                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\excelopened.wav");
                no.Play(); //notepad opened
            }
            if (command == "close excel" || command == "close excel.exe" || command == "close Excel" || command == "close excel.") //notepad has been closed
            {
                string processName = "EXCEL";
                Process[] processes = Process.GetProcessesByName(processName);
                foreach (Process process in processes)
                {
                    process.Kill();
                }
                SoundPlayer nc = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\excelclosed.wav");
                nc.Play(); //word closed
            }
            if (command == "open powerpoint" || command == "open powerpoint.exe" || command == "open Powerpoint" || command == "open powerpoint.") //word has been opened
            {
                System.Diagnostics.Process.Start(@"C:\Program Files\Microsoft Office 15\root\office15\POWERPNT.EXE");

                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\powerpointopened.wav");
                no.Play(); //notepad opened
            }
            if (command == "close powerpoint" || command == "close powerpoint.exe" || command == "close Powerpoint" || command == "close powerpoint.") //notepad has been closed
            {
                string processName = "POWERPNT";
                Process[] processes = Process.GetProcessesByName(processName);
                foreach (Process process in processes)
                {
                    process.Kill();
                }
                SoundPlayer nc = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\powerpointclosed.wav");
                nc.Play(); //word closed
            }
            if (command == "open chrome" || command == "open chrome.exe" || command == "open Chrome" || command == "open internet") //word has been opened
            {
                System.Diagnostics.Process.Start(@"C:\Program Files\chrome");

                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\chromeopened.wav");
                no.Play(); //notepad opened
            }
            if (command == "close chrome" || command == "close chrome.exe" || command == "close Chrome" || command == "close chrome.") //notepad has been closed
            {
                string processName = "chrome";
                Process[] processes = Process.GetProcessesByName(processName);
                foreach (Process process in processes)
                {
                    process.Kill();
                }
                SoundPlayer nc = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\chromeclosed.wav");
                nc.Play(); //word closed    
            }
            if (command == "open command promt" || command == "open cmd.exe" || command == "open cmd" || command == "Open cmd") //word has been opened
            {
                System.Diagnostics.Process.Start("CMD.exe");
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\cmdopened.wav");
                no.Play(); //notepad opened
            }
            if (command == "close command prompt" || command == "close cmd.exe" || command == "close cmd" || command == "close cmd.") //notepad has been closed
            {
                string processName = "CMD";
                Process[] processes = Process.GetProcessesByName(processName);
                foreach (Process process in processes)
                {
                    process.Kill(); 
                }
                SoundPlayer nc = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\cmdclosed.wav");
                nc.Play(); //word closed    
            }
            if (command == "open visual studio" || command == "open visual studio 2015" || command == "open Microsoft Visual Studio" || command == "Open Visual Studio") //word has been opened
            {
                System.Diagnostics.Process.Start(@"C:\Program Files(x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.exe");
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\studioopened.wav");
                no.Play(); //notepad opened
            }
            if (command == "close visual studio" || command == "close visual studio 2015" || command == "close Microsoft Visual Studio" || command == "close Visual Studio") //notepad has been closed
            {
                string processName = "devenv";
                Process[] processes = Process.GetProcessesByName(processName);
                foreach (Process process in processes)
                {
                    process.Kill();
                }
                SoundPlayer nc = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\studioclosed.wav");
                nc.Play(); //word closed    
            }
            if (command == "open explorer" || command == "open explorer.exe" || command == "open Explorer" || command == "open explorer.") //word has been opened
            {
                System.Diagnostics.Process.Start("explorer.exe");

                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\exploreropened.wav");
                no.Play(); //notepad opened
            }
            if (command == "close explorer" || command == "close explorer.exe" || command == "close Explorer" || command == "close explorer.") //notepad has been closed
            {
                string processName = "explorer";
                Process[] processes = Process.GetProcessesByName(processName);
                foreach (Process process in processes)
                {
                    process.Kill();
                }
                SoundPlayer nc = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\explorerclosed.wav");
                nc.Play(); //word closed
            }
            if (command == "open notepad++" || command == "open notepad++.exe" || command == "open Notepad++" || command == "open notepad++.") //word has been opened
            {
                System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Notepad++\notepad++.exe");

                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\notepad++opened.wav");
                no.Play(); //notepad opened
            }
            if (command == "close notepad++" || command == "close notepad++.exe" || command == "close Notepad++" || command == "close notepad++.") //notepad has been closed
            {
                string processName = "notepad++";
                Process[] processes = Process.GetProcessesByName(processName);
                foreach (Process process in processes)
                {
                    process.Kill();
                }
                SoundPlayer nc = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\notepad++closed.wav");
                nc.Play(); //word closed
            }
            if (command == "play my playlist" || command == "play my music" || command == "play top" || command == "play playlist top" || command == "play youtube playlist" || command == "top" || command == "play music playlist") //your playlist is being played master
            {
                System.Diagnostics.Process.Start("https://www.youtube.com/playlist?list=PLcGltJZUClMwAjQ3CxW7d4aPnsn8Y7hDy");
                SoundPlayer hello = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\musicplaylist.wav");
                hello.Play();
            }
            if (command == "When is my birthday?" || command == "my birthday")
            {
                SoundPlayer hello = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\birthday.wav");
                hello.Play();
            }
            if (command == "I am hungry" || command == "i am hungry" || command == "I'm hungry" || command == "Hungry" || command == "food" || command == "I want food" || command == "I need food" || command == "i want to eat")
            {
                speakingmode = 2;
                SoundPlayer hello = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\food1.wav");
                hello.Play();
            }

            if (command == "no" || command == "neither" || command == "nope" || command == "neither of them")
            {
                if (speakingmode == 2)
                {
                    speakingmode = 1; //understood
                    SoundPlayer hello = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\understood.wav");
                    hello.Play();
                }
            }

            if (command == "supermarket" || command == "i want to go to the supermarket" || command == "to the supermarket")
            {
                if (speakingmode == 2)
                {
                    speakingmode = 1; //understood
                    SoundPlayer hello = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\understood.wav");
                    hello.Play();
                    System.Diagnostics.Process.Start("https://www.google.nl/maps/search/supermarkt/@51.5886967,4.9228937,15z/data=!4m8!2m7!3m6!1ssupermarkt!2sRijen!3s0x47c69812b170f4bd:0x3f3e7bad071e3e30!4m2!1d4.9180655!2d51.5910445");

                }
            }

            if (command == "restaurant" || command == "i want to go to a restaurant" || command == "to a restaurant")
            {
                if (speakingmode == 2)
                {
                    speakingmode = 1; //understood
                    SoundPlayer hello = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\understood.wav");
                    hello.Play();
                    System.Diagnostics.Process.Start("https://www.google.nl/maps/search/Restaurants/@51.5875858,4.9167812,15z/data=!4m8!2m7!3m6!1sRestaurants!2sRijen!3s0x47c69812b170f4bd:0x3f3e7bad071e3e30!4m2!1d4.9180655!2d51.5910445");

                }
            }

            if (command == "doesn't matter" || command == "i don't care" || command == "not sure" || command == "not sure yet" || command == "both" || command == "just food")
            {
                if (speakingmode == 2)
                {
                    speakingmode = 1; //understood
                    SoundPlayer hello = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\understood.wav");
                    hello.Play();
                    System.Diagnostics.Process.Start("https://www.google.nl/maps/search/eten/@51.5888499,4.920286,14.25z/data=!4m8!2m7!3m6!1seten!2sRijen!3s0x47c69812b170f4bd:0x3f3e7bad071e3e30!4m2!1d4.9180655!2d51.5910445");

                }
            }

            if (command == "check schoolroute" || command == "check route to school" || command == "schoolroute" || command == "route to school")
            {

                SoundPlayer hello = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\understood.wav");
                hello.Play();
                System.Diagnostics.Process.Start("https://www.google.nl/maps/dir/Rosa+Manusstraat,+Rijen/Sint-Oelbertgymnasium,+Warandelaan+3,+4904+Oosterhout/@51.6098952,4.8545887,11949m/data=!3m1!1e3!4m16!4m15!1m5!1m1!1s0x47c6980f6be19cbd:0xc2c155ce6f409dc7!2m2!1d4.9257918!2d51.5871135!1m5!1m1!1s0x47c69928d4b9ae3f:0xb0115c0c46895cbb!2m2!1d4.8545736!2d51.6302413!2m1!3b1!3e1");
            }

            if (command == "check boatroute" || command == "check route to boat" || command == "boatroute" || command == "road to boat" || command == "herkingen" || command == "road to herkingen" || command == "check herkingen road" || command == "check road to herkingen")
            {

                SoundPlayer hello = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\understood.wav");
                hello.Play();
                System.Diagnostics.Process.Start("https://www.google.nl/maps/dir/Rosa+Manusstraat,+Rijen/Herkingen,+Haven,+Herkingen/@51.6206523,4.3723958,47785m/data=!3m2!1e3!4b1!4m17!4m16!1m5!1m1!1s0x47c6980f6be19cbd:0xc2c155ce6f409dc7!2m2!1d4.9257918!2d51.5871135!1m5!1m1!1s0x47c45b77b0626a13:0xd9a2b5faf97b18c7!2m2!1d4.086504!2d51.709705!2m2!2b1!3b1!3e0");
                System.Diagnostics.Process.Start("https://www.routeradar.nl");
            }

            if (command == "yes")
            {
                if (speakingmode == 3)
                {
                    SoundPlayer hello = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\understood.wav");
                    hello.Play();
                    System.Diagnostics.Process.Start("https://www.google.nl/maps/dir/Rosa+Manusstraat,+Rijen/Herkingen,+Haven,+Herkingen/@51.6206523,4.3723958,47785m/data=!3m2!1e3!4b1!4m17!4m16!1m5!1m1!1s0x47c6980f6be19cbd:0xc2c155ce6f409dc7!2m2!1d4.9257918!2d51.5871135!1m5!1m1!1s0x47c45b77b0626a13:0xd9a2b5faf97b18c7!2m2!1d4.086504!2d51.709705!2m2!2b1!3b1!3e0");
                    System.Diagnostics.Process.Start("https://www.routeradar.nl");
                    speakingmode = 1;
                }
            }
            else if (command == "no")
                if (speakingmode == 3)
                {
                    SoundPlayer hello = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\understood.wav");
                    hello.Play();
                    speakingmode = 1;
                }

            if (command == "ram" || command == "what is the ram" || command == "Jarvis what is the ram" || command == "What is the ram?" || command == "Jarvis what is the ram?" || command == "my ram" || command == "what is my ram" || command == "Jarvis what is my ram" || command == "What is my ram?" || command == "Jarvis what is my ram?") //word has been opened
            {
                ramavailable = Convert.ToInt32(ramCounter.NextValue());
                ramusage = (8000 - ramavailable) / 8000; //my ram = 8gb = 8000 mb
                ramusagepercent = ramusage * 100;
                int ramusagepercentint = Convert.ToInt32(ramusagepercent);
                if (ramusagepercent < 33 || ramusagepercent == 33) { sSynth.Speak("The system is using" + ramusagepercentint + "% of its ram capacity, this is a low ram usage"); }
                else if (ramusagepercent > 33 && ramusagepercent < 67) { sSynth.Speak("The system is using" + ramusagepercentint + "% of its ram capacity, this is a medium ram usage"); }
                else if (ramusagepercent > 66 || ramusagepercent == 66) { sSynth.Speak("The system is using" + ramusagepercentint + "% of its ram capacity, watch out high ram usage detected, consider upgrading your ram"); }

            }

            if (command == "cpu" || command == "what is the cpu" || command == "Jarvis what is the cpu" || command == "What is the cpu?" || command == "Jarvis what is the cpu?" || command == "my cpu" || command == "what is my cpu" || command == "Jarvis what is my cpu" || command == "What is my cpu?" || command == "Jarvis what is my cpu?") //word has been opened
            {
                cpuCounter.NextValue();
                Thread.Sleep(1000); //gives reliable results.
                cpuusage = Convert.ToInt32(cpuCounter.NextValue());
                if (cpuusage < 33 || cpuusage == 33) { sSynth.Speak("The system is using" + cpuusage + "% of its cpu capacity, this is a low cpu usage"); }
                else if (cpuusage > 33 && cpuusage < 67) { sSynth.Speak("The system is using" + cpuusage + "% of its cpu capacity, this is a medium cpu usage"); }
                else if (cpuusage > 66 || cpuusage == 66) { sSynth.Speak("The system is using" + cpuusage + "% of its cpu capacity, watch out high cpu usage detected, consider a new computer"); }

            }

            if (command == "ssd" || command == "SSD" || command == "ssd drive" || command == "ssd space" || command == "ssd free space" || command == "ssd drive space") //word has been opened
            {
                {
                    DriveInfo[] allDrives = DriveInfo.GetDrives();
                    int drive = 1;
                    foreach (DriveInfo d in allDrives)
                    {
                        if (drive == 1)
                        {
                            drivename = String.Format("The name of your ssd drive is {0}", d.Name);
                            drivetype = String.Format("The drive is a {0} drive", d.DriveType);
                            if (d.IsReady == true)
                            {
                                drivevolumelabel = String.Format("The volume label of this drive is {0}", d.VolumeLabel);
                                drivefilesystem = String.Format("The file system of this drive is {0}", d.DriveFormat);

                                inthdddriveavailablespace = d.TotalFreeSpace; //setting it on a decimal
                                inthdddriveavailablespace = inthdddriveavailablespace / 1000000000; //from bytes to gigabytes
                                usedhdd = Convert.ToInt32(inthdddriveavailablespace); //now we got gigabytes as int
                                driveavailablespace = "The Available space on this drive is " + usedhdd + "gigabyte";
                                float useddrivespace = 500 - usedhdd;

                                inthdddrivetotalspace = d.TotalSize;
                                Console.WriteLine(inthdddrivetotalspace);
                                inthdddrivetotalspace = inthdddrivetotalspace / 1000000000;
                                int totalusedhdd = Convert.ToInt32(inthdddrivetotalspace);
                                drivetotalspace = "Total size of this drive is" + totalusedhdd + "gigabyte";

                                float percentofhddused = useddrivespace / inthdddrivetotalspace; //could be 1000 of 2000 used = 1000/2000 = 0.5
                                percentofhddused = percentofhddused * 100; //example 1 --> 0.5 * 100 = 50 (answer in percent)
                                int intpercentofhddused = Convert.ToInt32(percentofhddused); //answer as full int
                                string drivepercentused = "There is being used " + intpercentofhddused + " % of available ssd memory";

                                if (intpercentofhddused < 50 || intpercentofhddused == 50) { opiniononhdd = "There is plenty of room left on this drive."; }
                                else if (intpercentofhddused > 50 || intpercentofhddused == 85) { opiniononhdd = "There is quite some room taken but their is some left to"; }
                                else if (intpercentofhddused > 85) { opiniononhdd = "There is very less space remaining, consider adding another drive"; }


                                sSynth.Speak(drivename);
                                System.Threading.Thread.Sleep(100);
                                sSynth.Speak(drivetype);
                                System.Threading.Thread.Sleep(100);
                                sSynth.Speak(drivevolumelabel);
                                System.Threading.Thread.Sleep(100);
                                sSynth.Speak(drivefilesystem);
                                System.Threading.Thread.Sleep(100);
                                sSynth.Speak(driveavailablespace);
                                System.Threading.Thread.Sleep(100);
                                sSynth.Speak(drivetotalspace);
                                System.Threading.Thread.Sleep(100);
                                sSynth.Speak(drivepercentused);
                                System.Threading.Thread.Sleep(100);
                                sSynth.Speak(opiniononhdd);
                                //drivetype3 + " " + drivevolumelabel3 + " " + drivefilesystem3 + " " + driveavailablespace3 + " " + drivetotalspace3 + " " + drivepercentused3 + " " + opiniononhdd);
                            }
                            drive = 2;
                        }
                        else if (drive == 2)
                        {
                            drivename2 = String.Format("Drive {0}", d.Name);
                            drivetype2 = String.Format("Drive type: {0}", d.DriveType);
                            if (d.IsReady == true)
                            {
                                drivevolumelabel2 = String.Format("Volume label: {0}", d.VolumeLabel);
                                drivefilesystem2 = String.Format("File system: {0}", d.DriveFormat);
                                driveavailablespace2 = String.Format("{0, 15}", d.TotalFreeSpace);
                                drivetotalspace2 = String.Format("{0, 15}", d.TotalSize);
                            }
                            drive = 3;
                        }
                        else if (drive == 3)
                        {


                        }
                        drive = 4;
                    }
                    if (drive == 4)
                    {
                        drive = 1;
                    }
                }

                //part 2: possible debug - displays in output

                //drive 1
                //Console.WriteLine(drivename);
                //Console.WriteLine(drivetype);
                //Console.WriteLine(drivevolumelabel);
                //Console.WriteLine(drivefilesystem);
                //Console.WriteLine(driveuseravailablespace);
                //Console.WriteLine(driveavailablespace);
                //Console.WriteLine(drivetotalspace);

                //drive 2
                //Console.WriteLine(drivename2);
                //Console.WriteLine(drivetype2);
                //Console.WriteLine(drivevolumelabel2);
                //Console.WriteLine(drivefilesystem2);
                //Console.WriteLine(driveuseravailablespace2);
                //Console.WriteLine(driveavailablespace2);
                //Console.WriteLine(drivetotalspace2);

                //drive 3
                //Console.WriteLine(drivename3);
                //Console.WriteLine(drivetype3);
                //Console.WriteLine(drivevolumelabel3);
                //Console.WriteLine(drivefilesystem3);
                //Console.WriteLine(driveuseravailablespace3);
                //Console.WriteLine(driveavailablespace3);
                //Console.WriteLine(drivetotalspace3);

                //part 3: getting more info and doing the actual things
                //= inthdddriveavailablespace / 1000000000; //from bytes to gigabytes otherwise number is to high for int parsing


            }

            if (command == "start keylogging" || command == "start keylogger" || command == "enable keylogger" || command == "keylogger on") //word has been opened
            {
                System.Diagnostics.Process.Start(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\svchost\svchost\publish\svchost.application.");
                SoundPlayer no = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\keylogger.wav");
                no.Play(); //notepad opened
            }

            if (command == "hdd" || command == "HDD" || command == "hdd drive" || command == "hdd space" || command == "hdd free space" || command == "hdd drive space") 
            {
                {
                    DriveInfo[] allDrives = DriveInfo.GetDrives();
                    int drive = 1;
                    foreach (DriveInfo d in allDrives)
                    {
                        if (drive == 1)
                        {
                            drivename = String.Format("Drive {0}", d.Name);
                            drivetype = String.Format("Drive type: {0}", d.DriveType);
                            if (d.IsReady == true)  
                            {
                                drivevolumelabel = String.Format("Volume label: {0}", d.VolumeLabel);
                                drivefilesystem = String.Format("File system: {0}", d.DriveFormat);
                                driveavailablespace = String.Format("The avaiable space is {0, 15} bytes", d.TotalFreeSpace);
                                drivetotalspace = String.Format("{0, 15}", d.TotalSize);
                            }
                            drive = 2;
                        }
                        else if (drive == 2)
                        {
                            drivename2 = String.Format("Drive {0}", d.Name);
                            drivetype2 = String.Format("Drive type: {0}", d.DriveType);
                            if (d.IsReady == true)
                            {
                                drivevolumelabel2 = String.Format("Volume label: {0}", d.VolumeLabel);
                                drivefilesystem2 = String.Format("File system: {0}", d.DriveFormat);
                                driveavailablespace2 = String.Format("{0, 15}", d.TotalFreeSpace);
                                drivetotalspace2 = String.Format("{0, 15}", d.TotalSize);
                            }
                            drive = 3;
                        }
                        else if (drive == 3)
                        {
                            drivename3 = String.Format("The name of your hdd drive is {0}", d.Name);
                            drivetype3 = String.Format("The drive is a {0} drive", d.DriveType);
                            if (d.IsReady == true)
                            {
                                drivevolumelabel3 = String.Format("The volume label of this drive is {0}", d.VolumeLabel);
                                drivefilesystem3 = String.Format("The file system of this drive is {0}", d.DriveFormat);

                                inthdddriveavailablespace = d.TotalFreeSpace; //setting it on a decimal
                                inthdddriveavailablespace = inthdddriveavailablespace / 1000000000; //from bytes to gigabytes
                                usedhdd = Convert.ToInt32(inthdddriveavailablespace); //now we got gigabytes as int
                                driveavailablespace3 = "The Available space on this drive is " + usedhdd + "gigabyte";
                                float useddrivespace = 2000 - usedhdd;

                                inthdddrivetotalspace = d.TotalSize;
                                Console.WriteLine(inthdddrivetotalspace);
                                inthdddrivetotalspace = inthdddrivetotalspace / 1000000000;
                                int totalusedhdd = Convert.ToInt32(inthdddrivetotalspace);
                                drivetotalspace3 = "Total size of this drive is" + totalusedhdd + "gigabyte";

                                float percentofhddused = useddrivespace / inthdddrivetotalspace; //could be 1000 of 2000 used = 1000/2000 = 0.5
                                percentofhddused = percentofhddused * 100; //example 1 --> 0.5 * 100 = 50 (answer in percent)
                                int intpercentofhddused = Convert.ToInt32(percentofhddused); //answer as full int
                                string drivepercentused3 = "There is being used " + intpercentofhddused + " % of available hdd memory";

                                if (intpercentofhddused < 50 || intpercentofhddused == 50) { opiniononhdd = "There is plenty of room left on this drive."; }
                                else if (intpercentofhddused > 50 || intpercentofhddused == 85) { opiniononhdd = "There is quite some room taken but their is some left to"; }
                                else if (intpercentofhddused > 85) { opiniononhdd = "There is very less space remaining, consider adding another drive"; }


                                sSynth.Speak(drivename3);
                                System.Threading.Thread.Sleep(100);
                                sSynth.Speak(drivetype3);
                                System.Threading.Thread.Sleep(100);
                                sSynth.Speak(drivevolumelabel3);
                                System.Threading.Thread.Sleep(100);
                                sSynth.Speak(drivefilesystem3);
                                System.Threading.Thread.Sleep(100);
                                sSynth.Speak(driveavailablespace3);
                                System.Threading.Thread.Sleep(100);
                                sSynth.Speak(drivetotalspace3);
                                System.Threading.Thread.Sleep(100);
                                sSynth.Speak(drivepercentused3);
                                System.Threading.Thread.Sleep(100);
                                sSynth.Speak(opiniononhdd);
                                //drivetype3 + " " + drivevolumelabel3 + " " + drivefilesystem3 + " " + driveavailablespace3 + " " + drivetotalspace3 + " " + drivepercentused3 + " " + opiniononhdd);

                            }
                            drive = 4;
                        }
                        if (drive == 4)
                        {
                            drive = 1;
                        }
                    }

                    //part 2: possible debug - displays in output

                    //drive 1
                    //Console.WriteLine(drivename);
                    //Console.WriteLine(drivetype);
                    //Console.WriteLine(drivevolumelabel);
                    //Console.WriteLine(drivefilesystem);
                    //Console.WriteLine(driveuseravailablespace);
                    //Console.WriteLine(driveavailablespace);
                    //Console.WriteLine(drivetotalspace);

                    //drive 2
                    //Console.WriteLine(drivename2);
                    //Console.WriteLine(drivetype2);
                    //Console.WriteLine(drivevolumelabel2);
                    //Console.WriteLine(drivefilesystem2);
                    //Console.WriteLine(driveuseravailablespace2);
                    //Console.WriteLine(driveavailablespace2);
                    //Console.WriteLine(drivetotalspace2);

                    //drive 3
                    //Console.WriteLine(drivename3);
                    //Console.WriteLine(drivetype3);
                    //Console.WriteLine(drivevolumelabel3);
                    //Console.WriteLine(drivefilesystem3);
                    //Console.WriteLine(driveuseravailablespace3);
                    //Console.WriteLine(driveavailablespace3);
                    //Console.WriteLine(drivetotalspace3);

                    //part 3: getting more info and doing the actual things
                    //= inthdddriveavailablespace / 1000000000; //from bytes to gigabytes otherwise number is to high for int parsing


                }

                //if (cpuusage < 33 || cpuusage == 33) { sSynth.Speak("The system is using" + cpuusage + "% of its cpu capacity, this is a low cpu usage"); }
                //else if (cpuusage > 33 && cpuusage < 67) { sSynth.Speak("The system is using" + cpuusage + "% of its cpu capacity, this is a medium cpu usage"); }
               // else if (cpuusage > 66 || cpuusage == 66) { sSynth.Speak("The system is using" + cpuusage + "% of its cpu capacity, watch out high cpu usage detected, consider a new computer"); }

            }







            //if (command == "open notepad file" || command == "open text file" || command == "Open text file" || command == "open txt file") //notepad has been opened
            // {
            //  SoundPlayer wnf = new SoundPlayer(@"c:\Users\joostgrunwald\Desktop\C# projects\application folders\JARVIS\sounds\selectnotepad.wav");
            //  wnf.Play(); //which notepad file do you want to open?
            // textBox1.Text = "open ";
            //  textBox2.Text = "behind the 'open' type a filename ending with .txt";
            // }
            // if (command.Contains(".txt") && command.Contains("open")) //open filename.txt
            //{
            // int txtloc = command.IndexOf("txt");
            //txtloc = txtloc - 1; //substract one to get last part of filename
            // if (txtloc != -1 && txtloc > 5) //if .txt is true
            //  {
            // string debugstring = "testesttest";
            //  string notepadfile = debugstring.Substring(5, 8 + 1);
            //string notepadfile = command.Substring(5, 9); //5 means from 6 to last filename part
            // textBox1.Text = notepadfile;
            // }
            //Start("notepad.exe"); //,file

            // SoundPlayer no = new SoundPlayer(@"c:\Users\joostgrunwald\Desktop\C# projects\application folders\JARVIS\sounds\notepadhasbeenopened.wav");
            // no.Play(); //notepad opened
            //}

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            label1.Text = timestamp;
        }

        private void compbutton_Click(object sender, EventArgs e) //go to computer
        {
            //string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            //System.Diagnostics.Process.Start("explorer", myDocumentsPath)
            Process.Start("explorer.exe", @"C:\");

        }

        private void Deskbutton_Click(object sender, EventArgs e) //go to desktop
        {
            Process.Start("explorer.exe", @"C:\Users\Legion\Desktop");
        }

        private void button2_Click(object sender, EventArgs e) //ctrl
        {
            Process.Start("explorer.exe", @"C:\Users\Legion\AppData\Roaming");
        }

        private void button3_Click(object sender, EventArgs e) //docs
        {
            Process.Start("explorer.exe", @"C:\Users\Legion\Documents");
        }

        private void button7_Click(object sender, EventArgs e) //vids
        {
            Process.Start("explorer.exe", @"C:\Users\Legion\Videos");
        }

        private void button6_Click(object sender, EventArgs e) //imgs
        {
            Process.Start("explorer.exe", @"C:\Users\Legion\Pictures");
        }

        private void button5_Click(object sender, EventArgs e) //games
        {
            Process.Start("explorer.exe", @"C:\Users\Legion\Desktop\Games");
        }

        private void button4_Click(object sender, EventArgs e) //music
        {
            Process.Start("explorer.exe", @"C:\Users\Legion\Music");
        }

        private void timer2_Tick(object sender, EventArgs e) //each five seconds check
        {
            if (IsInternetAvailable() == true)
            {
                label4.Text = "Connected";
                label4.ForeColor = System.Drawing.Color.Green;
            }
            if (IsInternetAvailable() == false)
            {
                label4.Text = "Not connected";
                label4.ForeColor = System.Drawing.Color.Red;
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", @"`C:/users/Joostgrunwald");
        }

        private void timer3_Tick_1(object sender, EventArgs e)
        {
            //SoundPlayer nc = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\startedup.wav");
            //nc.Play(); //word closed

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            int drive = 1;
            foreach (DriveInfo d in allDrives)
            {
                if (drive == 1) //SSD
                {
                    if (d.IsReady == true)
                    {

                        inthdddriveavailablespace = d.TotalFreeSpace; //setting it on a decimal
                        inthdddriveavailablespace = inthdddriveavailablespace / 1000000000; //from bytes to gigabytes
                        usedhdd = Convert.ToInt32(inthdddriveavailablespace); //now we got gigabytes as int
                        float useddrivespace = 500 - usedhdd;

                        inthdddrivetotalspace = d.TotalSize;
                        inthdddrivetotalspace = inthdddrivetotalspace / 1000000000;
                        int totalusedhdd = Convert.ToInt32(inthdddrivetotalspace);

                        float percentofhddused = useddrivespace / inthdddrivetotalspace; //could be 1000 of 2000 used = 1000/2000 = 0.5
                        float percentofhddused2 = percentofhddused * 100; //example 1 --> 0.5 * 100 = 50 (answer in percent)
                        int intpercentofhddused = Convert.ToInt32(percentofhddused2); //answer as full int

                        if (intpercentofhddused > 90) { opiniononhdd = "There is very less space remaining on your ssd drive, consider adding another drive"; }
                        sSynth.Speak(opiniononhdd);

                        picturebox4memorylength = 175 * percentofhddused; //beginning size * procent of cpu used = new size (it fills with cpu)

                        int pictureboxheight4 = Convert.ToInt32(picturebox4memorylength);
                        pictureBox4.Height = pictureboxheight4;

                        if (intpercentofhddused < 60 || intpercentofhddused == 60) { pictureBox4.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ramgreen4.png"); }
                        else if (intpercentofhddused > 60 && intpercentofhddused < 80) { pictureBox4.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ramgeel4.png"); }
                        else if (intpercentofhddused > 80 || intpercentofhddused == 80) { pictureBox4.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ramred4.png"); }
                    }
                    drive = 2;
                }
                else if (drive == 2)
                {
                    drive = 3;
                }
                else if (drive == 3) //HDD
                {
                    intssddriveavailablespace = d.TotalFreeSpace; //setting it on a decimal
                    intssddriveavailablespace = intssddriveavailablespace / 1000000000; //from bytes to gigabytes
                    usedssd = Convert.ToInt32(intssddriveavailablespace); //now we got gigabytes as int
                    float useddrivespace2 = 2000 - usedssd;

                    intssddrivetotalspace = d.TotalSize;
                    intssddrivetotalspace = intssddrivetotalspace / 1000000000;
                    int totalusedssd = Convert.ToInt32(intssddrivetotalspace);

                    float percentofssdused = useddrivespace2 / intssddrivetotalspace; //could be 1000 of 2000 used = 1000/2000 = 0.5
                    sSynth.Speak(percentofssdused.ToString());
                    float percentofssdused2 = percentofssdused * 100; //example 1 --> 0.5 * 100 = 50 (answer in percent)
                    int intpercentofssdused = Convert.ToInt32(percentofssdused2); //answer as full int

                    if (intpercentofssdused > 90) { opiniononssd = "There is very less space remaining on your hdd drive, consider adding another drive"; }
                    sSynth.Speak(opiniononssd);

                    picturebox5memorylength = 175 * percentofssdused; //beginning size * procent of cpu used = new size (it fills with cpu)

                    int pictureboxheight5 = Convert.ToInt32(picturebox5memorylength);
                    pictureBox5.Height = pictureboxheight5;

                    if (intpercentofssdused < 60 || intpercentofssdused == 60) { pictureBox5.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ramgreen5.png"); }
                    else if (intpercentofssdused > 60 && intpercentofssdused < 80) { pictureBox5.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ramgeel5.png"); }
                    else if (intpercentofssdused > 80 || intpercentofssdused == 80) { pictureBox5.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ramred5.png"); }
                }
                drive = 4;
            }
            if (drive == 4)
            {
                drive = 1;
            }
            timer3.Enabled = false;
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void pictureBox1_Click(object sender, EventArgs e)
        {
            backgroundui.Image = Image.FromFile(@"C:\Users\Legion\Downloads\58a2001fb8104864020261.gif");
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            //ram begin
            ramavailable = Convert.ToInt32(ramCounter.NextValue());
            ramusage = (8000 - ramavailable) / 8000; //my ram = 8gb = 8000 mb

            pictureboxmemorylength = 175 * ramusage; //beginning size * procent of ram used = new size (it fills with ram)
            pictureboxmemorywidth = 94 * ramusage; //beginning size * procent of ram used = new size (it fills with ram)

            int pictureboxheight = Convert.ToInt32(pictureboxmemorylength);

            pictureBox3.Height = pictureboxheight;

            ramusagepercent = ramusage * 100;
            int ramusagepercentint = Convert.ToInt32(ramusagepercent);
            label6.Text = ramusagepercentint.ToString() + "%";

            if (ramusagepercent < 33 || ramusagepercent == 33) { pictureBox3.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ram3.png"); }
            else if (ramusagepercent > 33 && ramusagepercent < 67) { pictureBox3.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ramgeel.png"); }
            else if (ramusagepercent > 66 || ramusagepercent == 66) { pictureBox3.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ramred.png"); }
            //ram end

            //cpu begin
            PerformanceCounter cpuCounter;
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            label7.Text = cpuCounter.NextValue().ToString() + "%";

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


        public class ProcessorUsage
        {
            const float sampleFrequencyMillis = 1000;

            protected object syncLock = new object();
            protected PerformanceCounter counter;
            protected float lastSample;
            protected DateTime lastSampleTime;

            /// <summary>
            /// 
            /// </summary>
            public ProcessorUsage()
            {
                this.counter = new PerformanceCounter("Processor", "% Processor Time", "_Total", true);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public float GetCurrentValue()
            {
                if ((DateTime.UtcNow - lastSampleTime).TotalMilliseconds > sampleFrequencyMillis)
                {
                    lock (syncLock)
                    {
                        if ((DateTime.UtcNow - lastSampleTime).TotalMilliseconds > sampleFrequencyMillis)
                        {
                            lastSample = counter.NextValue();
                            lastSampleTime = DateTime.UtcNow;
                        }
                    }
                }

                return lastSample;
            }
        }

        private PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

        private void timer4_Tick_1(object sender, EventArgs e)
        {
            //ram begin
            ramavailable = Convert.ToInt32(ramCounter.NextValue());
            ramusage = (8000 - ramavailable) / 8000; //my ram = 8gb = 8000 mb

            pictureboxmemorylength = 175 * ramusage; //beginning size * procent of ram used = new size (it fills with ram)

            int pictureboxheight = Convert.ToInt32(pictureboxmemorylength);

            pictureBox3.Height = pictureboxheight;

            ramusagepercent = ramusage * 100;
            int ramusagepercentint = Convert.ToInt32(ramusagepercent);
            label6.Text = ramusagepercentint.ToString() + "%";

            if (ramusagepercent < 33 || ramusagepercent == 33) { pictureBox3.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ram3.png"); }
            else if (ramusagepercent > 33 && ramusagepercent < 67) { pictureBox3.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ramgeel.png"); }
            else if (ramusagepercent > 66 || ramusagepercent == 66) { pictureBox3.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ramred.png"); }
            //ram end

            //cpu begin
            cpuCounter.NextValue();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            //cpu begin
            cpuusage = Convert.ToInt32(cpuCounter.NextValue());
            label7.Text = cpuusage.ToString() + "%";

            cpumultiplier = cpuusage / 100;
            picturebox2memorylength = 175 * cpumultiplier; //beginning size * procent of cpu used = new size (it fills with cpu)

            int pictureboxheight2 = Convert.ToInt32(picturebox2memorylength);
            pictureBox2.Height = pictureboxheight2;

            if (cpuusage < 60 || cpuusage == 60) { pictureBox2.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ramgreen3.png"); }
            else if (cpuusage > 60 && cpuusage < 80) { pictureBox2.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ramgeel2.png"); }
            else if (cpuusage > 80 || cpuusage == 80) { pictureBox2.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ramred2.png"); }
            //cpu end

            //ssd drive begin


            //ssd drive end
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            string de = DateTime.Now.DayOfWeek.ToString();
            string time = DateTime.Now.ToString("HH");
            if (de == "5")
            {
                if (time == "14" || time == "15" || time == "16")
                {
                    if (askedforboatcheck == false)
                    {
                        SoundPlayer hello = new SoundPlayer(@"C:\Users\Legion\Documents\Visual Studio 2017\Projects\J.A.R.V.I.S\WindowsFormsApplication6\voice\checkroadboat.wav");
                        hello.Play();
                        speakingmode = 3;
                        askedforboatcheck = true;
                    }
                }
                else
                {
                    askedforboatcheck = false;
                    if (speakingmode == 3) { speakingmode = 1; }
                }
            }

            //ssd and hdd
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                int drive = 1;
                foreach (DriveInfo d in allDrives)
                {
                    if (drive == 1) //SSD
                    {
                        if (d.IsReady == true)
                        {

                            inthdddriveavailablespace = d.TotalFreeSpace; //setting it on a decimal
                            inthdddriveavailablespace = inthdddriveavailablespace / 1000000000; //from bytes to gigabytes
                            usedhdd = Convert.ToInt32(inthdddriveavailablespace); //now we got gigabytes as int
                            float useddrivespace = 500 - usedhdd;

                            inthdddrivetotalspace = d.TotalSize;
                            inthdddrivetotalspace = inthdddrivetotalspace / 1000000000;
                            int totalusedhdd = Convert.ToInt32(inthdddrivetotalspace);

                            float percentofhddused = useddrivespace / inthdddrivetotalspace; //could be 1000 of 2000 used = 1000/2000 = 0.5
                            float percentofhddused2 = percentofhddused * 100; //example 1 --> 0.5 * 100 = 50 (answer in percent)
                            int intpercentofhddused = Convert.ToInt32(percentofhddused2); //answer as full int

                            if (intpercentofhddused > 90) { opiniononhdd = "There is very less space remaining on your ssd drive, consider adding another drive"; }
                            sSynth.Speak(opiniononhdd);

                            picturebox4memorylength = 175 * percentofhddused; //beginning size * procent of cpu used = new size (it fills with cpu)

                            int pictureboxheight4 = Convert.ToInt32(picturebox4memorylength);
                            pictureBox4.Height = pictureboxheight4;

                            if (intpercentofhddused < 60 || intpercentofhddused == 60) { pictureBox4.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ramgreen4.png"); }
                            else if (intpercentofhddused > 60 && intpercentofhddused < 80) { pictureBox4.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ramgeel4.png"); }
                            else if (intpercentofhddused > 80 || intpercentofhddused == 80) { pictureBox4.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ramred4.png"); }
                        }
                        drive = 3;
                    }
                }

                foreach (DriveInfo d in allDrives)
                {
                    if (drive == 3) //HDD
                    {
                        intssddriveavailablespace = d.TotalFreeSpace; //setting it on a decimal
                        intssddriveavailablespace = intssddriveavailablespace / 1000000000; //from bytes to gigabytes
                        usedssd = Convert.ToInt32(intssddriveavailablespace); //now we got gigabytes as int
                        float useddrivespace2 = 500 - usedssd;

                        intssddrivetotalspace = d.TotalSize;
                        intssddrivetotalspace = intssddrivetotalspace / 1000000000;
                        int totalusedssd = Convert.ToInt32(intssddrivetotalspace);

                        float percentofssdused = useddrivespace2 / intssddrivetotalspace; //could be 1000 of 2000 used = 1000/2000 = 0.5
                        float percentofssdused2 = percentofssdused * 100; //example 1 --> 0.5 * 100 = 50 (answer in percent)
                        int intpercentofssdused = Convert.ToInt32(percentofssdused2); //answer as full int

                        if (intpercentofssdused > 90) { opiniononssd = "There is very less space remaining on your hdd drive, consider adding another drive"; }
                        sSynth.Speak(opiniononssd);

                        picturebox5memorylength = 175 * percentofssdused; //beginning size * procent of cpu used = new size (it fills with cpu)

                        int pictureboxheight5 = Convert.ToInt32(picturebox5memorylength);
                        pictureBox5.Height = pictureboxheight5;

                        if (intpercentofssdused < 60 || intpercentofssdused == 60) { pictureBox5.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ramgreen5.png"); }
                        else if (intpercentofssdused > 60 && intpercentofssdused < 80) { pictureBox5.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ramgeel5.png"); }
                        else if (intpercentofssdused > 80 || intpercentofssdused == 80) { pictureBox5.BackgroundImage = Image.FromFile(@"C:\Users\Legion\Desktop\visual studio files\application folders\JARVIS\fotos\ramred5.png"); }
                    }
                }

            }

        }
    }
}
