using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace CitiesSorter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> cities = new List<string>();

        string result;

        string filePath;
        string fileName;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "C:\\Users\\NPC\\Desktop";
            fileDialog.Filter = "txt Files (*.txt)|*.txt;";
            fileDialog.FilterIndex = 1;

            fileDialog.ShowDialog();

            if (fileDialog != null)
            {
                filePath = fileDialog.FileName;
                string[] temp = filePath.Split(new char[] { '\\' });
                fileName = temp[temp.Length-1];
                File_Label.Content = fileName;
                cities = File.ReadAllLines(filePath).ToList();
                Process_Button.IsEnabled = true;
                Save_Button.IsEnabled = true;
            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.InitialDirectory = "c:\\";
            saveDialog.DefaultExt = ".txt";

            saveDialog.ShowDialog();

            if(saveDialog != null)
            {
                File.WriteAllText(saveDialog.FileName, TextDisplay.Text.ToString());
            }
        }

        //Main algorithm
        private void Process_Button_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            char ch;
            Random rnd = new Random();
            List<string> citiesCopy = cities;

            string first = citiesCopy[rnd.Next(0, citiesCopy.Count - 1)];
            citiesCopy.Remove(first);
            result = first;


            while (citiesCopy.Count >= 1 && flag != true)
            {
                ch = (char)result[result.Length-1];
                foreach (string item in citiesCopy)
                {
                    if((char)item[0] == char.ToUpper(ch))
                    {
                        result += " -> " + item;
                        citiesCopy.Remove(item);
                        break;
                    }
                    if (item == citiesCopy.Last()) flag = true;
                }
            }
            TextDisplay.Text = result;
        }
    }
}
