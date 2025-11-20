using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для MainScreen.xaml
    /// </summary>
    public partial class MainScreen : Page
    {
        public ObservableCollection<Pacient> Pacients { get; set; } = new();
        public Pacient? SelectedPacient { get; set; }
        public Doctor CurrentDoctor { get; set; }
        public MainScreen(Doctor? doctor = null)
        {
            CurrentDoctor = doctor;
            DataContext = this;
            Zagruzka();
            InitializeComponent();
        }
        private void CreatePacientButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreatePacient(Pacients));
        }
        private void StartPriemButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPacient == null)
            {
                MessageBox.Show("Пациент не выбран");
                return;
            }
            NavigationService.Navigate(new Priem(SelectedPacient, CurrentDoctor));
        }
        private void IzmeneniePacientButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPacient == null)
            {
                MessageBox.Show("Пациент не выбран");
                return;
            }
            NavigationService.Navigate(new IzmenenieInformation(SelectedPacient));
        }

        private void Zagruzka()
        {
            Pacients.Clear();
            var files = Directory.GetFiles(".", "P_*.txt");
            foreach (var file in files)
            {
                var json = File.ReadAllText(file);
                var pacient = JsonSerializer.Deserialize<Pacient>(json);
                Pacients.Add(pacient);
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
