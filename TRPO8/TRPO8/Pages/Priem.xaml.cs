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
    /// Логика взаимодействия для Priem.xaml
    /// </summary>
    public partial class Priem : Page
    {
        public ObservableCollection<AppointmentStories> Posechenia { get; set; } = new ();
        public Pacient ZapisPacient { get; set; }
        public Doctor CurrentDoctor { get; set; }
        public DateTime Appointment { get; set; } = DateTime.Now;
        public string Diagnosis { get; set; }
        public string Recomendations { get; set; }
        public Priem(Pacient? pacient = null, Doctor? doctor = null)
        {
            CurrentDoctor = doctor;
            ZapisPacient = pacient;
            Posechenia = ZapisPacient.Appointments;
            DataContext = this;
            InitializeComponent();
        }
        private void ZapisPacientButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Diagnosis))
            {
                MessageBox.Show("Поле 'Диагноз' обязательно!");
                return;
            }
            ZapisPacient.Appointments.Add(new AppointmentStories() {
                Appointment = Appointment,
                IDDoctor = CurrentDoctor.ID,
                Diagnosis = Diagnosis,
                Recomendations = Recomendations
            });
            var json = JsonSerializer.Serialize(ZapisPacient);
            File.WriteAllText($"P_{ZapisPacient.ID}.txt", json);
            MessageBox.Show("Приём успешно записан и сохранён!");
        }

        private void IzmeneniePacientButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new IzmenenieInformation(ZapisPacient));
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
