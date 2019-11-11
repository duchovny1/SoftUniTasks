using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data;
using System;

namespace Student_System
{
    public class P01_StudentSystem
    {
        static void Main(string[] args)
        {
          var db = new StudentSystemContext();

            db.Database.Migrate();
        }
    }
}
