using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VodafoneCashApi.Models;

namespace VodafoneCashApi.Interfaces
{
    public interface IOperationsDb 
    {
        public void AddNumber(string number, Decimal amount);
        public void AddTransaction(Transactions transaction);
        public void DeleteNumber(string number);
        public void DeleteTransaction(Guid transactionId);
        public void UpdateNumber(string number, Decimal amount);
        public void UpdateTransaction(Transactions transaction);
        public List<Numbers> GetAllNumber();
        public List<Transactions> GetAllTransaction();
        public Numbers GetNumber(string number);
        public Transactions GetTransaction(Guid transactionId);
        
    }
}