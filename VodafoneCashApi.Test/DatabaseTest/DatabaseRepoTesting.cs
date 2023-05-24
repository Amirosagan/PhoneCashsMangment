using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VodafoneCashApi.Data;
using VodafoneCashApi.Helpers;
using VodafoneCashApi.Interfaces;
using VodafoneCashApi.Models;
using Xunit;

namespace VodafoneCashApi.Test.DatabaseTest
{
    public class DbMoac : IDataBase
    {
        private List<Numbers> _number = new List<Numbers>();
        private List<Transactions> _transaction =new List<Transactions>();

    public void AddNumber(string number, decimal amount)
    {
      _number.Add(new Numbers
      {
        Number = number,
        Amount = amount
      });

    }

    public void AddTransaction(Transactions transaction)
    {
      _transaction.Add(transaction);
    }

    public void DeleteNumber(string number)
    {
      List<Transactions> transactions = this.GetTransactions().ToList();
      foreach (var transaction in transactions)
      {
        if (transaction.NumberId == number)
        {
          _transaction.Remove(transaction);
        }
      }
      _number.Remove(GetNumber(number));
    }

    public void DeleteTransaction(Guid transactionId)
    {
      _transaction.Remove(GetTransaction(transactionId));
    }

    public Transactions GetLastTransaction(string number)
    {
      var transaction = _transaction.Where(x => x.NumberId == number).OrderByDescending(x => x.Date).FirstOrDefault();
      if (transaction == null)
        throw new Exception("Transaction does not exist");
      return transaction;
    }

    public Numbers GetNumber(string number)
    {
      var Number = _number.FirstOrDefault(x => x.Number == number);
      if(Number == null)
        throw new Exception("Number does not exist");
      return Number;

    }

    public IEnumerable<Numbers> GetNumbers()
    {
      return _number;
    }

    public IEnumerable<Numbers> GetNumbersWithTransactions()
    {
      return _number;
    }

    public Transactions GetTransaction(Guid transactionId)
    {
      var Transaction = _transaction.FirstOrDefault(x => x.TransactionId == transactionId);
      if (Transaction == null)
        throw new Exception("Transaction does not exist");
      return Transaction;
    }

    public IEnumerable<Transactions> GetTransactions()
    {
      return _transaction;
    }

    public IEnumerable<Transactions> GetTransactionsByNumber(string number)
    {
      return _transaction.Where(x => x.NumberId == number); 
    }

    public bool NumberExists(string number)
    {
      return _number.Any(x => x.Number == number);
    }

    public void SaveChanges()
    {
      throw new NotImplementedException();
    }

    public void UpdateNumber(string number, decimal amount)
    {
      var Number = GetNumber(number);
      Number.Amount = amount;
      _number.Remove(GetNumber(number));
      _number.Add(Number);
    }

    public void UpdateTransaction(Transactions transaction)
    {
      var Transaction = GetTransaction(transaction.TransactionId);
      Transaction.NumberId = transaction.NumberId;
      Transaction.TransactionAmount = transaction.TransactionAmount;
      Transaction.CashBefore = transaction.CashBefore;
      Transaction.CashAfter = transaction.CashAfter;
      Transaction.Date = transaction.Date;
      _transaction.Remove(GetTransaction(transaction.TransactionId));
      _transaction.Add(Transaction);
    }
    
  }
  
  [Collection("OperationsDB")]
  public class DatabaseRepoTesting
  {
    private readonly IOperationsDb _operationsDb;

    public DatabaseRepoTesting()
    {
      _operationsDb = new OperationsDb(new DbMoac());
    }

    [Fact]
    [Trait("Category", "Database")]
    public void AddNumber()
    {
      _operationsDb.AddNumber("01000000000", 0);
      Assert.Equal(1, _operationsDb.GetAllNumber().Count());

      Assert.Equal("01000000000", _operationsDb.GetNumber("01000000000").Number);
      Assert.Equal(0, _operationsDb.GetNumber("01000000000").Amount);
    }
    [Fact]
    [Trait("Category", "Database")]
    public void AddNumber_ThrowsException()
    {
      _operationsDb.AddNumber("01000000000", 0);
      Assert.Throws<Exception>(() => _operationsDb.AddNumber("01000000000", 0));
    }

