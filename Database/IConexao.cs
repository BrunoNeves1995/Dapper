using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Dapper.Database
{
    public interface IConexao
    {
        public SqlConnection AbrirConexao();
        public void FecharConexao();
    }
}