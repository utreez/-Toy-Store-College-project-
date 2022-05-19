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

namespace BabyShop
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

        private void mainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }

        private void RegistrationButton_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            pwdTextBox.Text = PasswordBox.Password;
        }
        int but = 0;
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (but == 0)
            {
                pwdTextBox.Visibility = Visibility.Visible;
                PasswordBox.Visibility = Visibility.Hidden;
                but++;
                return;
            }
            else
            if (but == 1)
            {
                pwdTextBox.Visibility = Visibility.Hidden;
                PasswordBox.Visibility = Visibility.Visible;
                but--;
            }
        }

        private void ExitImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Если вы не помните логин или пароль, для восстановления вам потребуется обратиться к директору магазина.");
        }
    }
}