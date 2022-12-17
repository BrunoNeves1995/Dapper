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
            cadastrarCategoria();
            // ListarCategorias();
            // AtualizarUmaCategoria();

        }


        static void cadastrarCategoria()
        {
            var categoryInert = new Category();

            categoryInert.Id = Guid.NewGuid();
            categoryInert.Title = "trasaction";
            categoryInert.Url = "trasaction";
            categoryInert.Description = "Categoria de serviços da trasaction";
            categoryInert.Order = 15;
            categoryInert.Summary = "trasaction";
            categoryInert.Featured = true;

            IConexao conexao = new Conexao();
            SqlTransaction transaction;

            using (var conn = conexao.AbrirConexao())
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    transaction = conn.BeginTransaction();
                    command.Transaction = transaction;

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
                        // select
                        var selectSql = "SELECT [Id], [Title] FROM [Category] WHERE Id = @Id ORDER BY 2";
                        command.CommandText = selectSql;
                        SqlDataReader reader = command.ExecuteReader();
                        
    
                        while (reader.Read())
                        {
                            var category = new Category();

                            category.Id = reader.GetGuid(0);
                            category.Title = reader.GetString(1);
                            Console.WriteLine($"Registro atualizado, id: {category.Id}, title: {category.Title}");
                        }
                        reader.Close();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"E501 - Erro ao cadastrar categoria, as operações serão todas desfeitas.");
                        Console.WriteLine($"Mensagem: {ex.Message}");
                        transaction.Rollback();
                    }
                    conexao.FecharConexao();
                }

            }

        }

        static void ListarCategorias()
        {
            IConexao conexao = new Conexao();

            using (var con = conexao.AbrirConexao())
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = con;
                    try
                    {
                        var selectSql = "SELECT [Id], [Title], [Featured] FROM [Category] ORDER BY [Order]";
                        command.CommandText = selectSql;
                        SqlDataReader reader = command.ExecuteReader();
                        

                        while (reader.Read())
                        {
                            var categoria = new Category();

                            categoria.Id = reader.GetGuid(0);
                            categoria.Title = reader.GetString(1);
                            categoria.Featured = reader.GetBoolean(2);
                            Console.WriteLine($"id: {categoria.Id}, Titulo: {categoria.Title}, Faturado: {categoria.Featured}");
                        }

                    }
                    catch (System.Exception ex)
                    {

                        Console.WriteLine($"E502 - Erro ao buscar as categorias");
                        Console.WriteLine($"Mensagem: {ex.Message}");
                    }
                }
                conexao.FecharConexao();
            }
        }


        static void AtualizarUmaCategoria()
        {
            IConexao conexao = new Conexao();
            SqlTransaction trans;
           
            using (var con = conexao.AbrirConexao())
            {   
                ;  
                using (SqlCommand command = new SqlCommand())
                {   
                    
                    command.Connection = con;
                    trans = con.BeginTransaction();
                    command.Transaction = trans;

                    var categoria = new Category();
                    categoria.Featured = true;

                    categoria.Id = new Guid("09ce0b7b-cfca-497b-92c0-3290ad9d5142");
                    try
                    {   
                        var selectSql = "SELECT [Id] FROM [Category] WHERE Id = @Id";
                        
                        command.Parameters.AddWithValue("@Id", categoria.Id);
                        
                        command.CommandText = selectSql;
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var category = new Category();

                            category.Id = reader.GetGuid(0);
                            Console.WriteLine($"Id: {categoria.Id}");
                        }
                        reader.Close();

                        var updateSql = @"
                        UPDATE [Balta].[dbo].[Category] 
                            SET [Featured] = @Featured 
                        WHERE 
                            [Id] = @Id";

                        
                        command.Parameters.AddWithValue("@Featured", categoria.Featured);

                        command.CommandText = updateSql;
                        var linhasAfetadas = command.ExecuteNonQuery();
                        
                        Console.WriteLine($"{linhasAfetadas} registro atualizado");
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine($"E502 - Erro ao Atualizar registro");
                        Console.WriteLine($"Mensagem: {ex.Message}");
                        trans.Rollback();

                    }
                }
                trans.Commit();
                conexao.FecharConexao();
            }
        }

    }

}

