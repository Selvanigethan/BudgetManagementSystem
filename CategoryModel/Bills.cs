﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASDCw.Model;

namespace ASDCw.CategoryModel
{
    internal class Bills : Category
    {
        private List<Transaction> transactions = new List<Transaction>(); //adding an auto incrementing transaction id in the object
        private double totalAmount = 0.0;

        private static Bills instance;

        private Bills() { }

        public static Bills GetInstance()
        {
            if (instance == null)
            {
                instance = new Bills();
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
