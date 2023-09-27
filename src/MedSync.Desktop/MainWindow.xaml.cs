using MedSync.Desktop.Pages;
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
using MedSync.Service.Services;
using System.IO;
using MedSync.Service.DTOs;

namespace MedSync.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (emailText.Text.ToString() is not null && passwordText.Password.ToString() is not null)
                {
                    UserService userService = new UserService();
                    UserForLoginDto result = await userService.UserValidator(emailText.Text.ToString(), passwordText.Password.ToString());
                    if (result is not null)
                    {
                        if (result.role == 0)
                        {
                            Width = 600;
                            Height = 600;
                            _frame.Content = new UserAddAppointement();
                            File.WriteAllText("../../../LoginSession.txt", result.Id.ToString());
                        }
                        else
                        {
                            Width = 600;
                            Height = 600;
                            _frame.Content = new Appointement();
                            File.WriteAllText("../../../LoginSession.txt", result.Id.ToString());
                            MessageBox.Show("successfully login.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Incorrect Data!", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Data!", "Error");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _frame.Content = new Signup();
            
        }

    }
}
