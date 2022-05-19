using BabyShop.Model;
using BabyShop.Model.Entity;
using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace BabyShop.View
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        Tovar tovar = null;
        public EditWindow(Tovar tovar)
        {
            InitializeComponent();
            this.tovar = tovar;
            nameText.Text = tovar.Name;
            priceText.Text = tovar.Price.ToString();
            descText.Text = tovar.Descriptions;
            Image.Source = ClassImage.LoadImage(tovar.Image);
        }

        private void DeleteBut_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new BabyShopDBEntities())
            {
                try
                {
                    if (tovar != null)
                    {
                  
                        Organization delete = context.Organization.Where(x => x.TovarId == tovar.Id).FirstOrDefault();
                        Sales sales = context.Sales.Where(x => x.TovarId == tovar.Id).FirstOrDefault();
                  
                       if (delete != null && sales !=null)
                        {
                            context.Organization.Remove(delete);
                            context.SaveChanges();
                        }
                        Tovar game = context.Tovar.Where(x => x.Id == tovar.Id).FirstOrDefault();
                        context.Tovar.Remove(game);
                        context.SaveChanges();
                        this.Close();
                        BimBomWindow bimBomWindow = new BimBomWindow();
                        bimBomWindow.Show();
                        MessageBox.Show("Данные успешно удалены!");

                    }
                }
                catch { MessageBox.Show("Произошла ошибка, повторите запрос ещё раз!"); }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BimBomWindow bimBomWindow = new BimBomWindow();
            bimBomWindow.Show();
            this.Hide();

        }



        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.A && e.Key <= Key.Z))
            {
                e.Handled = true;
            }
        }

        private void saveBut_Click(object sender, RoutedEventArgs e)
        {
            Tovar save = null;
            using (var context = new BabyShopDBEntities())
            {
                save = context.Tovar.Where(x => x.Id == tovar.Id).FirstOrDefault();
                if (save == null || string.IsNullOrEmpty(nameText.Text) || string.IsNullOrEmpty(priceText.Text) || string.IsNullOrEmpty(descText.Text))
                {
                    MessageBox.Show("Вы ввели некорректные данные, либо вообще ничего не ввели");
                }
                else
                {
                    save.Name = nameText.Text;
                    save.Price = decimal.Parse(priceText.Text);

                    save.Descriptions = descText.Text;
                    try
                    {
                        using (FileStream fileStream = new FileStream(Directory.GetCurrentDirectory() + "\\Image\\" + save.Id + ".png", FileMode.Create))
                        {
                            BitmapFrame frame = BitmapFrame.Create((BitmapSource)Image.Source);
                            BitmapEncoder encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(frame);
                            encoder.Save(fileStream);
                        }
                    }
                    catch { Exception n; }
                    save.Image = ClassImage.BitmapSourceToByteArray((BitmapSource)Image.Source);
                    context.SaveChanges();
                    MessageBox.Show("Данные успешно обновлены!");
                    BimBomWindow bimBomWindow = new BimBomWindow();
                    bimBomWindow.Show();
                    this.Close();
                }
            }

        }

        private void grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { MessageBox.Show("Произошла ошибка, повторите попытку"); }
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
    }
}