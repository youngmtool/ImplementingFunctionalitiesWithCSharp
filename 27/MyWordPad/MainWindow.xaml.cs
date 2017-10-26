﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

//c Add a WPF project MyWordPad for testing nested panels to build a window's frame.

//c Add a menu system by Menu, MenuItem element inside of DockPanel in xaml file, with setting some events.

//c Add a ToolBar system which can be alternatively used for a menu system.

//c Add a StatusBar in xaml file.

//c Add codes for spell check UI in xaml file by using Grid, GridSplitter, Expander, TextBox..

//c Add codes implementing MouseEnter, MouseLeave event's event handlers in xaml.cs file.

//c Add codes implementing Spell checking logic in xaml.cs file.

//c Add new menu item by using functonality called intrinsic command object, this time, it implements Copy, Cut, Paste functionalities which can be used by Edit menu, key functionality like Ctrl+C, funtionality by context menu.

//c Add codes connnecting commands to action SetF1CommandBinding() by using CommandBinding object.

namespace MyWordPad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetF1CommandBinding();
        }

        protected void FileExit_Click(object sender, RoutedEventArgs args)
        {
            // Close this window.
            this.Close();
        }
        protected void MouseEnterExitArea(object sender, RoutedEventArgs args)
        {
            statBarText.Text = "Exit the Application";
        }
        protected void MouseEnterToolsHintsArea(object sender, RoutedEventArgs args)
        {
            statBarText.Text = "Show Spelling Suggestions";
        }
        protected void MouseLeaveArea(object sender, RoutedEventArgs args)
        {
            statBarText.Text = "Ready";
        }

        protected void ToolsSpellingHints_Click(object sender, RoutedEventArgs args)
        {
            string spellingHints = string.Empty;

            // Try to get a spelling error at the current caret location.
            SpellingError error = txtData.GetSpellingError(txtData.CaretIndex);
            if (error != null)
            {
                // Build a string of spelling suggestions.
                foreach (string s in error.Suggestions)
                {
                    spellingHints += $"{s}\n";
                }

                // Show suggestions and expand the expander.
                lblSpellingHints.Content = spellingHints;
                expanderSpelling.IsExpanded = true;
            }
        }

        private void SetF1CommandBinding()
        {
            CommandBinding helpBinding = new CommandBinding(ApplicationCommands.Help);
            helpBinding.CanExecute += CanHelpExecute;
            helpBinding.Executed += HelpExecuted;
            CommandBindings.Add(helpBinding);
        }

        private void CanHelpExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Here, you can set CanExecute to false if you want to prevent the
            // command from executing.
            e.CanExecute = true;
        }

        private void HelpExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Look, it is not that difficult. Just type something!",
                            "Help!");
        }

        private void OpenCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SaveCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // Create an open file dialog box and only show XAML files.
            var openDlg = new OpenFileDialog { Filter = "Text Files |*.txt" };

            // Did they click on the OK button?
            if (true == openDlg.ShowDialog())
            {
                // Load all text of selected file.
                string dataFromFile = File.ReadAllText(openDlg.FileName);
                // Show string in TextBox.
                txtData.Text = dataFromFile;
            }
        }

        private void SaveCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var saveDlg = new SaveFileDialog { Filter = "Text Files |*.txt" };

            // Did they click on the OK button?
            if (true == saveDlg.ShowDialog())
            {
                // Save data in the TextBox to the named file.
                File.WriteAllText(saveDlg.FileName, txtData.Text);
            }
        }
    }
}
