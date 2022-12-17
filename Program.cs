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
            categoryInert.Order = 12;
            categoryInert.Summary = "Java";
            categoryInert.Featured = false;

            IConexao conexao = new Conexao();
            SqlCommand command;
            SqlDataReader reader;

            var insertSql = @$"INSERT INTO 
                    [Category]
                        VALUES
                        (   
                            @Id,
                            @Title,
                            @Url,
                            @Summary,
                            @Order,
                            @Description,
                            @Featured
                        )";

            
            try
            {   
                command = new SqlCommand(insertSql, conexao.AbrirConexao());
                command.Parameters.AddWithValue("@Id", categoryInert.Id);
                command.Parameters.AddWithValue("@Title", categoryInert.Title);
                command.Parameters.AddWithValue("@Url", categoryInert.Url);
                command.Parameters.AddWithValue("@Summary", categoryInert.Summary);
                command.Parameters.AddWithValue("@Order", categoryInert.Order);
                command.Parameters.AddWithValue("@Description", categoryInert.Description);
                command.Parameters.AddWithValue("@Featured", categoryInert.Featured);
                var linhaAfetadas = command.ExecuteNonQuery();
                Console.WriteLine($"Linhas afetadas -  {linhaAfetadas}");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"E501 - Erro ao cadastrar categoria");
                Console.WriteLine($"Mensagem: {ex.Message}");
            }

            var selectSql = "SELECT [Id], [Title] FROM [Category] ORDER BY 2";
            

            try
            {   
                command = new SqlCommand(selectSql, conexao.AbrirConexao());
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var categorySelect = new Category();

                    categorySelect.Id = reader.GetGuid(0);
                    categorySelect.Title = reader.GetString(1);

                    Console.WriteLine($"id: {categorySelect.Id}, title: {categorySelect.Title}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"E502 - Erro ao buscar categoria");
                Console.WriteLine($"Mensagem: {ex.Message}");
            }



            conexao.FecharConexao();

        }
    }
}