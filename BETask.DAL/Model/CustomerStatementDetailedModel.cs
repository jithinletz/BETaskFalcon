﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
    public class CustomerStatementDetailedModel
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Qty { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
        public string TransactionType { get; set; }

    }
}
