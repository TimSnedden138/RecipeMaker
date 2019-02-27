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
        public ObservableCollection<Recipe> ValidRecipes = new ObservableCollection<Recipe>();
        public ObservableCollection<string> addedRec = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();
            Adding.ItemsSource = added;
            Ingredients.ItemsSource = possibleIngs;
            Addingrec.ItemsSource = addedRec;
            possibleIngs.Add(new Ingredient("Tomatoes", false));
            possibleIngs.Add(new Ingredient("Eggs", false));
            possibleIngs.Add(new Ingredient("Toast", false));
            possibleIngs.Add(new Ingredient("Cheese", false));
            possibleIngs.Add(new Ingredient("Beef", false));
            possibleIngs.Add(new Ingredient("Chicken", false));
            possibleIngs.Add(new Ingredient("Green Onion", false));
            possibleIngs.Add(new Ingredient("Purple Onion", false));
            possibleIngs.Add(new Ingredient("Bell Pepper", false));
            possibleIngs.Add(new Ingredient("Red Pepper", false));
            possibleIngs.Add(new Ingredient("Green Pepper", false));
            possibleIngs.Add(new Ingredient("Butter", false));
            ValidRecipes.Add(new Recipe("SunnySideUpEggs", new List<Ingredient>() { new Ingredient("Eggs", false),new Ingredient("Butter",false)}));
            ValidRecipes.Add(new Recipe("Toasty", new List<Ingredient>() { new Ingredient("Cheese", false), new Ingredient("Toast", false) }));
        }

        private void HandleCheckChanged(object sender, RoutedEventArgs e)
        {
            var check = (CheckBox)e.OriginalSource;
            var ing = (Ingredient)check.DataContext;
            int checkedIngs = 0;
            if (check.IsChecked == true)
            {
                added.Add(ing.FoodName);
            }
            else
            {
                added.Remove(ing.FoodName);
            }
            for (int i = 0; i < ValidRecipes.Count; i++)
            {
                for (int r = 0; r < added.Count; r++)
                {
                    for (int t = 0; t < ValidRecipes[i].RequiredIngs.Count; t++)
                    {
                        if (ing.FoodName == ValidRecipes[i].RequiredIngs[t].FoodName)
                        {
                            checkedIngs++;
                            if (checkedIngs == ValidRecipes.Count)
                            {
                                addedRec.Add(ValidRecipes[i].RecipeName);
                            }
                            else
                            {
                                addedRec.Remove(ValidRecipes[i].RecipeName);
                            }
                        }
                    }
                }
            }
            //if (checkedIngs == ValidRecipes.Count)
            //{
            //    addedRec.Add(ValidRecipes[recipeCounter].RecipeName);
            //}

        }

        private void Adding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
        public string FoodName { get; set; }

        public bool IsUsed { get; set; }
    }
    public class Recipe
    {
        public Recipe()
        {
            RecipeName = "not recipe";
            RequiredIngs = new List<Ingredient>();

        }
        public Recipe(string name, List<Ingredient> requiredIngs)
        {
            RecipeName = name;
            RequiredIngs = requiredIngs;
        }
        public string RecipeName { get; set; }
        public List<Ingredient> RequiredIngs { get; set; }

    }
}