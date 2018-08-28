using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading.Tasks;

namespace QuizAttempt2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog file = new Microsoft.Win32.OpenFileDialog();
            file.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";
            if (file.ShowDialog() == true)
            {
                list_update.image_details.Add(new image_class() { location = file.FileName, name = file.SafeFileName, size = new System.IO.FileInfo(file.FileName).Length });
                //listBox.DataContext = list_update.image_details;
                listBox.ItemsSource = list_update.image_details;
                listBox.Items.Refresh();
            }

        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                var result = MessageBox.Show("Delete image named: " + list_update.image_details[listBox.SelectedIndex].name + " ?", "Confirm delete", MessageBoxButton.YesNo);
                if (MessageBoxResult.Yes == result)
                {
                    list_update.image_details.RemoveAt(listBox.SelectedIndex);
                    listBox.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Select an image to remove.", "Warning");
            }
        }

        private void listBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as ListBox).SelectedItem = null;
        }

        private void removeDuplicateButton_Click(object sender, RoutedEventArgs e)
        {

            IEnumerable<image_class> noduplicates = list_update.image_details.Distinct().ToList();
            list_update.image_details.Clear();
            foreach (image_class obj in noduplicates)
            {
                list_update.image_details.Add(obj);
            }
            listBox.Items.Refresh();

        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = (comboBox1.SelectedValue.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last());
            if (value != " ")
            {
                if (value == "Name")
                {
                    list_update.image_details.Sort((a, b) => a.name.CompareTo(b.name));
                    listBox.Items.Refresh();
                    comboBox1.SelectedValue = " ";
                }
                if (value == "Size")
                {
                    list_update.image_details.Sort((a, b) => a.size.CompareTo(b.size));
                    listBox.Items.Refresh();
                    comboBox1.SelectedValue = " ";
                }
            }
        }

    }
}

public class list_update
{
    public static List<image_class> image_details = new List<image_class>();
}



public class image_class : IEquatable<image_class>
{
    public string location { get; set; }
    public string name { get; set; }
    public long size { get; set; }

    public bool Equals(image_class other)
    {

        // Check whether the compared object is null. 
        if (Object.ReferenceEquals(other, null)) return false;

        // Check whether the compared object references the same data. 
        if (Object.ReferenceEquals(this, other)) return true;

        // Check whether the products' properties are equal. 
        return name.Equals(other.name);
    }

    public override int GetHashCode()
    {

        // Get the hash code for the Name field if it is not null. 
        int hashProductName = name == null ? 0 : name.GetHashCode();

        // Calculate the hash code for the product. 
        return hashProductName;
    }

}