using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASDCw.Model;

namespace ASDCw.CategoryModel
{
    internal class Travel : Category
    {
        private List<Transaction> transactions = new List<Transaction>(); //adding an auto incrementing transaction id in the object
        private double totalAmount = 0.0;

        private static Travel instance;

        private Travel() { }

        public static Travel GetInstance()
        {
            if (instance == null)
            {
                instance = new Travel();
            }
            return instance;
        }

        public void InsertTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
            SetTotalAmount(transaction);
        }


        public double GetTotalAmount()
        {
            return totalAmount;
        }

        private void SetTotalAmount(Transaction transaction)
        {
            if (transaction != null)
            {
                if (transaction.IsIncome)
                {
                    totalAmount -= transaction.Amount;
                }
                else
                {
                    totalAmount += transaction.Amount;
                }
            }
        }


    }
}
