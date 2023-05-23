using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VodafoneCashApi.Models;

namespace VodafoneCashApi.Interfaces
{
    public interface IDataBase
    {
        public IEnumerable<Numbers> GetNumbers();
        public IEnumerable<Numbers> GetNumbersWithTransactions();
        public void AddNumber(string number, Decimal amount);
        public bool NumberExists(string number);
        public Numbers GetNumber(string number);
        public void AddTransaction(Transactions transaction);
        public IEnumerable<Transactions> GetTransactions();
        public IEnumerable<Transactions> GetTransactionsByNumber(string number);
        public Transactions GetTransaction(Guid transactionId);
        public void UpdateNumber(string number, Decimal amount);
        public void UpdateTransaction(Transactions transaction);
        public void DeleteNumber(string number);
        public void DeleteTransaction(Guid transactionId);   
    }
}