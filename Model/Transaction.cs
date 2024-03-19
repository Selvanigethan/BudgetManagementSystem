using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASDCw.Model
{
    internal class Transaction
    {
        private double amount;
        private bool isIncome;
        private string categoryName;
        private DateTime dateTime;

        private int transactionId;

        private static int nextTransactionId = 1;

        public Transaction(double amount, bool isIncome)
        {
            this.amount = amount;
            this.isIncome = isIncome;
            transactionId = nextTransactionId++;
            DateTime = DateTime.Now;
        }

        public double Amount { get => amount; }
        public bool IsIncome { get => isIncome; }
        public string CategoryName { get => categoryName; set => categoryName = value; }
        public int TransactionId { get => transactionId; }
        public DateTime DateTime { get => dateTime; set => dateTime = value; }
    }
}
