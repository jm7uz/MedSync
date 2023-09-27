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
using MedSync.Desktop.Components;
using MedSync.Service.DTOs;
using MedSync.Service.Services;

namespace MedSync.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for Appointement.xaml
    /// </summary>
    public partial class Appointement : Page
    {
        public Appointement()
        {
            InitializeComponent();
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            AppointmentService appointmentService = new AppointmentService();
            var res = await appointmentService.GetAllAsync();
            foreach (var item in res)
            {
                if (item != null)
                {
                    AppointmentWidget appointmentWidget = new AppointmentWidget();
                    long id = item.Id;
                    long patientId = item.PatientId;
                    DateTime ScheduledDateTime = item.ScheduledDateTime; ;
                    string Description = item.Description;
                    if (Description.Length > 50)
                    {
                        Description = Description.Substring(0, 50) + "...";
                    }
                    string oneData = $"{id}  |  {patientId}  |  {ScheduledDateTime}  |  {Description}";
                    DockPanel docpanel = new DockPanel();
                    Button button1 = new Button();
                    Button button2 = new Button();
                    TextBlock textBlock1 = new TextBlock();
                    
                    textBlock1.Text = oneData;
                    textBlock1.VerticalAlignment = VerticalAlignment.Center;
                    textBlock1.HorizontalAlignment = HorizontalAlignment.Center;

                    textBlock1.FontSize = 18;
                    textBlock1.Margin = new Thickness(10, 20, 5, 15);

                    button1.Content = $"Cancel";
                    button1.Name = $"c_{id}";
                    button1.Width = 150;
                    button1.HorizontalAlignment = HorizontalAlignment.Center;
                    button1.VerticalAlignment = VerticalAlignment.Center;
                    button1.Click += Button3_Click;

                    button2.Content = $"Apporve";
                    button2.Name = $"a_{id}";
                    button2.Width = 150;
                    button2.HorizontalAlignment = HorizontalAlignment.Center;
                    button2.VerticalAlignment = VerticalAlignment.Center;
                    button2.Click += Button2_Click;

                    stackPanel.Children.Add(textBlock1);

                    docpanel.Children.Add(button1);
                    docpanel.Children.Add(button2);
                    stackPanel.Children.Add(docpanel);

                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = Application.Current.MainWindow as MainWindow;
            if (main != null)
            {
                main.Close();
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                try
                {
                    AppointmentService appointmentService = new AppointmentService();
                    string buttonText = clickedButton.Name.ToString();
                    string[] parts = buttonText.Split('_');
                    string numberPart = parts[1];
                    AppointmentForUpdateDto appointmentForUpdateDto = new AppointmentForUpdateDto()
                    {
                        Id = int.Parse(numberPart),
                        Status = Domain.Enums.AppointmentStatus.Canceled,
                    };
                    appointmentService.UpdateAsync(appointmentForUpdateDto);
                    MessageBox.Show("Canceled successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                try
                {
                    AppointmentService appointmentService = new AppointmentService();
                    string buttonText = clickedButton.Name.ToString();
                    string[] parts = buttonText.Split('_');
                    string numberPart = parts[1];
                    AppointmentForUpdateDto appointmentForUpdateDto = new AppointmentForUpdateDto()
                    {
                        Id = int.Parse(numberPart),
                        Status = Domain.Enums.AppointmentStatus.Scheduled,
                    };

                    appointmentService.UpdateAsync(appointmentForUpdateDto);
                    MessageBox.Show("Approved successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }
    }
}
