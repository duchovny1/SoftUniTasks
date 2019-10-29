using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp2
{
    public static class Queries

    {
        public const string VillianId = @"SELECT Id FROM Villains WHERE Name = @Name";

        public const string MinionId = @"SELECT Id FROM Minions WHERE Name = @Name";

        public const string AddMinionToVillian = @"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@villainId, @minionId)";

        public const string AddVillian = @"INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";

        public const string InsertMinion = @"INSERT INTO Minions (Name, Age, TownId) VALUES (@name, @age, @townId)";

        public const string InsertNewTown = @"INSERT INTO Towns (Name) VALUES (@townName)";

        public const string SelectTown = @"SELECT Id FROM Towns WHERE Name = @townName";



    }
}
