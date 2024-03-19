using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASDCw.Model;

namespace ASDCw.CategoryModel
{
    internal interface Category
    {
        public double GetTotalAmount();
        public void InsertTransaction(Transaction transaction);

    }
}
