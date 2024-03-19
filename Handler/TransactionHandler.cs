using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASDCw.CategoryModel;
using ASDCw.Model;

namespace ASDCw.Handler
{
    internal class TransactionHandler
    {
        private bool isIncome;
        private List<Transaction> transactions = new List<Transaction>();
        private User user;

        public void StartApp()
        {
            while (true)
            {
                Console.WriteLine("Enter your name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Do you have Budget? (Y-Yes, N-No): ");
                string budgetResponse = Console.ReadLine();

                if (budgetResponse == "Y")
                {
                    Console.WriteLine("Enter your planned budget amount (non zero): ");
                    string plannedBudgetAmount = Console.ReadLine();
                    if (!double.TryParse(plannedBudgetAmount, out double plannedBudgetAmountInDouble))
                    {
                        Console.WriteLine("String could not be parsed to double.");
                        continue;
                    }
                    Budget budget = new Budget(plannedBudgetAmountInDouble);
                    user = new User(name, budget);

                    Console.WriteLine("Enter your planned budget amount (non zero) for each category!");

                    Console.WriteLine("Travel (Type N if it does not apply): ");
                    string travelExpenses = Console.ReadLine();
                    if (!ReadTravelExpenses(travelExpenses))
                    {
                        continue;
                    }

                    Console.WriteLine("Clothes (Type N if it does not apply): ");
                    string clothesExpenses = Console.ReadLine();
                    if (!ReadClothesExpenses(travelExpenses))
                    {
                        continue;
                    }

                    Console.WriteLine("Food (Type N if it does not apply): ");
                    string foodExpenses = Console.ReadLine();
                    if (!ReadFoodExpenses(foodExpenses))
                    {
                        continue;
                    }

                    Console.WriteLine("Health (Type N if it does not apply): ");
                    string healthExpenses = Console.ReadLine();
                    if (!ReadHealthExpenses(healthExpenses))
                    {
                        continue;
                    }

                    Console.WriteLine("Bills (Type N if it does not apply): ");
                    string billsExpenses = Console.ReadLine();
                    if (!ReadBillsExpenses(billsExpenses))
                    {
                        continue;
                    }

                    Console.WriteLine("Entertainment (Type N if it does not apply): ");
                    string entertainmentExpenses = Console.ReadLine();
                    if (!ReadEntertainmentExpenses(entertainmentExpenses))
                    {
                        continue;
                    }

                    Console.WriteLine("\nWelcome " + user.Name + "!");
                    break;
                }
                else if (budgetResponse == "N")
                {
                    user = new User(name);
                    Console.WriteLine("\nWelcome " + user.Name + "!");
                    break;
                }

            }


            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Enter a choice from below: \n1 - Add a transaction \n2 - view transactions \n3 - view category based expenses with Budget \n4 - Delete transaction \n5 - Edit transaction");
                Console.WriteLine();
                string userInput = Console.ReadLine();

                if (userInput != "1" && userInput != "2" && userInput != "3" && userInput != "4" && userInput != "5" && userInput != "6")
                {
                    Console.WriteLine("Please Enter a valid input!");
                    Console.WriteLine();
                    continue;
                }
                else if (userInput == "1")
                {
                    Console.WriteLine("Enter the amount");
                    string amount = Console.ReadLine();
                    Console.WriteLine("Tell it is income or expense (I/E) ? ");
                    string type = Console.ReadLine();
                    Console.WriteLine("Tell us the category (T-Travel, C-Clothes, B-Bills, E-Entertainment, F-Food, H-Health) ? ");
                    string category = Console.ReadLine();

                    bool isValuePass = double.TryParse(amount, out double amountInDouble);

                    if (!isValuePass)
                    {
                        Console.WriteLine("String could not be parsed to double.");
                    }

                    if (type == "I")
                    {
                        isIncome = true;
                    }
                    else if (type == "E")
                    {
                        isIncome = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input for the income/expense type!");
                        continue;
                    }

                    Transaction transaction = new Transaction(amountInDouble, isIncome);
                    addTransactionToCategories(transaction, category);

                }
                else if (userInput == "2")
                {
                    Console.WriteLine("These are the transactions: ");
                    PrintTransactions();
                }
                else if (userInput == "3")
                {
                    if (user.Budget == null)
                    {
                        Console.WriteLine("You don't have a planned budget entered!");
                        continue;
                    }
                    else
                    {

                        if (transactions.Count() == 0)
                        {
                            Console.WriteLine("You don't have any transactions added!");
                            continue;
                        }
                        user.Budget.updateBalance(transactions);
                        PrintBudgetBasedExpenses();
                    }
                }
                else if (userInput == "4")
                {
                    Console.WriteLine("These are the existing transactions: ");
                    PrintTransactions();
                    Console.WriteLine("Enter the transaction id you want to delete: ");
                    string transactionIdToDelete = Console.ReadLine();
                    if (!int.TryParse(transactionIdToDelete, out int transactionIdToDeleteInt))
                    {
                        Console.WriteLine("String could not be parsed to integer.");
                        continue;
                    }

                    bool idFound = false;


                    for (int i = 0; i < transactions.Count; i++)
                    {
                        if (transactions[i].TransactionId == transactionIdToDeleteInt)
                        {
                            idFound = true;
                            transactions.RemoveAt(i);
                            Console.WriteLine("Successfully deleted!");
                        }
                    }
                    if (!idFound)
                    {
                        Console.WriteLine("Please enter a valid available id!");
                        continue;
                    }
                    PrintTransactions();
                }
            }
        }

