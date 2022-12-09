using System;
using Microsoft.Data.SqlClient;

namespace Dapper 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string conectString
            = @"Server=TERMINAL01\SQLEXPRESS;Database=Balta;User ID=admin;Password=12345;Trusted_Connection=false;TrustServerCertificate=true";


            var connection = new SqlConnection(conectString);
            connection.Open();
              
              


            
            connection.Close();
            Console.ReadKey();
        }
    }
}