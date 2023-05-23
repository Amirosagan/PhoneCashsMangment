using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VodafoneCashApi.Data;
using VodafoneCashApi.Interfaces;
using VodafoneCashApi.Models;

namespace VodafoneCashApi.Helpers
{
  public class DataBase : IDataBase
  {
    private data _context;

    public DataBase(data context)
    {
      _context = context;
    }

    public void AddNumber(string number, decimal amount)
    {
      if (NumberExists(number))
      {
        throw new Exception("Number already exists");
      }

      var newNumber = new Numbers
      {
        Number = number,
        Amount = amount
      };

      _context.Numbers.Add(newNumber);
      this.SaveChanges();
    }

    public void AddTransaction(Transactions transaction)
    {
      if (!NumberExists(transaction.NumberId))
      {
        throw new Exception("Number does not exist");
      }

      var newTransaction = new Transactions
      {
        NumberId = transaction.NumberId,
        TransactionAmount = transaction.TransactionAmount,
        CashBefore = transaction.CashBefore,
        CashAfter = transaction.CashAfter,
        Date = DateTime.Now
      };
      _context.Transactions.Add(newTransaction);
      
      this.SaveChanges();
    }

    public void DeleteNumber(string number)
    {
      var Number = GetNumber(number);
      _context.Numbers.Remove(Number);
      this.SaveChanges();
    }

    public void DeleteTransaction(Guid transactionId)
    {
      _context.Transactions.Remove(GetTransaction(transactionId));

      this.SaveChanges();
    }

    public Numbers GetNumber(string number)
    {
      var Number = _context.Numbers.FirstOrDefault((e)=> e.Number.Equals(number));

      if(Number == null)
        throw new Exception("Number not Exect");

        return Number;
    }


    public IEnumerable<Numbers> GetNumbers()
    {
      return _context.Numbers.ToList();
    }

    public IEnumerable<Numbers> GetNumbersWithTransactions()
    {
      var Numbers = _context.Numbers.ToList();
      foreach (var Number in Numbers)
      {
        Number.Transactions = GetTransactionsByNumber(Number.Number).ToList();
      }
      return Numbers;
    }

    public Transactions GetTransaction(Guid transactionId)
    {
      var Transaction = _context.Transactions.FirstOrDefault((e)=> e.TransactionId.Equals(transactionId));

      if(Transaction == null)
        throw new Exception("Transaction not Exect");

        return Transaction;
    }

    public IEnumerable<Transactions> GetTransactions()
    {
      return _context.Transactions.ToList();
    }

    public IEnumerable<Transactions> GetTransactionsByNumber(string number)
    {
      var Transactions = _context.Transactions.Where((e)=> e.NumberId.Equals(number)).ToList();
      if (Transactions == null)
        throw new Exception("Number not Exect"); 
      return Transactions;
    }

    public bool NumberExists(string number)
    {
      return _context.Numbers.Any(e => e.Number == number);
    }

    private void SaveChanges()
    {
      _context.SaveChanges();
    }

    public void UpdateNumber(string number, decimal amount)
    {
      var Number = GetNumber(number);
      Number.Amount = amount;
      this.SaveChanges();
    }

    public void UpdateTransaction(Transactions transaction)
    {
      var Transaction = GetTransaction(transaction.TransactionId);
      Transaction.TransactionAmount = transaction.TransactionAmount;
      Transaction.CashBefore = transaction.CashBefore;
      Transaction.CashAfter = transaction.CashAfter;
      Transaction.Date = transaction.Date;
      this.SaveChanges();
    }
  }
}