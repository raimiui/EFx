using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Account
    {
        public Account(decimal capital)
        {
            Balance = capital;
        }

        public decimal Balance { get; private set; }
     
        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }
    }
}

