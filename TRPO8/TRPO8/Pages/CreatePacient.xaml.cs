using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace TRPO8.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreatePacient.xaml
    /// </summary>
    public partial class CreatePacient : Page
    {
        private ObservableCollection<Pacient> _pacientList;
        public Pacient AddPacient { get; set; }
        public CreatePacient(ObservableCollection<Pacient> PacientList)
        {
            _pacientList = PacientList;
            AddPacient = new Pacient();
            DataContext = this;
            InitializeComponent();
        }
        private void AddPacientButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AddPacient.LastName) ||
                string.IsNullOrWhiteSpace(AddPacient.Name) ||
                string.IsNullOrWhiteSpace(AddPacient.MiddleName) ||
                string.IsNullOrWhiteSpace(AddPacient.PhoneNumber) )
            {
                MessageBox.Show("Все поля обязательны для заполнения!");
                return;
            }
            string phone = AddPacient.PhoneNumber.Trim();
            if (phone.StartsWith("+7"))
            {
                if (phone.Length != 12)
                {
                    MessageBox.Show("Номер должен содержать всего 11 цифр");
                    return;
                }
                for (int i = 2; i < phone.Length; i++)
                {
                    if (!char.IsDigit(phone[i]))
                    {
                        MessageBox.Show("После '+7' должны идти только цифры");
                        return;
                    }
                }
            }
            else if (phone.StartsWith("8"))
            {
                if (phone.Length != 11)
                {
                    MessageBox.Show("Номер с '8' должен содержать 11 цифр");
                    return;
                }
                for (int i = 1; i < phone.Length; i++)
                {
                    if (!char.IsDigit(phone[i]))
                    {
                        MessageBox.Show("В номере должны быть только цифры");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Номер должен начинаться с '+7' или '8'.");
                return;
            }
            AddPacient.ID = GenerateUniquePacientId();
            var jsonString = JsonSerializer.Serialize(AddPacient);
            File.WriteAllText($"P_{AddPacient.ID}.txt", jsonString);
            _pacientList.Add(AddPacient);
            MessageBox.Show($"Добавление успешно завершено!\nID пациента: {AddPacient.ID}");
        }
        private int GenerateUniquePacientId()
        {
            Random rnd = new Random();
            int id;
            do
            {
                id = rnd.Next(1000000, 9999999);
            } while (File.Exists($"P_{id}.txt"));
            return id;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
