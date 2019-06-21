using FilterNbaSuperstar.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FilterNbaSuperstar
{
    public class Program
    {
        private const string inputPathSchemaName = "inputPath";
        private const string maxYearsSchemaName = "maxYears";
        private const string minRatingSchemaName = "minRating";
        private const string outputPathSchemaName = "outputPath";

        public static void Main(string[] arguments)
        {
            var schema =
                $"{inputPathSchemaName}*, " +
                $"{maxYearsSchemaName}#, " +
                $"{minRatingSchemaName}#, " +
                $"{outputPathSchemaName}*";
            var args = new Args(schema, arguments);

            var inputPath = args.GetStringArgument(inputPathSchemaName);
            var allPlayers = GetPlayersFromJsonFile(inputPath);

            var minRating = args.GetIntArgument(minRatingSchemaName);
            var maxPlayedYearsCount = args.GetIntArgument(maxYearsSchemaName);
            var filteredPlayers = FilterPlayers(allPlayers, minRating, maxPlayedYearsCount).ToArray();

            Array.Sort(filteredPlayers, new PlayerRatingDescComparer());

            var outputPath = args.GetStringArgument(outputPathSchemaName);
            using (File.Create(outputPath)) { }
            var csvText = GenerateCsvReportString(filteredPlayers);
            File.WriteAllText(outputPath, csvText);
        }

        private static IEnumerable<Player> GetPlayersFromJsonFile(string path)
        {
            var json = File.ReadAllText(path);
            var allPlayers = JsonConvert.DeserializeObject<Player[]>(json);
            if (allPlayers == null)
            {
                allPlayers = new Player[0];
            }

            return allPlayers;
        }

        private static IEnumerable<Player> FilterPlayers(IEnumerable<Player> players, int minRating, int maxPlayedYearsCount)
        {
            Validator.ValidatePositiveNumber(minRating, nameof(minRating));
            Validator.ValidatePositiveNumber(maxPlayedYearsCount, nameof(maxPlayedYearsCount));

            var filteredPlayers = new List<Player>();

            foreach (var player in players)
            {
                var playedYearsCount = DateTime.UtcNow.Year - player.PlayingSince;
                if (player.Rating >= minRating && playedYearsCount <= maxPlayedYearsCount)
                {
                    filteredPlayers.Add(player);
                }
            }

            return filteredPlayers;
        }

        private static string GenerateCsvReportString(IEnumerable<Player> players)
        {
            var result = new StringBuilder();
            result.AppendLine("Name,Rating");

            foreach (var player in players)
            {
                result.AppendLine($"{player.Name}, {player.Rating}");
            }

            return result.ToString();
        }
    }
}
