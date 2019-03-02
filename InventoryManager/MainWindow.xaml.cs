using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Win32;

namespace InventoryManager
{
    class loadFile
    {
        private string boxName;
        private string item;
        private string textName;
        private string quantity;

        public string BoxName{ get => boxName; set => boxName = value; }
        public string Item { get => item; set => item = value; }
        public string TextName { get => textName; set => textName = value; }
        public string Quantity { get => quantity; set => quantity = value; }

        public loadFile(string v1, string v2, string v3, string v4)
        {
            BoxName = v1;
            Item = v2;
            TextName = v3;
            Quantity = v4;
        }
    }

    public partial class MainWindow : Window
    {

        public ObservableCollection<string> options = new ObservableCollection<string>();
        List<loadFile> loadFiles = new List<loadFile>();

        public int rowNum;
        public int columnNum;
        public MainWindow()
        {
            InitializeComponent();

            options.Add("Deadpool");
            options.Add("Hello Kitty");
            options.Add("Storm");
            options.Add("X-23");
            options.Add("Wolverine");
            options.Add("Stan Lee");
            options.Add("Raven");
            options.Add("Batman");
            options.Add("Wonder Woman");
            options.Add("Uno");

            string temp = "";
            for(int i = 0; i < 24; i++)
            {
                temp = "_0x" + i.ToString() + "Box";
                object tempOb = FindName(temp);
                ((ComboBox)tempOb).ItemsSource = options;
            }
        }

        //private void RowBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    String stringValue = RowBox.SelectedValue.ToString();
        //    rowNum = int.Parse(stringValue);
        //    if (rowNum == 2)
        //    {
        //        _0x0.Fill = Brushes.MediumPurple;
        //        Console.WriteLine("Wat");
        //    }
        //}

        private void PictureBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string temp = ((ComboBox)sender).Name;
            temp = temp.Remove((temp.Length - 3), 3);
            object wanted = FindName(temp);
            BitmapImage bitmapImage = new BitmapImage();

            bitmapImage.BeginInit();
            if (File.Exists(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "/Pictures/" + ((ComboBox)sender).SelectedItem + ".jpg"))
            {
                bitmapImage.UriSource = new Uri(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "/Pictures/" + ((ComboBox)sender).SelectedItem + ".jpg");

                bitmapImage.EndInit();
                ((Image)wanted).Source = bitmapImage;
            }
            else
            {
                ((ComboBox)sender).SelectedItem = null;
                ((Image)wanted).Source = null;
            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(((TextBox)sender).Text, @"^[0-9]+$"))
            {
                if ((Convert.ToInt32(((TextBox)sender).Text)) >= 100)
                {
                    ((TextBox)sender).Text = "99";
                }
            }
        }
         
        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            IEnumerable<ComboBox> comboBoxes = MainGrid.Children.OfType<ComboBox>();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt;)|*.txt;|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                int i = -1;
                using (StreamWriter sw = File.CreateText(saveFileDialog.FileName))
                {
                    foreach (ComboBox combo in comboBoxes)
                    {
                        i++;
                        string temp;
                        temp = "_0x" + i.ToString() + "Text";
                        object tempOb = FindName(temp);
                        if (combo.SelectedItem != null)
                        {
                            sw.WriteLine(combo.Name + "," + combo.SelectedItem.ToString() + "," + ((TextBox)tempOb).Name + "," + ((TextBox)tempOb).Text);
                        }
                        else
                        {
                            sw.WriteLine(combo.Name + ",null," + ((TextBox)tempOb).Name + "," + "0");
                        }
                    }
                }
            }
        }

        private void OnLoadClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Click");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string[] temp;
                string[] lines = File.ReadAllLines(openFileDialog.FileName);
                foreach(string s in lines)
                {
                    temp = s.Split(',');
                    string temps;
                    temps = (temp[0]);
                    object tempOb = FindName(temps);

                    string tempT;
                    tempT = (temp[2]);
                    object tempText = FindName(tempT);

                    if (temp[1] != "null")
                    {
                        ((ComboBox)tempOb).SelectedItem = temp[1];
                    }
                    else
                    {
                        ((ComboBox)tempOb).SelectedItem = null;
                    }
                    ((TextBox)tempText).Text = temp[3];
                }
            }
        }
    }
}