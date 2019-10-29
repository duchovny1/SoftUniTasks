using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeTownNames
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;


    public class StartUp
    {
        private const string updateTowns = @"UPDATE Towns
                                                 SET Name = UPPER(Name)
                           WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)";

        private const string selectTowns =  @" SELECT t.Name 
                                                            FROM Towns as t
                                                            JOIN Countries AS c ON c.Id = t.CountryCode
                                                            WHERE c.Name = @countryName";

        public static void Main(string[] args)
        {
            string country = Console.ReadLine();

            SqlConnection connection = new SqlConnection(@"Server=.\\SQLEXPRESS;" +
                "Database=MinionsDB;Integrated Security=true");

            connection.Open();

            using (connection)
            {
                SqlCommand countryCommand = new SqlCommand(selectTowns, connection);
                countryCommand.Parameters.AddWithValue("@countryName", country);

                //check if the country exists

                object targetCountry = countryCommand.ExecuteScalar();

                if (targetCountry != null)
                {
                    targetCountry = (string)targetCountry;

                    SqlCommand updateCommand = new SqlCommand(updateTowns, connection);
                    updateCommand.Parameters.AddWithValue("@countryName", country);

                    int affectedTowns = updateCommand.ExecuteNonQuery();
                    Console.WriteLine($"{affectedTowns} town names were affected.");

                    SqlDataReader reader = countryCommand.ExecuteReader();

                    List<string> townNames = new List<string>(affectedTowns);

                    while (reader.Read())
                    {
                        string townName = (string)reader["TownName"];
                        townNames.Add(townName);
                    }

                    Console.WriteLine($"[{string.Join(", ", townNames)}]");
                }
                else
                {
                    Console.WriteLine("No town names were affected.");
                }
            }
        }
    }
}
