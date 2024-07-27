using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTGKDotNetCoreBath4.Shared;
using PTGKDotNetCoreBath4.WebApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace PTGKDotNetCoreBath4.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerDapper2Controller : ControllerBase
{
    private readonly DapperService _dapperService = new DapperService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
    [HttpGet]
    public IActionResult Read()
    {
        string query = "select * from Tbl_Customer";
        var lst = _dapperService.Query<CustomerModel>(query).ToList();
        return Ok(lst);
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var item = FindById(id);
        if (item is null)
        {
            return NotFound("No data found.");
        }
        return Ok(item);
    }

    [HttpPost]
    public IActionResult Create(CustomerModel customer)
    {
        string query = @"INSERT INTO [dbo].[Tbl_Customer]
          ([CustomerName]
          ,[PhoneNo]
          ,[Address]
          ,[Gender]
          ,[CustomerCode])
    VALUES
          (@CustomerName
          ,@PhoneNo
          ,@Address
          ,@Gender
          ,@CustomerCode)";

        var result = _dapperService.Execute(query, customer);
        var message = result > 0 ? "Saving Successful!" : "Saving Failed!";
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, CustomerModel customer)
    {
        var item = FindById(id);
        if (item is null)
        {
            return NotFound("No data found.");
        }
        string query = @"UPDATE [dbo].[Tbl_Customer]
                      SET [CustomerName] = @CustomerName
                         ,[PhoneNo] = @PhoneNo
                         ,[Address] = @Address
                         ,[Gender] = @Gender
                         ,[CustomerCode] = @CustomerCode
                    WHERE CustomerId = @CustomerId";
        customer.CustomerId = id;

        var result = _dapperService.Execute(query,customer);
        var message = result > 0 ? "Updating Successful!" : "Updating Failed!";
        return Ok(message);
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, CustomerModel customer)
    {
        var item = FindById(id);
        if (item is null)
        {
            return NotFound("No data found.");
        }
        string condition = string.Empty;
        if (!string.IsNullOrEmpty(customer.CustomerName))
        {
            condition += " [CustomerName] = @CustomerName, ";
        }
        if (!string.IsNullOrEmpty(customer.Gender))
        {
            condition += " [Gender] = @Gender, ";
        }
        if (!string.IsNullOrEmpty(customer.Address))
        {
            condition += " [Address] = @Address, ";
        }
        if (!string.IsNullOrEmpty(customer.CustomerCode))
        {
            condition += " [CustomerCode] = @CustomerCode, ";
        }
        if (!string.IsNullOrEmpty(customer.PhoneNo))
        {
            condition += " [PhoneNo] = @PhoneNo, ";
        }
        if (condition.Length == 0)
        {
            return NotFound("No data to update.");
        }
        condition = condition.Substring(0, condition.Length - 2);
        customer.CustomerId = id;

        string query = $@"UPDATE [dbo].[Tbl_Customer]
                      SET {condition}
                      WHERE CustomerId = @CustomerId";

        var result = _dapperService.Execute(query, customer);
        var message = result > 0 ? "Updating Successful!" : "Updating Failed!";
        return Ok(message);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = FindById(id);
        if (item is null)
        {
            return NotFound("No data found.");
        }
        string query = @"DELETE FROM [dbo].[Tbl_Customer]
                           WHERE CustomerId=@CustomerId";

        var result = _dapperService.Execute(query, item);
        var message = result > 0 ? "Deleting Successful!" : "Deleting Failed!";
        return Ok(message);
    }

    private CustomerModel? FindById(int id)
    {
        string query = "select * from Tbl_Customer where CustomerId = @CustomerId";
        var item = _dapperService.QueryFirstOrDefault<CustomerModel>(query, new CustomerModel { CustomerId = id });
        return item;
    }
}
