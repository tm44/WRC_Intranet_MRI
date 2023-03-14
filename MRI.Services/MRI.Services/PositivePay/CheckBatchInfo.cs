using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MRIServices
{
    public class CheckBatchInfo
    {
        public CheckBatchInfo()
        {
            CheckList = new List<CheckInfo>();
        }

        public List<CheckInfo> CheckList { get; set; }
        public decimal BatchTotal
        {
            get
            {
                return CheckList.Sum(c => c.CheckAmount);
            }
        }
        public int CheckCount
        {
            get
            {
                return CheckList.Count;
            }
        }
    }
}