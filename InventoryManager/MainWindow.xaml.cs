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

namespace InventoryManager
{
    public partial class MainWindow : Window
    {

        public ObservableCollection<string> options = new ObservableCollection<string>();

        public int rowNum;
        public int columnNum;
        public MainWindow()
        {
            InitializeComponent();

            options.Add("Deadpool");
            options.Add("Hello Kitty");
            options.Add("Storm");

            _0x0Box.ItemsSource = options;
            _0x1Box.ItemsSource = options;
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
            bitmapImage.UriSource = new Uri(@"C:\Users\s189075\Downloads\" + ((ComboBox)sender).SelectedItem + ".jpg");

            bitmapImage.EndInit();
            ((Image)wanted).Source = bitmapImage;

        }
    }
}