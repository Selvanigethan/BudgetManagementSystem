using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASDCw.Model;

namespace ASDCw.CategoryModel
{
    internal class Clothes : Category
    {
        private List<Transaction> transactions = new List<Transaction>();
        private double totalAmount = 0.0;

        private static Clothes clothInstance;


        private Clothes() { }

        public static Clothes GetInstance()
        {
            if (clothInstance == null)
            {
                clothInstance = new Clothes();
            }
            return clothInstance;
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