        private void PrintTransactions()
        {
            foreach (Transaction transaction in transactions)
            {
                if (transaction == null) { continue; }
                string inComeOrExpense;
                if (transaction != null && transaction.IsIncome) { inComeOrExpense = "Income"; }
                else { inComeOrExpense = "Expense"; }
                Console.WriteLine("id: " + transaction.TransactionId + ", " + "transaction category: " + transaction.CategoryName + ", " + "amount:" + transaction.Amount + ", " + "Income or Expense: " + inComeOrExpense + ", on : " + transaction.DateTime);
            }
        }

        private bool ReadClothesExpenses(string inputString)
        {
            if (inputString != "N")
            {
                if (!double.TryParse(inputString, out double inputStringDouble))
                {
                    Console.WriteLine("String could not be parsed to integer.");
                    return false;
                }
                else
                {
                    user.Budget.CategoryBasedPlannedExpenses.Add(Clothes.GetInstance(), inputStringDouble);
                }
            }
            else
            {
                user.Budget.CategoryBasedPlannedExpenses.Add(Clothes.GetInstance(), 0.00);
            }
            return true;
        }

        private bool ReadTravelExpenses(string inputString)
        {
            if (inputString != "N")
            {
                if (!double.TryParse(inputString, out double inputStringDouble))
                {
                    Console.WriteLine("String could not be parsed to integer.");
                    return false;
                }
                else
                {
                    user.Budget.CategoryBasedPlannedExpenses.Add(Travel.GetInstance(), inputStringDouble);
                }
            }
            else
            {
                user.Budget.CategoryBasedPlannedExpenses.Add(Travel.GetInstance(), 0.00);
            }
            return true;
        }

        private bool ReadFoodExpenses(string inputString)
        {
            if (inputString != "N")
            {
                if (!double.TryParse(inputString, out double inputStringDouble))
                {
                    Console.WriteLine("String could not be parsed to integer.");
                    return false;
                }
                else
                {
                    user.Budget.CategoryBasedPlannedExpenses.Add(Food.GetInstance(), inputStringDouble);
                }
            }
            else
            {
                user.Budget.CategoryBasedPlannedExpenses.Add(Food.GetInstance(), 0.00);
            }
            return true;
        }

        private bool ReadHealthExpenses(string inputString)
        {
            if (inputString != "N")
            {
                if (!double.TryParse(inputString, out double inputStringDouble))
                {
                    Console.WriteLine("String could not be parsed to integer.");
                    return false;
                }
                else
                {
                    user.Budget.CategoryBasedPlannedExpenses.Add(Health.GetInstance(), inputStringDouble);
                }
            }
            else
            {
                user.Budget.CategoryBasedPlannedExpenses.Add(Health.GetInstance(), 0.00);
            }
            return true;
        }

