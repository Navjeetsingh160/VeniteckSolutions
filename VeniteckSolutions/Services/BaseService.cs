using Microsoft.Data.SqlClient;
using System.Data;

namespace VeniteckSolutions.Services
{
    public abstract class BaseService
    {
        private readonly IConfiguration _configuration;

        protected BaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected async Task ExecuteStoredProcedureAsync(string storedProcedureName, Action<SqlCommand> configureCommand)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (var command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                   
                    configureCommand(command);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
