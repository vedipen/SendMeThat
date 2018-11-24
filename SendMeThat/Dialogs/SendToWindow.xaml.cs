using Dialog;
using Microsoft.Win32;
using SendMeThat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace SendMeThat
{
    /// <summary>
    /// Interaction logic for SendToWindow.xaml
    /// </summary>
    public partial class SendToWindow : BaseDialogWindow
    {
        public SendToWindow(string documentPath, TextViewSelection selection)
        {
            InitializeComponent();
            this._documentPath = documentPath;
            this._selectionText = selection;
            this.Loaded += (s, e) => this.SelectionTextBox.Text = selection.Text;
        }

        private const string UriString = "https://sendmethatapi.azurewebsites.net/api/";
        private string _documentPath;
        private TextViewSelection _selectionText;
        
        private void OnCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void OnSend(object sender, RoutedEventArgs e)
        {
            string Input = SelectionTextBox.Text;
            string ReceiversEmail = ReceiversEmailTextBox.Text;
            string SendersEmail = GetUserEmailAddress();
            if(SendersEmail == null)
            {
                MessageBox.Show("Please Login to Visual Studio to continue");
                this.Close();
                return;
            }
            string SendersName = GetUserName();
            HttpClient client = new HttpClient();
            SendMeThatModel codeModel = new SendMeThatModel();
            client.BaseAddress = new Uri(UriString);
            codeModel.ReceiversEmail = ReceiversEmail;
            codeModel.SendersEmail = SendersEmail;
            codeModel.SharedCode = Input;
            codeModel.SharedDate = DateTime.Now;
            HttpResponseMessage response = await client.PostAsJsonAsync("SendMeThat" ,codeModel); 
            if(response.IsSuccessStatusCode)
            {
                MessageBox.Show("Code Sent! :)\nLogged in as "+ SendersName);
            } else
            {
                MessageBox.Show("Unable To Send :(");
            }
            this.Close();
        }

        private static string GetUserEmailAddress()
        {
            // It's a good practice to request explicit permission from
            // the user that you want to use his email address and any
            // other information. This enables the user to be in control
            // of his/her privacy.

            // Assuming permission is granted, we obtain the email address.

            const string SubKey = "Software\\Microsoft\\VSCommon\\ConnectedUser\\IdeUser\\Cache";
            const string EmailAddressKeyName = "EmailAddress";
            const string UserNameKeyName = "DisplayName";

            RegistryKey root = Registry.CurrentUser;
            RegistryKey sk = root.OpenSubKey(SubKey);
            if (sk == null)
            {
                // The user is currently not signed in.
                return null;
            }
            else
            {
                // Get user email address.
                return (string)sk.GetValue(EmailAddressKeyName);

                // You can also get user name like this.
                // return (string)sk.GetValue(UserNameKeyName);
            }
        }

        private static string GetUserName()
        {
            // It's a good practice to request explicit permission from
            // the user that you want to use his email address and any
            // other information. This enables the user to be in control
            // of his/her privacy.

            // Assuming permission is granted, we obtain the email address.

            const string SubKey = "Software\\Microsoft\\VSCommon\\ConnectedUser\\IdeUser\\Cache";
            const string EmailAddressKeyName = "EmailAddress";
            const string UserNameKeyName = "DisplayName";

            RegistryKey root = Registry.CurrentUser;
            RegistryKey sk = root.OpenSubKey(SubKey);
            if (sk == null)
            {
                // The user is currently not signed in.
                return null;
            }
            else
            {
                // You can also get user name like this.
                return (string)sk.GetValue(UserNameKeyName);
            }
        }

    }
}
