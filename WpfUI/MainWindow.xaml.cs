using System;
using System.Collections.Generic;
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

namespace WpfUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ButtonControl buttonControl;
        private bool buttonIsPressed;
        public MainWindow()
        {
            InitializeComponent();
            buttonIsPressed = false;
            buttonStartRootines.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (buttonIsPressed)
            {
                buttonControl.StopPress();
                buttonControl = null;
                buttonStartRootines.Content = "start";
                buttonIsPressed = false;
            }
            else
            {
                if (textBox.Text == "")
                    return;
                buttonControl = new ButtonControl(/*string.Concat('{', textBox.Text, '}')*/textBox.Text);
                buttonControl.Press();
                buttonStartRootines.Content = "stop";
                buttonIsPressed = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            buttonStartRootines.IsEnabled = textBox.Text == "" ? false : true;
            //if (textBox.Text == "")
            //    buttonStartRootines.IsEnabled = false;
            //else
            //    buttonStartRootines.IsEnabled = true;
        }

        

        private void TextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            textBox.Text = "";
        }

        
    }
}
