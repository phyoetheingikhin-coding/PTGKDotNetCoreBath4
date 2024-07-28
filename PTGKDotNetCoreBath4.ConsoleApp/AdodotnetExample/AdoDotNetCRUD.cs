using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTGKDotNetCoreBath4.RestApiWithNlayer.ConsoleApp.AdodotnetExample
{
    public class AdoDotNetCRUD
    {
        public void Read()
        {
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=PractiseDb;User Id=sa;Password=sasa@123");
            Console.WriteLine("Connection is opening");
            connection.Open();
            Console.WriteLine("Connection is opened");
            string query = "select * from Tbl_Customer";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();
            Console.WriteLine("Connection is closed!");
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("CustomerId=>" + dr["CustomerId"]);
                Console.WriteLine("CustomerName=>" + dr["CustomerName"]);
                Console.WriteLine("PhoneNo=>" + dr["PhoneNo"]);
                Console.WriteLine("Address=>" + dr["Address"]);
                Console.WriteLine("Gender=>" + dr["Gender"]);
                Console.WriteLine("CustomerCode=>" + dr["CustomerCode"]);
                Console.WriteLine("------------------------------------------");
            }
        }
        public void Create(string name, string phoneno, string address, string gender, string code)
        {
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=PractiseDb;User Id=sa;Password=sasa@123");
            Console.WriteLine("Connection is opening");
            connection.Open();
            Console.WriteLine("Connection is opened");
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
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CustomerName", name);
            cmd.Parameters.AddWithValue("@PhoneNo", phoneno);
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@Gender", gender);
            cmd.Parameters.AddWithValue("@CustomerCode", code);

            int result = cmd.ExecuteNonQuery();
            connection.Close();
            var message = result > 0 ? "Saving Successful!" : "Saving Failed!";
            Console.WriteLine(message);
        }
        public void Update(int id, string name, string phoneno, string address, string gender, string code)
        {
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=PractiseDb;User Id=sa;Password=sasa@123");
            Console.WriteLine("Connection is opening");
            connection.Open();
            string query = @"UPDATE [dbo].[Tbl_Customer]
            SET [CustomerName] = @CustomerName
                ,[PhoneNo] =@PhoneNo
                ,[Address] =@Address
                ,[Gender] = @Gender
                ,[CustomerCode] = @CustomerCode
            WHERE CustomerId=@CustomerId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CustomerId", id);
            cmd.Parameters.AddWithValue("@CustomerName", name);
            cmd.Parameters.AddWithValue("@PhoneNo", phoneno);
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@Gender", gender);
            cmd.Parameters.AddWithValue("@CustomerCode", code);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            var message = result > 0 ? "Updating Successful!" : "Updadting Failed!";
            Console.WriteLine(message);
        }
        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=PractiseDb;User Id=sa;Password=sasa@123");
            connection.Open();
            Console.WriteLine("Connection is opened");
            string query = @"DELETE FROM [dbo].[Tbl_Customer]
             WHERE CustomerId=@CustomerId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CustomerId", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            var message = result > 0 ? "Deleting Successful!" : "Deleting Failed!";
            Console.WriteLine(message);
        }
        public void Edit(int id)
        {
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=PractiseDb;User Id=sa;Password=sasa@123");
            connection.Open();
            string query = "select * from Tbl_Customer where CustomerId=@CustomerId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CustomerId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();
            Console.WriteLine("Connection is closed!");
            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found");
                return;
            }
            DataRow dr = dt.Rows[0];
            Console.WriteLine("CustomerId=>" + dr["CustomerId"]);
            Console.WriteLine("CustomerName=>" + dr["CustomerName"]);
            Console.WriteLine("PhoneNo=>" + dr["PhoneNo"]);
            Console.WriteLine("Address=>" + dr["Address"]);
            Console.WriteLine("Gender=>" + dr["Gender"]);
            Console.WriteLine("CustomerCode=>" + dr["CustomerCode"]);
            Console.WriteLine("------------------------------------------");


        }
    }
}
