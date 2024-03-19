using ASDCw.Handler;

namespace ASDCw
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TransactionHandler transactionHandler = new TransactionHandler();
            transactionHandler.StartApp();
        }
    }
}
