using Dapper;
using PTGKDotNetCoreBath4.ConsoleApp.Dto;
using PTGKDotNetCoreBath4.ConsoleApp.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTGKDotNetCoreBath4.ConsoleApp.DapperExample;

public class DapperCRUD
{
    public void Run()
    {
        //Read();
        //Edit(3);
        //Edit(22);
        //Create("Alex", "0912344567", "NewZeland", "Male", "C00012");
        //Update(11, "John", "0912344569", "NewZeland", "Male", "C00012");
        //Update(109, "John", "0912344569", "NewZeland", "Male", "C00012");
        Delete(11);
        Delete(100);

    }
    public void Read()
    {
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        List<CustomerDto> lst = db.Query<CustomerDto>("select * from Tbl_Customer").ToList();
        foreach (CustomerDto item in lst)
        {
            Console.WriteLine(item.CustomerId);
            Console.WriteLine(item.CustomerName);
            Console.WriteLine(item.Gender);
            Console.WriteLine(item.Address);
            Console.WriteLine(item.CustomerCode);
            Console.WriteLine(item.PhoneNo);
            Console.WriteLine("_______________________________________________________");
        }
        //using (IDbConnection db1 = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString))
        //{
        //    db1.Open();
        //};
        //db1.Open();
    }
    public void Edit(int id)
    {
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        var item = db.Query("select * from Tbl_Customer where CustomerId = @CustomerId", new CustomerDto { CustomerId = id }).FirstOrDefault();
        if (item is null)
        {
            Console.WriteLine("No data found");
            return;
        }
        Console.WriteLine(item.CustomerId);
        Console.WriteLine(item.CustomerName);
        Console.WriteLine(item.Gender);
        Console.WriteLine(item.Address);
        Console.WriteLine(item.CustomerCode);
        Console.WriteLine(item.PhoneNo);
    }
    public void Create(string name, string phone, string address, string gender, string code)
    {
        var item = new CustomerDto
        {
            CustomerName = name,
            PhoneNo = phone,
            Address = address,
            Gender = gender,
            CustomerCode = code
        };

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
        var result = db.Execute(query, item);
        //if(result > 0)
        //{
        //    Console.WriteLine("Saving Successful!");
        //    return;
        //}
        //else
        //{
        //    Console.WriteLine("Saving Failed!");
        //    return;
        //}
        var message = result > 0 ? "Saving Successful!" : "Saving Failed!";
        Console.WriteLine(message);
    }
    public void Delete(int id)
    {
        var item = new CustomerDto { CustomerId = id };

        string query = @"DELETE FROM [dbo].[Tbl_Customer]
      WHERE CustomerId=@CustomerId";

        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        int result = db.Execute(query, item);

        var message = result > 0 ? "Deleting Successful!" : "Deleting Failed!";
        Console.WriteLine(message);
    }

    public void Update(int id, string name, string phone, string address, string gender, string code)
    {
        var item = new CustomerDto
        {
            CustomerId = id,
            CustomerName = name,
            Address = address,
            Gender = gender,
            CustomerCode = code,
            PhoneNo = phone
        };

        string query = @"UPDATE [dbo].[Tbl_Customer]
   SET [CustomerName] = @CustomerName
      ,[PhoneNo] = @PhoneNo
      ,[Address] = @Address
      ,[Gender] = @Gender
      ,[CustomerCode] = @CustomerCode
 WHERE CustomerId = @CustomerId";

        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        var result = db.Execute(query, item);

        var message = result > 0 ? "Updating Successful!" : "Updating Failed!";
        Console.WriteLine(message);
    }
}
