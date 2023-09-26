using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using MedSync.Service.DTOs;
using MedSync.Service.Services;

namespace MedSync.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : Page
    {
        private MainWindow _page1;

        public Signup()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string dateFormat = "dd.MM.yyyy";
                if (dateTime.Text != null)
                {
                    DateTime dateOfBirth = DateTime.ParseExact(dateTime.Text.ToString(), dateFormat, CultureInfo.InvariantCulture);
                    if (firstName.Text != null && lastName.Text != null && phoneNumber.Text != null && signupEmail.Text != null && signupPassword.Password != null)
                    {
                        UserForCreationDto userForCreationDto = new UserForCreationDto()
                        {
                            FirstName = firstName.Text.ToString(),
                            LastName = lastName.Text.ToString(),
                            DateOfBirth = dateOfBirth,
                            PhoneNumber = phoneNumber.Text.ToString(),
                            Email = signupEmail.Text.ToString(),
                            Password = signupPassword.Password.ToString()
                        };

                        UserService userService = new UserService();
                        UserForResultDto result = await userService.CreateAsync(userForCreationDto);

                        if (result != null)
                        {
                            
                            File.WriteAllText("LoginSession.txt", result.Id.ToString());
                            _frame.Navigate(new Signup());
                            Frame newFrame = new Frame();
                            _frame.Content = newFrame;
                            newFrame.Navigate(new UserAddAppointement());
                            MessageBox.Show($"{result.Id} {result.LastName} {result.FirstName}");
                        }
                        else
                        {
                            MessageBox.Show("User creation failed.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Some input fields are null.");
                    }
                }
                else
                {
                    MessageBox.Show("Date input field is null.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
