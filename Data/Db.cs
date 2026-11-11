using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace BakeryApp.Data;

public static class Db
{
    public static BakeryContext Create()
    {

        string path = System.IO.Path.Combine(AppContext.BaseDirectory, "connection.txt");
        string connection = System.IO.File.ReadAllText(path).Trim();

        var options = new DbContextOptionsBuilder<BakeryContext>()
            .UseSqlServer(connection)
            .Options;

        return new BakeryContext(options);
    }
}