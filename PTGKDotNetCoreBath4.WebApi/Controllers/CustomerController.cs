using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTGKDotNetCoreBath4.RestApiWithNlayer.WebApi.Db;
using PTGKDotNetCoreBath4.RestApiWithNlayer.WebApi.Models;
namespace PTGKDotNetCoreBath4.RestApiWithNlayer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        //private readonly AppDbContext _db;

        //public CustomerController(AppDbContext db)
        //{
        //    _db = db;
        //}
        private readonly AppDbContext _db = new AppDbContext();

        [HttpGet]
        public IActionResult Read()
        {
            var list = _db.Customer.ToList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _db.Customer.FirstOrDefault(x => x.CustomerId == id);
            if (item is null)
            {
                return NotFound("Customer doesn't exist");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(CustomerModel customer)
        {
            _db.Customer.Add(customer);
            var result = _db.SaveChanges();

            var message = result > 0 ? "Saving Successful!" : "Saving Failed!";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CustomerModel customer)
        {
            var item = _db.Customer.FirstOrDefault(x => x.CustomerId == id);
            if (item is null)
            {
                return NotFound("No data found!");
            }
            item.CustomerName = customer.CustomerName;
            item.Address = customer.Address;
            item.Gender = customer.Gender;
            item.CustomerCode = customer.CustomerCode;
            item.PhoneNo = customer.PhoneNo;
            var result = _db.SaveChanges();

            var message = result > 0 ? "Updating Successful!" : "Updating Failed!";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, CustomerModel customer)
        {
            var item = _db.Customer.FirstOrDefault(x => x.CustomerId == id);
            if (item is null)
            {
                return NotFound("No data found");
            }
            if (!string.IsNullOrEmpty(customer.CustomerName))
            {
                item.CustomerName = customer.CustomerName;
            }
            if (!string.IsNullOrEmpty(customer.Address))
            {
                item.Address = customer.Address;
            }
            if (!string.IsNullOrEmpty(customer.Gender))
            {
                item.Gender = customer.Gender;
            }
            if (!string.IsNullOrEmpty(customer.CustomerCode))
            {
                item.CustomerCode = customer.CustomerCode;
            }
            if (!string.IsNullOrEmpty(customer.PhoneNo))
            {
                item.PhoneNo = customer.PhoneNo;
            }
            var result = _db.SaveChanges();

            var message = result > 0 ? "Updating Successful!" : "Updating Failed!";

            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _db.Customer.FirstOrDefault(x => x.CustomerId == id);
            if (item is null)
            {
                return NotFound("No data found");
            }
            _db.Customer.Remove(item);
            var result = _db.SaveChanges();

            var message = result > 0 ? "Deleting successful!" : "Deleting Failed";
            return Ok(message);
        }
    }
}
