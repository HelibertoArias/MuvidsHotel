using Muvids.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Muvids.Persistence.Data;

public static class Seed
{
    public static async Task SeedBookings(MuvidsDbContext context)
    {
        //if (!context.Movies.Any())
        //{
        //    string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        //    var data = await System.IO.File.ReadAllTextAsync(assemblyFolder + @"\Data\movies-data.json");

        //    var options = new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true
        //    };

        //    var list = JsonSerializer.Deserialize<List<Movie>>(data, options);
        //    foreach (var item in list)
        //    {
        //        // Avoid to  use override method
        //        context.Movies.Add(item);
        //    }


        //    context.SaveChanges();

        //}
    }
}
