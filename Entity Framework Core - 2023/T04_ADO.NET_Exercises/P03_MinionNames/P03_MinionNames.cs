using Microsoft.Data.SqlClient;
using P03_MinionNames;

internal class Program
{

    const string connectionString = "Server=.;Database=T04_MinionsDB;User Id=sa;Password=N45tejvWcK;TrustServerCertificate=True;";
    static SqlConnection? connection;

    static async Task Main(string[] args)
    {
        try
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            await GetOrderedMinionsByVillainId(1);
        }
        finally
        {
            connection?.Close();
        }
    }

    static async Task GetOrderedMinionsByVillainId(int id)
    {
        using SqlCommand command = new SqlCommand(SqlQueries.getVillainById, connection);
        command.Parameters.AddWithValue("@Id", id);
        var result = await command.ExecuteScalarAsync();

        if (result is null)
        {
            await Console.Out.WriteLineAsync($"No villain with ID {id} exists in the database.");
        }
        else
        {
            await Console.Out.WriteLineAsync($"Vallain: {result}");

            using SqlCommand commandGetMinionData = new SqlCommand(SqlQueries.GetOrderedMinionsByVillainId, connection);
            commandGetMinionData.Parameters.AddWithValue("@Id", id);

            var minionsRearder = await commandGetMinionData.ExecuteReaderAsync();

            while (await minionsRearder.ReadAsync())
            {
                await Console.Out.WriteLineAsync($"{minionsRearder["RowNum"]}. " + $"{minionsRearder["Name"]} {minionsRearder["Age"]}");
            }

        }
    }
}