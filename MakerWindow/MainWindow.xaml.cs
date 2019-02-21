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

namespace MakerWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> added = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
            Adding.ItemsSource = added;
        }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            var check = sender as CheckBox;
            added.Add(check.Name);
        }

        private void HandleUnchecked(object sender, RoutedEventArgs e)
        {
            var check = sender as CheckBox;
            added.Remove(check.Name);
        }
    }

    public class Ingredient
    {
        public string FoodName {get;set;}

        public bool IsUsed { get; set; }
    }
}

