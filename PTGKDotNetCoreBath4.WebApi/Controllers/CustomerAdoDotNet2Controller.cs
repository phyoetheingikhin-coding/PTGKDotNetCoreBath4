using Microsoft.AspNetCore.Mvc;
using PTGKDotNetCoreBath4.Shared;
using PTGKDotNetCoreBath4.WebApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace PTGKDotNetCoreBath4.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAdoDotNet2Controller : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);

        [HttpGet]
        public IActionResult Read()
        {
            string query = "select * from Tbl_Customer";
            var lst = _adoDotNetService.Query<CustomerModel>(query);

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            string query = "select * from Tbl_Customer where CustomerId=@CustomerId";

            //AdoDotNetParameter[] parameters = new AdoDotNetParameter[1];
            //parameters[0] = new AdoDotNetParameter("@CustomerId", id);
            //var item = _adoDotNetService.Query<CustomerModel>(query, parameters);

            var item = _adoDotNetService.QueryFirstOrDefault<CustomerModel>(query, new AdoDotNetParameter("@CustomerId", id));

            if (item is null)
            {
                return NotFound("No data found");
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

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@CustomerName", customer.CustomerName),
                new AdoDotNetParameter("@PhoneNo", customer.PhoneNo),
                new AdoDotNetParameter("@Address", customer.Address),
                new AdoDotNetParameter("@Gender", customer.Gender),
                new AdoDotNetParameter("@CustomerCode", customer.CustomerCode)
                ); 
            var message = result > 0 ? "Saving Successful!" : "Saving Failed!";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CustomerModel customer)
        {
           // string getQuery = "select count(*) from Tbl_Customer where CustomerId=@CustomerId";
            string getQuery = "select * from Tbl_Customer where CustomerId=@CustomerId";

            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(getQuery, connection);
            cmd.Parameters.AddWithValue("@CustomerId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                return NotFound("No data found");
            }
            //var count = (int) cmd.ExecuteScalar();

            // if (count == 0)
            // {
            //     return NotFound("No data found");
            // }

            string updateQuery = @"UPDATE [dbo].[Tbl_Customer]
            SET [CustomerName] = @CustomerName
                ,[PhoneNo] =@PhoneNo
                ,[Address] =@Address
                ,[Gender] = @Gender
                ,[CustomerCode] = @CustomerCode
            WHERE CustomerId=@CustomerId";
            SqlCommand updatecmd = new SqlCommand(updateQuery, connection);
            updatecmd.Parameters.AddWithValue("@CustomerId", id);
            updatecmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
            updatecmd.Parameters.AddWithValue("@PhoneNo", customer.PhoneNo);
            updatecmd.Parameters.AddWithValue("@Address", customer.Address);
            updatecmd.Parameters.AddWithValue("@Gender", customer.Gender);
            updatecmd.Parameters.AddWithValue("@CustomerCode", customer.CustomerCode);
            int result = updatecmd.ExecuteNonQuery();
            connection.Close();

            var message = result > 0 ? "Updating Successful!" : "Updadting Failed!";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, CustomerModel customer)
        {
            string getQuery = "select * from Tbl_Customer where CustomerId=@CustomerId";

            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(getQuery, connection);
            cmd.Parameters.AddWithValue("@CustomerId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                return NotFound("No data found");
            }
            //var count = (int)cmd.ExecuteScalar();

            //if (count == 0)
            //{
            //    return NotFound("No data found");
            //}

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
            string updateQuery = $@"UPDATE [dbo].[Tbl_Customer]
                                SET {condition}
                                WHERE CustomerId=@CustomerId";

            SqlCommand updatecmd = new SqlCommand(updateQuery, connection);
            updatecmd.Parameters.AddWithValue("@CustomerId", id);
            updatecmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
            updatecmd.Parameters.AddWithValue("@PhoneNo", customer.PhoneNo);
            updatecmd.Parameters.AddWithValue("@Address", customer.Address);
            updatecmd.Parameters.AddWithValue("@Gender", customer.Gender);
            updatecmd.Parameters.AddWithValue("@CustomerCode", customer.CustomerCode);
            int result = updatecmd.ExecuteNonQuery();
            connection.Close();

            var message = result > 0 ? "Updating Successful!" : "Updadting Failed!";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string getQuery = "select * from Tbl_Customer where CustomerId=@CustomerId";

            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(getQuery, connection);
            cmd.Parameters.AddWithValue("@CustomerId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                return NotFound("No data found");
            }
            //var count = (int)cmd.ExecuteScalar();

            //if (count == 0)
            //{
            //    return NotFound("No data found");
            //}

            string query = @"DELETE FROM [dbo].[Tbl_Customer]
             WHERE CustomerId=@CustomerId";
            SqlCommand deleteCmd = new SqlCommand(query, connection);
            deleteCmd.Parameters.AddWithValue("@CustomerId", id);
            int result = deleteCmd.ExecuteNonQuery();
            connection.Close();

            var message = result > 0 ? "Deleting Successful!" : "Deleting Failed!";
            return Ok(message);
        }
    }
}
