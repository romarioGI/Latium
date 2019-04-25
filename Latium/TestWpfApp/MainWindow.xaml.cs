using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using PropositionalCalculusLibrary;
using PropositionLibrary;
using InferenceFinderBruteForceLibrary;
using InferenceFinderBruteForce2Library;
using PropositionalCalculusTextBox;
using PropositionalCalculusInferenceFinder;
using InferenceFinder = PropositionalCalculusInferenceFinder.InferenceFinder;

namespace TestWpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PushButton_Click(object sender, RoutedEventArgs e)
        {
            var str = InputFormulaTextBox.GetTextWithoutSubs();
            if (Formula.IsFormula(str) == false)
            {
                MessageBox.Show("это не формула");
                return;
            }

            var formula = new Formula(str);

            var B = new Formula("(A→(¬(¬A)))");
            var C = new Formula("(A→A)");
            var L = new List<Formula>();
            //L.Add(B);
            L.Add(C);        

            //var hyp = new Hypotheses(L);
            Hypotheses hyp = null;

            //var inference = (new InferenceFinderBruteForce(formula, hyp)).BuildInference();
            //var inference = InferenceFinder.FindInference(formula, hyp);
            var inference = InferenceFinder.FindInference(hyp, formula);

            InferenceTextBox.Text = inference.ToString();
        }
    }
}
