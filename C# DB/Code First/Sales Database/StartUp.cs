namespace P03_SalesDatabase
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using P03_SalesDatabase.Data;

    public class StartUp
    {
        static void Main(string[] args)
        {
            SalesContext context = new SalesContext();

            context.Database.EnsureCreated();
        }
    }
}
