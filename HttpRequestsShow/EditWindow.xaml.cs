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
using System.Windows.Shapes;

namespace HttpRequestsShow
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {

        public User user
        {
            get { return (User)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        public static User userStatic { get; set; }

        public static readonly DependencyProperty UserProperty =
            DependencyProperty.Register("User", typeof(User), typeof(EditWindow));



        public EditWindow(User user)
        {
            this.user = user;
            userStatic = user;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (user is Client client) { Task.Run(() => ClientAdd()); MessageBox.Show($"Добавлен новый клиент {(user as Client).Surname}"); }
            if (user is Realtor realtor) { Task.Run(() => RealtorAdd()); MessageBox.Show($"Добавлен новый риелтор {(user as Realtor).Surname}"); }

            Close();
        }

        private static async void RealtorAdd()
        {
            await RequestManager.Post<Realtor>($"Realtors/Add", userStatic as Realtor);
        }

        private static async void ClientAdd()
        {
            await RequestManager.Post<Client>($"Clients/Add", userStatic as Client);
        }
    }
}
