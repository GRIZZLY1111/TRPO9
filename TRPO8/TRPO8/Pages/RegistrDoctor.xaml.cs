using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    /// Логика взаимодействия для RegistrDoctor.xaml
    /// </summary>
    public partial class RegistrDoctor : Page
    {
        public Doctor ReadDoctor { get; set; }
        public Doctor PasswordDoctor { get; set; }
        public RegistrDoctor()
        {
            ReadDoctor = new Doctor();
            PasswordDoctor = new Doctor();
            InitializeComponent();
            DataContext = this;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ReadDoctor.LastName) ||
                string.IsNullOrWhiteSpace(ReadDoctor.Name) ||
                string.IsNullOrWhiteSpace(ReadDoctor.MiddleName) ||
                string.IsNullOrWhiteSpace(ReadDoctor.Specialisation) ||
                string.IsNullOrWhiteSpace(ReadDoctor.Password) ||
                string.IsNullOrWhiteSpace(PasswordDoctor.Password))
            {
                MessageBox.Show("Все поля обязательны для заполнения!");
                return;
            }
            if (ReadDoctor.Password != PasswordDoctor.Password)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }
            ReadDoctor.ID = GenerateUniqueDoctorId();
            var jsonString = JsonSerializer.Serialize(ReadDoctor);
            File.WriteAllText($"D_{ReadDoctor.ID}.txt", jsonString);
            MessageBox.Show($"Регистрация успешна!\nВаш ID: {ReadDoctor.ID}");
            NavigationService.GoBack();
        }
        private int GenerateUniqueDoctorId()
        {
            Random rnd = new Random();
            int id;
            do
            {
                id = rnd.Next(10000, 99999);
            } while (File.Exists($"D_{id}.txt"));
            return id;
        }
    }
}
