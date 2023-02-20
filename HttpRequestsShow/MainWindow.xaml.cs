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
        public MainWindow()
        {
            InitializeComponent();

            
        }

        async void RequestGetUsers()
        {
            var clients = await RequestManager.Get<List<Client>>("Users/GetAllUsers");
            DataGridShowDatainHTTPRequest.ItemsSource = clients;
        }

        async void RequestGetRealtors()
        {
            var realtors = await RequestManager.Get<List<Realtor>>("Realtors/GetAllRealtors");
            DataGridShowDatainHTTPRequest.ItemsSource = realtors;
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
    }


    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public partial class Realtor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public Nullable<double> DealShare { get; set; }
    }
}
