using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SendMeThat
{
    public class CodeViewModel
    {
        public enum SendToResult { Cancel, Send, Delete }
        public SendToResult Result { get; set; }
        public event Action CloseRequest;
        private string _documentPath;
        private TextViewSelection _selection;

        public CodeViewModel()
        {
        }

        public CodeViewModel(string documentPath, TextViewSelection selection) : this()
        {
            this._documentPath = documentPath;
            this._selection = selection;
        }

        
        public string SelectionText => _selection.Text;
        public ICommand CancelCommand;
        public ICommand SendCommand;
    }
}
