using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using Extract.Files;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;

namespace InnoSetupInstaller
{
    class Program
    {
        static void Main(string[] args)
        {
            SoundPlayer gjc;
            string a = "1 - Install Inno Setup Without Registry";
            string hn = "2 - Install Inno Setup with Registry";
            string c = "Please Choose Options to Install Inno Setup";
            Console.Title = "Inno Setup Installer";
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition((Console.WindowWidth - c.Length) / 2, Console.CursorTop);
            Console.SetCursorPosition((Console.WindowWidth - a.Length) / 2, Console.CursorTop);
            Console.SetCursorPosition((Console.WindowWidth - hn.Length) / 2, Console.CursorTop);
            Directory.CreateDirectory(@"C:\MusicHeart");
            ExtractX xam = new ExtractX();
            xam.ExtractFiles("InnoSetupInstaller", @"C:\MusicHeart", "Resources", "HeartOfCourage.wav");
            gjc = new SoundPlayer(@"C:\MusicHeart\HeartOfCourage.wav");
            gjc.PlayLooping();
            Console.WriteLine(c);
            Thread.Sleep(2000);
            Console.WriteLine(a);
            Console.WriteLine(hn);
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Directory.CreateDirectory(@"C:\Temp");
                    Directory.CreateDirectory(@"C:\Program Files\Inno Setup");
                    Console.Write("Installing Inno Setup...");
                    ExtractX zzz = new ExtractX();
                    zzz.ExtractFiles("InnoSetupInstaller", @"C:\Temp", "Resources", "InnoSetup.zip");
                    ZipFile.ExtractToDirectory(@"C:\Temp\InnoSetup.zip", @"C:\Program Files\Inno Setup");
                    File.Delete(@"C:\Temp\InnoSetup.zip");
                    Directory.Delete(@"C:\Temp");
                    Thread.Sleep(5500);
                    gjc.Stop();
                    File.Delete(@"C:\MusicHeart\HeartOfCourage.wav");
                    Directory.Delete(@"C:\MusicHeart");
                    break;
                case "2":
                    Console.Clear();
                    Console.Write("Installing Inno Setup...");
                    Directory.CreateDirectory(@"C:\Temp");
                    Directory.CreateDirectory(@"C:\Program Files\Inno Setup");
                    ExtractX extract = new ExtractX();
                    extract.ExtractFiles("InnoSetupInstaller", @"C:\Temp", "Resources", "InnoSetup.zip");
                    ZipFile.ExtractToDirectory(@"C:\Temp\InnoSetup.zip", @"C:\Program Files\Inno Setup");
                    File.Delete(@"C:\Temp\InnoSetup.zip");
                    Directory.Delete(@"C:\Temp");
                    Registry.ClassesRoot.CreateSubKey(".iss");
                    Registry.ClassesRoot.CreateSubKey(@".iss\shell\InnoSetup");
                    Registry.ClassesRoot.CreateSubKey(@".iss\shell\InnoSetup\command");
                    var key1 = Registry.ClassesRoot.OpenSubKey(@".iss\shell\InnoSetup", true);
                    key1.SetValue("Icon", @"C:\Program Files\Inno Setup\InnoSetup.ico", RegistryValueKind.String);
                    key1.SetValue(String.Empty, "Open with Inno Setup", RegistryValueKind.String);
                    var key2 = Registry.ClassesRoot.OpenSubKey(@".iss\shell\InnoSetup\command", true);
                    const string quote = "\"";
                    key2.SetValue(String.Empty, @"C:\Program Files\InnoSetup\Compil32.exe " + quote + "%1" + quote);
                    Thread.Sleep(5500);
                    gjc.Stop();
                    File.Delete(@"C:\MusicHeart\HeartOfCourage.wav");
                    Directory.Delete(@"C:\MusicHeart");
                    break;
            }
            Thread.Sleep(5000);
            MessageBox.Show("Thank you for Using this Program" + Environment.NewLine + "Made By GlebYoutuber", "Inno Setup Installer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Process.Start(@"C:\Program Files\Inno Setup\Compil32.exe");
            Environment.Exit(-2341);
        }
    }
}
