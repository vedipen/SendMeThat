using Dialog;
using Microsoft.Win32;
using Newtonsoft.Json;
using SendMeThat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
    /// Interaction logic for ReceiverWindow.xaml
    /// </summary>
    public partial class ReceiverWindow : BaseDialogWindow
    {
        public ReceiverWindow()
        {
            InitializeComponent();
            codeList.ItemsSource = null;
            OnLoad();
        }

        private const string UriString = "https://sendmethatapi.azurewebsites.net/api/";

        private async void OnLoad()
        {
            string UserEmail = GetUserEmailAddress();
            if(UserEmail == null)
            {
                if (GeneralSettings.Default.UserEmail == "Empty" || GeneralSettings.Default.UserEmail == null)
                {
                    var dialog = new GetUserEmailID();
                    if (dialog.ShowDialog() == true)
                    {
                        if (dialog.ResponseText != null && dialog.ResponseText.Count() != 0)
                        {
                            UserEmail = dialog.ResponseText;
                            GeneralSettings.Default.UserEmail = UserEmail;
                        }
                        else
                        {
                            this.Close();
                            MessageBox.Show("Unable to receive.");
                            return;
                        }
                    }
                }
                else
                {
                    UserEmail = GeneralSettings.Default.UserEmail;
                }
            }
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UriString);
            string ReceiversEmail = UserEmail;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("SendMeThat?ReceiversEmail=" + ReceiversEmail);
            List<SendMeThatModel> outputList = null;
            if (response.IsSuccessStatusCode)
            {
                var readTask = await response.Content.ReadAsStringAsync();
                outputList = JsonConvert.DeserializeObject<List<SendMeThatModel>>(readTask);
            }
            else
            {
                if (ReceiversEmail == null)
                {
                    MessageBox.Show("Please Login :(");
                }
                else
                {
                    MessageBox.Show("Unable to Receive :(");
                }
            }
            codeList.ItemsSource = outputList;
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
    }
}
