using BabyShop.Model;
using BabyShop.Model.Entity;
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
using System.Windows.Shapes;

namespace BabyShop.View
{
    /// <summary>
    /// Логика взаимодействия для BimBomWindow.xaml
    /// </summary>
    public partial class BimBomWindow : Window
    {
        public BimBomWindow()
        {
            InitializeComponent();
            nameText.Text = CurrentUser.Name;

            using (var context = new BabyShopDBEntities())
            {
                tovar = new ObservableCollection<Tovar>(context.Tovar.ToList());
            }
            ListView.ItemsSource = Tovar;
        }


        private void Exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        public ObservableCollection<Tovar> tovar { get; set; } = new ObservableCollection<Tovar>();
        public ObservableCollection<Tovar> Tovar { get { return tovar; } }

        private void editBut_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = ListView.Items.IndexOf(item);

            EditWindow editWindow = new EditWindow(Tovar[index]);

            this.Close();
            editWindow.Show();

        }

        private void addBut_Click(object sender, RoutedEventArgs e)
        {
            AddToysWindow addToysWindow = new AddToysWindow();

            this.Hide();
            addToysWindow.Show();
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new BabyShopDBEntities())
            {
                tovar = new ObservableCollection<Tovar>(context.Tovar.Where(exp => exp.Name == Search.Text).ToList());
                ListView.ItemsSource = Tovar;
            }
        }
    }
}