    [Fact]
    public void AddNumber_notValidNumber()
    {
      Assert.Throws<Exception>(() => _operationsDb.AddNumber("0102", 0));
      Assert.Throws<Exception>(() => _operationsDb.AddNumber("010000000000", 0));
      Assert.Throws<Exception>(() => _operationsDb.AddNumber("0167979799", 0));
      Assert.Throws<Exception>(() => _operationsDb.AddNumber("005616161", 0));
      Assert.Throws<Exception>(() => _operationsDb.AddNumber("+201116338319", 0));
      Assert.IsNotType<Exception>(() => _operationsDb.AddNumber("01145954251", 0));

    }

    [Fact]
    public void AddTransaction()
    {
        var transaction = new Transactions
      {
        NumberId = "01000000000",
        TransactionAmount = 10,
        CashBefore = 0,
        CashAfter = 10,
        Date = DateTime.Now
        ,TransactionId = Guid.Parse("00000000-0000-0000-0000-000000000000")
      };
      
      _operationsDb.AddNumber("01000000000", 0);
      _operationsDb.AddTransaction(transaction);
      Assert.Equal(1, _operationsDb.GetAllTransaction().Count());
      Assert.Equal("01000000000", _operationsDb.GetTransaction(Guid.Parse("00000000-0000-0000-0000-000000000000")).NumberId);
      Assert.Equal(10, _operationsDb.GetTransaction(Guid.Parse("00000000-0000-0000-0000-000000000000")).TransactionAmount);
      Assert.Equal(0, _operationsDb.GetTransaction(Guid.Parse("00000000-0000-0000-0000-000000000000")).CashBefore);
      Assert.Equal(10, _operationsDb.GetTransaction(Guid.Parse("00000000-0000-0000-0000-000000000000")).CashAfter);
    }


    [Fact]
    public void AddMultaple_Transactions ()
    {
      var transactions = new List<Transactions> 
      {
        new Transactions
        {
          NumberId = "01000000000",
          TransactionAmount = 10,
          CashBefore = 0,
          CashAfter = 10,
          Date = DateTime.Now
          ,TransactionId = Guid.Parse("00000000-0000-0000-0000-000000000000")
        },
        new Transactions
        {
          NumberId = "01000000000",
          TransactionAmount = 10,
          CashBefore = 10,
          CashAfter = 20,
          Date = DateTime.Now
          ,TransactionId = Guid.Parse("00000000-0000-0000-0000-000000000001")
        },
        new Transactions
        {
          NumberId = "01000000000",
          TransactionAmount = 10,
          CashBefore = 20,
          CashAfter = 30,
          Date = DateTime.Now
          ,TransactionId = Guid.Parse("00000000-0000-0000-0000-000000000002")
        }
      };
      
      _operationsDb.AddNumber("01000000000", 0);
      Assert.Equal(1, _operationsDb.GetAllNumber().Count());
      Assert.Equal(0, _operationsDb.GetNumber("01000000000").Amount);
      foreach (var transaction in transactions)
      {
        _operationsDb.AddTransaction(transaction);
      }
      Assert.Equal(3, _operationsDb.GetAllTransaction().Count());
      Assert.Equal("01000000000", _operationsDb.GetTransaction(Guid.Parse("00000000-0000-0000-0000-000000000000")).NumberId);
      Assert.Equal(10, _operationsDb.GetTransaction(Guid.Parse("00000000-0000-0000-0000-000000000000")).TransactionAmount);
      Assert.Equal(30, _operationsDb.GetNumber("01000000000").Amount);

    }

    [Fact]
    public void AddTransaction_ThrowsException()
    {
      var transaction = new Transactions
      {
        NumberId = "01000000000",
        TransactionAmount = 10,
        CashBefore = 0,
        CashAfter = 10,
        Date = DateTime.Now
        ,TransactionId = Guid.Parse("00000000-0000-0000-0000-000000000000")
      };
      
      
      
      Assert.Throws<Exception>(() => _operationsDb.AddTransaction(transaction));
      _operationsDb.AddNumber("01000000000", 0);
      transaction.TransactionAmount = 0;
      Assert.Throws<Exception>(() => _operationsDb.AddTransaction(transaction));
    }

