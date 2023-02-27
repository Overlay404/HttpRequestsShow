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

namespace HttpRequestsShow
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public List<User> Users
        {
            get { return (List<User>)GetValue(ClientsProperty); }
            set { SetValue(ClientsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Clients.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClientsProperty =
            DependencyProperty.Register("Users", typeof(List<User>), typeof(MainWindow));


        public MainWindow()
        {
            InitializeComponent();
        }

        async void RequestGetUsers()
        {
            var clients = await RequestManager.Get<List<Client>>("Users/GetAllUsers");
            Users = clients.Cast<User>().ToList();
            
        }

        async void RequestGetRealtors()
        {
            var realtors = await RequestManager.Get<List<Realtor>>("Realtors/GetAllRealtors");
            Users = realtors.Cast<User>().ToList();
        }

        async void RequestGetUser()
        {
            var name = SearchUser.Text.Trim();
            var realtor = await RequestManager.Get<List<Realtor>>($"Realtors/{name}");
            

            var client = await RequestManager.Get<List<Realtor>>($"Users/{name}");

            if (realtor.Count != 0)
                DataGridShowDatainHTTPRequest.ItemsSource = realtor;
            else if (client.Count != 0)
                DataGridShowDatainHTTPRequest.ItemsSource = client;
            else
                SearchUser.Text = "Нет такого пользователя";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RequestGetUsers();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RequestGetRealtors();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            RequestGetUser();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (DataGridShowDatainHTTPRequest.SelectedItem == null) return;
            new EditWindow((User)DataGridShowDatainHTTPRequest.SelectedItem).ShowDialog();
            Hide();
        }
    }


    public class Client : User
    {
        public string Phone { get; set; }
        public string Email { get; set; }

        public Client(int Id, string Name, string Surname, string Patronymic, string Phone, string Email) : base(Id, Name, Surname, Patronymic) 
        {
            this.Phone = Phone;
            this.Email = Email;
        }
    }

    public partial class Realtor : User
    {
        public Nullable<double> DealShare { get; set; }

        public Realtor(int Id, string Name, string Surname, string Patronymic, Nullable<double> DealShare) : base(Id, Name, Surname, Patronymic) 
        {
            this.DealShare = DealShare;
        }
    }

    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public User(int Id, string Name, string Surname, string Patronymic)
        {
            this.Id = Id;
            this.Name = Name;
            this.Surname = Surname;
            this.Patronymic = Patronymic;
        }
    }
}
