using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTGKDotNetCoreBath4.WebApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace PTGKDotNetCoreBath4.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerDapperController : ControllerBase
{
    [HttpGet]
    public IActionResult Read()
    {
        string query = "select * from Tbl_Customer";
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        List<CustomerModel> lst = db.Query<CustomerModel>(query).ToList();
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
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        var result = db.Execute(query, customer);
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
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        var result = db.Execute(query, customer);
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
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        var result = db.Execute(query, customer);

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

        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        int result = db.Execute(query, item);

        var message = result > 0 ? "Deleting Successful!" : "Deleting Failed!";
        return Ok(message);
    }

    private CustomerModel? FindById(int id)
    {
        string query = "select * from Tbl_Customer where CustomerId = @CustomerId";
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        var item = db.Query<CustomerModel>(query, new CustomerModel { CustomerId = id }).FirstOrDefault();
        return item;
    }
}