    [Fact]
    public void deleteNumber (){
      _operationsDb.AddNumber("01000000000", 0);
      Assert.Equal(1, _operationsDb.GetAllNumber().Count());
      _operationsDb.DeleteNumber("01000000000");
      Assert.Equal(0, _operationsDb.GetAllNumber().Count());
    }

    [Fact]
    public void deleteNumber_ThrowsException (){
      Assert.Throws<Exception>(() => _operationsDb.DeleteNumber("01000000000"));
    }
    [Fact]
    public void deleteNumber_ThrowsException2 (){
      _operationsDb.AddNumber("01000000000", 0);
      Assert.Throws<Exception>(() => _operationsDb.DeleteNumber("01000000001"));
    }
    
    [Fact]
    public void deleteNumber_withTransactions()
    {
      _operationsDb.AddNumber("01000000000", 0);
      _operationsDb.AddTransaction(new Transactions
      {
        NumberId = "01000000000",
        TransactionAmount = 10,
        CashBefore = 0,
        CashAfter = 10,
        Date = DateTime.Now
        ,TransactionId = Guid.Parse("00000000-0000-0000-0000-000000000000")
      });
      _operationsDb.AddTransaction(new Transactions
      {
        NumberId = "01000000000",
        TransactionAmount = 10,
        CashBefore = 10,
        CashAfter = 20,
        Date = DateTime.Now
        ,TransactionId = Guid.Parse("00000000-0000-0000-0000-000000000001")
      });

      Assert.Equal(2, _operationsDb.GetAllTransaction().Count());
      Assert.Equal(1, _operationsDb.GetAllNumber().Count());
      _operationsDb.DeleteNumber("01000000000");
      Assert.Equal(0, _operationsDb.GetAllNumber().Count());
      Assert.Equal(0, _operationsDb.GetAllTransaction().Count());
      Assert.Throws<Exception>(() => _operationsDb.GetTransaction(Guid.Parse("00000000-0000-0000-0000-000000000000")));
    }

    [Fact]
    public void UpdateNumber () 
    {
      _operationsDb.AddNumber("01000000000", 0);
      Assert.Equal(1, _operationsDb.GetAllNumber().Count());
      Assert.Equal(0, _operationsDb.GetNumber("01000000000").Amount);
      _operationsDb.UpdateNumber("01000000000", 10);
      Assert.Equal(10, _operationsDb.GetNumber("01000000000").Amount);
    }


    [Fact]
    public void UpdateNumber_ThrowsException () 
    {
      Assert.Throws<Exception>(() => _operationsDb.UpdateNumber("01000000000", 10));
    }

    [Fact]
    public void UpdateNumber_ThrowsException2 () 
    {
      _operationsDb.AddNumber("01000000000", 0);
      Assert.Throws<Exception>(() => _operationsDb.UpdateNumber("01000000001", 10));
    }


    [Fact]
    public void Update_Transactions ()
    {
      _operationsDb.AddNumber("01000000000", 0);

      var transaction = new Transactions
      {
        NumberId = "01000000000",
        TransactionAmount = 10,
        CashBefore = 0,
        CashAfter = 10,
        Date = DateTime.Now
        ,TransactionId = Guid.Parse("00000000-0000-0000-0000-000000000000")
      };

      _operationsDb.AddTransaction(transaction);

      Assert.Equal(1, _operationsDb.GetAllTransaction().Count());
      Assert.Equal(10, _operationsDb.GetNumber("01000000000").Amount);
      transaction.TransactionAmount = 20;
      _operationsDb.UpdateTransaction(transaction);
      Assert.Equal(20, _operationsDb.GetTransaction(Guid.Parse("00000000-0000-0000-0000-000000000000")).TransactionAmount);
    }

    [Fact]
    public void Update_Transactions_ThrowsException ()
    {
      var transaction = new Transactions
      {
        NumberId = "01000000000",
        TransactionAmount = 10,
        CashBefore = 0,
        CashAfter = 10,
        Date = DateTime.Now
        ,TransactionId = Guid.Parse("00000000-0000-0000-0000-000000000000")
      };
      Assert.Throws<Exception>(() => _operationsDb.UpdateTransaction(transaction));
    }

