using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
using MedSync.Domain.Enums;
using MedSync.Service.DTOs;
using MedSync.Service.Services;

namespace MedSync.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for UserAddAppointement.xaml
    /// </summary>
    public partial class UserAddAppointement : Page
    {
        public UserAddAppointement()
        {
            InitializeComponent();
            InitializeAsync();
        }

        private async void AllAppointements()
        {
            AppointmentService appointmentService = new AppointmentService();
            UserService userService = new UserService();
            var allAppointments = await appointmentService.GetAllAsync();
            string filePath = "../../../LoginSession.txt";
            string fileContent = File.ReadAllText(filePath);
            stackPanel.Children.Clear();
            for (int i = 0; i < allAppointments.Count; i++)
            {
                var appointement = allAppointments[i];
                if (appointement.PatientId == long.Parse(fileContent))
                {
                    
                    var doctorName = await userService.GetByIdAsync(appointement.DoctorId);
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = $"ID: {appointement.Id} | Doctor: {doctorName.FirstName} {doctorName.LastName} | {appointement.ScheduledDateTime.ToString()} | {appointement.Status}";
                    textBlock.FontSize = 16;
                    textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    stackPanel.Children.Add(textBlock);
                }
            }
        } 

        private async void InitializeAsync()
        {
            List<string> names = new List<string>();
            UserService userService = new UserService();
            
            var userNames = await userService.GetAllAsync();
            for (int i = 0; i < userNames.Count; i++)
            {
                var name = userNames[i];
                if (name.role == Role.Doctor)
                    names.Add($"{name.Id} | {name.FirstName} {name.LastName}");
            }

            doctorNames.ItemsSource = names;
            doctorNames.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = Application.Current.MainWindow as MainWindow;
            if (main != null)
            {
                main.Close();
            }
        }

        private void createAppointement_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedDoctor = doctorNames.SelectedItem.ToString();
                string[] partsDoctor = selectedDoctor.Split('|');
                string numberPart = partsDoctor[0].Trim();
                string filePath = "../../../LoginSession.txt";
                string fileContent = File.ReadAllText(filePath);

                DateTime selectedDate = schedueledDay.SelectedDate ?? DateTime.Now;
                
                string newHour = scheduledTime.Text.ToString();
                
                if (DateTime.TryParseExact(newHour, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedNewHour))
                {
                    selectedDate = new DateTime(
                        selectedDate.Year,
                        selectedDate.Month,
                        selectedDate.Day,
                        parsedNewHour.Hour,
                        parsedNewHour.Minute,
                        0
                    );

                    MessageBox.Show(selectedDate.ToString("yyyy-MM-dd hh:mm tt"));
                }

                AppointmentService appointmentService = new AppointmentService();
                AppointmentForCreationDto appointmentForCreationDto = new AppointmentForCreationDto()
                {
                    DoctorId = int.Parse(numberPart),
                    PatientId = int.Parse(fileContent),
                    Description = $"{diseaseCause.Text} - {descriiptionText.Text}",
                    ScheduledDateTime = selectedDate,
                };
                appointmentService.CreateAsync(appointmentForCreationDto);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AllAppointements();
            mainTab.SelectedIndex = 1;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            mainTab.SelectedIndex = 0;
        }

    }
}
