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
using Microsoft.Win32;
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
        public ObservableCollection<Recipe> addedRec = new ObservableCollection<Recipe>();
        public ObservableCollection<RecipeSteps> Steps = new ObservableCollection<RecipeSteps>();
        public ObservableCollection<string> addedRecsteps = new ObservableCollection<string>();
        public Recipes outRec;
        public Ingredients outIng;
        void LoadtheFile()
        {
            OpenFileDialog filepathRec = new OpenFileDialog();
            filepathRec.Title = "Open Rec.json";
            if (filepathRec.ShowDialog() == true)
            {
                 outRec = JsonConvert.DeserializeObject<Recipes>(File.ReadAllText(filepathRec.FileName));
            }
            else
            {
                return;
            }
            OpenFileDialog filepathIng = new OpenFileDialog();
            filepathIng.Title = "Open Ing.json";
            if (filepathIng.ShowDialog() == true)
            {
                outIng = JsonConvert.DeserializeObject<Ingredients>(File.ReadAllText(filepathIng.FileName));
            }
            else
            {
                return;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            LoadtheFile();
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
                ValidRecipes.Add(new Recipe(outRec.Recs[r].RecipeName, outRec.Recs[r].RequiredIngs, outRec.Recs[r].ListofSteps));
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
                            if (checkedIngs == ValidRecipes[i].RequiredIngs.Count)
                            {
                                addedRec.Add(ValidRecipes[i]);
                            }
                            else if (checkedIngs != ValidRecipes[i].RequiredIngs.Count)
                            {
                                addedRec.Remove(ValidRecipes[i]);
                            }
                        }
                    }
                }
            }
        }
        private void Adding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox recipeBox = sender as ListBox;
            Recipe selectedRecipe = recipeBox.SelectedItem as Recipe;
            OpenFileDialog Search = new OpenFileDialog();
            // exit early if nothing is selected
            if (selectedRecipe == null) { return; }

            // if you have a variable referring to the selected recipe (called selectedRecipe), how can
            // you use it to populate the "Steps for Selected Recipe" listbox?
            if (selectedRecipe != null)
            {
                Steps.Clear();
                for (int i = 0; i < selectedRecipe.ListofSteps.Count; i++)
                {
                    Steps.Add(new RecipeSteps(selectedRecipe.ListofSteps[i]));
                }
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text File|*.txt";
                saveFileDialog.Title = "Save the Steps if you want to";
                saveFileDialog.ShowDialog();
                if (saveFileDialog.FileName == "")
                {
                    return;
                }
                if (saveFileDialog.FileName != "")
                {
                    string name = saveFileDialog.FileName;
                    File.WriteAllLines(name, selectedRecipe.ListofSteps);
                }
            }
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
            ListofSteps = new List<string>();

        }
        public Recipe(string name, List<Ingredient> requiredIngs, List<string> _Steps)
        {
            RecipeName = name;
            RequiredIngs = requiredIngs;
            ListofSteps = _Steps;
        }
        public string RecipeName { get; set; }
        public List<Ingredient> RequiredIngs { get; set; }
        public List<string> ListofSteps { get; set; }
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