    [Fact]
    public void Delete_Transaction()
    {
      var transaction = new Transactions
      {
        NumberId = "01000000000",
        TransactionAmount = 10,
        CashBefore = 0,
        CashAfter = 10,
        Date = DateTime.Now
        ,TransactionId = Guid.Parse("00000000-0000-0000-0000-000000000000")
      };
      _operationsDb.AddNumber("01000000000", 0);
      _operationsDb.AddTransaction(transaction);
      Assert.Equal(1, _operationsDb.GetAllTransaction().Count());
      _operationsDb.DeleteTransaction(Guid.Parse("00000000-0000-0000-0000-000000000000"));
      Assert.Equal(0, _operationsDb.GetAllTransaction().Count());
    }
    [Fact]
    public void Delete_Transaction_with_no_Number()
    {
      var transaction = new Transactions
      {
        NumberId = "01000000000",
        TransactionAmount = 10,
        CashBefore = 0,
        CashAfter = 10,
        Date = DateTime.Now
        ,TransactionId = Guid.Parse("00000000-0000-0000-0000-000000000000")
      };

      Assert.Throws<Exception>(() => _operationsDb.DeleteTransaction(Guid.Parse("00000000-0000-0000-0000-000000000000")));
    }
    [Fact]

    public void Delete_Transaction_ThrowError()
    {
      var transaction = new Transactions
      {
        NumberId = "01000000000",
        TransactionAmount = 10,
        CashBefore = 0,
        CashAfter = 10,
        Date = DateTime.Now
        ,TransactionId = Guid.Parse("00000000-0000-0000-0000-000000000000")
      };

      _operationsDb.AddNumber("01000000000", 0);
      _operationsDb.AddTransaction(transaction);
      Assert.Equal(1, _operationsDb.GetAllTransaction().Count());
      Assert.Throws<Exception>(() => _operationsDb.DeleteTransaction(Guid.Parse("00000000-0000-0000-0000-000000000001")));
    }

    [Fact]
    public void GetLastTransacion()
    {
      _operationsDb.AddNumber("01000000000", 0);
      _operationsDb.AddTransaction(new Transactions
      {
        NumberId = "01000000000",
        TransactionAmount = 10,
        CashBefore = 0,
        CashAfter = 10,
        Date = DateTime.Now
        ,TransactionId = Guid.Parse("00000000-0000-0000-0000-000000000000")
      });
      _operationsDb.AddTransaction(new Transactions
      {
        NumberId = "01000000000",
        TransactionAmount = 10,
        CashBefore = 10,
        CashAfter = 20,
        Date = DateTime.Now
        ,TransactionId = Guid.Parse("00000000-0000-0000-0000-000000000001")
      });

      Assert.Equal(2, _operationsDb.GetAllTransaction().Count());
      Assert.Equal(1, _operationsDb.GetAllNumber().Count());
      Assert.Equal(20, _operationsDb.GetLastTransaction("01000000000").CashAfter);
    }
    [Fact]
    public void GetLastTransacion_ThrowsException()
    {
      Assert.Throws<Exception>(() => _operationsDb.GetLastTransaction("01000000000"));
    }
    [Fact]
    public void GetTransactions_with_numbers()
    {
      _operationsDb.AddNumber("01000000000", 0);
      _operationsDb.AddTransaction(new Transactions
      {
        NumberId = "01000000000",
        TransactionAmount = 10,
        CashBefore = 0,
        CashAfter = 10,
        Date = DateTime.Now
        ,TransactionId = Guid.Parse("00000000-0000-0000-0000-000000000000")
      });
      _operationsDb.AddTransaction(new Transactions
      {
        NumberId = "01000000000",
        TransactionAmount = 10,
        CashBefore = 10,
        CashAfter = 20,
        Date = DateTime.Now
        ,TransactionId = Guid.Parse("00000000-0000-0000-0000-000000000001")
      });

      Assert.Equal(2, _operationsDb.GetAllTransaction().Count());
      Assert.Equal(1, _operationsDb.GetAllNumber().Count());
      Assert.Equal(20, _operationsDb.GetLastTransaction("01000000000").CashAfter);
      Assert.Equal(2, _operationsDb.GetTransactionsByNumber("01000000000").Count());
    }
  }
}