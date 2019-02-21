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
        public ObservableCollection<Ingredient> possibleIngs = new ObservableCollection<Ingredient>();
        public ObservableCollection<string> added = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
            Adding.ItemsSource = added;
            Ingredients.ItemsSource = possibleIngs;

            possibleIngs.Add(new Ingredient("Tomatoes", false));
            possibleIngs.Add(new Ingredient("Eggs", false));
            possibleIngs.Add(new Ingredient("Toast", false));
        }

        private void HandleCheckChanged(object sender, RoutedEventArgs e)
        {
            var check = (CheckBox)e.OriginalSource;
            var ing = (Ingredient)check.DataContext;

            if(check.IsChecked == true)
            {
                added.Add(ing.FoodName);
            }
            else
            {
                added.Remove(ing.FoodName);
            }
            
            
        }
    }

    public class Ingredient
    {
        public Ingredient()
        {
           
            FoodName = "not food";
            IsUsed = true;
        }
        public Ingredient(string name, bool use)
        {
            FoodName = name;
            IsUsed = use;
        }
        public string FoodName {get;set;}

        public bool IsUsed { get; set; }
    }
}

