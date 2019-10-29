using System;
using System.Data.SqlClient;


namespace Minion_Names
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int inputId = int.Parse(Console.ReadLine());

            string sqlQueryText = $@"SELECT Name FROM Villains WHERE Id = @Id";




            SqlConnection connection = new SqlConnection("Server=.\\SQLEXPRESS;" +
                "Database=MinionsDB;Integrated Security=true");

            connection.Open();

            using (connection)
            {
                SqlCommand command = new SqlCommand(sqlQueryText, connection);

                command.Parameters.AddWithValue("@Id", inputId);


                object villianName = command.ExecuteScalar();

                if (villianName is null)
                {
                    Console.WriteLine($"No villian with ID {inputId} exists in database.");
                    return;
                }

                Console.WriteLine($"Villian: " + villianName);


                sqlQueryText = @"SELECT ROW_NUMBER() OVER(ORDER BY m.Name) as RowNum,
                      m.Name, 
                      m.Age
                 FROM MinionsVillains AS mv
                 JOIN Minions As m ON mv.MinionId = m.Id
                WHERE mv.VillainId = @Id
                ORDER BY m.Name";

                command = new SqlCommand(sqlQueryText, connection);

                command.Parameters.AddWithValue("@Id", inputId);

                var reader = command.ExecuteReader();

                using(reader)
                {
                    while(reader.Read())
                    {
                        int i = 1;
                        string minionName = (string)reader["Name"];
                        int minionAge = (int)reader["Age"];
                        if (i == 1 & !reader.HasRows)
                        {
                            Console.WriteLine($"(no minions)");
                            break;
                        }

                        Console.WriteLine($"{i}. {minionName} {minionAge}");
                        i++;
                    }
                }







                //SqlDataReader reader = command.ExecuteReader();

                //using (reader)
                //{


                //    while (reader.Read())
                //    {
                //        int i = 1;
                //        {

                //    Console.WriteLine($"{reader["Name"]}"); 
                //        }

                //    }
                //}
            }

        }
    }
}
