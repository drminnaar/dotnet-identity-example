using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Identity.Database.Seeder
{
    public sealed class JsonFileParser
    {
        public Task<IReadOnlyList<T>> LoadDataFromFileAsync<T>(string jsonFilePath)
        {
            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                throw new ArgumentException(
                    $"The specified parameter '{nameof(jsonFilePath)}' may not be null, empty, or whitespace.",
                    nameof(jsonFilePath));
            }

            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException(
                   $"The path '{jsonFilePath}' could not be found.",
                   jsonFilePath);
            }

            async Task<IReadOnlyList<T>> LoadDataFromFileAsync()
            {
                var content = await File.ReadAllTextAsync(jsonFilePath);

                if (string.IsNullOrWhiteSpace(content))
                {
                    throw new InvalidDataException(
                        $"The specified file '{jsonFilePath}' contains no data.");
                }

                return JsonSerializer.Deserialize<T[]>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }

            return LoadDataFromFileAsync();
        }
    }
}
