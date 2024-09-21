// See https://aka.ms/new-console-template for more information
using PTGKDotNetCoreBath4.RestApiWithNlayer.ConsoleApp.AdodotnetExample;
using PTGKDotNetCoreBath4.RestApiWithNlayer.ConsoleApp.DapperExample;
using PTGKDotNetCoreBath4.RestApiWithNlayer.ConsoleApp.EfcoreExample;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");
AdoDotNetCRUD adoDotNetCRUD = new AdoDotNetCRUD();
adoDotNetCRUD.SampleTransaction();
//adoDotNetCRUD.Read();
//adoDotNetCRUD.Create("Aye Aye","09364522868","Yangon","Female","C000230");
//adoDotNetCRUD.Read();
//adoDotNetCRUD.Update(3, "Aye Aye", "09364522868", "Yangon", "Female", "C000230");
//adoDotNetCRUD.Update(333, "Aye Aye", "09364522868", "Yangon", "Female", "C000230");
//adoDotNetCRUD.Delete(1);
//adoDotNetCRUD.Delete(72);
Console.ReadKey();

//DapperCRUD dapperCRUD = new DapperCRUD();
//dapperCRUD.Run();
//EfcoreCRUD efcoreCRUD = new EfcoreCRUD();
//efcoreCRUD.Run();

