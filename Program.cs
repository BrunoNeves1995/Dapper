﻿using System;
using Dapper.Models;
using Microsoft.Data.SqlClient;

namespace Dapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string conectString
            = @"Server=TERMINAL01\SQLEXPRESS;Database=Balta;User ID=admin;Password=12345;Trusted_Connection=false;TrustServerCertificate=true";


            using (var connection = new SqlConnection(conectString))
            {   connection.Open();
                Console.WriteLine("Conectado ao banco");

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT [Id], [Title] FROM [Category] ORDER BY 2";

                    // Executa o  command.CommandText
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var category = new Category
                        (
                            reader.GetGuid(0),
                            reader.GetString(1)
                        );

                        Console.WriteLine($"id: {category.Id}, title: {category.Title}");
                    }
                }

                connection.Close();
            }





        }
    }
}