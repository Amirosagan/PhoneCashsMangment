using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VodafoneCashApi.Interfaces;

namespace VodafoneCashApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IOperationsDb _operationsDb;
        
        public TransactionsController(IOperationsDb operationsDb)
        {
            _operationsDb = operationsDb;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<IEnumerable<Models.Transactions>> Get()
        {
            return Ok(_operationsDb.GetAllTransaction());
        }

        [HttpPost]
        [Route("Add")]
        public ActionResult<Models.Transactions> Post(Models.Transactions transaction)
        {

            try
            {
                _operationsDb.AddTransaction(transaction);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


            return Ok(transaction);
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(Guid transactionId)
        {
            try
            {
                _operationsDb.DeleteTransaction(transactionId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();           
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult<Models.Transactions> Put(Models.Transactions transaction)
        {
            if(_operationsDb.GetTransaction(transaction.TransactionId) == null)
            {
                return BadRequest("Transaction does not exist");
            }

            if(_operationsDb.GetNumber(transaction.NumberId) == null)
            {
                return BadRequest("Number does not exist");
            }

            if(transaction.TransactionAmount == 0)
            {
                return BadRequest("Amount must be must not Equal 0");
            }

            try
            {
                _operationsDb.UpdateTransaction(transaction);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(transaction);
        }
        
    }
}