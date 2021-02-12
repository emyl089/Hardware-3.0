using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.IO;

namespace Hardware3._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Overview
            Componenta("Win32_Processor", "SystemName", label10);
            Componenta("Win32_Processor", "Name", label14);
            Componenta("Win32_OperatingSystem", "Caption", label11);
            Componenta("Win32_VideoController", "Name", label15);
            Componenta("Win32_BaseBoard", "Manufacturer", label12);
            Componenta("Win32_BaseBoard", "Product", label13);
            Componenta("Win32_BIOS", "Name", label18);
            //Afisare RAM in MB
            Int64 ram = RAM();
            label89.Text = ram.ToString() + " MB";
            //Afisare nume cont Microsoft;
            label42.Text = SystemInformation.UserName;

            //CPU
            Componenta("Win32_Processor", "Name", label46);
            Componenta("Win32_Processor", "Architecture", label48);
            if (label48.Text == "9")
                label48.Text = "x64";
            Componenta("Win32_Processor", "NumberOfCores", label58);
            Componenta("Win32_Processor", "NumberOfLogicalProcessors", label60);
            Componenta("Win32_Processor", "L2CacheSize", label54);
            Componenta("Win32_Processor", "L3CacheSize", label56);
            Componenta("Win32_Processor", "CurrentClockSpeed", label50);
            Componenta("Win32_Processor", "Family", label52);
            if (label52.Text == "206")
                label52.Text = "Coffee Lake";

            //GPU
            Componenta("Win32_VideoController", "Caption", label27);
            Componenta("Win32_VideoController", "Name", label28);
            Componenta("Win32_VideoController", "VideoProcessor", label29);
            Componenta("Win32_VideoController", "AdapterDACType", label30);
            //VRAM
            String vram = VRAM().ToString();
            label31.Text = vram + " MB";
            //Shared Memory
            Int64 shared = RAM() / 2;
            label32.Text = shared.ToString() + " MB";
            //Rezolutie
            String disp = Display();
            label33.Text = disp;
            Componenta("Win32_DesktopMonitor", "Name", label34);
            //Refresh Rate
            Int64 rr = RR();
            if (rr == 0)
                label44.Text = "n/a";
            else
                label44.Text = rr.ToString() + " Hz";
            Componenta("Win32_VideoController", "DriverVersion", label39);
            //Afisarea datii driverului
            string date = Data();
            string an = date.Substring(0, 4);
            string luna = date.Substring(4, 2);
            string zi = date.Substring(6, 2);
            label40.Text = zi + "/" + luna + "/" + an;
            //Notes GPU
            String notG;
            Int32 noteG = NotesG();
            try
            {
                if (noteG == 0)
                    notG = "'This device is working properly.'";
                else if (noteG == 1)
                    notG = "'This device is not configured correctly.'";
                else if (noteG == 2)
                    notG = "'Windows cannot load the driver for this device.'";
                else if (noteG == 3)
                    notG = "'The driver for this device might be corrupted'";
                else if (noteG == 4)
                    notG = "'or your system may be running low on memory or other resources.'";
                else if (noteG == 5)
                    notG = "'This device is not working properly. One of its drivers or your registry might be corrupted.'";
                else if (noteG == 6)
                    notG = "'The driver for this device needs a resource that Windows cannot manage.'";
                else if (noteG == 7)
                    notG = "'The boot configuration for this device conflicts with other devices.'";
                else if (noteG == 8)
                    notG = "'Cannot filter.'";
                else if (noteG == 9)
                    notG = "'The driver loader for the device is missing.'";
                else if (noteG == 10)
                    notG = "'This device is not working properly because the controlling firmware is reporting the resources for the device incorrectly.'";
                else if (noteG == 11)
                    notG = "'This device cannot start.'";
                else if (noteG == 12)
                    notG = "'This device failed.'";
                else if (noteG == 13)
                    notG = "'This device cannot find enough free resources that it can use.'";
                else if (noteG == 14)
                    notG = "'Windows cannot verify this devices resources.'";
                else if (noteG == 15)
                    notG = "'This device cannot work properly until you restart your computer.'";
                else if (noteG == 16)
                    notG = "'This device is not working properly because there is probably a re - enumeration problem.'";
                else if (noteG == 17)
                    notG = "'Windows cannot identify all the resources this device uses.'";
                else if (noteG == 18)
                    notG = "'This device is asking for an unknown resource type.'";
                else if (noteG == 19)
                    notG = "'Reinstall the drivers for this device.'";
                else if (noteG == 20)
                    notG = "'Failure using the VxD loader.'";
                else if (noteG == 21)
                    notG = "'Your registry might be corrupted.'";
                else if (noteG == 22)
                    notG = "'System failure: Try changing the driver for this device.If that does not work'";
                else if (noteG == 23)
                    notG = "'See your hardware documentation. Windows is removing this device.'";
                else if (noteG == 24)
                    notG = "'This device is disabled.'";
                else if (noteG == 25)
                    notG = "'System failure: Try changing the driver for this device.If that doesnt work  '";
                else if (noteG == 26)
                    notG = "'see your hardware documentation.'";
                else if (noteG == 27)
                    notG = "'This device is not present'";
                else if (noteG == 28)
                    notG = "'is not working properly'";
                else if (noteG == 29)
                    notG = "'or does not have all its drivers installed.'";
                else
                    notG = "'Windows is still setting up this device.'";
            }
            catch (Exception)
            {
                notG = "Io nu mai inteleg nimic.";
            }
            textBox1.Text = notG;

            //Memory
            Componenta("Win32_DiskDrive", "Caption", label62);
            Componenta("Win32_DiskDrive", "Manufacturer", label64);
            Componenta("Win32_DiskDrive", "InterfaceType", label66);
            Componenta("Win32_DiskDrive", "MediaType", label68);
            Componenta("Win32_DiskDrive", "Partitions", label70);
            Componenta("Win32_DiskDrive", "SerialNumber", label72);
            Componenta("Win32_DiskDrive", "Status", label76);
            //HDD Memory
            String hdd = HDD().ToString();
            label74.Text = hdd + " GB";
            //SSD
            label96.Text = "Nu exista modul SSD instalat.";

            //RAM
            Componenta("Win32_PhysicalMemory", "Name", label84);
            Componenta("Win32_PhysicalMemory", "Manufacturer", label88);
            Componenta("Win32_PhysicalMemory", "BankLabel", label92);
            Componenta("Win32_PhysicalMemory", "Speed", label85);
            Componenta("Win32_PhysicalMemory", "DataWidth", label81);

        }

        //Clasa principala
        private static void Componenta(string hardclass, string prop, Label labelx)
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM " + hardclass);
            foreach (ManagementObject mo in mos.Get())
            {
                labelx.Text = mo[prop].ToString();
            }
        }

        //Clasa pentru receptionarea informatiei despre data driverului
        private String Data()
        {
            ManagementClass mc = new ManagementClass("Win32_VideoController");
            ManagementObjectCollection moc = mc.GetInstances();
            String date = String.Empty;
            foreach (ManagementObject mo in moc)
            {
                date = mo["DriverDate"].ToString();
            }
            return date;
        }

        //Clasa pentru memorie RAM
        private Int64 RAM()
        {
            ManagementClass mc = new ManagementClass("Win32_PhysicalMemory");
            ManagementObjectCollection moc = mc.GetInstances();
            Int64 ram = 0;
            Int64 MemSize = 0;
            foreach (ManagementObject mo in moc)
            {
                ram = Convert.ToInt64(mo["Capacity"]);
                MemSize += ram;
            }
            MemSize = MemSize / 1048576;
            return MemSize;
        }

        //Clasa pentru rezolutie display
        private String Display()
        {
            ManagementClass mc = new ManagementClass("Win32_VideoController");
            ManagementObjectCollection moc = mc.GetInstances();
            String disp = String.Empty;
            String h = String.Empty;
            String v = String.Empty;
            try
            {
                foreach (ManagementObject mo in moc)
                {
                    h = mo["CurrentHorizontalResolution"].ToString();
                    v = mo["CurrentVerticalResolution"].ToString();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Nu inteleg nimic.");
            }
            disp = h + " x " + v;
            return disp;
        }

        //Clasa pentru Memoria Video
        private Int64 VRAM()
        {
            ManagementClass mc = new ManagementClass("Win32_VideoController");
            ManagementObjectCollection moc = mc.GetInstances();
            Int64 vram = 0;
            foreach (ManagementObject mo in moc)
            {
                vram = Convert.ToInt64(mo["AdapterRAM"]);
            }
            return vram / 1048576;
        }

        //Clasa pentru Memorie HDD
        private Int64 HDD()
        {
            ManagementClass mc = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc = mc.GetInstances();
            Int64 hdd = 0;
            foreach (ManagementObject mo in moc)
            {
                hdd = Convert.ToInt64(mo["Size"]);
            }
            return hdd / 1024 / 1024 / 1024;
        }
        //Clasa pentru Refresh Rate
        private Int64 RR()
        {
            ManagementClass mc = new ManagementClass("Win32_VideoController");
            ManagementObjectCollection moc = mc.GetInstances();
            Int64 rr = 0;
            foreach (ManagementObject mo in moc)
            {
                rr = Convert.ToInt64(mo["CurrentRefreshRate"]);
            }
            return rr;
        }

        //Clasa pentru Notes Video
        private Int32 NotesG()
        {
            ManagementClass mc = new ManagementClass("Win32_VideoController");
            ManagementObjectCollection moc = mc.GetInstances();
            Int32 note = 0;
            foreach (ManagementObject mo in moc)
            {
                note = Convert.ToInt32(mo["ConfigManagerErrorCode"]);
            }
            return note;
        }

        //Clasa pentru Notes Memory
        private Int32 NotesM()
        {
            ManagementClass mc = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc = mc.GetInstances();
            Int32 note = 0;
            foreach (ManagementObject mo in moc)
            {
                note = Convert.ToInt32(mo["ConfigManagerErrorCode"]);
            }
            return note;
        }

        //Panel 1
        private void Button1_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            panel7.Enabled = false;
            panel1.Visible = true;
            panel7.Visible = false;
        }

        //Panel 2
        private void Button2_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            panel7.Enabled = true;
            panel1.Visible = false;
            panel7.Visible = true;
        }

        //Save info
        private void Button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                StreamWriter sw = new StreamWriter("D:\\Hardware.txt");
                sw.WriteLine("===================================");
                sw.WriteLine("System Information");
                sw.WriteLine(" ");
                sw.WriteLine("Computer Name: " + label10.Text);
                sw.WriteLine("Computer System: " + label11.Text);
                sw.WriteLine("User Name: " + label42.Text);
                sw.WriteLine("Manufacter: " + label12.Text);
                sw.WriteLine("Model: " + label13.Text);
                sw.WriteLine("Processor: " + label14.Text);
                sw.WriteLine("GPU: " + label15.Text);
                sw.WriteLine("RAM: " + label16.Text);
                sw.WriteLine("System: " + label18.Text);
                sw.WriteLine("===================================");
                sw.WriteLine("===================================");
                sw.WriteLine("CPU");
                sw.WriteLine(" ");
                sw.WriteLine("Name: " + label46.Text);
                sw.WriteLine("Arhitecture: " + label48.Text);
                sw.WriteLine("Nr. of Cores: " + label58.Text);
                sw.WriteLine("Threads: " + label60.Text);
                sw.WriteLine("Cache lvl 2: " + label54.Text);
                sw.WriteLine("Cache lvl 3: " + label56.Text);
                sw.WriteLine("Clock speed: " + label50.Text);
                sw.WriteLine("Family: " + label52.Text);
                sw.WriteLine("===================================");
                sw.WriteLine("===================================");
                sw.WriteLine("GPU");
                sw.WriteLine(" ");
                sw.WriteLine("Name: " + label27.Text);
                sw.WriteLine("Manufacturer: " + label28.Text);
                sw.WriteLine("Chip Type: " + label29.Text);
                sw.WriteLine("DAC Type: " + label30.Text);
                sw.WriteLine("VRAM: " + label31.Text);
                sw.WriteLine("Shared Memory: " + label32.Text);
                sw.WriteLine("Display Mode: " + label33.Text);
                sw.WriteLine("Monitor: " + label34.Text);
                sw.WriteLine("Refresh Rate: " + label44.Text);
                sw.WriteLine("===================================");
                sw.WriteLine("===================================");
                sw.WriteLine("RAM");
                sw.WriteLine(" ");
                sw.WriteLine("Device Name: " + label84.Text);
                sw.WriteLine("Manufacturer: " + label88.Text);
                sw.WriteLine("Bank Label: " + label92.Text);
                sw.WriteLine("Capacity: " + label89.Text);
                sw.WriteLine("Speed: " + label85.Text);
                sw.WriteLine("Data Width: " + label81.Text);
                sw.WriteLine("===================================");
                sw.WriteLine("===================================");
                sw.WriteLine("HDD");
                sw.WriteLine(" ");
                sw.WriteLine("Device Name: " + label62.Text);
                sw.WriteLine("Manufacturer: " + label64.Text);
                sw.WriteLine("Interface Type: " + label66.Text);
                sw.WriteLine("Device Type: " + label68.Text);
                sw.WriteLine("Partition: " + label70.Text);
                sw.WriteLine("Serial Nr.: " + label72.Text);
                sw.WriteLine("Size: " + label74.Text);
                sw.WriteLine("Status: " + label76.Text);
                sw.WriteLine("===================================");

                sw.Close();
                MessageBox.Show("Text salvat in directorul 'D:\\'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        //YES
        private void Button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Enabled = true;
            pictureBox2.Enabled = false;
            pictureBox1.Visible = true;
            pictureBox2.Visible = false;
        }

        //NO
        private void Button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Enabled = false;
            pictureBox2.Enabled = true;
            pictureBox1.Visible = false;
            pictureBox2.Visible = true;
        }
    }
}
