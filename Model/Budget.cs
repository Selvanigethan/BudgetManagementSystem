using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASDCw.CategoryModel;

namespace ASDCw.Model
{
    internal class Budget
    {
        private double plannedExpenseAmount;
        private double totalAmountSpent;
        private double balance;

        private Dictionary<Category, double> categoryBasedPlannedExpenses = new Dictionary<Category, double>();


        public Budget(double plannedExpenseAmount)
        {
            this.plannedExpenseAmount = plannedExpenseAmount;
        }

        public double PlannedExpenseAmount { get => plannedExpenseAmount; }
        public double Balance { get => balance; set => balance = value; }
        public double TotalAmountSpent { get => totalAmountSpent; set => totalAmountSpent = value; }
        internal Dictionary<Category, double> CategoryBasedPlannedExpenses { get => categoryBasedPlannedExpenses; set => categoryBasedPlannedExpenses = value; }

        public void updateBalance(List<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                TotalAmountSpent += transaction.Amount;
            }
            balance = plannedExpenseAmount - TotalAmountSpent;
        }

        public double CalculateExpensePercentage()
        {
            return totalAmountSpent / plannedExpenseAmount * 100;

        }
    }
}
