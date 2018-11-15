using Dialog;
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
using static SendMeThat.CodeViewModel;

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

        private string _documentPath;
        private TextViewSelection _selectionText;

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            string Input = SelectionTextBox.Text;
            MessageBox.Show("Input received : " + Input);
        }

    }
}
