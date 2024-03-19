using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASDCw.Model
{
    internal class User
    {
        private string name;
        private double salary;
        private Budget budget;

        public User(string name)
        {
            this.name = name;
        }

        public User(string name, Budget budget)
        {
            this.name = name;
            this.budget = budget;
        }

        public string Name { get => name; set => name = value; }
        public double Salary { get => salary; set => salary = value; }
        internal Budget Budget { get => budget; set => budget = value; }
    }
}
