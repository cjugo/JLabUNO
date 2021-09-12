using JLabUNO.Components;
using JLabUNO.Helpers;
using JLabUNO.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace JLabUNO.Pages
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            EmailTextBox.Text = "claudio@yopmail.com";
            PasswordPasswordBox.Password = "123456";
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = await ValidForm();
            if (!isValid)
            {
                return;
            }

            Loader loader = new Loader("Por favor espere...");
            loader.Show();

            Response response = await ApiService.LoginAsync(new LoginRequest
            {
                Email = EmailTextBox.Text,
                Password = PasswordPasswordBox.Password
            });
            loader.Close();

            MessageDialog messageDialog;
            if (!response.IsSuccess)
            {
                messageDialog = new MessageDialog(response.Message, "Error");
                await messageDialog.ShowAsync();
                return;

            }
            User user = (User)response.Result;
            if (user == null)
            {
                messageDialog = new MessageDialog("Usuario o contraseña incorrectos", "Error");
                await messageDialog.ShowAsync();
                return;
            }


            Frame.Navigate(typeof(MainPage), user);

        }

        private async Task<bool> ValidForm()
        {
            MessageDialog messageDialog;

            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                messageDialog = new MessageDialog("Debe ingresar Email.", "Login");
                await messageDialog.ShowAsync();
                return false;
            }

            if (!RegexUtilities.IsValidEmail(EmailTextBox.Text))
            {
                messageDialog = new MessageDialog("Debe ingresar un Email válido.", "Error");
                await messageDialog.ShowAsync();
                return false;
            }


            if (PasswordPasswordBox.Password.Length < 6)
            {
                messageDialog = new MessageDialog("Debe ingresar Password de al menos seis(6) carácteres.", "Login");
                await messageDialog.ShowAsync();
                return false;
            }

            return true;
        }
    }
}
