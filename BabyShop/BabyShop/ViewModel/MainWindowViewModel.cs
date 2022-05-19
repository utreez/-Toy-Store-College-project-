using BabyShop.Model;
using BabyShop.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BabyShop.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _password;
        private string _login;
        private RelayCommand _loginCommand;
        List<User> Users = new List<User>();
        public string Password
        {
            get => _password;
            set => SetPropertyChanged(ref _password, value, nameof(_password));
        }
        public string Login
        {
            get => _login;
            set => SetPropertyChanged(ref _login, value, nameof(_login));
        }

        public RelayCommand LoginCommand
        {
            get { return _loginCommand ?? (_loginCommand = new RelayCommand(obj => { enterbtn(); })); }
        }

        public MainWindowViewModel()
        {
            User authorization = null;
            using (var context = new BabyShopDBEntities())
            {
                foreach (User user in Users)
                {
                    authorization = context.User.Where(x => x.FullName == user.FullName).FirstOrDefault();

                    if (authorization == null)
                    {
                        User imya = user;
                        context.User.Add(imya);
                        context.SaveChanges();
                    }
                }
            }
        }

            public void enterbtn()
        {
         
                using (var context = new BabyShopDBEntities())
                {
                    var user = context.User.FirstOrDefault(e => e.Login == Login && e.Password == Password);

                    if (user != null)
                    {
                        CurrentUser.Name = user.FullName;
                        View.BimBomWindow contentWindow = new View.BimBomWindow();
                        contentWindow.Show();
                        Application.Current.MainWindow.Hide();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Данные введенны неверно. Повторите попытку.");
                    }
                    
        }
               
            
        
        }
    }
}
