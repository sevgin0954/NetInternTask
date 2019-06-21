using CsvHelper;
using FilterNbaSuperstar.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace FilterNbaSuperstar.Tests.ProgramTests
{
    public class MainTests
    {
        private const string inputPath = @"C:\Users\Sevgin\Documents\FilterNbaSuperstarTests\allPlayers.json";
        private const string directoryPath = @"C:\Users\Sevgin\Documents\FilterNbaSuperstarTests";
        private const string outputPath = @"C:\Users\Sevgin\Documents\FilterNbaSuperstarTests\superstars.csv";

        public MainTests()
        {
            this.DeleteFilesIfExist(inputPath, outputPath);
        }

        [Fact]
        public void WithFileNoPlayers_ShouldCreateCsvFileWithOnlyHeader()
        {
            this.CreateEmptyInputFile();
            var args = this.GenerateArguments();

            Program.Main(args);

            var expectedText = "Name,Rating" + Environment.NewLine;
            var outputText = this.GetOutputFileText(outputPath);
            Assert.Equal(expectedText, outputText);
        }

        [Fact]
        public void WithNegativeMaxYearsCount_ShouldThrowException()
        {
            this.CreateEmptyInputFile();
            var args = this.GenerateArguments(-1);

            Assert.Throws<ArgumentException>(() => Program.Main(args));
        }

        [Fact]
        public void WithNegativeMinRating_ShouldThrowException()
        {
            this.CreateEmptyInputFile();
            var args = this.GenerateArguments(0, -1);

            Assert.Throws<ArgumentException>(() => Program.Main(args));
        }

        [Fact]
        public void WithPlayers_ShouldFilterPlayersByMinimumRating()
        {
            var player2Name = "sevgin";
            var player1 = this.CreatePlayer(DateTime.UtcNow.Year, rating: 5);
            var player2 = this.CreatePlayer(DateTime.UtcNow.Year, player2Name, rating: 10);
            this.CreateInputFileWithPlayers(player1, player2);

            var args = this.GenerateArguments(minRating: 10);
            Program.Main(args);

            var filteredPlayers = this.DeserializeOutput();
            Assert.Single(filteredPlayers);
            Assert.Equal(player2Name, filteredPlayers[0].Name);
        }

        [Fact]
        public void WithPlayers_ShouldFilterPlayersByMaximumYears()
        {
            var player2Name = "sevgin";
            var oldTime = DateTime.UtcNow - TimeSpan.FromDays(365 * 6);
            var player1 = this.CreatePlayer(oldTime.Year);
            var player2 = this.CreatePlayer(DateTime.UtcNow.Year, player2Name);
            this.CreateInputFileWithPlayers(player1, player2);

            var args = this.GenerateArguments(maxYears: 5);
            Program.Main(args);

            var filteredPlayers = this.DeserializeOutput();
            Assert.Single(filteredPlayers);
            Assert.Equal(player2Name, filteredPlayers[0].Name);
        }

        [Fact]
        public void WithPlayers_ShouldOrderPlayersByRatingDescending()
        {
            var player2 = this.CreatePlayer(DateTime.UtcNow.Year, "player2", rating: 5);
            var player1 = this.CreatePlayer(DateTime.UtcNow.Year, "player1", rating: 10);
            this.CreateInputFileWithPlayers(player2, player1);

            var args = this.GenerateArguments();
            Program.Main(args);

            var filteredPlayers = this.DeserializeOutput();
            Assert.Equal(player1.Name, filteredPlayers[0].Name);
            Assert.Equal(player2.Name, filteredPlayers[1].Name);
        }

        private Player CreatePlayer(int playingSince, string name = "", string position = "", int rating = 0)
        {
            var player = new Player()
            {
                Name = name, 
                PlayingSince = playingSince,
                Position = position,
                Rating = rating
            };
            return player;
        }

        private void DeleteFilesIfExist(string inputPath, string outputPath)
        {
            if (File.Exists(inputPath))
            {
                File.Delete(inputPath);
            }
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
        }

        private void CreateEmptyInputFile()
        {
            Directory.CreateDirectory(directoryPath);
            using (File.Create(inputPath)) { }
        }

        private void CreateInputFileWithPlayers(params Player[] players)
        {
            Directory.CreateDirectory(directoryPath);

            var jsonText = JsonConvert.SerializeObject(players);
            File.WriteAllText(inputPath, jsonText);
        }

        private string GetOutputFileText(string outputPath)
        {
            var outputFileText = File.ReadAllText(outputPath);
            return outputFileText;
        }

        private PlayerCsv[] DeserializeOutput()
        {
            using (var reader = new StreamReader(outputPath))
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords<PlayerCsv>();

                return records.ToArray();
            }
        }

        private string[] GenerateArguments(int maxYears = 1, int minRating = 0)
        {
            return new string[] { inputPath, maxYears.ToString(), minRating.ToString(), outputPath };
        }
    }
}
