using Azure;
using BabyShop.Model;
using BabyShop.Model.Entity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
using System.Windows.Shapes;

namespace BabyShop.View
{
    /// <summary>
    /// Логика взаимодействия для AddToysWindow.xaml
    /// </summary>
    public partial class AddToysWindow : Window
    {
        public AddToysWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BimBomWindow bimBomWindow = new BimBomWindow();
            bimBomWindow.Show();
            this.Close();
        }

        private void saveBut_Click(object sender, RoutedEventArgs e)
        {


            if (string.IsNullOrEmpty(nameText.Text) || string.IsNullOrEmpty(priceText.Text) || string.IsNullOrEmpty(descText.Text) && Image.Source.ToString() == null)
            {
                MessageBox.Show("Вы ввели некорректные данные, либо вообще ничего не ввели");
            }
            else
            {
                Tovar save = null;
                using (var context = new BabyShopDBEntities())
                {
                    save = new Tovar();
                    save.Name = nameText.Text;
                    save.Price = decimal.Parse(priceText.Text);
                    save.Descriptions = descText.Text;
                    save.IsDeleted = false;
                    save.Image = ClassImage.BitmapSourceToByteArray((BitmapSource)Image.Source);
                    context.Tovar.Add(save);
                    context.SaveChanges();
                
                }
                using (FileStream fileStream = new FileStream(Directory.GetCurrentDirectory() + "\\Image\\" + save.Id + ".png", FileMode.Create))
                {

                    using (var context = new BabyShopDBEntities())
                    {
                        Tovar imya = context.Tovar.Where(x => x.Id == save.Id).FirstOrDefault();
                        imya.Image = ClassImage.BitmapSourceToByteArray((BitmapSource)Image.Source);

                   

                            context.SaveChanges();

                        
                    }
                }
                BimBomWindow bimBomWindow = new BimBomWindow();
                bimBomWindow.Show();
                this.Close();
                MessageBox.Show("Вы добавили новый товар!");
            }
        }




        private void imageBut_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog avatar = new OpenFileDialog();
            avatar.Title = "Выберете картинку";
            avatar.Filter = "Image Files (*.bmp;*.png;(.jpg)|*.bmp;*.png;*.jpg";
            if (avatar.ShowDialog() == true)
            {
                BitmapImage image = new BitmapImage(new Uri(avatar.FileName));
                Image.Source = image;
            }
        }

        private void priceText_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.A && e.Key <= Key.Z))
            {
                e.Handled = true;
            }
        }

        private void grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
