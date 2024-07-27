using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTGKDotNetCoreBath4.WebApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Reflection;

namespace PTGKDotNetCoreBath4.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAdoDotNetController : ControllerBase
    {
        [HttpGet]
        public IActionResult Read()
        {
            string query = "select * from Tbl_Customer";

            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();

            //List<CustomerModel> lst = new List<CustomerModel>();
            //foreach ( DataRow dr in dt.Rows)
            //{
            //    CustomerModel customer = new CustomerModel
            //    {
            //        CustomerId = Convert.ToInt32(dr["CustomerId"]),
            //        CustomerName = Convert.ToString(dr["Name"]),
            //        Gender = Convert.ToString(dr["Gender"]),
            //        Address = Convert.ToString(dr["Address"]),
            //        PhoneNo = Convert.ToString(dr["PhoneNo"]),
            //        CustomerCode = Convert.ToString(dr["CustomerCode"])
            //    };

            //    lst.Add(customer);
            //}
            List<CustomerModel> lst = dt.AsEnumerable().Select(dr => new CustomerModel
            {
                CustomerId = Convert.ToInt32(dr["CustomerId"]),
                CustomerName = Convert.ToString(dr["CustomerName"]),
                Gender = Convert.ToString(dr["Gender"]),
                Address = Convert.ToString(dr["Address"]),
                PhoneNo = Convert.ToString(dr["PhoneNo"]),
                CustomerCode = Convert.ToString(dr["CustomerCode"])
            }).ToList();

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            string query = "select * from Tbl_Customer where CustomerId=@CustomerId";

            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CustomerId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();

            if (dt.Rows.Count == 0)
            {
                return NotFound("No data found");
            }

            DataRow dr = dt.Rows[0];
            var item = new CustomerModel
            {
                CustomerId = Convert.ToInt32(dr["CustomerId"]),
                CustomerName = Convert.ToString(dr["CustomerName"]),
                Gender = Convert.ToString(dr["Gender"]),
                Address = Convert.ToString(dr["Address"]),
                PhoneNo = Convert.ToString(dr["PhoneNo"]),
                CustomerCode = Convert.ToString(dr["CustomerCode"])
            };

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

            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
            cmd.Parameters.AddWithValue("@PhoneNo", customer.PhoneNo);
            cmd.Parameters.AddWithValue("@Address", customer.Address);
            cmd.Parameters.AddWithValue("@Gender", customer.Gender);
            cmd.Parameters.AddWithValue("@CustomerCode", customer.CustomerCode);

            int result = cmd.ExecuteNonQuery();
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
