using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMeThat.Models
{
    public class SendMeThatModel
    {
        public string SendersEmail { get; set; }
        public string ReceiversEmail { get; set; }
        public System.DateTime SharedDate { get; set; }
        public string SharedCode { get; set; }
    }
}
