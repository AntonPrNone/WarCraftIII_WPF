﻿using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WarCraftIII_Logic;

namespace WarCraftIII_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DistributionOfPointsWindow : Window
    {
        private Unit unit;
        private Unit[] units;
        private Unit warrior = MongoExamples.Find("Warrior");
        private Unit rogue = MongoExamples.Find("Rogue");
        private Unit wizard = MongoExamples.Find("Wizard");

        private bool mouseScrolled = false;

        public DistributionOfPointsWindow()
        {
            InitializeComponent();
            units = new Unit[] { warrior, rogue, wizard };
            unit = warrior;
            Application.Current.Resources["DefoultColor"] = Application.Current.Resources["WarriorColor"];
            ComboBoxUnits.SelectedIndex = 0;

            UpdateData();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {

        }

        private void UpdateData() // Updating the display of data
        {
            SkillPointsTextBox.Text = unit.SkillPoints.ToString();

            MaxHPLabel.Content = unit.MaxHP;
            MaxMPLabel.Content = unit.MaxMP;
            PAttackLabel.Content = unit.PAttack;
            MAttackLabel.Content = unit.MAttack;
            PDefLabel.Content = unit.PDef;

            StrengthTextBox.Text = unit.Strength[1].ToString();
            DexterityTextBox.Text = unit.Dexterity[1].ToString();
            ConstitutionTextBox.Text = unit.Constitution[1].ToString();
            IntelligenceTextBox.Text = unit.Intelligence[1].ToString();

            ProgressEx.Value = unit.Exp;
            ProgressEx.Maximum = unit.MaxEx;
            Ex_Label.Content = unit.Exp + "/" + unit.MaxEx;
            Lvl_Label.Content = unit.Lvl + " LVL";
        }

        private bool CheckingLimit(Unit unit, int[] characteristic, Button buttonplus, Button buttonminus) // Checking the achievement of the limit of skill points and levels of characteristics
        {
            if (unit.SkillPoints == 0)
            {
                MessageBox.Show("Skill points are over", "The limit has been reached",
         MessageBoxButton.OK, MessageBoxImage.Information);
            }

            else if (characteristic[1] >= characteristic[2])
            {
                MessageBox.Show("The maximum characteristic level has been reached", "The limit has been reached",
         MessageBoxButton.OK, MessageBoxImage.Information);
                buttonplus.Foreground = System.Windows.Media.Brushes.Black;
            }

            else if (characteristic[0] >= characteristic[1])
            {
                MessageBox.Show("The minimum characteristic level has been reached", "The limit has been reached",
         MessageBoxButton.OK, MessageBoxImage.Information);
                buttonminus.Foreground = System.Windows.Media.Brushes.Black;
            }

            else
            {
                buttonplus.Foreground = System.Windows.Media.Brushes.White;
                buttonminus.Foreground = System.Windows.Media.Brushes.White;
                return false;
            }

            return true;
        }

        // -------------------------------------------- MANAGING CHARACTERISTICS -------------------------------------------

        private void AddStrength_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementStrengthWarrior('+');
            if (!CheckingLimit(unit, unit.Strength, AddStrength_Button, ReduceStrength_Button)) unit.Exp++;
            UpdateData();
        }

        private void AddDexterity_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementDexterityWarrior('+');
            if (!CheckingLimit(unit, unit.Dexterity, AddDexterity_Button, ReduceDexterity_Button)) unit.Exp++;
            UpdateData();
        }

        private void AddConstitution_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementConstitutionWarrior('+');
            if (!CheckingLimit(unit, unit.Constitution, AddConstitution_Button, ReduceConstitution_Button)) unit.Exp++;
            UpdateData();
        }

        private void AddIntelligence_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementIntelligenceWarrior('+');
            if (!CheckingLimit(unit, unit.Intelligence, AddIntelligence_Button, ReduceIntelligence_Button)) unit.Exp++;
            UpdateData();
        }

        private void ReduceStrength_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementStrengthWarrior('-');
            if (!CheckingLimit(unit, unit.Strength, AddStrength_Button, ReduceStrength_Button)) unit.Exp--;
            UpdateData();
        }

        private void ReduceDexterity_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementDexterityWarrior('-');
            if (!CheckingLimit(unit, unit.Dexterity, AddDexterity_Button, ReduceDexterity_Button)) unit.Exp--;
            UpdateData();
        }

        private void ReduceConstitution_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementConstitutionWarrior('-');
            if (!CheckingLimit(unit, unit.Constitution, AddConstitution_Button, ReduceConstitution_Button)) unit.Exp--;
            UpdateData();
        }

        private void ReduceIntelligence_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementIntelligenceWarrior('-');
            if (!CheckingLimit(unit, unit.Intelligence, AddIntelligence_Button, ReduceIntelligence_Button)) unit.Exp--;
            UpdateData();
        }

        // ----------------------------------------------- СhangingСharacters ----------------------------------------------

        private void SwitchingUnits(Unit unit) // Changing a character
        {
            MongoExamples.SaveValues(this.unit.Name, this.unit);
            this.unit = unit;

            if (unit.Name == "Warrior")
            {
                UnitImg.Source = new BitmapImage(new Uri("/img/Warrior.png", UriKind.Relative));
                Application.Current.Resources["DefoultColor"] = Application.Current.Resources["WarriorColor"];
            }

            if (unit.Name == "Rogue")
            {
                UnitImg.Source = new BitmapImage(new Uri("/img/Rogue.png", UriKind.Relative));
                Application.Current.Resources["DefoultColor"] = Application.Current.Resources["RogueColor"];
            }

            if (unit.Name == "Wizard")
            {
                UnitImg.Source = new BitmapImage(new Uri("/img/Wizard.png", UriKind.Relative));
                Application.Current.Resources["DefoultColor"] = Application.Current.Resources["WizardColor"];
            }

            UpdateData();
            ResetColorButtons();
        }

        private void ComboBoxUnits_SelectionChanged(object sender, SelectionChangedEventArgs e) // Changing a character with ComboBox
        {
            if (!mouseScrolled)
            {
                if (ComboBoxUnits.SelectedIndex == 0 && unit.Name != warrior.Name)
                {
                    SwitchingUnits(warrior);
                }

                if (ComboBoxUnits.SelectedIndex == 1 && unit.Name != rogue.Name)
                {
                    SwitchingUnits(rogue);
                }

                else if (ComboBoxUnits.SelectedIndex == 2 && unit.Name != wizard.Name)
                {
                    SwitchingUnits(wizard);
                }
            }
        }

        private void UnitImg_MouseWheel(object sender, MouseWheelEventArgs e) // Changing a character with Scroll
        {
            if (e.Delta > 0)
            {
                if (Array.IndexOf(units, unit) == units.Length - 1)
                {
                    mouseScrolled = true;
                    ComboBoxUnits.SelectedIndex = 0;
                    mouseScrolled = false;

                    SwitchingUnits(units[0]);
                }

                else
                {
                    mouseScrolled = true;
                    ComboBoxUnits.SelectedIndex = Array.IndexOf(units, unit) + 1;
                    mouseScrolled = false;

                    SwitchingUnits(units[Array.IndexOf(units, unit) + 1]);
                }
            }

            else if (e.Delta < 0)
            {
                if (Array.IndexOf(units, unit) == 0)
                {
                    mouseScrolled = true;
                    ComboBoxUnits.SelectedIndex = units.Length - 1;
                    mouseScrolled = false;

                    SwitchingUnits(units[^1]);
                }

                else
                {
                    mouseScrolled = true;
                    ComboBoxUnits.SelectedIndex = Array.IndexOf(units, unit) - 1;
                    mouseScrolled = false;

                    SwitchingUnits(units[Array.IndexOf(units, unit) - 1]);
                }
            }
        }

        // ------------------------------------------------------ Anim -----------------------------------------------------

        private void ButtonBack_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            new StartWindow().Show();
            Close();
        }

        private void ButtonBack_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonBack.Source = new BitmapImage(new Uri("/img/iconBack2.png", UriKind.Relative));
            Anim.AnimElementSize_MouseEnter((Image)sender);
        }

        private void ButtonBack_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonBack.Source = new BitmapImage(new Uri("/img/iconBack.png", UriKind.Relative));
            Anim.AnimElementSize_MouseLeave((Image)sender);
        }

        private void ProgressEx_MouseEnter(object sender, MouseEventArgs e)
        {
            ProgressEx.Foreground = System.Windows.Media.Brushes.White;
            ProgressEx.Background = (System.Windows.Media.Brush)Application.Current.Resources["ProgressBarColorGradient"];
            ExPlus_Label.Content = "+" + (unit.MaxEx - unit.Exp);
            ExPlus_Label.Visibility = Visibility.Visible;
        }

        private void ProgressEx_MouseLeave(object sender, MouseEventArgs e)
        {
            ProgressEx.Foreground = (System.Windows.Media.Brush)Application.Current.Resources["ProgressBarColorGradient"];
            ProgressEx.Background = System.Windows.Media.Brushes.White;
            ExPlus_Label.Visibility = Visibility.Hidden;
        }

        private void AnimButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Anim.AnimElementSize_MouseEnter((Button)sender);
        }

        private void AnimElement_MouseLeave(object sender, MouseEventArgs e)
        {
            Anim.AnimElementSize_MouseLeave((Button)sender);
        }

        // -----------------------------------------------------------------------------------------------------------------

        private void ResetButton_Click(object sender, RoutedEventArgs e) // Reset characteristics
        {
            MongoExamples.ResetValues(unit.Name);
            var inv = unit.Inventory;
            var body = unit.Body;
            unit = MongoExamples.Find(unit.Name);
            unit.EditInventory(inv);
            unit.EditBody(body);
            unit.RecoverBody();

            for (int un = 0; un < units.Length; un++)
            {
                if (units[un].Name == unit.Name) units[un] = unit;
            }

            UpdateData();
            ResetColorButtons();
        }

        private void ResetColorButtons() // Reset button colors
        {
            AddConstitution_Button.Foreground = System.Windows.Media.Brushes.White;
            AddDexterity_Button.Foreground = System.Windows.Media.Brushes.White;
            AddStrength_Button.Foreground = System.Windows.Media.Brushes.White;
            AddIntelligence_Button.Foreground = System.Windows.Media.Brushes.White;
            ReduceConstitution_Button.Foreground = System.Windows.Media.Brushes.White;
            ReduceDexterity_Button.Foreground = System.Windows.Media.Brushes.White;
            ReduceStrength_Button.Foreground = System.Windows.Media.Brushes.White;
            ReduceIntelligence_Button.Foreground = System.Windows.Media.Brushes.White;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MongoExamples.SaveValues(unit.Name, unit);
        }

        private void UpdateData0(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }
    }
}
