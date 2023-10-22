using Microsoft.Data.SqlClient;
using P02_VillainNames;

internal class Program
{
    //1.Connection String
    const string _connectionString = "Server=.;Database=T04_MinionsDB;User Id=sa;Password=N45tejvWcK;TrustServerCertificate=True;";

    static void Main(string[] args)
    {
        //2.SqlConnection
        using SqlConnection sqlConnection = new SqlConnection(_connectionString);
        sqlConnection.Open();

        //3.Create SqlCommand
        using SqlCommand getVillansCommand = new SqlCommand(SqlQueries.getVillainsWithNumberOfminions, sqlConnection);

        //4.Data reader
        using SqlDataReader sqlDataReader = getVillansCommand.ExecuteReader();

        while (sqlDataReader.Read())
        {
            Console.WriteLine($"{sqlDataReader["Name"]} - {sqlDataReader["MinionsCount"]}");
        }
    }
}