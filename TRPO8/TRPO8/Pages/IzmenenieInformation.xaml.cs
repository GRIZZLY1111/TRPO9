using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для IzmenenieInformation.xaml
    /// </summary>
    public partial class IzmenenieInformation : Page
    {
        public Pacient IzmeneniePacient { get; set; }
        public IzmenenieInformation(Pacient? pacient = null)
        {
            IzmeneniePacient = pacient;
            DataContext = this;
            InitializeComponent();
        }
        private void IzmeneniePacientButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IzmeneniePacient.LastName) ||
                string.IsNullOrWhiteSpace(IzmeneniePacient.Name) ||
                string.IsNullOrWhiteSpace(IzmeneniePacient.MiddleName)||
                string.IsNullOrWhiteSpace(IzmeneniePacient.PhoneNumber))
            {
                MessageBox.Show("Все поля обязательны для заполнения!");
                return;
            }
            string phone = IzmeneniePacient.PhoneNumber.Trim();
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
            var jsonString = JsonSerializer.Serialize(IzmeneniePacient);
            File.WriteAllText($"P_{IzmeneniePacient.ID}.txt", jsonString);
            MessageBox.Show($"Изменение успешно завершено!");
        }
        private void SbrosButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = $"P_{IzmeneniePacient.ID}.txt";
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Сброс не может быть произведен пациент не найден");
                return;
            }
            var json = File.ReadAllText(filePath);
            var pacient = JsonSerializer.Deserialize<Pacient>(json);
            IzmeneniePacient.LastName = pacient.LastName;
            IzmeneniePacient.Name = pacient.Name;
            IzmeneniePacient.MiddleName = pacient.MiddleName;
            IzmeneniePacient.Birthday = pacient.Birthday;
            IzmeneniePacient.PhoneNumber = pacient.PhoneNumber;
            MessageBox.Show("Все не сохранённые изменения успешно сброшены");
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
