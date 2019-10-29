using System;
using System.Data.SqlClient;
using System.Linq;
using StartUp2;

public class StartUp
{
    public static void Main(string[] args)
    {
        SqlConnection connection = new SqlConnection("Server=.\\SQLEXPRESS;" +
                "Database=MinionsDB;Integrated Security=true");

        string[] infoMinion = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
        string[] infoVillian = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();

        string minionName = infoMinion[1];
        int minionAge = int.Parse(infoMinion[2]);
        string minionCity = infoMinion[3];

        string villianName = infoVillian[1];


        connection.Open();

        using (connection)
        {
            try
            {

                int villianId = 0;

                SqlCommand command = new SqlCommand(Queries.MinionId, connection);
                command.Parameters.AddWithValue("@Name", minionName);

                int minionId = (int)command.ExecuteScalar();
                // get the minion Id

                command = new SqlCommand(Queries.VillianId, connection);
                command.Parameters.AddWithValue("@Name", villianName);

                object villain = command.ExecuteScalar();
                // get the villain Id


                command = new SqlCommand(Queries.SelectTown, connection);
                command.Parameters.AddWithValue("@townName", minionCity);

                int townId = (int)command.ExecuteScalar();
                //get the town Id


                if (townId == null)
                {
                    command = new SqlCommand(Queries.InsertNewTown, connection);
                    command.Parameters.AddWithValue("@townName", minionCity);

                    command.ExecuteNonQuery();
                    // if town is null, we add it to the database
                    Console.WriteLine($"Town {minionCity} was added to the database.");

                    command = new SqlCommand(Queries.SelectTown, connection);
                    command.Parameters.AddWithValue("@townName", minionCity);

                    townId = (int)command.ExecuteScalar();

                    // here we get the town id we already added

                    command = new SqlCommand(Queries.InsertMinion, connection);
                    command.Parameters.AddWithValue("@name", minionName);
                    command.Parameters.AddWithValue("@age", minionAge);
                    command.Parameters.AddWithValue("@townId", minionName);

                    // here we add the recent city to the minion

                }


                if (villain != null)
                {
                    villianId = (int)villain;
                    // if the object villain is not null, we get the villain Id
                }
                else
                {

                    // if object villain is null, we create a new one andd add it to the database
                    command = new SqlCommand(Queries.AddVillian, connection);

                    command.Parameters.AddWithValue("@villainName", villianName);

                    command.ExecuteNonQuery();

                    Console.WriteLine($"Villain {villianName} was added to the database.");
                }

                //
                if (villianId == 0)
                {
                    // if object villain didnt exist, we had created him from above
                    // and here we get the ID of newly created object
                    command = new SqlCommand(Queries.VillianId, connection);
                    command.Parameters.AddWithValue("@Name", villianName);
                    villianId = (int)command.ExecuteScalar();
                }
                // 
                //

                // we add the minion servant to the villain
                command = new SqlCommand(Queries.AddMinionToVillian, connection);
                command.Parameters.AddWithValue("@villainId", villianId);
                command.Parameters.AddWithValue("@minionId", minionId);
                command.ExecuteNonQuery();



                Console.WriteLine($"Successfully added {minionName} to be minion of {villianName}");



            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        ;


    }
}