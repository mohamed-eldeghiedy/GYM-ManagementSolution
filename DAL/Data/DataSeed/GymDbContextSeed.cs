using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DAL.Data.DataSeed
{
    public class GymDbContextSeed
    {
        public static bool SeedData(GymDbContext context)
        {
            var HasPlans = context.Plans.Any();
            var HasCategories = context.Categories.Any();
            if (HasCategories&&HasPlans) return false;
            if (!HasPlans)
            {
                var plans= LoadDataFromJsonFile<Plan>("plans.json");
                if(plans.Any())
                    context.Plans.AddRange(plans);
            }
            if (!HasCategories)
            {
                var categories = LoadDataFromJsonFile<Category>("categories.json");
                if(categories.Any())
                    context.Categories.AddRange(categories);
            }
            context.SaveChanges();
            return true;
        }
        private static List<T> LoadDataFromJsonFile<T>(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", fileName);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {fileName} was not found at path {filePath}");
            }
            var data = File.ReadAllText(filePath);

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var objects = JsonSerializer.Deserialize<List<T>>(data, jsonOptions);
            if (objects == null)
            {
                throw new InvalidOperationException($"Failed to deserialize data from file {fileName}");
            }
            return objects;
        }
    }
}
    