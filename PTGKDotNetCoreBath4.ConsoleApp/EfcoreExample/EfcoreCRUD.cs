using PTGKDotNetCoreBath4.ConsoleApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTGKDotNetCoreBath4.ConsoleApp.EfcoreExample
{
    internal class EfcoreCRUD
    {
        private readonly AppDbContext db = new AppDbContext();
        public void Run()
        {
            //Read();
            //Edit(3);
            //Edit(300);
            //Update(2, "Tonny", "0912345677", "New Yourk", "Male", "C0033");
            //Update(200, "Tonny", "0912345677", "New Yourk", "Male", "C0033");
            //Create("Johny", "0912345666", "New Yourk", "Female", "C00333");
            Delete(13);
            Delete(14);

        }
        private void Read()
        {
            var list = db.Customer.ToList();
            foreach (var item in list)
            {
                Console.WriteLine(item.CustomerId);
                Console.WriteLine(item.CustomerName);
                Console.WriteLine(item.Gender);
                Console.WriteLine(item.Address);
                Console.WriteLine(item.CustomerCode);
                Console.WriteLine(item.PhoneNo);
                Console.WriteLine("_______________________________________________________");
            }
        }
        private void Edit(int id)
        {
            var item = db.Customer.Find(id);
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
        private void Create(string name, string phone, string address, string gender, string code)
        {
            var customer = new CustomerDto
            {
                CustomerName = name,
                PhoneNo = phone,
                Address = address,
                Gender = gender,
                CustomerCode = code

            };

            db.Customer.Add(customer);
            var result = db.SaveChanges();

            var message = result > 0 ? "Saving Successful!" : "Saving Failed!";
            Console.WriteLine(message);
        }
        private void Update(int id, string name, string phone, string address, string gender, string code)
        {
            var item = db.Customer.Find(id);
            if (item is null)
            {
                Console.WriteLine("Customer Does not exist");
                return;
            }

            item.CustomerCode = code;
            item.CustomerName = name;
            item.Gender = gender;
            item.Address = address;
            item.PhoneNo = phone;
           
            var result = db.SaveChanges();

            var message = result > 0 ? "Updating Successful!" : "Updating Failed!";
            Console.WriteLine(message);
        }
        private void Delete(int id)
        {
            var item = db.Customer.FirstOrDefault(x => x.CustomerId == id);
            if (item is null)
            {
                Console.WriteLine("Customer Does Not Exist");
                return;
            }

            db.Remove(item);
            var result =db.SaveChanges();

            var message = result > 0 ? "Deleting Successful!" : "Deleting Failed!";
            Console.WriteLine(message);
        }
    }
}
