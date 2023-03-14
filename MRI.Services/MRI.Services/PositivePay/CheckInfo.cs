using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MRIServices
{
    public class CheckInfo
    {
        public string CheckNumber { get; set; }
        public decimal CheckAmount { get; set; }
        public string Payee { get; set; }
        public string AccountNumber { get; set; }
        public string TransactionCode { get; set; }
        public DateTime CheckDate { get; set; }
    }
}