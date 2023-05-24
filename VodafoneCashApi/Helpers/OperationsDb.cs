using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VodafoneCashApi.Interfaces;
using VodafoneCashApi.Models;

namespace VodafoneCashApi.Helpers
{
  public class OperationsDb : IOperationsDb
  {

    private readonly IDataBase _context;

    public OperationsDb(IDataBase context)
    {
      _context = context;
    }
    public void AddNumber(string number, decimal amount)
    {
       var regex = new System.Text.RegularExpressions.Regex(@"^01[0125][0-9]{8}$");
        if(_context.NumberExists(number))
            throw new Exception("Number already exists");
        if(!regex.IsMatch(number))
            throw new Exception("Invalid Number");
        _context.AddNumber(number, amount);
    }

    public void AddTransaction(Transactions transaction)
    {
        if(transaction.TransactionAmount == 0)
            throw new Exception("Invalid Transaction Amount");
        var Number = _context.GetNumber(transaction.NumberId);
        if(Number == null)
            throw new Exception("Number does not exist");
        transaction.CashBefore = Number.Amount;
        Number.Amount += transaction.TransactionAmount;
        transaction.CashAfter = Number.Amount;
        _context.AddTransaction(transaction);
        UpdateNumber(Number.Number, Number.Amount);
        
    }

    public void DeleteNumber(string number)
    {
      _context.DeleteNumber(number);
    }

    public void DeleteTransaction(Guid transactionId)
    {
        _context.DeleteTransaction(transactionId);
    }

    public List<Numbers> GetAllNumber()
    {
      return _context.GetNumbers().ToList();
    }

    public List<Transactions> GetAllTransaction()
    {
        return _context.GetTransactions().ToList();
    }

    public Transactions GetLastTransaction(string number)
    {
        if(!_context.NumberExists(number))
            throw new Exception("Number does not exist");
        return _context.GetLastTransaction(number);
    }

    public Numbers GetNumber(string number)
    {
      return _context.GetNumber(number);
    }

    public Transactions GetTransaction(Guid transactionId)
    {
        return _context.GetTransaction(transactionId);
    }

    public List<Transactions> GetTransactionsByNumber(string number)
    {
        if(!_context.NumberExists(number))
            throw new Exception("Number does not exist");
        return _context.GetTransactionsByNumber(number).ToList();
    }

    public void UpdateNumber(string number, decimal amount)
    {
        if(!_context.NumberExists(number))
            throw new Exception("Number does not exist");
        _context.UpdateNumber(number, amount);
    }

    public void UpdateTransaction(Transactions transaction)
    {
      
      if(transaction == null)
        throw new Exception("Transaction does not exist");
        _context.UpdateTransaction(transaction);
    }
  }
}