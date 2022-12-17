using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Dapper.Database
{
    public class Conexao : IConexao
    {
         const string stringConnection
            = @"Server=TERMINAL01\SQLEXPRESS;Database=Balta;User ID=admin;Password=12345;Trusted_Connection=false;TrustServerCertificate=true";

        public SqlConnection? conexao;

        public SqlConnection AbrirConexao()
        {   
            
            try
            {
                conexao = new SqlConnection(stringConnection);
                conexao.Open();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("M01 - Conectado ao banco");
                Console.ResetColor();
                return conexao;

            }
            catch (System.Exception ex)
            {   
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("E500 - Erro interno do servidor");
                Console.ResetColor();
                Console.WriteLine($"Mensagem do erro: {ex.Message}");
                
            }
            
            return conexao!;
        }

        public void FecharConexao()
        {
            conexao = new SqlConnection(stringConnection);
            conexao.Close();
            conexao.Dispose();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"M03 - Conecxão foi encerrada");
            Console.ResetColor();
        
        }
    }
}