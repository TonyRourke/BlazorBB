using System;
using System.IO;
using System.Threading.Tasks;
using BlazorAppBB.Models;
using System.Text.Json;

namespace BlazorAppBB.Data
{
    public class FileDataService
    {
        private readonly string _filePath = "D:\\Temp\\Apes.json";

        public async Task<List<InputDataModel>> GetDataAsync()
        {
            try
            {
                using var reader = new StreamReader(_filePath);
                //var json = await reader.ReadToEndAsync();
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<InputDataModel>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading data: {ex.Message}");
                return new List<InputDataModel>();
            }
        }

        public async Task<List<InputDataModel>> SetDataAsync(List<InputDataModel> idm)
        {
            using var writer = new StreamWriter(_filePath);
            var json = JsonSerializer.Serialize(idm);
            await writer.WriteAsync(json);
            return idm;
        }
        
    }
}
