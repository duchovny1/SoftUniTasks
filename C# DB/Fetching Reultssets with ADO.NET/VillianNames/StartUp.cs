using System;
using System.Data.SqlClient;

namespace VillianNames
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection("Server=.\\SQLEXPRESS;" +
                "Database=MinionsDB;Integrated Security=true");

            connection.Open();

            using (connection)
            {
                string sqlQuery = @"SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
                                                                     FROM Villains AS v 
                                   JOIN MinionsVillains AS mv ON v.Id = mv.VillainId 
                                                             GROUP BY v.Id, v.Name 
                                                HAVING COUNT(mv.VillainId) > 3 
                                                        ORDER BY COUNT(mv.VillainId)";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} - {reader["MinionsCount"]}");
                    }
                }
                

            }
        }
    }
}