        private bool ReadBillsExpenses(string inputString)
        {
            if (inputString != "N")
            {
                if (!double.TryParse(inputString, out double inputStringDouble))
                {
                    Console.WriteLine("String could not be parsed to integer.");
                    return false;
                }
                else
                {
                    user.Budget.CategoryBasedPlannedExpenses.Add(Bills.GetInstance(), inputStringDouble);
                }
            }
            else
            {
                user.Budget.CategoryBasedPlannedExpenses.Add(Bills.GetInstance(), 0.00);
            }
            return true;
        }

        private bool ReadEntertainmentExpenses(string inputString)
        {
            if (inputString != "N")
            {
                if (!double.TryParse(inputString, out double inputStringDouble))
                {
                    Console.WriteLine("String could not be parsed to integer.");
                    return false;
                }
                else
                {
                    user.Budget.CategoryBasedPlannedExpenses.Add(Entertainment.GetInstance(), inputStringDouble);
                }
            }
            else
            {
                user.Budget.CategoryBasedPlannedExpenses.Add(Entertainment.GetInstance(), 0.00);
            }
            return true;
        }

        private void PrintBudgetBasedExpenses()
        {
            Console.WriteLine("Expenses for Travel: " + Travel.GetInstance().GetTotalAmount() + ", Budget: " + user.Budget.CategoryBasedPlannedExpenses[Travel.GetInstance()]);
            Console.WriteLine("Expenses for Clothes: " + Clothes.GetInstance().GetTotalAmount() + ", Budget: " + user.Budget.CategoryBasedPlannedExpenses[Clothes.GetInstance()]);
            Console.WriteLine("Expenses for Bills: " + Bills.GetInstance().GetTotalAmount() + ", Budget: " + user.Budget.CategoryBasedPlannedExpenses[Bills.GetInstance()]);
            Console.WriteLine("Expenses for Entertainment: " + Entertainment.GetInstance().GetTotalAmount() + ", Budget: " + user.Budget.CategoryBasedPlannedExpenses[Entertainment.GetInstance()]);
            Console.WriteLine("Expenses for Food: " + Food.GetInstance().GetTotalAmount() + ", Budget: " + user.Budget.CategoryBasedPlannedExpenses[Food.GetInstance()]);
            Console.WriteLine("Expenses for Travel: " + Health.GetInstance().GetTotalAmount() + ", Budget: " + user.Budget.CategoryBasedPlannedExpenses[Health.GetInstance()]);


            Console.WriteLine("Your Total Budget : " + user.Budget.PlannedExpenseAmount);
            Console.WriteLine("Your Total Expenses : " + user.Budget.TotalAmountSpent);
            Console.WriteLine("Percentage spent : " + user.Budget.CalculateExpensePercentage() + "%");
        }

        private void addTransactionToCategories(Transaction transaction, string category)
        {
            if (category == "T")
            {
                transaction.CategoryName = "Travel";
                transactions.Add(transaction);

                Category travel = Travel.GetInstance();
                travel.InsertTransaction(transaction);
                Console.WriteLine("Successfully Added Transaction");
            }
            else if (category == "C")
            {
                transaction.CategoryName = "Clothes";
                transactions.Add(transaction);

                Category clothes = Clothes.GetInstance();
                clothes.InsertTransaction(transaction);
                Console.WriteLine("Successfully Added Transaction");
            }
            else if (category == "H")
            {
                transaction.CategoryName = "Health";
                transactions.Add(transaction);

                Category health = Health.GetInstance();
                health.InsertTransaction(transaction);
                Console.WriteLine("Successfully Added Transaction");
            }
            else if (category == "B")
            {
                transaction.CategoryName = "Bills";
                transactions.Add(transaction);

                Category bills = Bills.GetInstance();
                bills.InsertTransaction(transaction);
                Console.WriteLine("Successfully Added Transaction");
            }
            else if (category == "F")
            {
                transaction.CategoryName = "Food";
                transactions.Add(transaction);

                Category food = Food.GetInstance();
                food.InsertTransaction(transaction);
                Console.WriteLine("Successfully Added Transaction");
            }
            else if (category == "E")
            {
                transaction.CategoryName = "Entertainment";
                transactions.Add(transaction);

                Category entertainment = Entertainment.GetInstance();
                entertainment.InsertTransaction(transaction);
                Console.WriteLine("Successfully Added Transaction");
            }
        }
    }
}
