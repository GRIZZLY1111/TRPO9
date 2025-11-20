using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using System.IO;

namespace TRPO8.Pages
{
    /// <summary>
    /// Логика взаимодействия для Vhod.xaml
    /// </summary>
    public partial class Vhod : Page
    {
        public Doctor LoginDoctor { get; set; }
        public ObservableCollection<Doctor> Doctor { get; set; } = new();
        public Doctor? SelectedDoctor { get; set; }
        public Vhod()
        {
            LoginDoctor = new Doctor();
            DataContext = this;
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var id = LoginDoctor.ID;
            var passwordBox = LoginDoctor.Password;
            if (id == 0 || string.IsNullOrWhiteSpace(passwordBox))
            {
                MessageBox.Show("Введите ID и пароль!");
                return;
            }
            string filePath = $"D_{id}.txt";
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Пользователь с таким ID не найден!");
                return;
            }
            try
            {
                var json = File.ReadAllText(filePath);
                var doctor = JsonSerializer.Deserialize<Doctor>(json);
                if (doctor.Password != passwordBox)
                {
                    MessageBox.Show("Неверный пароль!");
                    return;
                }
                LoginDoctor.LastName = doctor.LastName;
                LoginDoctor.Name = doctor.Name;
                LoginDoctor.MiddleName = doctor.MiddleName;
                LoginDoctor.Specialisation = doctor.Specialisation;
                MessageBox.Show("Успешный вход!");
                NavigationService.Navigate(new MainScreen(LoginDoctor));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при входе: {ex.Message}");
            }
        }

        private void RegistrButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrDoctor());
        }
    }
}
