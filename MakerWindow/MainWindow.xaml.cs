using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using Newtonsoft.Json;
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
        public ObservableCollection<RecipeSteps> Steps = new ObservableCollection<RecipeSteps>();
        public ObservableCollection<string> addedRecsteps = new ObservableCollection<string>();
        public MainWindow()
        {

            InitializeComponent();
            Recipes outRec = JsonConvert.DeserializeObject<Recipes>(File.ReadAllText(@"C:\Users\s189066\source\repos\RecipeMaker\MakerWindow\rec.json"));
            Ingredients outIng = JsonConvert.DeserializeObject<Ingredients>(File.ReadAllText(@"C:\Users\s189066\source\repos\RecipeMaker\MakerWindow\ing.json"));
            Adding.ItemsSource = added;
            Ingredients.ItemsSource = possibleIngs;
            Addingrec.ItemsSource = addedRec;
            RecipeSteps.ItemsSource = Steps;
            for (int i = 0; i < outIng.Ings.Count(); i++)
            {
                possibleIngs.Add(new Ingredient(outIng.Ings[i].FoodName, outIng.Ings[i].IsUsed));
            }
            for (int r = 0; r < outRec.Recs.Count(); r++)
            {
                ValidRecipes.Add(new Recipe(outRec.Recs[r].RecipeName, outRec.Recs[r].RequiredIngs));
            }
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
        }
        private void Adding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Steps.Add(new RecipeSteps("NAMES\nTesting"));
        }
    }


    public class RecipeSteps
    {
        public RecipeSteps()
        {

            Stepstomake = "not food";
        }
        public RecipeSteps(string _Stepstomake)
        {
            Stepstomake = _Stepstomake;
        }
        public string Stepstomake { get; set; }
    }

    public class Ingredient
    {
        public Ingredient()
        {

            FoodName = "not food";
            IsUsed = false;
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
    public class Ingredients
    {
        public Ingredient[] Ings { get; set; }
    }

    public class Recipes
    {
        public Recipe[] Recs { get; set; }
    }
}
