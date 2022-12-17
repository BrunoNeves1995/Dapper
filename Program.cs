using System;
using System.Data;
using Dapper.Database;
using Dapper.Models;
using Microsoft.Data.SqlClient;

namespace Dapper
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var categoryInert = new Category();

            categoryInert.Id = Guid.NewGuid();
            categoryInert.Title = "Java";
            categoryInert.Url = "Java";
            categoryInert.Description = "Categoria de serviços da Java";
            categoryInert.Order = 13;
            categoryInert.Summary = "Java";
            categoryInert.Featured = false;

            IConexao conexao = new Conexao();

            using (var conn = conexao.AbrirConexao())
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;

                    try
                    {

                        // insert 
                        var insertSql = @$"INSERT INTO 
                    [Category]
                        VALUES
                        (  
                            @Id,  @Title, @Url,  @Summary, @Order, @Description, @Featured

                        )";

                        command.Parameters.AddWithValue("@Id", categoryInert.Id);
                        command.Parameters.AddWithValue("@Title", categoryInert.Title);
                        command.Parameters.AddWithValue("@Url", categoryInert.Url);
                        command.Parameters.AddWithValue("@Summary", categoryInert.Summary);
                        command.Parameters.AddWithValue("@Order", categoryInert.Order);
                        command.Parameters.AddWithValue("@Description", categoryInert.Description);
                        command.Parameters.AddWithValue("@Featured", categoryInert.Featured);

                        command.CommandText = insertSql;
                        var linhasAfetadas = command.ExecuteNonQuery();
                        Console.WriteLine($"{linhasAfetadas} registr afetas");

                        // select
                        var selectSql = "SELECT [Id], [Title] FROM [Category] WHERE Id = @Id ORDER BY 2";
                        command.CommandText = selectSql;
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var category = new Category();

                            category.Id = reader.GetGuid(0);
                            category.Title = reader.GetString(1);
                            Console.WriteLine($"id: {category.Id}, title: {category.Title}");
                        }
                    }
                    catch (System.Exception ex)
                    {

                        Console.WriteLine($"Erro ao cadastrar categoria");
                        Console.WriteLine($"Mensagem: {ex.Message}");
                    }
                    conexao.FecharConexao();
                }

            }

        }
    }

}

