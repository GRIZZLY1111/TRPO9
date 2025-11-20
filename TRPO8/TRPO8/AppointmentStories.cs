using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TRPO8
{
    public class AppointmentStories
    {
        private DateTime _appointment = DateTime.Now;
        private int _idDoctor;
        private string _diagnosis;
        private string _recomendations;

        public DateTime Appointment
        {
            get => _appointment;
            set
            {
                if (_appointment != value)
                {
                    _appointment = value;
                    OnPropertyChanged();
                }
            }
        }
        public int IDDoctor
        {
            get => _idDoctor;
            set
            {
                if (_idDoctor != value)
                {
                    _idDoctor = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Diagnosis
        {
            get => _diagnosis;
            set
            {
                if (_diagnosis != value)
                {
                    _diagnosis = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Recomendations
        {
            get => _recomendations;
            set
            {
                if (_recomendations != value)
                {
                    _recomendations = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}