using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace api.Models
{
    public class DapperDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public DapperDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // method for postgreSql
        // public IDbConnection CreateConnection() => new NpgsqlConnection(connectionString);
        
        // method for sql server
        public IDbConnection CreateConnection() => new SqlConnection(connectionString);
    }
}