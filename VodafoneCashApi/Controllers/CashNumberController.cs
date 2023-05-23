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
    public class CashNumberController : ControllerBase
    {
        private readonly IOperationsDb _dataBase;

        public CashNumberController(IOperationsDb dataBase)
        {
            _dataBase = dataBase;
        }



        [HttpGet]
        public ActionResult<IEnumerable<Models.Numbers>> Get()
        {
            return _dataBase.GetAllNumber().ToList();
        }

        [HttpPost]
        [Route("Add")]
        public  ActionResult<Models.NumbersViewModel> Post(Models.NumbersViewModel number)
        {

            try
            {
                _dataBase.AddNumber(number.Number, number.Amount);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(number);
        }

        


        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(string number)
        {

            try
            {
                _dataBase.DeleteNumber(number);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult<Models.NumbersViewModel> Update(Models.NumbersViewModel number)
        {
            try
            {
                _dataBase.UpdateNumber(number.Number, number.Amount);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(number);
        }
    }
}